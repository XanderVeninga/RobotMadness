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
            if (itemHolder.GetComponentInChildren<Resource>() == null)
            {
                applianceInventory.AddItem(resource.data.Id);
                resource.resourceObject.transform.parent = itemHolder.transform;
                itemHolder.transform.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
            else if (itemHolder2.GetComponentInChildren<Resource>() == null)
            {
                applianceInventory.AddItem(resource.data.Id);
                resource.resourceObject.transform.parent = itemHolder2.transform;
                itemHolder2.transform.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
            player.playerInventory.RemoveItem(player.playerInventory.itemIds[0]);
            Craft();
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
