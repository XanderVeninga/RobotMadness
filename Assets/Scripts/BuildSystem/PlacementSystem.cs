using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    public Material[] materials;
    public BuildManager buildManager;
    [SerializeField] private BuildInputManager inputManager;
    [SerializeField] Grid grid;
    [SerializeField] public ObjectDatabaseSO database;
    private int selectedObjectIndex = -1;
    [SerializeField] private GameObject gridVisualisation;
    [SerializeField] private GameObject placementIndicator;
    private GridData gridData;
    public List<GameObject> placedObjects = new();

    private void Start()
    {
        buildManager = BuildManager.Instance;
        BuildManager.Instance.placementSystem = this;
        StopPlacement();
        gridData = new();
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
        placementIndicator = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        placementIndicator.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        materials[0] = placementIndicator.transform.GetChild(0).GetComponent<Renderer>().material;
        gridVisualisation.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
    }

    private void PlaceStructure()
    {
        Vector3 playerPosistion = inputManager.GetSelectedMapPosistion();
        Vector3Int gridPosistion = grid.WorldToCell(playerPosistion);
        bool placementValidity = CheckPlacementValidity(gridPosistion, selectedObjectIndex);
        if(!placementValidity)
        {
            return;
        }
        GameObject newBuilding = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newBuilding.transform.position = grid.CellToWorld(gridPosistion);
        newBuilding.transform.GetChild(0).transform.eulerAngles = placementIndicator.transform.GetChild(0).transform.eulerAngles;
        placedObjects.Add(newBuilding);

        GridData selectedData = gridData;
        selectedData.AddObjectAt(gridPosistion, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID, placedObjects.Count-1);
        placementIndicator.transform.GetChild(0).GetComponent<Renderer>().material = materials[0];
        placementIndicator.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;

        Destroy(placementIndicator);
        if(newBuilding.GetComponentInChildren<ApplianceClass>())
        {
            var appliance = newBuilding.GetComponentInChildren<ApplianceClass>();
            buildManager.AddBuilding(appliance);
            OrderManager.Instance.AddItemAvailability(appliance);
        }
        StopPlacement();
    }

    private bool CheckPlacementValidity(Vector3Int gridPosistion, int selectedObjectIndex)
    {
        GridData selectedData = gridData;

        return selectedData.CanPlaceObjectAt(gridPosistion, database.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualisation.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
    }
    private void Update()
    {
        if (selectedObjectIndex < 0)
        {
            return;
        }
        Vector3 playerPosistion = inputManager.GetSelectedMapPosistion();
        Vector3Int gridPosistion = grid.WorldToCell(playerPosistion);
        if(placementIndicator != null)
        {
            placementIndicator.transform.position = grid.CellToWorld(gridPosistion);
        }
        bool placementValidity = CheckPlacementValidity(gridPosistion, selectedObjectIndex);
        placementIndicator.transform.GetChild(0).GetComponent<Renderer>().material = placementValidity ? materials[1] : materials[2];
        if (Input.GetKeyDown(KeyCode.R))
        {
            Vector3 curEulers = placementIndicator.transform.GetChild(0).eulerAngles;
            curEulers.y = placementIndicator.transform.GetChild(0).eulerAngles.y + 90;
            placementIndicator.transform.GetChild(0).transform.eulerAngles = curEulers;
        }
    }
}
