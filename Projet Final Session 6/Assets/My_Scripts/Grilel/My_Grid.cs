using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class My_Grid {

    private int column;//le nb de colonnes
    private int row;//le nb de rangee
    private float cellSize;
    private int[,] gridArray;
    private TextMesh[,] debugTxtArray;
    private Vector3 originPos;

    private int min;
    private int max;

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs:EventArgs
    {
        public int x;
        public int y;
    }

    public float CellSize { get => cellSize;}
    public int Max { get => max; set => max = value; }
    public int Min { get => min; set => min = value; }

    public My_Grid(int width, int height, float cellSize, Vector3 originPos, int min, int max)
    {
        this.min = min;
        this.max = max;
        this.column = width;
        this.row = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        gridArray = new int[width, height];
        debugTxtArray = new TextMesh[width, height];

        Debug.Log("grid::" + width + height);

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTxtArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.green, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, 1 + y), Color.red, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red, 100f);

            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.red, 100f);//debug
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.red, 100f);//debug
    }

    public My_Grid(int width, int height, float cellSize, Vector3 originPos) {

        max = 0;
        this.column = width;
        this.row = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        gridArray = new int[width, height];
        debugTxtArray = new TextMesh[width, height];

        Debug.Log("grid::"+ width + height );

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTxtArray[x,y]= UtilsClass.CreateWorldText(gridArray[x,y].ToString(),null, GetWorldPosition(x,y)+new Vector3(cellSize,cellSize)*0.5f, 20, Color.green, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, 1 + y), Color.red, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red, 100f);
                
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.red,100f);//debug
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.red, 100f);//debug

        
     }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPos;
    }
    public void GetCoordinates(Vector3 WorldPosition , out int x, out int y)//turn a worldposition into the x,y indexes 
    {
        x = Mathf.FloorToInt((WorldPosition-originPos).x / cellSize);
        y = Mathf.FloorToInt((WorldPosition-originPos).y / cellSize);
    }

    public void SetValue(int x, int y, int val) {
        if (x >= 0 && y>=0 && x<column && y<row)
        { 
                       
            if (max > 0) {
                gridArray[x, y] = Mathf.Clamp(val, min, max); 
            }
            else { gridArray[x, y] = val; }

            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });

            debugTxtArray[x, y].text = gridArray[x, y].ToString();
        }//avoid array out of bounds
    }

    public void SetValue(Vector3 WorldPosition, int val) {
        int x, y;
        GetCoordinates(WorldPosition, out x, out y);
        SetValue(x, y, val);
    }

    public int GetValue(int x, int y) {
        if (x >= 0 && y >= 0 && x < column && y < row)
        {
            return gridArray[x, y];
        }
        return -1;
    }

    public int GetValue(Vector3 WorldPosition) 
    {
        int x, y;
        GetCoordinates(WorldPosition, out x, out y);
        return GetValue(x, y);
    }

    public int GetWidth()
    {
        return column;
    }

    public int GetHeight() {
        return row;
    }


}

