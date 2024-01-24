using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    [SerializeField] ResourceManager resourceManager;
    [SerializeField] Inventory conveyorInventory = new();
    [SerializeField] Transform itemHolder;
    [SerializeField] Transform backChecker;
    [SerializeField] Transform frontChecker;
    [SerializeField] private int maxItems;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        resourceManager = ResourceManager.Instance;
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    public int GetMaxItems()
    {
        return maxItems;
    }
    public void InsertItem(Resource resource, GameObject source)
    {
        conveyorInventory.AddItem(resource.data.Id);
        if (source.GetComponent<ApplianceClass>())
        {
            var applianceClass = backChecker.GetComponent<ApplianceClass>();
            applianceClass.RemoveItem();
        }
        if(source.GetComponent<PlayerController>())
        {
            var playerInventory = source.GetComponent<PlayerController>().playerInventory;
            playerInventory.RemoveItem(resource.data.Id);
        }
    }

    public void RemoveItem(GameObject receiver)
    {
        if (receiver.GetComponent<ApplianceClass>())
        {
            receiver.GetComponent<ApplianceClass>().InsertItem(itemHolder.GetComponentInChildren<Resource>(), gameObject);
        }
        if (receiver.GetComponent<PlayerController>())
        {
            receiver.GetComponent<PlayerController>().playerInventory.AddItem(receiver.GetComponent<PlayerController>().playerInventory.itemIds[0]);
        }
    }

    private void AdjustSpeed (float speed)
    {
        _meshRenderer.materials[1].SetFloat("_Float", speed);
    }
}
