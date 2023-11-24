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
    [SerializeField] bool holdingitem = false;

    private void Start()
    {
        
    }
    void Update()
    {
        // Get input from arrow keys or WASD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 right = cameraTransform.right;
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        right.y = 0f;

        Vector3 movement = (forward.normalized * verticalInput + right.normalized * horizontalInput).normalized;

        // Move and rotate the player
        MoveAndRotatePlayer(movement);

        if(resourceHolder.transform.childCount == 0 && holdingitem)
        {
            holdingitem = false;
        }
        else if(resourceHolder.transform.childCount > 0 && !holdingitem)
        {
            holdingitem = true;
        }


        //Get Input From MouseButtons and check for
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit target, pickupDistance))
            {
                if (holdingitem)
                {
                    var appliance = target.transform.GetComponentInChildren<ApplianceClass>();
                    resourceHolder.transform.GetChild(0).parent = appliance.itemHolder.transform;
                    appliance.AddResource(appliance.itemHolder.transform.GetChild(0).GetComponent<Resource>().data);
                    appliance.itemHolder.transform.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                }
                else
                {
                    if (target.transform.gameObject.GetComponent<ResourceSpawner>())
                    {
                        target.transform.gameObject.GetComponent<ResourceSpawner>().SpawnResource(gameObject);
                        holdingitem = true;
                    }
                    else if(target.transform.gameObject.GetComponent<ApplianceClass>())
                    {
                        var appliance = target.transform.GetComponentInChildren<ApplianceClass>();
                        if (appliance.itemHolder.transform.GetChild(0) != null)
                        {
                            if (appliance.resources.Contains(appliance.itemHolder.transform.GetChild(0).gameObject))
                            {
                                appliance.resources.Remove(appliance.itemHolder.transform.GetChild(0).gameObject);
                            }
                            appliance.itemHolder.transform.GetChild(0).parent = resourceHolder.transform;
                            resourceHolder.transform.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        }
                    }
                }
                Debug.DrawLine(this.transform.position, target.transform.position);
            }
        }
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
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
