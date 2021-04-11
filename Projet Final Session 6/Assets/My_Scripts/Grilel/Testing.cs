using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    [SerializeField] private  int min;
    [SerializeField] private  int max;
    [SerializeField] private HeatMapVisual heat;
    private My_Grid grid;
    // Start is called before the first frame update
    void Start()
    {
         grid = new My_Grid(10, 10 , 10.0f, Vector3.zero,min,max);

        heat.SetGrid(grid, max,min);
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = UtilsClass.GetMouseWorldPosition();
            int value = grid.GetValue(pos);
            grid.SetValue(pos, value + 5);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));//read value
        }*/
        
    }
}
