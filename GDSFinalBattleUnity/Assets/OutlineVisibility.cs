using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineVisibility : MonoBehaviour
{
    private GameObject player;
    private float fullVisibility = 5f;
    private float noVisibility = 15f;
    private float alpha;
    Knife.HDRPOutline.Core.OutlineObject outline;
    Knife.HDRPOutline.Core.OutlineObject[] outlinesList;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player;
        outline = GetComponentInChildren<Knife.HDRPOutline.Core.OutlineObject>();
        outlinesList = GetComponentsInChildren<Knife.HDRPOutline.Core.OutlineObject>();
        outline.Color = new Color(1, 1, 1, 0f);
        //alpha = outline.material.color.a;
        //alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Knife.HDRPOutline.Core.OutlineObject outline in outlinesList)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < fullVisibility) outline.Color = new Color(1, 1, 1, 1f);
            else if (distance > noVisibility) outline.Color = new Color(1, 1, 1, 0f);
            else
            {
                outline.Color = new Color(1, 1, 1, Mathf.Lerp(0, 1, (noVisibility - distance) / (noVisibility - fullVisibility)));
            }
        }
        //outline.Material.color.a = alpha;
    }
}
