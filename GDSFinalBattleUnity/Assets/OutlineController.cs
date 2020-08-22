using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    MeshRenderer renderer;
    Camera cam;
    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
        renderer.enabled = false;
        cam = Camera.FindObjectOfType<Camera>();
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

        if(Physics.Raycast(ray, out hit, dist))
        {
            if (hit.collider.CompareTag("Interact"))
            {
                renderer.enabled = true;
            }

        }
        else
        {
            renderer.enabled = false;
        }


    }
}
