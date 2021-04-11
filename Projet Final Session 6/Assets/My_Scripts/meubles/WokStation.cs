using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WokStation : Comptoire, IInteractible
{
    new public void  Interact(Player player)
    {
        if (player.HeldItem != null)
        {
            Drop(player);
        }
        else
        {
            PickUp(player);
        }
    }


}
