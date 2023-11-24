using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resources")]
public class ResourceData : ScriptableObject
{
    public GameObject prefab;
    [SerializeField] private int id;
    public int Id
    {
        get { return id; }
    }
}
