using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    public GameObject[] bluePrints;
    public List<ApplianceClass> applianceList = new();
    public PlacementSystem placementSystem;
    public BuildInputManager inputManager;
    [SerializeField] private int money;
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    
    public void AddMoney(int value)
    {
        money += value;
    }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddBuilding(ApplianceClass appliance)
    {
        applianceList.Add(appliance);
    }
    public void RemoveBuilding(ApplianceClass appliance)
    {
        applianceList.Remove(appliance);
    }

    public int GetBuildingID(ApplianceClass appliance)
    {
        int applianceID = 0;
        foreach (var blueprint in bluePrints)
        {
            if(appliance.gameObject == placementSystem.database.objectsData[blueprint.GetComponent<BluePrintScript>().bluePrintData.ID].Prefab)
            {
                applianceID = placementSystem.database.objectsData[blueprint.GetComponent<BluePrintScript>().bluePrintData.ID].ID;
            }
        }
        return applianceID;
    }
}
