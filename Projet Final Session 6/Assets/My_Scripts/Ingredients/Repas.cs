using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Repas : Item
{
    //[SerializeField] Recette recette;
    [SerializeField] private List<ModificationType> mods;
    [SerializeField] private List<IngredientType> ing;
    //[SerializeField] private bool isDubious;
    //public List<IngModif> requires;//didnt work :X
    public Image img;
   // public Mesh mesh;



    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<MeshFilter>().mesh = mesh;
    }

    public List<IngredientType> Ing { get => ing; }
    public List<ModificationType> Mods { get => mods; }




}
