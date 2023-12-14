using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ApplianceClass : MonoBehaviour
{
    [SerializeField] private List<CraftingRecipe> recipes;
    public CraftingRecipe currentRecipe;
    public ResourceManager resourceManager;
    public Inventory applianceInventory = new Inventory();
    public GameObject itemHolder;
    bool working = false;

    public bool Working
    {
        get { return working; }
        set { working = value; }
    }

    private void Start()
    {
        resourceManager = ResourceManager.Instance;
        currentRecipe = recipes[0];
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

    public void InsertItem(Resource resource)
    {
        if(itemHolder.GetComponentInChildren<Resource>() == null)
        {
            applianceInventory.AddItem(resource.data.Id);
            resource.resourceObject.transform.parent = itemHolder.transform;
            itemHolder.transform.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
        else
        {
            applianceInventory.AddItem(resource.data.Id);
            Destroy(resource.gameObject);
        }
        Craft();
    }
    public void RemoveItem(GameObject player)
    {
        applianceInventory.RemoveItem(applianceInventory.itemIds[0]);
        if(itemHolder.GetComponentInChildren<Resource>() == null)
        {
            GameObject spawnedObject = Instantiate(resourceManager.resources[applianceInventory.itemIds[0]].prefab, player.GetComponent<PlayerController>().resourceHolder.transform);
            spawnedObject.transform.localPosition = Vector3.zero;
            spawnedObject.GetComponent<Resource>().resourceObject = spawnedObject;
        }
    }
    
    public void Craft()
    {
        List<ResourceData> requiredResources = new List<ResourceData>(currentRecipe.inputItemlist);

        foreach(ResourceData item in requiredResources)
        {
            for(int i = 0; i <= applianceInventory.itemIds.Count; i++)
            {
                if (applianceInventory.itemIds[i] == item.Id)
                {
                    applianceInventory.RemoveItem(applianceInventory.itemIds.IndexOf(item.Id));
                    requiredResources.Remove(item);
                    break;
                }
            }
        }
        if(requiredResources.Count > 0)
        {
            
            GameObject spawnedObject = Instantiate(currentRecipe.outputItem.prefab, itemHolder.transform);
            spawnedObject.transform.localPosition = Vector3.zero;
            spawnedObject.GetComponent<Resource>().resourceObject = spawnedObject;
            applianceInventory.InsertItemAtTop(currentRecipe.outputItem.Id);
        }
    }
}
