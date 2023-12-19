using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    public List<int> itemIds = new();
    ResourceManager resourceManager;

    public Inventory()
    {
        resourceManager = ResourceManager.Instance;
    }
    public void AddItem(int id)
    {
        itemIds.Add(id);
    }
    public void InsertItemAtTop(int id)
    {
        itemIds.Insert(0, id);
    }
    public void RemoveItem(int id)
    {
        itemIds.Remove(id);
    }
    public ResourceData GetItemData(int id)
    {
        return resourceManager.resources[itemIds[id]];
    }
}
