using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Comptoire : MonoBehaviour, IInteractible
{
    public Transform counterTop;
    //must hold at least 1 item
    private Item item;
    //public Item Item { get => item; set => item = value; }

    //interact est e
    public void Interact(Player player)
    {

        //czek if player is holding an item
        if (player.HeldItem != null) {
            Drop(player);
        }
        else
        {
            PickUp(player);
        }
    }

    //prend l'item storer sur le comptoire et le met sur le joueur
    protected virtual void PickUp(Player player) 
    {
       
       
        if(item != null) {
            Debug.Log("Pick Up");
            player.HeldItem = this.item;
            this.item.ToLocation(player.hands);//prend l'item
            this.item = null;
        }
        else
        {
            //son du genre shwoosh pour montrer que y a rien
        }
        
    }

    protected virtual void Drop(Player player)
    {
        
        if(item == null) { //si le comptoire est libre
            Debug.Log("Drop");
            this.item = player.HeldItem;
            this.item.ToLocation(counterTop);//met l'item sur le comptoire
            player.HeldItem = null;
        }
        else
        {
            Debug.Log("Not Drop");
            //son pour commande invalide
        }
    }

    public virtual void Modify() { }
}
