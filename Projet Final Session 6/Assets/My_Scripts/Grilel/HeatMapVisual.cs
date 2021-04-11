using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapVisual : MonoBehaviour
{
    // Start is called before the first frame update

    private My_Grid grid;
    private Mesh mesh;
    private int MaxValue;
    private int MinValue;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(My_Grid grid, int max, int min)
    {
        this.grid = grid;
        MaxValue = max;
        MinValue = min;
        UpdateVisual();

        grid.OnGridValueChanged += Grid_OnGridValueChanged;
    }

    private void Grid_OnGridValueChanged(object sender, My_Grid.OnGridValueChangedEventArgs e)
    {
        Debug.Log("event value changed");
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth()*grid.GetHeight(), out Vector3[] vertices,out Vector2[] uv, out int[] triangles);

        for(int x= 0; x < grid.GetWidth(); x++)
        {
            for(int y = 0; y< grid.GetHeight(); y++)
            {
                int index = x * grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.CellSize;
                int gridValue = grid.GetValue(x, y);
                float NormalizedGridValue = (float)gridValue / (float)MaxValue;
                Vector2 gridValueUV = new Vector2(NormalizedGridValue, 0f);
                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y)+quadSize*0.5f, 0f, quadSize,gridValueUV, gridValueUV);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
