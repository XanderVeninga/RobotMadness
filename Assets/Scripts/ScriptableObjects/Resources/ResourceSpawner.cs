using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public ResourceData resourceToSpawn;
    private GameObject spawnedObject;
    public void SpawnResource(GameObject player)
    {
        spawnedObject = Instantiate(resourceToSpawn.prefab, player.GetComponent<PlayerController>().resourceHolder.transform);
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
}
