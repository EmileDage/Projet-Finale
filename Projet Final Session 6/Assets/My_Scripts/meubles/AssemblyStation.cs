using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyStation : Comptoire, IInteractible
{
    [SerializeField] private int maxItem = 3;
    private List<Ingredient> ingredients;
    [SerializeField] private List<Transform> pos;
    //[SerializeField] private Repas repasDebug;

    private Repas repas = null;
    private GameManager gm;
    private bool undoo =false;
    


    private void Start()
    {
        gm = GameManager.Instance;
        ingredients = new  List<Ingredient>();
        //Assemble(repasDebug);
    }

    new public void Interact(Player player)
    {
        if (player.HeldItem != null)
        {
            Debug.Log("droping");
            Drop(player);
        }
        else
        {
            if (undoo ||  repas !=null)
            {
                PickUp(player);
            }
            else
            {
                Debug.Log("mergin");
                TryMerge();
            }
            
            
        }
    }

    protected override void Drop(Player player)
    {
       
        Item item = player.HeldItem;
        if (ingredients.Count < maxItem) //s'il reste de la place dans la station
        {
            

            if(item is Ingredient) {
                if (ingredients.Count < maxItem) 
                {
                    ingredients.Add((Ingredient)item);//add to list
                    item.ToLocation(pos[ingredients.Count-1]);
                    player.HeldItem = null; 
                }

            }
            else if(item is Repas)
            {
                if(repas == null)
                {
                    repas = (Repas)item;
                    item.ToLocation(counterTop);
                    player.HeldItem = null;
                }
            }           
        
        }
    }

    protected override void PickUp(Player player)
    {
        if (undoo) { 
            player.HeldItem = ingredients[ingredients.Count-1];
            ingredients[ingredients.Count-1].ToLocation(player.hands);
            ingredients.RemoveAt(ingredients.Count-1);
            if (ingredients.Count < 1) 
            {
                undoo = false;
            }
        }
        else
        {
            player.HeldItem = repas;
            repas.ToLocation(player.hands);
            repas = null;
        }
    }



    private void TryMerge()
    {
        bool r;
        Repas work;//variable de travail
        r = gm.IsRecette(out work, ingredients);//si les ingrédients match une recette
        // work = recette, où recette est une instance de Repas
        if (r) //si les ingredient font un repas
        {
            Assemble(work);//spawn le repas
        }else if (ingredients.Count == maxItem)
        {
            undoo = true;
        }

    }

    private void Assemble(Repas meal)
    {
        foreach (var ingredient in ingredients)
        {
            Destroy(ingredient.gameObject);
        }
        ingredients.Clear();
        repas = Instantiate(meal);
        repas.transform.position = counterTop.position;
    }


}
