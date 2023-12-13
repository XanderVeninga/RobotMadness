using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<int> itemIDs;
    ResourceManager resourceManager;

    public Inventory()
    {
        resourceManager = ResourceManager.Instance;
    }
    public void AddItem(int id)
    {
        itemIDs.Add(id);
    }
    public void RemoveItem(int id)
    {
        itemIDs.Remove(id);
    }
    public ResourceData GetItemData(int id)
    {
        return resourceManager.resources[itemIDs[id]];
    }
}
