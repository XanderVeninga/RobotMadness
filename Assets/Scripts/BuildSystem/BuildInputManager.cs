using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildInputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector3 lastPosistion;
    [SerializeField] LayerMask placementMask;

    public event Action OnClicked, OnExit;

    public void Start()
    {
        BuildManager.Instance.inputManager = this;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }
    }
    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
    
    public Vector3 GetSelectedMapPosistion()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePos);
        if(Physics.Raycast(ray, out RaycastHit target, 1000, placementMask))
        {
            lastPosistion = target.point;
        }
        return lastPosistion;
    }
}
