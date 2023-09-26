using System.Net.Http.Headers;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid : MonoBehaviour, IProvidable
{
    public GridCell gridCellPrefab;  // Prefab for the grid cell
    public int gridRows = 10;  // Number of rows
    public int gridColumns = 10;  // Number of columns
    public float gapSize = 0.1f; // Size of the gap between cells
    public float cellSize = 1f;
    private GridCell[,] _grid;  // The grid represented as a 2D array

    private Transform _gridHolder;

    public void OnInitialize()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _gridHolder = new GameObject("GridHolder").transform;
        _gridHolder.position = CalculateGridCenter();
        _grid = new GridCell[gridRows, gridColumns];

        for (int y = 0; y < gridRows; y++)
        {
            for (int x = 0; x < gridColumns; x++)
            {
                // Calculate the position for this cell
                Vector3 cellPosition = new Vector3((x * (cellSize + gapSize)), (y * (cellSize + gapSize)), 0);

                // Instantiate the cell prefab at this position
                GridCell cell = Instantiate(gridCellPrefab, cellPosition, Quaternion.identity);

                cell.GridPos = new Vector2(x, y);

                // Set the cell's size
                cell.transform.localScale = new Vector3(cellSize, cellSize, cellSize);

                _grid[x, y] = cell;
                cell.transform.SetParent(_gridHolder);
            }
        }
        _gridHolder.position = Vector3.zero;
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

        if (_grid[(int)closestCell.GridPos.x, (int)closestCell.GridPos.y].Cell != null) return false;

        foreach (var pos in figureDragHandler.FigureData.Keys)
        {
            if (_grid[(int)closestCell.GridPos.x + (int)pos.x, (int)closestCell.GridPos.y + (int)pos.y].Cell != null) return false;
        }

        List<Vector2> touchedGridCellsPos = new List<Vector2>();
        foreach (var pos in figureDragHandler.FigureData.Keys)
        {
            _grid[(int)closestCell.GridPos.x + (int)pos.x, (int)closestCell.GridPos.y + (int)pos.y].Cell = figureDragHandler.FigureData[pos];

            figureDragHandler.FigureData[pos].transform.SetParent(_grid[(int)closestCell.GridPos.x + (int)pos.x, (int)closestCell.GridPos.y + (int)pos.y].Cell.transform);

            touchedGridCellsPos.Add(closestCell.GridPos + pos);
            Debug.Log(pos);
        }
        //Destroy(figureDragHandler.gameObject);
        CheckLinesToDelete(touchedGridCellsPos);

        return true;
    }

    public void CheckLinesToDelete(List<Vector2> touchedCells)
    {
        List<int> horizontalLinesToDelete = new List<int>();
        List<int> verticalLinesToDelete = new List<int>();
        
        foreach (var cell in touchedCells)
        {
            bool isHorizontalDelete = true;
            bool isVerticalDelete = true;

            for (var i = 0; i < gridRows; i++)
            {
                if(_grid[(int)i, (int)cell.y].Cell == null)
                {
                    isHorizontalDelete = false;
                    break;
                }
            }
            
            for (var i = 0; i < gridColumns; i++)
            {
                if(_grid[(int)cell.x, (int)i].Cell == null)
                {
                    isVerticalDelete = false;
                    break;
                }
                
            }

            if(isHorizontalDelete) horizontalLinesToDelete.Add((int)cell.y); 
            if(isVerticalDelete) verticalLinesToDelete.Add((int)cell.x);
        }
        if(horizontalLinesToDelete.Count > 0)
        {
            foreach (var line in horizontalLinesToDelete)
            {
                for (int i = 0; i < gridRows; i++)
                {
                    Destroy(_grid[i, line].Cell.gameObject);
                }
            }

        }
        if(verticalLinesToDelete.Count > 0)
        {
            foreach (var line in verticalLinesToDelete)
            {
                for (int i = 0; i < gridColumns; i++)
                {
                    Destroy(_grid[line, i].Cell.gameObject);
                }
            }
        }
    }

    public void ClearGrid()
    {
        Destroy(_gridHolder.gameObject);
        Array.Clear(_grid, 0, _grid.Length);
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}