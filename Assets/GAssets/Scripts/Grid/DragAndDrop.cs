using UnityEngine;
namespace Core.GridElements
{
    public class DragAndDrop : MonoBehaviour, IProvidable
    {
        public ComplexGridElement draggingElement;
        private Vector3 offset;
        public Grid gridComponent;  // Unity's inbuilt Grid component
        public GridVisualizer gridVisualizer;  // Drag your GridVisualizer here in the inspector.

        public void OnInitialize()
        {
            //throw new System.NotImplementedException();
        }
        public void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Raycast to check if we clicked on a ComplexGridElement
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null)
                {
                    draggingElement = hit.collider.GetComponent<ComplexGridElement>();
                    if (draggingElement != null)
                    {
                        // If we did, calculate the offset between the cursor and the element's center
                        offset = draggingElement.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0) && draggingElement != null)
            {
                // Snap the element to the grid when the mouse button is released
                Vector3 finalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

                // Get the cell position closest to the final position
                Vector3Int cell = gridComponent.WorldToCell(finalPosition);

                // Check if all cells that the element would occupy are within the grid
                bool canPlace = true;
                foreach (Vector2 offset in draggingElement.Shape)
                {
                    Vector3Int cellPosition = cell + Vector3Int.RoundToInt(offset);
                    if (cellPosition.x < 0 || cellPosition.x >= gridVisualizer.gridWidth || cellPosition.y < 0 || cellPosition.y >= gridVisualizer.gridHeight)
                    {
                        canPlace = false;
                        break;
                    }
                }

                if (canPlace)
                {
                    draggingElement.transform.position = gridComponent.GetCellCenterWorld(cell);
                }
                draggingElement = null; // Clear the dragging element
            }
            else if (draggingElement != null)
            {
                // Move the dragging element with the cursor
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                draggingElement.transform.position = newPosition;
            }
        }

        
    }
}