using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    Knife.HDRPOutline.Core.OutlineObject outline;
    Knife.HDRPOutline.Core.OutlineObject[] outlinesList;
    OutlineVisibility outVis;
    private SelectecedInfo _selectedInfo;

    void Start()
    {
        _selectedInfo = GetComponentInChildren<SelectecedInfo>();
        if(_selectedInfo) _selectedInfo.gameObject.SetActive(false);

        outline = GetComponentInChildren<Knife.HDRPOutline.Core.OutlineObject>();
        outlinesList = GetComponentsInChildren<Knife.HDRPOutline.Core.OutlineObject>();
        //outline.enabled = false;
        outVis = GetComponentInChildren<OutlineVisibility>();
    }

    public void ShowOutline()
    {
        if (this.isActiveAndEnabled)
        {
            if (outVis) { outVis.enabled = false; }

            if (_selectedInfo)
            {
                if (!_selectedInfo.isActiveAndEnabled)
                {
                    _selectedInfo.gameObject.SetActive(true);
                }
            }

            foreach (Knife.HDRPOutline.Core.OutlineObject outline in outlinesList)
            {

                //outline.enabled = true;
                outline.Color = new Color(1, 0, 0, 1f);

            }
            //outlinesList[1].enabled = true;
            //outlinesList[0].enabled = false;
        }



    }

    public void HideOutline()
    {
        if (this.isActiveAndEnabled)
        {
            if (_selectedInfo)
            {
                if (_selectedInfo.isActiveAndEnabled)
                {
                    _selectedInfo.gameObject.SetActive(false);
                }
            }

            foreach (Knife.HDRPOutline.Core.OutlineObject outline in outlinesList)
            {

                outline.Color = new Color(0, 0, 0, 0f);

            }
            //outlinesList[1].enabled = false;
            //outlinesList[0].enabled = true;
            if (outVis) { outVis.enabled = true; }
        }
        
    }

}
