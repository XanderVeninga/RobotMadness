using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public void TrashItem(Resource resource)
    {
        Destroy(resource.gameObject);
    }
}
