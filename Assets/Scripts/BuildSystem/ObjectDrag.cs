using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void OnMouseDown()
    {
        offset = transform.position - BuildManager.GetMouseWorldPosistion();
    }

    private void OnMouseDrag()
    {
        Vector3 pos = BuildManager.GetMouseWorldPosistion() + offset;
        transform.position = BuildManager.Instance.SnapCoordinateToGrid(pos);
    }
}
