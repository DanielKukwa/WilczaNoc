using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineLookAt : MonoBehaviour
{
    Camera cam;
    private OutlineController prevController;
    private OutlineController currController;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleLookAtRay();

    }

    private void HandleLookAtRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.GetComponent<Interactable>())
            {
                currController = hit.collider.GetComponent<OutlineController>();

                if (prevController != currController)
                {
                    DeactivateOutline();
                    ActivateOutline();
                    
                }
                prevController = currController;
            }
            else
            {
                DeactivateOutline();
            }

        }
        else
        {
            DeactivateOutline();
        }


    }

    private void ActivateOutline()
    {
        if (currController != null)
        {
            currController.ShowOutline();
        }
    }


    private void DeactivateOutline()
    {
        if (prevController != null)
        {
            prevController.HideOutline();
            prevController = null;
        }


    }
}
