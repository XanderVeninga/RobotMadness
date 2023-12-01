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
        float cellSize = 2f;
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z));
    }
    public class GridObject
    {
        private Grid<GridObject> grid;
        private int x;
        private int z;
        private Transform transform;
        public GridObject(Grid<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetTransform(Transform transform)
        {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x,z);
        }

        public void ClearTransform()
        {
            transform = null;
            grid.TriggerGridObjectChanged(x,z);
        }

        public override string ToString()
        {
            return x + "," + z + "\n" + transform;
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        { 
        }
    }
}
