using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    Knife.HDRPOutline.Core.OutlineObject outline;
    Knife.HDRPOutline.Core.OutlineObject[] outlinesList;

    void Start()
    {
        
        outline = GetComponentInChildren<Knife.HDRPOutline.Core.OutlineObject>();
        outlinesList = GetComponentsInChildren<Knife.HDRPOutline.Core.OutlineObject>();
        //outline.enabled = false;
    }

    public void ShowOutline()
    {
        foreach(Knife.HDRPOutline.Core.OutlineObject outline in outlinesList) 
        {
            outline.enabled = true;
        }

    }

    public void HideOutline()
    {
        foreach (Knife.HDRPOutline.Core.OutlineObject outline in outlinesList)
        {
            outline.enabled = false;
        }
    }

}
