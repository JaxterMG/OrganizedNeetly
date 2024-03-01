using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Zenject;
using DG.Tweening;
using Newtonsoft.Json;

public class Grid : MonoBehaviour, IProvidable, ISavable
{
    [SerializeField] GameObject _cellPrefab;
    public GridCell gridCellPrefab;
    public int gridRows = 10;
    public int gridColumns = 10;
    public float gapSize = 0.1f;
    public float cellSize = 1f;
    public GridCell[,] GridCells {get; private set;}

    [SerializeField] Transform _placedCellsHolder;

    private Transform _gridHolder;

    [Inject] IScoreController _scoreController;
    [Inject] EventBus _eventBus;

    [SerializeField] private FieldColor _fieldColor;

    public event Action<IScoreController> Fail;
    [Inject]ColorsChanger _colorsChanger;

    public void OnInitialize()
    {
        SaveLoadHandler saveLoadHandler = FindObjectOfType<SaveLoadHandler>();
        saveLoadHandler.RegisterSavable(this);

        _colorsChanger.LoadGridColor(PlayerPrefs.GetString("GridColor", "DefaultGrid"));
        _eventBus.Subscribe<FieldColor>(BusEventType.ChangeGridColor, ChangeGridColor);
        _eventBus.Subscribe<List<FigureDragHandler>>(BusEventType.SpawnFigures, CheckAvailableSpaceForFigures);
        
        if(!saveLoadHandler.HasSaveFile())
            GenerateGrid();
        else
        {
            saveLoadHandler.LoadAll();
        }
    }
    void OnDestroy()
    {
        _eventBus.Unsubscribe<FieldColor>(BusEventType.ChangeGridColor, ChangeGridColor);
        _eventBus.Unsubscribe<List<FigureDragHandler>>(BusEventType.SpawnFigures, CheckAvailableSpaceForFigures);
    }

    void GenerateGrid()
    {
        _gridHolder = new GameObject("GridHolder").transform;
        _gridHolder.position = CalculateGridCenter();
        GridCells = new GridCell[gridRows, gridColumns];

        for (int y = 0; y < gridRows; y++)
        {
            for (int x = 0; x < gridColumns; x++)
            {
                // Calculate the position for this cell
                Vector3 cellPosition = new Vector3((x * (cellSize + gapSize)), (y * (cellSize + gapSize)), 0);

                // Instantiate the cell prefab at this position
                GridCell cell = Instantiate(gridCellPrefab, cellPosition, Quaternion.identity);
                cell.GetComponent<SpriteRenderer>().color = _fieldColor.Color;
                cell.GridPos = new Vector2(x, y);

                // Set the cell's size
                cell.transform.localScale = new Vector3(cellSize, cellSize, cellSize);

                GridCells[x, y] = cell;
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
        if (!closestCell) return false;

        figureDragHandler.transform.position = closestCell.transform.position;

        if (GridCells[(int)closestCell.GridPos.x, (int)closestCell.GridPos.y].Cell != null) return false;

        if (!CanPlaceFigure(figureDragHandler.Shape, (int)closestCell.GridPos.x, (int)closestCell.GridPos.y)) return false;

        List<Vector2> touchedGridCellsPos = new List<Vector2>();
        foreach (var pos in figureDragHandler.FigureData.Keys)
        {
            GridCell gridCell = GridCells[(int)closestCell.GridPos.x + (int)pos.x, (int)closestCell.GridPos.y + (int)pos.y];

            
            gridCell.Cell = figureDragHandler.FigureData[pos];

            //TODO: Clean handlers without breaking scale animation
            figureDragHandler.FigureData[pos].transform.SetParent(_placedCellsHolder);
            figureDragHandler.FigureData[pos].transform.position = gridCell.transform.position;
            figureDragHandler.FigureData[pos].transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

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
            if (((int)col + (int)pos.x) > gridColumns - 1
            || ((int)col + (int)pos.x) < 0
            || ((int)row + (int)pos.y) < 0
            || ((int)row + (int)pos.y) > gridRows - 1)
            {
                return false;
            }
            if (GridCells[(int)col + (int)pos.x, (int)row + (int)pos.y].Cell != null)
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
    public void CheckAvailableSpaceForFigures(List<FigureDragHandler> figures)
    {
        foreach (var figure in figures)
        {
            if (IsSpaceAvailableForFigure(figure.Shape))
            {
                return ;
            }
        }

        Debug.Log("fail");
        Fail?.Invoke(_scoreController);
    }

    public async void CheckLinesToDelete(List<Vector2> touchedCells)
    {
        List<int> horizontalLinesToDelete = new List<int>();
        List<int> verticalLinesToDelete = new List<int>();

        foreach (var cell in touchedCells)
        {
            bool isHorizontalDelete = true;
            bool isVerticalDelete = true;

            if (!horizontalLinesToDelete.Contains((int)cell.y))
            {
                for (var i = 0; i < gridRows; i++)
                {
                    if (GridCells[i, (int)cell.y].Cell == null)
                    {
                        isHorizontalDelete = false;
                        break;
                    }
                }
            }
            else isHorizontalDelete = false;

            if (!verticalLinesToDelete.Contains((int)cell.x))
            {
                for (var i = 0; i < gridColumns; i++)
                {
                    if (GridCells[(int)cell.x, i].Cell == null)
                    {
                        isVerticalDelete = false;
                        break;
                    }

                }
            }
            else isVerticalDelete = false;

            if (isHorizontalDelete) horizontalLinesToDelete.Add((int)cell.y);
            if (isVerticalDelete) verticalLinesToDelete.Add((int)cell.x);
        }

        int linesToDelete = horizontalLinesToDelete.Count + verticalLinesToDelete.Count;

        if (linesToDelete > 0)
        {
            _eventBus.Publish<int>(BusEventType.IncreaseScore, linesToDelete);
            _eventBus.Publish<(int, Vector3)>(BusEventType.AddMoney, (linesToDelete, GridCells[(int)touchedCells[0].x, (int)touchedCells[0].y].Cell.transform.position));
        }

        if (horizontalLinesToDelete.Count > 0)
        {
            _eventBus.Publish<string>(BusEventType.PlaySound, "LineDelete");
            foreach (var line in horizontalLinesToDelete)
            {
                for (int i = 0; i < gridRows; i++)
                {
                    GridCells[i, line].Cell?.transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.Linear).OnComplete(() => 
                    {
                        Destroy(GridCells[i, line].Cell?.gameObject);
                    });
                    await Task.Delay(100);
                }
            }

        }
        if (verticalLinesToDelete.Count > 0)
        {
            _eventBus.Publish<string>(BusEventType.PlaySound, "LineDelete");
            foreach (var line in verticalLinesToDelete)
            {
                for (int i = 0; i < gridColumns; i++)
                {
                    GridCells[line, i].Cell?.transform.DOScale(Vector3.zero, 0.05f).SetEase(Ease.Linear).OnComplete(() => 
                    {
                        Destroy(GridCells[line, i].Cell?.gameObject);
                    });
                    await Task.Delay(50);
                }
            }
        }
    }

    public void ClearGrid()
    {
        foreach (Transform item in _placedCellsHolder)
        {
            Destroy(item.gameObject);
        }

        Destroy(_gridHolder.gameObject);
        Array.Clear(GridCells, 0, GridCells.Length);
    }

    private void ChangeGridColor(FieldColor fieldColor)
    {
        _fieldColor = fieldColor;
        if(GridCells == null)return;
        for (var i = 0; i < GridCells.GetLength(0); i++)
        {
            for (var j = 0; j < GridCells.GetLength(1); j++)
            {
                GridCells[i,j].GetComponent<SpriteRenderer>().color = fieldColor.Color;
            }
        }
    }

    public void OnUpdate()
    {
    }

    private struct GridSaveData
    {
        public GridCellData[,] GridCells;
        public GridSaveData(GridCell[,] cell)
        {
            GridCells = new GridCellData[cell.GetLength(0), cell.GetLength(1)];
            for (var y = 0; y < cell.GetLength(1); y++)
            {
                for (var x = 0; x < cell.GetLength(0); x++)
                {
                    GridCellData gridCell = new GridCellData
                    (
                        (int)cell[x,y].GridPos.x,
                        (int)cell[x,y].GridPos.y,
                        cell[x,y]?.Cell == null ? false : true,
                        cell[x,y]?.Cell?.FigureName
                        //cell[i,j].Cell.CellTheme
                    );
                    GridCells[x,y] = gridCell;
                }
            }
        }
    }
    private struct GridCellData
    {
        public int GridPosX;
        public int GridPosY;
        public bool IsOccupied;
        public string FigureName;

        public GridCellData(int gridPosX, int gridPosY, bool isOccupied, string figureName)
        {
            GridPosX = gridPosX;
            GridPosY = gridPosY;
            IsOccupied = isOccupied;
            FigureName = figureName;
        }
    }
    public string Save()
    {
        GridSaveData gridSaveData = new GridSaveData(GridCells);
        return JsonConvert.SerializeObject(gridSaveData);
    }

    public void Load(string jsonData)
    {
        var data = JsonConvert.DeserializeObject<GridSaveData>(jsonData);
        LoadSavedGrid(data);
    }
    private void LoadSavedGrid(GridSaveData data)
    {
        if(_gridHolder != null)
            Destroy(_gridHolder.gameObject);
        GridCells = null;
            
        _gridHolder = new GameObject("GridHolder").transform;
        _gridHolder.position = CalculateGridCenter();
        GridCells = new GridCell[data.GridCells.GetLength(0), data.GridCells.GetLength(1)];

        for (int y = 0; y < data.GridCells.GetLength(1); y++)
        {
            for (int x = 0; x < data.GridCells.GetLength(0); x++)
            {
                // Calculate the position for this cell
                Vector3 cellPosition = new Vector3((x * (cellSize + gapSize)), (y * (cellSize + gapSize)), 0);

                // Instantiate the cell prefab at this position
                GridCell cell = Instantiate(gridCellPrefab, cellPosition, Quaternion.identity);
                cell.GetComponent<SpriteRenderer>().color = _fieldColor.Color;
                cell.GridPos = new Vector2(x, y);


                // Set the cell's size
                cell.transform.localScale = new Vector3(cellSize, cellSize, cellSize);

                GridCells[x, y] = cell;

                cell.transform.SetParent(_gridHolder);
                if(data.GridCells[x,y].IsOccupied)
                {
                    Cell newCell = Instantiate(_cellPrefab, GridCells[x,y].transform.position, Quaternion.identity).GetComponent<Cell>();
                    newCell.FigureName = data.GridCells[x,y].FigureName;
                    newCell.SetColor(_colorsChanger.GetFiguresColors().Figures[newCell.FigureName]);
                    newCell.transform.SetParent(_placedCellsHolder);
                    GridCells[x, y].Cell = newCell;
                    newCell.transform.position = cell.transform.localPosition;
                    newCell.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                }
            }
        }
        _gridHolder.position = Vector3.zero;
    }
}