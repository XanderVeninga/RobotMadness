using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    public List<ApplianceClass> applianceList = new();

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTileMap;
    [SerializeField] private TileBase tileBase;

    public GameObject[] prefabs;
    private PlaceableObject objectToPlace;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            InitializeWithObject(prefabs[0]);
        }
    }

    public static Vector3 GetMouseWorldPosistion()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo) )
        {
            return hitInfo.point;
        }
        else { return Vector3.zero; }
    }
    
    public Vector3 SnapCoordinateToGrid(Vector3 posistion)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(posistion);
        posistion = grid.GetCellCenterWorld(cellPos);
        return posistion;
    }

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
    }
}
