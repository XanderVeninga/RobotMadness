using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridManager : MonoBehaviour
{
    private Grid<GridObject> grid;

    private void Awake()
    {
        int gridWidth = 10;

        int gridHeight = 10;

        float cellSize = 1f;
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z));
    }

    public class GridObject
    {



        private Grid<GridObject> grid;

        private int x;

        private int z;



        public GridObject(Grid<GridObject> grid, int x, int z)
        {

            this.grid = grid;

            this.x = x;

            this.z = z;
        }

        public override string ToString()
        {
            return x + "," + z;
        }
    }
}
