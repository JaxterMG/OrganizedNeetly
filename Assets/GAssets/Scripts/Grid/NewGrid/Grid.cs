using System.Net.Http.Headers;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour, IProvidable
{
    public GridCell gridCellPrefab;  // Prefab for the grid cell
    public int gridRows = 10;  // Number of rows
    public int gridColumns = 10;  // Number of columns
    public float gapSize = 0.1f; // Size of the gap between cells
    public float cellSize = 1f;
    private GridCell[,] grid;  // The grid represented as a 2D array

    private Dictionary<Vector2, GridCell> _gridCellsPositions = new Dictionary<Vector2, GridCell>();

    public void OnInitialize()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Transform gridHolder = new GameObject("GridHolder").transform;
        gridHolder.position = CalculateGridCenter();
        grid = new GridCell[gridRows, gridColumns];

        for (int y = 0; y < gridRows; y++)
        {
            for (int x = 0; x < gridColumns; x++)
            {
                // Calculate the position for this cell
                Vector3 cellPosition = new Vector3((x * (cellSize + gapSize)), (y * (cellSize + gapSize)), 0);

                // Instantiate the cell prefab at this position
                GridCell cell = Instantiate(gridCellPrefab, cellPosition, Quaternion.identity);

                cell.GridPos = new Vector2(x, y);

                _gridCellsPositions.TryAdd(cell.GridPos, cell);

                // Set the cell's size
                cell.transform.localScale = new Vector3(cellSize, cellSize, cellSize);

                grid[x, y] = cell;
                cell.transform.SetParent(gridHolder);
            }
        }
        gridHolder.position = Vector3.zero;
    }
    Vector3 CalculateGridCenter()
    {
        // Calculate the total width and height of the grid
        float totalWidth = (gridColumns * cellSize) + ((gridColumns - 1) * gapSize);
        float totalHeight = (gridRows * cellSize) + ((gridRows - 1) * gapSize);

        // The center point is at half the total width and height, accounting for the pivot in the center of the cells
        Vector3 center = new Vector3(totalWidth / 2f - cellSize / 2f, totalHeight / 2f - cellSize / 2f, 0);

        return center;
    }

    public bool TryPlaceFigure(FigureDragHandler figureDragHandler, GridCell closestCell)
    {
        if(!closestCell) return false;
        
        figureDragHandler.transform.position = closestCell.transform.position;

        if (!_gridCellsPositions.TryGetValue(closestCell.GridPos, out var startGridCell)) return false;

        foreach (var pos in figureDragHandler.FigureData.Keys)
        {
            if (!_gridCellsPositions.TryGetValue(closestCell.GridPos + pos, out var gridCell)) return false;
            if (gridCell.Cell != null) return false;
        }

        foreach (var pos in figureDragHandler.FigureData.Keys)
        {
            _gridCellsPositions.TryGetValue(closestCell.GridPos + pos, out var gridCell);
            gridCell.Cell = figureDragHandler.FigureData[pos];
        }
        return true;
    }


    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}