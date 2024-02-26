using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

public class FiguresSpawner : MonoBehaviour, ISavable
{
    [Inject] EventBus _eventBus;
    [Inject] ColorsChanger _colorsChanger;

    [SerializeField] private int _spawnDelay = 200;
    [SerializeField] private List<FigureDragHandler> _figures;
    [SerializeField] private Transform _spawnPoint;

    private int _currentFiguresCount;
    [SerializeField] private int _desiredFiguresCount = 3;
    private Grid _grid;
    private FiguresHolder _figuresHolder;

    void OnEnable()
    {
        _grid = FindAnyObjectByType<Grid>();
        _figuresHolder = GetComponent<FiguresHolder>();
        FigureDragHandler.FigurePlaced += OnFigurePlaced;
        FindObjectOfType<SaveLoadHandler>().RegisterSavable(this);
    }
    public void OnInititalize()
    {
        if (!FindObjectOfType<SaveLoadHandler>().HasSaveFile())
            SpawnFigures();
    }

    private void OnFigurePlaced()
    {
        FindObjectOfType<RandomNumbersGenerator>().IncreaseRandomCounter();
        _currentFiguresCount--;

        if (_currentFiguresCount <= 0)
        {
            _currentFiguresCount = _desiredFiguresCount;
            SpawnFigures();
        }
    }

    private async void SpawnFigures()
    {
        RandomNumbersGenerator randomNumbersGenerator = FindObjectOfType<RandomNumbersGenerator>();
        for (_currentFiguresCount = 0; _currentFiguresCount < _desiredFiguresCount; _currentFiguresCount++)
        {
            _eventBus.Publish<string>(EventType.PlaySound, "Spawn");
            await Task.Delay(_spawnDelay);
            // Можно сделать рандом с сидом для испытаний
            int figureId = randomNumbersGenerator.RequestRandomNumber(0, _figures.Count);

            FigureDragHandler figureDragHandler = _figures[figureId];
            var figure = Instantiate(figureDragHandler, _spawnPoint.position, Quaternion.identity);
            figure.Initialize(_eventBus, _figuresHolder, _grid, _colorsChanger.GetFiguresColors().Figures[figureDragHandler.FigureName], figureId);
            _figuresHolder.AddFigure(figure);
        }
        _eventBus.Publish<List<FigureDragHandler>>(EventType.SpawnFigures, _figuresHolder.GetFigures());
    }

    public async void SpawnReviveFigures(int figureIndex = 3)
    {
        ClearFigures();
        for (_currentFiguresCount = 0; _currentFiguresCount < _desiredFiguresCount; _currentFiguresCount++)
        {
            _eventBus.Publish<string>(EventType.PlaySound, "Spawn");
            await Task.Delay(_spawnDelay);
            // Можно сделать рандом с сидом для испытаний
            FigureDragHandler figureDragHandler = _figures[figureIndex];
            var figure = Instantiate(figureDragHandler, _spawnPoint.position, Quaternion.identity);
            figure.Initialize(_eventBus, _figuresHolder, _grid, _colorsChanger.GetFiguresColors().Figures[figureDragHandler.FigureName], figureIndex);
            _figuresHolder.AddFigure(figure);
        }
        _eventBus.Publish<List<FigureDragHandler>>(EventType.SpawnFigures, _figuresHolder.GetFigures());
    }


    public void ClearFigures()
    {
        _figuresHolder.ClearFigures();
    }

    void OnDisable()
    {
        FigureDragHandler.FigurePlaced -= OnFigurePlaced;
    }

    private struct FiguresSpawnerData
    {
        public int FiguresLeft;
        public int[] FigureIds;

        public FiguresSpawnerData(int figuresLeft, int[] figureIds)
        {
            FiguresLeft = figuresLeft;
            FigureIds = figureIds;
        }
    }

    public void Load(string jsonData)
    {
        var data = JsonConvert.DeserializeObject<FiguresSpawnerData>(jsonData);
        LoadFigures(data);
    }
    private async void LoadFigures(FiguresSpawnerData data)
    {
        for (_currentFiguresCount = 0; _currentFiguresCount < data.FiguresLeft; _currentFiguresCount++)
        {
            _eventBus.Publish<string>(EventType.PlaySound, "Spawn");
            await Task.Delay(_spawnDelay);
            // Можно сделать рандом с сидом для испытаний
            int figureId = data.FigureIds[_currentFiguresCount];
            FigureDragHandler figureDragHandler = _figures[data.FigureIds[_currentFiguresCount]];

            var figure = Instantiate(figureDragHandler, _spawnPoint.position, Quaternion.identity);
            figure.Initialize(_eventBus, _figuresHolder, _grid, _colorsChanger.GetFiguresColors().Figures[figureDragHandler.FigureName], figureId);
            _figuresHolder.AddFigure(figure);
        }
        _eventBus.Publish<List<FigureDragHandler>>(EventType.SpawnFigures, _figuresHolder.GetFigures());
    }
    public string Save()
    {
        var figures = _figuresHolder.GetFigures();
        int[] figuresIds = new int[figures.Count];

        for (int i = 0; i < figures.Count; i++)
        {
            figuresIds[i] = figures[i].FigureId;
        }

        FiguresSpawnerData figuresSpawnerData = new FiguresSpawnerData(_currentFiguresCount, figuresIds);
        return JsonConvert.SerializeObject(figuresSpawnerData);
    }
}
