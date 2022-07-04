using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CariabilityHandler : MonoBehaviour
{
    Player player;
    Transform carriable;
    int oldLayer;
    Transform oldParent;
    public bool isCarrying;
    public Transform carryPoint;
    public LayerMask WhatIsCarriable;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();   
    }

    void Update()
    {
        PickDropHandling();
        if (IsTouchingCarriable() && !isCarrying)
        {
            player.InteractButton.GetComponent<Animator>().SetBool("touching", true);
        }
        if (!IsTouchingCarriable())
        {
            player.InteractButton.GetComponent<Animator>().SetBool("touching", false);
        }

    }

    public Transform IsTouchingCarriable()
    {
        Collider2D interactable = Physics2D.OverlapBox(this.transform.position,
         new Vector2(1, 1), 0, WhatIsCarriable);
        if (interactable != null)
        {
            return interactable.GetComponentInParent<SpriteRenderer>().transform;
        }
        else
        {
            return null;
        }
    }

    public void PickUp()
    {
        isCarrying = true;
        //Limiting of states accesible during carrying
        player.canSlash = false;
        player.canStab = false;
        player.canDashOrEvade = false;
        //Getting of transform of carriable
        carriable = IsTouchingCarriable();
        //Saving of original layer and transform for proper drop handling
        oldParent = carriable.parent;
        oldLayer = carriable.gameObject.layer;
        //Setting of parent
        carriable.transform.SetParent(carryPoint);
        //Setting of layer and transform to carry point
        carriable.gameObject.layer = this.gameObject.layer;
        carriable.transform.position = carryPoint.transform.position;
        carriable.GetComponent<BoxCollider2D>().enabled = false;
        carriable.GetComponentInChildren<Carriability>().GetComponent<BoxCollider2D>().enabled = false;
        if (carriable.GetComponent<Rigidbody2D>() != null)
        {
            carriable.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void Drop()
    { 
        //Unlocking of states for full movability
        isCarrying = false;
        player.canSlash = true;
        player.canStab = true;
        player.canDashOrEvade = true;
        //Setting attributes to original state
        carriable.gameObject.layer = oldLayer;
        carriable.GetComponent<BoxCollider2D>().enabled = true;
        carriable.GetComponentInChildren<Carriability>().GetComponent<BoxCollider2D>().enabled = true;
        if (carriable.GetComponent<Rigidbody2D>() != null)
        {
            carriable.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        carriable.transform.SetParent(oldParent);
    }

    public void PickDropHandling()
    {
        //Picking up interactable
        if (Input.GetButton("Interact") && IsTouchingCarriable() != null && !isCarrying && player.CanInteract)
        {
            player.CanInteract.StartCooldownTimer();
            player.InteractButton.GetComponent<Animator>().SetBool("pressed", true);
            PickUp();
        }
        //Dropping interactable
        else if (Input.GetButton("Interact") && isCarrying && player.CanInteract)
        {
            player.CanInteract.StartCooldownTimer();
            player.InteractButton.GetComponent<Animator>().SetBool("pressed", false);
            Drop();
        }
    }
}
