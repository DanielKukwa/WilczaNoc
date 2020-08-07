using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable
{
    GameObject handAxe;

    // Start is called before the first frame update
    void Start()
    {
        handAxe = GameObject.Find("HandAxe");
        handAxe.SetActive(false);
    }

    public override void Interact()
    {
        base.Interact();
        handAxe.SetActive(true);
        Destroy(gameObject);
    }
    
      
    
}
