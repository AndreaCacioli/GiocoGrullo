using UnityEngine;
using UnityEngine.Tilemaps;

public class Selectable : MonoBehaviour
{
    Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }


    private void OnMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider == null) return;

        if (tilemap == null)
        {
            Debug.Log("Selected a non-tilemap ~" + gameObject.name);
            SelectionManager.getInstance().Select(this);
        }
        else
        {
            Debug.Log("Selected a tilemap ~" + gameObject.name);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int selectedCellCoord = tilemap.WorldToCell(mousePosition);
            selectedCellCoord.z = 0;
            SelectionManager.getInstance().Select(DataStructureManager.getInstance().getNode(selectedCellCoord));
        }
    }

}
