using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Grid grid;

    private void Start()
    {
        grid = new Grid(5,5,10f);
    }


}
