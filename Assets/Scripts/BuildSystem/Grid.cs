using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    private int[,] gridArray;
    private float cellSize;
    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        //create grid array
        gridArray = new int[width, height];

        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                
            }
        }
    }
    private Vector3 GetWorldPosistion(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }
}
