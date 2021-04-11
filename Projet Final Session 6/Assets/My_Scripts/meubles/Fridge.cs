using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fridge : MonoBehaviour, IInteractible
{
    public Ingredient Legume;
    public Ingredient Riz;
    public Ingredient Viande;
    [SerializeField] private Canvas fridgeUI;
    Player joueur;
    GameManager GM;

    public void Start()
    {
        GM = GameManager.Instance;
        joueur = GM.player;
        //fridgeUI = GetComponent<Canvas>();
        fridgeUI.gameObject.SetActive(false);

    }

    public void Interact(Player player)
    {

        if (player.HeldItem == null)
        {
            fridgeUI.gameObject.SetActive(true);
        }
        else {
            Debug.Log("You already have an object in your hands can't take more.");
        }
    }

    public void giveLegume()
    {
        joueur.HeldItem = Instantiate(Legume);
        joueur.HeldItem.ToLocation(joueur.hands);

        Return();
    }
    public void giveRiz()
    {
        joueur.HeldItem = Instantiate(Riz);
        joueur.HeldItem.ToLocation(joueur.hands);
    
        Return();

    }
    public void giveViande()
    {
        joueur.HeldItem = Instantiate(Viande);
        joueur.HeldItem.ToLocation(joueur.hands);
       
        Return();
    }

    public void Return() { 
        fridgeUI.gameObject.SetActive(false);
    }
}
