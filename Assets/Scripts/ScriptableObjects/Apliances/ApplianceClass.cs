using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ApplianceClass : MonoBehaviour
{
    [SerializeField] private List<CraftingRecipe> recipes = new();
    public CraftingRecipe currentRecipe;
    public ResourceManager resourceManager;
    public Inventory applianceInventory = new();
    public GameObject itemHolder;
    bool working = false;
    [SerializeField] private int maxItems;

    public bool Working
    {
        get { return working; }
        set { working = value; }
    }

    private void Start()
    {
        resourceManager = ResourceManager.Instance;
        currentRecipe = recipes[0];


        if (gameObject.transform.childCount > 0)
        {
            itemHolder = gameObject.transform.GetChild(0).gameObject;
        }
    }
    

    public void InsertItem(Resource resource, PlayerController player)
    {
        if(applianceInventory.itemIds.Count < maxItems)
        {
            if (itemHolder.GetComponentInChildren<Resource>() == null)
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
            player.playerInventory.RemoveItem(player.playerInventory.itemIds[0]);
            Craft();
        }
    }
    public void RemoveItem(GameObject player)
    {
        if(applianceInventory.itemIds.Count > 0)
        {
            applianceInventory.RemoveItem(applianceInventory.itemIds[0]);
            if(itemHolder.GetComponentInChildren<Resource>() == null )
            {
                if (applianceInventory.itemIds.Count != 0)
                {
                    GameObject spawnedObject =
                        Instantiate(resourceManager.resources[applianceInventory.itemIds[0]].prefab,
                        player.GetComponent<PlayerController>().resourceHolder.transform);
                    spawnedObject.transform.localPosition = Vector3.zero;
                    spawnedObject.GetComponent<Resource>().resourceObject = spawnedObject;
                }
            }
            
        }
    }
    
    public void Craft()
    {
        List<ResourceData> requiredResources = new List<ResourceData>(currentRecipe.inputItemlist);

        foreach(ResourceData item in currentRecipe.inputItemlist)
        {
            for(int i = 0; i < applianceInventory.itemIds.Count; i++)
            {
                if (applianceInventory.itemIds[i] == item.Id)
                {
                    requiredResources.Remove(item);
                    applianceInventory.RemoveItem(applianceInventory.itemIds.IndexOf(item.Id));
                    break;
                }
            }
        }
        if (requiredResources.Count <= 0)
        {
            gameObject.GetComponent<Animation>().Play();
            
        }
    }
    public void ItemSwapOnAnimation()
    {
        for (int d = 0; d < itemHolder.transform.childCount; d++)
        {
            if (itemHolder.transform.GetChild(d).GetComponent<Resource>())
            {
                Destroy(itemHolder.transform.GetChild(d).gameObject);
            }
        }
        GameObject spawnedObject = Instantiate(currentRecipe.outputItem.prefab, itemHolder.transform);
        spawnedObject.transform.localPosition = Vector3.zero;
        spawnedObject.GetComponent<Resource>().resourceObject = spawnedObject;
        applianceInventory.InsertItemAtTop(currentRecipe.outputItem.Id);
    }
}
