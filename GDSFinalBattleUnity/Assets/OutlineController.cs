using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtRay();
        
    }

    private void LookAtRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.GetComponent<Interactable>())
            {
                hit.collider.GetComponentInChildren<Knife.HDRPOutline.Core.OutlineObject>().enabled = true;
                
            }
            else
            {
               
            }
           
        }



    }
}
