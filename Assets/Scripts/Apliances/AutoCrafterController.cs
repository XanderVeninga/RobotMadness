using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCrafterController : ApplianceClass
{
    public GameObject itemHolder2;
    public override void InsertItem(Resource resource, GameObject source)
    {
        if (source.GetComponent<ConveyorScript>())
        {
            if (applianceInventory.itemIds.Count < GetMaxItems())
            {
                if (itemHolder.GetComponentInChildren<Resource>() == null)
                {
                    applianceInventory.AddItem(resource.data.Id);
                    resource.resourceObject.transform.parent = itemHolder.transform;
                    itemHolder.transform.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                }
                Craft();
            }
        }
        if (source.GetComponent<PlayerController>())
        {
            var player = source.GetComponent<PlayerController>();
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
    }
    public override void RemoveItem()
    {
        base.RemoveItem();
    }
    public override void Craft()
    {
        base.Craft();
        if (this.applianceInventory.itemIds.Contains(itemHolder2.GetComponentInChildren<Resource>().data.Id))
        {
            Destroy(itemHolder2.GetComponentInChildren<Resource>().gameObject);
        }
    }
    public override void ItemSwapOnAnimation()
    {
        base.ItemSwapOnAnimation();
    }


}
