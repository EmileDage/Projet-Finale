using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oven : Comptoire, IInteractible
{
    private Item itemOven;
    [SerializeField]private float timer = 5f;
    private float current_time;
    private bool IsSomethingCooking;
    [SerializeField] private Image progressBar;
    private Canvas canva;


    public void Start()
    {
        current_time = timer;
        //progressBar.fillAmount = current_time/ timer;
        //progressBar.gameObject.SetActive(false);
        canva = this.GetComponentInChildren<Canvas>();
        if (canva)
        {
            canva.gameObject.SetActive(false);
        }
    }
    public new void Interact(Player player)
    {
        Debug.Log("oven");
        if (itemOven == null)
        {
            if (player.HeldItem != null)
            {
                if (player.HeldItem is Ingredient) 
                {
                    Ingredient temp = (Ingredient)player.HeldItem;
                    if (temp.Modif == ModificationType.None) { //u cant cook a cooked object
                        Debug.Log("Putting in the oven");
                        itemOven = player.HeldItem;
                        Drop(player);
                        Cooking();
                    }
                }
            }
            else
            {
                Debug.Log("You have nothing to put in the oven.");
            }
        }
        else {
            if (current_time <= 0) {
                Debug.Log("You pick up the thing in the oven !");
                PickUp(player);

            }
            else { 
                
                Debug.Log("It's not ready !");
            }

        }
        
    }

    public void Cooking() {

        Debug.Log("Putting in the oven");
        canva.gameObject.SetActive(true);
        current_time = timer;
        IsSomethingCooking = true;
    }

    private void EndCooking()
    {
        Debug.Log("Object is cooked !");
        itemOven.Modify(ModificationType.Baked);
        IsSomethingCooking = false;
        canva.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (IsSomethingCooking) {
            current_time -= Time.deltaTime;
            if (current_time <= 0)
            {
                EndCooking();
            }
            else {
                Debug.Log(current_time);
                progressBar.fillAmount = current_time/timer;

            }
        }
    }


}
