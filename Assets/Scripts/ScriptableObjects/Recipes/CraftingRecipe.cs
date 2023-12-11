using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Recipe")]
[Serializable]
public class CraftingRecipe : ScriptableObject
{
    public List<ResourceData> inputItemlist;
    public ResourceData outputItem;
}
