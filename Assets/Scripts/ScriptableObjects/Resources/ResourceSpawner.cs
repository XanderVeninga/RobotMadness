using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public ResourceData resourceToSpawn;
    
    public void SpawnResource(GameObject player)
    {
        GameObject spawnedObject = Instantiate(resourceToSpawn.resourceObject, player.transform.GetChild(0));
        spawnedObject.transform.localPosition = Vector3.zero;
    }
}
