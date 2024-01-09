using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Zenject;

public class Grid : MonoBehaviour, IProvidable
{
    public GridCell gridCellPrefab;  // Prefab for the grid cell
    public int gridRows = 10;  // Number of rows
    public int gridColumns = 10;  // Number of columns
    public float gapSize = 0.1f; // Size of the gap between cells
    public float cellSize = 1f;
    private GridCell[,] _grid;  // The grid represented as a 2D array

    [SerializeField] Transform _placedCellsHolder;

    private Transform _gridHolder;

    [Inject] IScoreController _scoreController;
    [Inject] FiguresHolder _figuresHolder;
    [Inject] EventBus _eventBus;

    public event Action Fail;

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
        
        if(!CanPlaceFigure(figureDragHandler.Shape, (int)closestCell.GridPos.x, (int)closestCell.GridPos.y)) return false;

        List<Vector2> touchedGridCellsPos = new List<Vector2>();
        foreach (var pos in figureDragHandler.FigureData.Keys)
        {
            _grid[(int)closestCell.GridPos.x + (int)pos.x, (int)closestCell.GridPos.y + (int)pos.y].Cell = figureDragHandler.FigureData[pos];

            //TODO: Clean handlers without breaking scale animation
            figureDragHandler.FigureData[pos].transform.SetParent(_placedCellsHolder);

            touchedGridCellsPos.Add(closestCell.GridPos + pos);
            //Debug.Log(pos);
        }

        CheckLinesToDelete(touchedGridCellsPos);

        return true;
    }
    bool CanPlaceFigure(List<Vector2> figure, int col, int row)
    {
        foreach (var pos in figure)
        {
            if(((int)col + (int)pos.x) > gridColumns - 1
            || ((int)col + (int)pos.x) < 0
            || ((int)row + (int)pos.y) < 0 
            || ((int)row + (int)pos.y) > gridRows - 1 )
            {
                return false;
            }
            if (_grid[(int)col + (int)pos.x, (int)row + (int)pos.y].Cell != null) 
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Check if there is space to place a figure on the entire grid
    /// </summary>
    /// <param name="figure"></param>
    /// <returns></returns>  
    bool IsSpaceAvailableForFigure(List<Vector2> figure)
    {
        for (int col = 0; col < gridColumns; col++)
        {
            for (int row = 0; row < gridRows; row++)
            {
                if (CanPlaceFigure(figure, col, row))
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if ability to place a figure on the entire grid
    /// </summary>
    /// <param name="figure"></param>
    bool CheckAvailableSpaceForFigures(List<FigureDragHandler> figures)
    {
        foreach (var figure in figures)
        {
            if (IsSpaceAvailableForFigure(figure.Shape))
            {
                return true;
            }            
        }

        Debug.Log("fail");
        Fail?.Invoke();
        return false;

    }

    public async void CheckLinesToDelete(List<Vector2> touchedCells)
    {
        List<int> horizontalLinesToDelete = new List<int>();
        List<int> verticalLinesToDelete = new List<int>();
        
        foreach (var cell in touchedCells)
        {
            bool isHorizontalDelete = true;
            bool isVerticalDelete = true;

            if(!horizontalLinesToDelete.Contains((int)cell.y))
            {
                for (var i = 0; i < gridRows; i++)
                {
                    if(_grid[i, (int)cell.y].Cell == null)
                    {
                        isHorizontalDelete = false;
                        break;
                    }
                }
            }
            else isHorizontalDelete = false;

            if(!verticalLinesToDelete.Contains((int)cell.x))
            {
                for (var i = 0; i < gridColumns; i++)
                {
                    if(_grid[(int)cell.x, i].Cell == null)
                    {
                        isVerticalDelete = false;
                        break;
                    }
                    
                }
            }
            else isVerticalDelete = false;

            if(isHorizontalDelete) horizontalLinesToDelete.Add((int)cell.y); 
            if(isVerticalDelete) verticalLinesToDelete.Add((int)cell.x);
        }

        int linesToDelete = horizontalLinesToDelete.Count + verticalLinesToDelete.Count;
        
        if(linesToDelete > 0)
            _eventBus.Publish<int>(EventType.IncreaseScore, linesToDelete);

        if(horizontalLinesToDelete.Count > 0)
        {
            foreach (var line in horizontalLinesToDelete)
            {
                for (int i = 0; i < gridRows; i++)
                {
                    await Task.Delay(50);
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
                    await Task.Delay(50);
                    Destroy(_grid[line, i].Cell.gameObject);
                }
            }
        }
        await Task.Delay(1);
        CheckAvailableSpaceForFigures(_figuresHolder.GetFigures());
    }

    public void ClearGrid()
    {
        foreach (Transform item in _placedCellsHolder)
        {
           Destroy(item.gameObject); 
        }

        Destroy(_gridHolder.gameObject);
        Array.Clear(_grid, 0, _grid.Length);
    }
    // Check if there is space to place a figure represented by a List<Vector2>
    

    public void OnUpdate()
    {
    }
}