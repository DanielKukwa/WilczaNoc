using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextures : MonoBehaviour
{
    [SerializeField] private Texture[] _textures;
    [SerializeField] private Material _material;

    private int _currentTex = 0;
    // Start is called before the first frame update
    void Start()
    {
        _material.SetTexture("_BaseColorMap", _textures[_currentTex]);
        _material.SetTexture("_EmissiveColorMap", _textures[_currentTex]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _currentTex++;
            if (_currentTex >= _textures.Length) _currentTex = 0;
            _material.SetTexture("_BaseColorMap", _textures[_currentTex]);
            _material.SetTexture("_EmissiveColorMap", _textures[_currentTex]);
        }
    }
}
