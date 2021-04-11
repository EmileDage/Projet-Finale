using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum IngredientType
{
    none, riz, legume, viande, demo
}

[RequireComponent(typeof(ParticleSystem))]
public class Ingredient : Item
{
    private ModificationType modif = ModificationType.None;
    private ParticleSystem steam;
    [SerializeField] private IngredientType ingredientType;
    

    public ModificationType Modif { get => modif;}
    public IngredientType IngredientType { get => ingredientType; set => ingredientType = value; }

    //public Sprite visualModif;

    // Start is called before the first frame update
    void Start()
    {
        steam = this.GetComponent<ParticleSystem>();
        steam.Stop();
    }

    public override void ToLocation(Transform destination)
    {
        base.ToLocation(destination);
    }

    public override void Modify(ModificationType type) 
    {
        modif = type;
        steam.Play();        
    }

    


}
