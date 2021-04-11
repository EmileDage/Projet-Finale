using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private IInteractible tempInteractible;
    public PlayerInput controls;
    public Transform hands;//la position des objets tenus, un gameobject enfant de player
    private Item heldItem = null;
    private float delay = 0.3f;
    private bool action = true;

    public Item HeldItem { get => heldItem; set => heldItem = value; }

    public void Interact(InputAction.CallbackContext context)
    {
        if (action) {
            action = false;
            Invoke("Delay", delay);
            if (tempInteractible != null) {
                tempInteractible.Interact(this);
            }
        }

    }

    private void Delay()
    {
        action = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        tempInteractible = other.GetComponent<IInteractible>();
        
    }

    private void OnTriggerExit(Collider other)
    {

        tempInteractible = null;

    }


}
