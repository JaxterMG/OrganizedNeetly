using System.Collections.Generic;
using UnityEngine;

namespace Core.GridElements
{
    public class GridHighlighter: MonoBehaviour, IProvidable
    {
        private List<CellHighlighter> highlightedCells = new List<CellHighlighter>();
        public DragAndDrop DragAndDropScript;
        public GridVisualizer DridVisualizer;

        public void OnInitialize()
        {
            
        }

        public void OnUpdate()
        {
            if (DragAndDropScript.draggingElement != null)
            {
                ClearHighlightedCells();  // Clear the previously highlighted cells.

                // Calculate the grid position where the element is currently located.
                Vector3 currentPosition = DragAndDropScript.draggingElement.transform.position;
                int gridX = Mathf.FloorToInt((currentPosition.x - DridVisualizer.transform.position.x) / DridVisualizer.cellWidth);
                int gridY = Mathf.FloorToInt((currentPosition.y - DridVisualizer.transform.position.y) / DridVisualizer.cellHeight);

                // Get all the cells that the element currently covers and highlight them.
                foreach (Vector2 offset in DragAndDropScript.draggingElement.Shape)
                {
                    int cellX = gridX + Mathf.RoundToInt(offset.x);
                    int cellY = gridY + Mathf.RoundToInt(offset.y);
                    CellHighlighter cell = GetCellAt(cellX, cellY);
                    //Debug.Log("Dragging element at grid position (" + cellX + ", " + cellY + ")");
                    if (cell != null)
                    {
                        cell.Highlight(true);
                        highlightedCells.Add(cell);
                    }
                }

            }
            else
            {
                ClearHighlightedCells();  // Clear the highlighted cells when the mouse button is released.
            }
        }

        private void ClearHighlightedCells()
        {
            foreach (CellHighlighter cell in highlightedCells)
            {
                cell.Highlight(false);
            }

            highlightedCells.Clear();
        }

        private CellHighlighter GetCellAt(int x, int y)
        {
            if (x < 0 || x >= DridVisualizer.gridWidth || y < 0 || y >= DridVisualizer.gridHeight)
            {
                return null;  // Return null if the coordinates are out of the grid bounds.
            }

            // Calculate the index of the cell in the grid's children and get the CellHighlighter component.
            int index = y * DridVisualizer.gridWidth + x;
            return DridVisualizer.transform.GetChild(index).GetComponent<CellHighlighter>();
        }
    }
}
