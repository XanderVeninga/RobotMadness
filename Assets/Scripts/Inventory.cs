using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if(itemIds.Count == 1)
        {
            itemIds.Clear();
        }
        else
        {
            itemIds.Remove(id);
        }
    }
    public ResourceData GetItemData(int id)
    {
        return resourceManager.resources[itemIds[id]];
    }
}
