using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ApplianceClass: MonoBehaviour
{
    public ResourceManager resourceManager;
    public Inventory applianceInventory = new Inventory();
    public GameObject itemHolder;
    bool working = false;

    public const int Steel_ID = 0;
    public const int Copper_ID = 1;
    public const int Screws_ID = 2;
    public const int Plastic_ID = 3;
    public const int Glass_ID = 4;
    public const int SteelPlate_ID = 5;
    public const int CopperWire_ID = 6;
    public const int Battery_ID = 7;
    public const int LightBulb_ID = 8;
    public const int SteelPipe_ID = 9;



    public bool Working
    {
        get { return working; }
        set { working = value; }
    }

    private void Start()
    {
        resourceManager = ResourceManager.Instance;
        try
        {
            
            if (gameObject.transform.childCount > 0)
            {
                itemHolder = gameObject.transform.GetChild(0).gameObject;
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void InsertItem(ResourceData resource)
    {
        applianceInventory.AddItem(resource.Id);
        resource.GetComponent<GameObject>().transform.parent = itemHolder.transform;
        itemHolder.transform.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
    public void RemoveItem(GameObject player)
    {
        applianceInventory.RemoveItem(applianceInventory.itemIDs[0]);

    }
    public void AddResource(ResourceData resource)
    {

    }
    public void ChangeResource(Resource oldRes, ResourceData newRes)
    {
        Debug.Log(oldRes.name);
        Destroy(oldRes.resourceObject);
        GameObject newItem = Instantiate(newRes.prefab, this.itemHolder.transform, false);
        newItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        Working = false;
    }
}
