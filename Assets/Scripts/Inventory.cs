using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<int> itemIDs;
    ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = ResourceManager.Instance;
    }

    public void AddItem(ResourceData data)
    {
        itemIDs.Add(data.Id);
    }

    public ResourceData GetItem(int id)
    {
        return resourceManager.resources[itemIDs[id]];
    }
}
