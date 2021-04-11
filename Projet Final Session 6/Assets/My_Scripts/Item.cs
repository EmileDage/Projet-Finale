using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{

    private Vector3 defaultScale;

    private void Awake()
    {
        defaultScale = transform.lossyScale;
    }
    public virtual void ToLocation(Transform destination)//move the gameObject to the hands position
    {
        this.transform.parent = null;
        this.transform.localScale = defaultScale;
        this.transform.SetParent(destination,true);
        this.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.rotation = destination.rotation;


    }

    public virtual void Modify(ModificationType type) { }
}

public enum ModificationType
{
    None, Bouilli, Saute, Frit, Baked 
}
