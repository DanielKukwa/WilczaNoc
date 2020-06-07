using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    Camera cam;
    public LayerMask movementMask;
    PlayerMotor motor;
    public Interactable focus;
    public bool isFirstAttack;



    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            isFirstAttack = true;
            if (Physics.Raycast(ray, out hit, 100))
            {
                
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
                else
                {
                    motor.MoveToPoint(hit.point);
                    RemoveFocus();
                }

                

            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            isFirstAttack = false;
            if (Physics.Raycast(ray, out hit, 100))
            {
                
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }

            }

        }

    }

    public bool GetAttackInfo()
    {
        return isFirstAttack;
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if(focus!=null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        
        newFocus.OnFocused(transform);
        
    }

    void RemoveFocus()
    {
        if (focus!=null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }

}
