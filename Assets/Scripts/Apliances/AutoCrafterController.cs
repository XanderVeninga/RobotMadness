using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCrafterController : ApplianceClass
{
    public GameObject itemHolder2;
    public override void InsertItem(Resource resource, PlayerController player)
    {
        if (applianceInventory.itemIds.Count < GetMaxItems())
        {
            
            player.playerInventory.RemoveItem(player.playerInventory.itemIds[0]);
            Craft();
        }
        if (applianceInventory.itemIds.Count >= 1)
        {
            GameObject spawnedObject =
                        Instantiate(resourceManager.resources[applianceInventory.itemIds[1]].prefab,
                        itemHolder2.transform);
            spawnedObject.transform.localPosition = Vector3.zero;
            spawnedObject.GetComponent<Resource>().resourceObject = spawnedObject;
        }
    }
    public override void RemoveItem(GameObject player)
    {
        base.RemoveItem(player);
        Destroy(itemHolder2.GetComponentInChildren<Resource>().gameObject);
    }
    public override void Craft()
    {
        base.Craft();
    }
    public override void ItemSwapOnAnimation()
    {
        base.ItemSwapOnAnimation();
    }

    
}
