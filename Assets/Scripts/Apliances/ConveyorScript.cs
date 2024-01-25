using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Inventory conveyorInventory = new();
    public Transform itemHolder;
    [SerializeField] Transform backChecker;
    [SerializeField] Transform frontChecker;
    [SerializeField] private int maxItems;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        gameManager = GameManager.Instance;
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    public int GetMaxItems()
    {
        return maxItems;
    }
    [SerializeField]
    public void InsertItem(Resource resource, GameObject source)
    {
        if (conveyorInventory.itemIds.Count < GetMaxItems())
        {
            conveyorInventory.AddItem(resource.data.Id);
            if (source.GetComponent<ApplianceClass>())
            {
                var applianceClass = backChecker.GetComponent<ApplianceClass>();
                applianceClass.RemoveItem();
            }
            if (source.GetComponent<PlayerController>())
            {
                var playerInventory = source.GetComponent<PlayerController>().playerInventory;
                playerInventory.RemoveItem(resource.data.Id);
            }
        }
    }

    public void InsertItemEvent()
    {
        if (Physics.Raycast(backChecker.position, frontChecker.forward, out RaycastHit backTarget, 2))
        {
            if (frontChecker.transform.GetComponent<ApplianceClass>())
            {
                frontChecker.transform.GetComponent<ApplianceClass>().InsertItem(itemHolder.GetComponentInChildren<Resource>(), gameObject);
            }
        }
    }
    public void SpawnResourceEvent()
    {
        if (Physics.Raycast(backChecker.position, backChecker.forward, out RaycastHit backTarget, 2))
        {
            if(backTarget.transform.GetComponent<ResourceSpawner>())
            {
                backTarget.transform.GetComponent<ResourceSpawner>().SpawnResource(gameObject);
                conveyorInventory.AddItem(itemHolder.GetComponentInChildren<Resource>().data.Id);
            }
            else if(backTarget.transform.GetComponent<ApplianceClass>())
            {
                var appliance = backTarget.transform.GetComponent<ApplianceClass>();
                conveyorInventory.AddItem(appliance.applianceInventory.itemIds[0]);
                appliance.itemHolder.GetComponentInChildren<Resource>().transform.parent = itemHolder.transform;
                itemHolder.GetComponentInChildren<Resource>().transform.localPosition = Vector3.zero;
                appliance.RemoveItem();
            }
        }
    }
    public void RemoveItemEvent()
    {
        //Debug.Log("remove triggered");
        gameObject.GetComponent<Animator>().SetBool("depositItem", false);
        if (Physics.Raycast(frontChecker.position, frontChecker.forward, out RaycastHit frontTarget, 2))
        {
            Debug.Log(frontTarget.transform.gameObject);
            RemoveItem(frontTarget.transform.gameObject);
        }
    }
    public void RemoveItem(GameObject receiver)
    {
        if (receiver.GetComponent<ApplianceClass>())
        {
            conveyorInventory.RemoveItem(0);
            receiver.GetComponent<ApplianceClass>().InsertItem(itemHolder.GetComponentInChildren<Resource>(), gameObject);
        }
        if (receiver.GetComponent<PlayerController>())
        {
            conveyorInventory.RemoveItem(0);
            receiver.GetComponent<PlayerController>().playerInventory.AddItem(receiver.GetComponent<PlayerController>().playerInventory.itemIds[0]);
        }
    }

    public void AdjustSpeed(float speed)
    {

        _meshRenderer.materials[1].SetFloat("_Speed", speed);
    }

    private void FixedUpdate()
    {
        if(gameManager.currentRoundType == GameManager.RoundType.Play)
        {
            RaycastHit frontTarget;
            RaycastHit backTarget;
            //front target checks
            if (itemHolder.GetComponentInChildren<Resource>())
            {
                if (Physics.Raycast(frontChecker.position, frontChecker.forward, out frontTarget, 2))
                {
                    var animator = gameObject.GetComponent<Animator>();
                    var appliance = frontTarget.transform.GetComponent<ApplianceClass>();
                    var conveyor = frontTarget.transform.GetComponent<ConveyorScript>();
                    var spawner = frontTarget.transform.GetComponent<ResourceSpawner>();

                    if (appliance)
                    {
                        if (appliance.applianceInventory.itemIds.Count != appliance.GetMaxItems() && appliance.Working == false)
                        {
                            animator.SetBool("depositItem", true);
                        }
                    }
                    else if (conveyor)
                    {
                        animator.SetTrigger("depositItem");
                        conveyor.InsertItem(itemHolder.GetComponentInChildren<Resource>(), gameObject);
                    }
                    else if (spawner)
                    {
                        if (spawner.resourceToSpawn == itemHolder.GetComponentInChildren<Resource>().data)
                        {
                            animator.SetTrigger("depositItem");
                        }
                    }
                }
            }
            else
            {
                if (Physics.Raycast(backChecker.position, backChecker.forward, out backTarget, 2))
                {
                    //back target check
                    var animator = gameObject.GetComponent<Animator>();
                    var appliance = backTarget.transform.GetComponent<ApplianceClass>();
                    var conveyor = backTarget.transform.GetComponent<ConveyorScript>();
                    var spawner = backTarget.transform.GetComponent<ResourceSpawner>();

                    if (appliance)
                    {
                        if (appliance.Working == false && appliance.applianceInventory.itemIds.Count > 0)
                        {
                            Debug.Log("Removing from appliance");
                            animator.SetTrigger("hasItem");
                        }

                    }
                    else if (conveyor)
                    {
                        animator.SetTrigger("hasItem");
                    }
                    else if (spawner)
                    {
                        animator.SetTrigger("hasItem");
                    }
                }
            }
        }
    }
}