using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public ResourceData resourceToSpawn;
    private GameObject spawnedObject;
    
    public void SpawnResource(GameObject player)
    {
        spawnedObject = Instantiate(resourceToSpawn.prefab, player.transform.GetChild(0));
        spawnedObject.transform.localPosition = Vector3.zero;
        spawnedObject.GetComponent<Resource>().resourceObject = spawnedObject;
    }
}
