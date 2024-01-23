using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject mouseIndicator, cellIndicator;
    [SerializeField] private BuildInputManager inputManager;
    [SerializeField] Grid grid;
    [SerializeField] ObjectDatabaseSO database;
    private int selectedObjectIndex = -1;
    [SerializeField] private GameObject gridVisualisation;

    private void Start()
    {
        BuildManager.Instance.placementSystem = this;
        StopPlacement();
    }
    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisualisation.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement; 
    }

    private void PlaceStructure()
    {
        if(inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosistion = inputManager.GetSelectedMapPosistion();
        Vector3Int gridPosistion = grid.WorldToCell(mousePosistion);
        GameObject newBuilding = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newBuilding.transform.position = grid.CellToWorld(gridPosistion);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualisation.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if(selectedObjectIndex < 0)
        {
            return;
        }
        Vector3 mousePosistion = inputManager.GetSelectedMapPosistion();
        Vector3Int gridPosistion = grid.WorldToCell(mousePosistion);
        mouseIndicator.transform.position = mousePosistion;
        cellIndicator.transform.position = grid.CellToWorld(gridPosistion);
    }
}
