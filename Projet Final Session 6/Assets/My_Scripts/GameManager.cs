using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public Player player;
    public List<Repas> menu;

    public List<Sprite> menu_icon;
    int food_choosen = 0;
    public List<Vector3> chaises;
    Hasard rnd;
    private int money = 0;
    [SerializeField] Text moneyTracker;
    
    public static GameManager Instance { get => _instance; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        chaises = new List<Vector3>();
        rnd = Hasard.Get_Instance();
        moneyTracker = GetComponent<Text>();

        //moneyTracker.text = "Money : 0";
    }

    public bool IsRecette(out Repas repas, List<Ingredient> ingredients) 
    {
        //procede par elimination
        List<Repas> work = CopyList();

        //List<Repas> tempList = CopyList();
        for(int i= 0; i < ingredients.Count; i++) //les ingredients mis sur la table
        {

            
            foreach (Repas recette in work.ToArray()) //le menu
            {

                if (!CompareIngredients(ingredients[i].IngredientType, ingredients[i].Modif, recette))
                {
                    work.Remove(recette);
                }
                
            }
        }

        if (work.Count > 0)
        {
            repas = work[0];
            return true;
        }
        //vieux code moins efficient
        /*foreach (Repas recette in menu)
        {
            if (recette.CompareToIngredients(ingredients)) {
                repas = recette;
                return true;
            }
        }*/
        repas = null;
        return false; 
    }

    //vieux, mauvais code qui force a creer un ingmodif chaque fois qu'on tente de faire un repas
   /* private bool CompareIngredients(IngModif ingredient, Repas recette)//compare ingredient au repas
    {
       List<IngModif> temp = ToIngModifList(recette);
        for (int x = 0; x < temp.Count; x++) //les ingredient necessaire au repas
        {            
            if (ingredient.CompareTo(temp[x]))
            {
                return true;
            }
        }
        return false; 
    }
   */

    private bool CompareIngredients(IngredientType ingredientType, ModificationType modification, Repas recette)//compare ingredient au repas
    {
        
        for (int x = 0; x < recette.Mods.Count; x++) //les ingredient necessaire au repas
        {
            if (ingredientType.Equals(recette.Ing[x]) && modification.Equals(recette.Mods[x]))
            {
                return true;
            }
        }
        return false;

    }


    private List<Repas> CopyList()
    {
        List<Repas> copie = new List<Repas>();
        foreach (Repas recette in menu)
        {
            copie.Add(recette);
        }
        return copie;
    }

    public void RemoveChair(Vector3 position) {
        Debug.Log("Chair removed from registry. Count = " + chaises.Count);
        chaises.Remove(position);
    }

    public void AddChair(Vector3 position)
    {
        Debug.Log("Chair added to registry. Count = " + chaises.Count);
        chaises.Add(position);
    }

    public Vector3 GetChair()
    {
        Debug.Log("Assigning a chair to the client");
        return chaises[0];
    }

    public Repas ChooseWhatEat() {//Cette fonction choisit une recette rnd pour le client
        if (menu.Count > 0) {
            food_choosen = rnd.Next(0, menu.Count);
            return menu[food_choosen];
        }
        else { 
            return null;
        }
    }

    public Sprite Food() {
        return menu_icon[food_choosen];
    }

    public void payForFuud(int tip)//fnt called par le client pour donnez son money
    {
        //le 10 cest le prix de la bouffe le client donne seulement le tip
        money += tip + 10;
        moneyTracker.text = "Money : "+money;
    }
}
