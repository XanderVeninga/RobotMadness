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



    private void Start()
    {
        playerInventory = new Inventory();
    }
    void Update()
    {
        #region Player Movement
        // Get input from arrow keys or WASD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            if(!animator.GetBool("Moving"))
            {
                animator.SetBool("Moving", true);
            }
            
        }
        else if (verticalInput == 0 && horizontalInput == 0)
        {
            if(animator.GetBool("Moving"))
            {
                animator.SetBool("Moving", false);
            }
        }
        Vector3 right = cameraTransform.right;
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        right.y = 0f;

        Vector3 movement = (forward.normalized * verticalInput + right.normalized * horizontalInput).normalized;

        // Move and rotate the player
        MoveAndRotatePlayer(movement);

        if(resourceHolder.transform.childCount == 1 && isHoldingItem)
        {
            isHoldingItem = false;
        }
        else if(resourceHolder.transform.childCount > 1 && !isHoldingItem)
        {
            isHoldingItem = true;
        }
        #endregion
        #region Player Interactions
        //Get Input From MouseButtons and check for
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit target, pickupDistance))
            {
                if (isHoldingItem) // holding an item
                {
                    if (target.transform.gameObject.GetComponent<ApplianceClass>()) // if its an appliance
                    {
                        var appliance = target.transform.gameObject.GetComponent<ApplianceClass>();
                        appliance.InsertItem(resourceHolder.transform.GetChild(0).GetComponent<Resource>().data);
                        playerInventory.RemoveItem(playerInventory.itemIDs[0]);
                    }
                    else if(target.transform.gameObject.GetComponent<ResourceSpawner>()) // if its a resource spawner
                    {
                        var spawner = target.transform.gameObject.GetComponent<ResourceSpawner>();
                        if (resourceHolder.transform.GetComponent<Resource>().data == spawner.resourceToSpawn)
                        {
                            Destroy(resourceHolder.transform.GetChild(0).gameObject);
                        }
                    }
                    // if its a conveyorbelt
                }
                else //not holding item
                {
                    if (target.transform.gameObject.GetComponent<ResourceSpawner>()) // get an item
                    {
                        target.transform.gameObject.GetComponent<ResourceSpawner>().SpawnResource(gameObject);
                        playerInventory.AddItem(resourceHolder.GetComponent<Resource>().data.Id);
                        isHoldingItem = true;
                    }
                    else if(target.transform.gameObject.GetComponent<ApplianceClass>()) // get a potential item out of the appliance
                    {
                        var appliance = target.transform.GetComponentInChildren<ApplianceClass>();
                        if (appliance.itemHolder.transform.GetChild(0) != null)
                        {
                            
                            appliance.itemHolder.transform.GetChild(0).parent = resourceHolder.transform;
                            resourceHolder.transform.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        }
                    }
                }
                Debug.DrawLine(this.transform.position, target.transform.position);
            }
        }
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
        else if(!isHoldingItem && animator.GetBool("HoldingItem"))
        {
            animator.SetBool("HoldingItem", false);
        }
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
