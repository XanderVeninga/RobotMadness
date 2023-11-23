using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resources")]
public class ResourceData : ScriptableObject
{
    public GameObject resourceObject;
    [SerializeField] private int id;
    public int Id
    {
        get { return id; }
    }
}
