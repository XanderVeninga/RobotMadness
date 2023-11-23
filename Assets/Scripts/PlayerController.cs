using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float pickupDistance;
    bool holdingitem = false;

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

        //Get Input From MouseButtons and check for
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit target, pickupDistance))
            {
                Debug.DrawLine(this.transform.position, target.transform.position);
                if(target.transform.gameObject.GetComponent<ResourceSpawner>())
                {
                    target.transform.gameObject.GetComponent<ResourceSpawner>().SpawnResource(this.gameObject);
                    holdingitem = true;
                }
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
