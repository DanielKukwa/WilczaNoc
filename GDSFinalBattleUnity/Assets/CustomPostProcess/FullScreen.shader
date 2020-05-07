Shader "FullScreen/FullScreen"
{
	Properties{
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
		_OutlineWidth("Outline Width", Float) = 5
		_EdgeDepthThreshold("Edge Depth Threshold", Float) = 5
		_EdgeNormalThreshold("Edge Normal Threshold", Float) = 5
		_DepthMultiply("Depth multiply", float) = 0
		_NormalMultiply("Normal multiply", float) = 0
		_Multiply("Multiply", float) = 0
		 _Bias("Bias", float) = 0
		 _X("X", float) = 0
		 _Y("Y", float) = 0
	}
		HLSLINCLUDE

#pragma vertex Vert

#pragma target 4.5
#pragma only_renderers d3d11 ps4 xboxone vulkan metal switch

#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/RenderPass/CustomPass/CustomPassCommon.hlsl"
#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/NormalBuffer.hlsl"

		// The PositionInputs struct allow you to retrieve a lot of useful information for your fullScreenShader:
		// struct PositionInputs
		// {
		//     float3 positionWS;  // World space position (could be camera-relative)
		//     float2 positionNDC; // Normalized screen coordinates within the viewport    : [0, 1) (with the half-pixel offset)
		//     uint2  positionSS;  // Screen space pixel coordinates                       : [0, NumPixels)
		//     uint2  tileCoord;   // Screen tile coordinates                              : [0, NumTiles)
		//     float  deviceDepth; // Depth from the depth buffer                          : [0, 1] (typically reversed)
		//     float  linearDepth; // View space Z coordinate                              : [Near, Far]
		// };

		// To sample custom buffers, you have access to these functions:
		// But be careful, on most platforms you can't sample to the bound color buffer. It means that you
		// can't use the SampleCustomColor when the pass color buffer is set to custom (and same for camera the buffer).
		// float4 SampleCustomColor(float2 uv);
		// float4 LoadCustomColor(uint2 pixelCoords);
		// float LoadCustomDepth(uint2 pixelCoords);
		// float SampleCustomDepth(float2 uv);

		// There are also a lot of utility function you can use inside Common.hlsl and Color.hlsl,
		// you can check them out in the source code of the core SRP package.
	float _EdgeDepthThreshold;
	float _DepthMultiply;
	float _NormalMultiply;
	float _EdgeNormalThreshold;
	float4 _OutlineColor;
	float _OutlineWidth;
	float _Multiply;
	float  _Bias;
	float _X;
	float _Y;

	float SampleClampedDepth(float2 uv) { return SampleCameraDepth(clamp(uv, _ScreenSize.zw, 1 - _ScreenSize.zw)).r; }


    float4 FullScreenPass(Varyings varyings) : SV_Target
    {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(varyings);
        float depth = LoadCameraDepth(varyings.positionCS.xy);
        PositionInputs posInput = GetPositionInput(varyings.positionCS.xy, _ScreenSize.zw, depth, UNITY_MATRIX_I_VP, UNITY_MATRIX_V);

		
        float3 viewDirection = GetWorldSpaceNormalizeViewDir(posInput.positionWS);
        float4 color = float4(0.0, 0.0, 0.0, 0.0);

        // Load the camera color buffer at the mip 0 if we're not at the before rendering injection point
        if (_CustomPassInjectionPoint != CUSTOMPASSINJECTIONPOINT_BEFORE_RENDERING)
            color = float4(CustomPassLoadCameraColor(varyings.positionCS.xy, 0), 1);

        // Add your custom pass code here
		//color.r = depth0 * 100;
		float depthCenter = LoadCameraDepth(posInput.positionSS.xy);
		//float normalCenter = LoadCameraNormals(posInput.positionSS.xy);
		float depthRight = LoadCameraDepth(posInput.positionSS.xy + uint2(_OutlineWidth,0));
		float depthLeft = LoadCameraDepth(posInput.positionSS.xy + uint2(-_OutlineWidth, 0));
		float depthUp = LoadCameraDepth(posInput.positionSS.xy + uint2(0, -_OutlineWidth));
		float depthDown = LoadCameraDepth(posInput.positionSS.xy + uint2(0, _OutlineWidth));
		float finalDepth = ((depthCenter - depthRight) + (depthCenter - depthLeft) + (depthCenter - depthUp) + (depthCenter - depthDown)) * _Multiply;
		//float finalDepth = ((depthRight- depthCenter) + (depthLeft- depthCenter) + (depthUp- depthCenter) +  (depthDown- depthCenter)) * _Multiply;
		finalDepth = finalDepth * finalDepth * finalDepth* finalDepth;
		finalDepth = clamp(finalDepth, 0.0, 1.0);
		//finalDepth = tex2D(sampler_CameraDepthTexture, float2(_X,_Y));

		//float2 colorMapUv = TRANSFORM_TEX(fragInputs.texCoord0.xy, _ColorMap);
		//float4 result = SAMPLE_TEXTURE2D(_ColorMap, s_trilinear_clamp_sampler, colorMapUv) * _Color;

		//color.r = finalDepth;
		//color.g = finalDepth;
		//color.b = finalDepth;
		//color = lerp(color, _OutlineColor, finalDepth);

		float normalThreshold = _EdgeNormalThreshold;
		float depthThreshold =  _EdgeDepthThreshold;
		float halfScaleFloor = floor(_OutlineWidth * 0.5);
		float halfScaleCeil = ceil(_OutlineWidth * 0.5);

		// Compute uv position to fetch depth informations
		float2 bottomLeftUV = posInput.positionNDC.xy - float2(_ScreenSize.zw.x, _ScreenSize.zw.y) * halfScaleFloor;
		float2 topRightUV = posInput.positionNDC.xy + float2(_ScreenSize.zw.x, _ScreenSize.zw.y) * halfScaleCeil;
		float2 bottomRightUV = posInput.positionNDC.xy + float2(_ScreenSize.zw.x * halfScaleCeil, -_ScreenSize.zw.y * halfScaleFloor);
		float2 topLeftUV = posInput.positionNDC.xy + float2(-_ScreenSize.zw.x * halfScaleFloor, _ScreenSize.zw.y * halfScaleCeil);

		// Depth from camera buffer
		float depth0 = SampleClampedDepth(bottomLeftUV);
		float depth1 = SampleClampedDepth(topRightUV);
		float depth2 = SampleClampedDepth(bottomRightUV);
		float depth3 = SampleClampedDepth(topLeftUV);

		float depthDerivative0 = depth1 - depth0;
		float depthDerivative1 = depth3 - depth2;

		float edgeDepth = sqrt(pow(depthDerivative0, 2) + pow(depthDerivative1, 2)) * _DepthMultiply;

		float newDepthThreshold = depthThreshold * depth0;
		edgeDepth = edgeDepth > newDepthThreshold ? 1 : 0;
		//edgeDepth = clamp(edgeDepth, 0.0, 1.0);
		// Normals extracted from the camera normal buffer
		NormalData normalData0, normalData1, normalData2, normalData3;
		DecodeFromNormalBuffer(_ScreenSize.xy * bottomLeftUV, normalData0);
		DecodeFromNormalBuffer(_ScreenSize.xy * topRightUV, normalData1);
		DecodeFromNormalBuffer(_ScreenSize.xy * bottomRightUV, normalData2);
		DecodeFromNormalBuffer(_ScreenSize.xy * topLeftUV, normalData3);

		float3 normalFiniteDifference0 = normalData1.normalWS - normalData0.normalWS;
		float3 normalFiniteDifference1 = normalData3.normalWS - normalData2.normalWS;

		float edgeNormal = sqrt(dot(normalFiniteDifference0, normalFiniteDifference0) + dot(normalFiniteDifference1, normalFiniteDifference1)) * _NormalMultiply;
		//edgeNormal = edgeNormal > normalThreshold ? 1 : 0;
		edgeNormal = clamp(edgeNormal, 0.0, 1.0);

		// Combined
		float3 edgeDetectColor = max(edgeDepth, edgeNormal);
		//color.r = edgeNormal;
		//color.g = edgeNormal;
		//color.b = edgeNormal;

		//return float4(edgeDetectColor.rgb, 1);
		//if(depthRight - depthLeft > _Cut) color = color * _OutlineColor;
		//if (depthLeft - depthRight > _Cut) color = color * _OutlineColor;
		//if (depthUp - depthDown > _Cut) color = color * _OutlineColor;
		//if (depthDown - depthUp > _Cut) color = color * _OutlineColor;

		//float depthRight = LoadCameraDepth(posInput.positionSS.xy + uint2(_OutlineWidth,0)) * _Multiply;
		//float depthLeft = LoadCameraDepth(posInput.positionSS.xy + uint2(-_OutlineWidth, 0)) * _Multiply;
		//float depthUp = LoadCameraDepth(posInput.positionSS.xy + uint2(0, _OutlineWidth)) * _Multiply;
		//float depthDown = LoadCameraDepth(posInput.positionSS.xy + uint2(0, -_OutlineWidth)) * _Multiply;
		//if(depthRight - depthLeft > _Cut) color = color * _OutlineColor;
		//if (depthLeft - depthRight > _Cut) color = color * _OutlineColor;
		//if (depthUp - depthDown > _Cut) color = color * _OutlineColor;
		//if (depthDown - depthUp > _Cut) color = color * _OutlineColor;


        // Fade value allow you to increase the strength of the effect while the camera gets closer to the custom pass volume
        //float f = 1 - abs(_FadeValue * 2 - 1);
		//Load depth and color information from the custom buffer
		//float meshDepthPos = LoadCustomDepth(posInput.positionSS.xy);
		//float4 meshColor = LoadCustomColor(posInput.positionSS.xy);

		// Change the color of the icosahedron mesh
		//meshColor = float4(_OutlineColor, 1) * meshColor;

		// Transform the raw depth into eye space depth
		//float sceneDepth = LinearEyeDepth(depth, _ZBufferParams);
		//float meshDepth = LinearEyeDepth(meshDepthPos, _ZBufferParams);

		//if (_BypassMeshDepth != 0)
		//	meshDepth = _BypassMeshDepth;

		// Add the intersection with mesh and scene depth to the edge detect result
		//edgeDetectColor = lerp(edgeDetectColor, _OutlineColor, saturate(2 - abs(meshDepth - sceneDepth) * 200 * rcp(_EdgeRadius)));

		// Blend the mesh color and edge detect color using the mesh alpha transparency
		//float3 edgeMeshColor = lerp(edgeDetectColor, meshColor.xyz, (meshDepth < sceneDepth) ? meshColor.a : 0);

		// Avoid edge detection effect to leak inside the isocahedron mesh
		//float3 finalColor = saturate(meshDepth - sceneDepth) > 0 ? color.xyz : edgeMeshColor;
		float3 finalColor = lerp(color, _OutlineColor, edgeDetectColor);
		return float4(finalColor, 1);
    }

    ENDHLSL

    SubShader
    {
        Pass
        {
            Name "Custom Pass 0"

            ZWrite Off
            ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            HLSLPROGRAM
                #pragma fragment FullScreenPass
            ENDHLSL
        }
    }
    Fallback Off
}
