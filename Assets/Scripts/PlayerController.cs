using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float pickupDistance;
    public GameObject resourceHolder;
    public Inventory playerInventory;
    [SerializeField] bool isHoldingItem = false;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particle;
    [SerializeField] int parEmOn, parEmOf = 0;
    private ParticleSystem.EmissionModule em;


    private void Start()
    {
        playerInventory = new Inventory();
        em = particle.emission;
        em.enabled = true;
    }
    void Update()
    {
        #region Player Movement
        // Get input from arrow keys or WASD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (!animator.GetBool("Moving"))
            {
                animator.SetBool("Moving", true);
            }
            em.rate = parEmOn;

        }
        else if (verticalInput == 0 && horizontalInput == 0)
        {
            if (animator.GetBool("Moving"))
            {
                animator.SetBool("Moving", false);
            }
            em.rate = parEmOf;
        }
        Vector3 right = cameraTransform.right;
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        right.y = 0f;

        Vector3 movement = (forward.normalized * verticalInput + right.normalized * horizontalInput).normalized;

        // Move and rotate the player
        MoveAndRotatePlayer(movement);

        if (resourceHolder.transform.childCount == 1 && isHoldingItem)
        {
            isHoldingItem = false;
        }
        else if (resourceHolder.transform.childCount > 1 && !isHoldingItem)
        {
            isHoldingItem = true;
        }
        #endregion
        #region Player Interactions
        #region LeftMouse press
        //Get Input From MouseButtons and check for
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit target, pickupDistance))
            {
                #region Holding Item
                if (isHoldingItem) // holding an item
                {
                    #region Hit appliance
                    if (target.transform.gameObject.GetComponent<ApplianceClass>()) // if its an appliance
                    {
                        var appliance = target.transform.gameObject.GetComponent<ApplianceClass>();
                        appliance.InsertItem(resourceHolder.transform.GetComponentInChildren<Resource>(), this);
                    }
                    #endregion
                    #region hit resource spawner
                    else if (target.transform.gameObject.GetComponent<ResourceSpawner>()) // if its a resource spawner
                    {
                        var spawner = target.transform.gameObject.GetComponent<ResourceSpawner>();
                        if (resourceHolder.transform.GetComponentInChildren<Resource>().data.Id == spawner.resourceToSpawn.Id)
                        {
                            Destroy(resourceHolder.transform.GetChild(1).gameObject);
                            playerInventory.RemoveItem(0);
                        }
                    }
                    #endregion
                    #region hit order terminal
                    else if (target.transform.gameObject.GetComponent<OrderManager>()) // if its an appliance
                    {
                        var terminal = target.transform.gameObject.GetComponent<OrderManager>();
                        for (int i = 0; i < terminal.orders.Count; i++)
                        {
                            if (terminal.orders[i].wantedItem == resourceHolder.GetComponentInChildren<Resource>().data)
                            {
                                terminal.ClearOrder(this);
                                //add money
                            }
                        }
                    }
                    #endregion
                    #region hit conveyor
                    // if its a conveyor belt
                    #endregion
                   

                }
                #endregion
                #region Not Holding Item
                else //not holding item
                {
                    if (target.transform.gameObject.GetComponent<ResourceSpawner>()) // get an item
                    {
                        target.transform.gameObject.GetComponent<ResourceSpawner>().SpawnResource(gameObject);
                        playerInventory.AddItem(resourceHolder.GetComponentInChildren<Resource>().data.Id);
                        isHoldingItem = true;
                    }
                    else if (target.transform.gameObject.GetComponent<ApplianceClass>()) // get a potential item out of the appliance
                    {
                        var appliance = target.transform.GetComponentInChildren<ApplianceClass>();
                        if (appliance.itemHolder.transform.GetChild(0) != null)
                        {
                            playerInventory.AddItem(appliance.itemHolder.transform.GetComponentInChildren<Resource>().data.Id);
                            appliance.itemHolder.transform.GetChild(0).parent = resourceHolder.transform;
                            resourceHolder.transform.GetChild(1).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                            appliance.RemoveItem(this.gameObject);
                        }
                    }
                }
                #endregion
            }
        }
        #endregion
        #region RightMouse press
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit target, pickupDistance))
            {
                if (target.transform.gameObject.GetComponent<ApplianceClass>())
                {
                    var appliance = target.transform.GetComponentInChildren<ApplianceClass>();
                    appliance.CycleRecipes();
                }
                if (target.transform.gameObject.GetComponent<OrderManager>())
                {
                    var orderManager = target.transform.gameObject.GetComponent<OrderManager>();
                    orderManager.GenerateOrder();
                }
            }
        }
        #endregion
        #endregion
    }
    void MoveAndRotatePlayer(Vector3 movement)
    {
        // Check if there is any movement
        if (movement.magnitude >= 0.1f)
        {
            // Calculate the rotation angle based on the movement direction
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;

            // Immediately set the rotation to the target angle
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }

        // Move the player
        if (isHoldingItem && !animator.GetBool("HoldingItem"))
        {
            animator.SetBool("HoldingItem", true);
        }
        else if (!isHoldingItem && animator.GetBool("HoldingItem"))
        {
            animator.SetBool("HoldingItem", false);
        }
        transform.Translate(moveSpeed * Time.deltaTime * movement, Space.World);
    }
}
