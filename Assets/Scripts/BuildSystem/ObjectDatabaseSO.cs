using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buildings")]
public class ObjectDatabaseSO : ScriptableObject
{
    [SerializeField] public List<ObjectData> objectsData;
}
[Serializable]
public class ObjectData
{
    [field: SerializeField] public string Name {  get; private set; }
    [field: SerializeField] public int ID {  get; private set; }
    [field: SerializeField] public Vector2 Size { get; private set; } = Vector2Int.one;
    [field: SerializeField] public GameObject Prefab { get; private set; }
}