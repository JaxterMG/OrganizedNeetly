using System.Collections.Generic;
using UnityEngine;

public class GridHighlighter : MonoBehaviour
{
    private List<CellHighlighter> highlightedCells = new List<CellHighlighter>();
    public DragAndDrop dragAndDropScript;
    public GridVisualizer gridVisualizer;

    void Update()
    {
        if (dragAndDropScript.draggingElement != null)
        {
            ClearHighlightedCells();  // Clear the previously highlighted cells.

            // Calculate the grid position where the element is currently located.
            Vector3 currentPosition = dragAndDropScript.draggingElement.transform.position;
            int gridX = Mathf.FloorToInt((currentPosition.x - gridVisualizer.transform.position.x) / gridVisualizer.cellWidth);
            int gridY = Mathf.FloorToInt((currentPosition.y - gridVisualizer.transform.position.y) / gridVisualizer.cellHeight);

            // Get all the cells that the element currently covers and highlight them.
            foreach (Vector2 offset in dragAndDropScript.draggingElement.Shape)
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
        if (x < 0 || x >= gridVisualizer.gridWidth || y < 0 || y >= gridVisualizer.gridHeight)
        {
            return null;  // Return null if the coordinates are out of the grid bounds.
        }

        // Calculate the index of the cell in the grid's children and get the CellHighlighter component.
        int index = y * gridVisualizer.gridWidth + x;
        return gridVisualizer.transform.GetChild(index).GetComponent<CellHighlighter>();
    }
    private void GetCellTransform()
    {
        
    }
}