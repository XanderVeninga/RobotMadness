using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public ResourceData resourceToSpawn;
    private GameObject spawnedObject;
    public void SpawnResource(GameObject receiver)
    {
        if (receiver.GetComponent<PlayerController>())
        {
            spawnedObject = Instantiate(resourceToSpawn.prefab, receiver.GetComponent<PlayerController>().resourceHolder.transform);
        }
        else if(receiver.GetComponent<ConveyorScript>())
        {
            spawnedObject = Instantiate(resourceToSpawn.prefab, receiver.GetComponent<ConveyorScript>().itemHolder.transform);
        }
        spawnedObject.transform.localPosition = Vector3.zero;
        spawnedObject.GetComponent<Resource>().resourceObject = spawnedObject;
    }

    public bool CheckResource(Resource resource)
    {
        if(resource.data == resourceToSpawn)
        {
            Destroy(resource.gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Resource>())
        {
            Destroy(collision.gameObject);
        }
    }
}
