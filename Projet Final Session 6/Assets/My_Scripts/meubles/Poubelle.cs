using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poubelle : MonoBehaviour, IInteractible
{
    Item item;
    public void Interact(Player player)
    {
            
        if (player.HeldItem)
        {
            Destroy(player.HeldItem.gameObject);
        }

        //sound
    }

}
