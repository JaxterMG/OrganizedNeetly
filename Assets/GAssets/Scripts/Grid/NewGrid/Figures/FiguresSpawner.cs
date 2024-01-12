using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class FiguresSpawner : MonoBehaviour
{
    [Inject] EventBus _eventBus;
    [Inject] ColorsChanger _colorsChanger;
    
    [SerializeField] private int _spawnDelay = 200;
    [SerializeField] private List<FigureDragHandler> _figures;

    private int _currentFiguresCount;
    [SerializeField] private int _desiredFiguresCount = 3;  
    private Grid _grid;
    private FiguresHolder _figuresHolder;

    void OnEnable()
    {
        _grid = FindAnyObjectByType<Grid>();
        _figuresHolder = GetComponent<FiguresHolder>();
        FigureDragHandler.FigurePlaced += OnFigurePlaced;
    }
    public void OnInititalize()
    {
        SpawnFigures();
    }

    private void OnFigurePlaced()
    {
        _currentFiguresCount--;

        if(_currentFiguresCount <= 0)
        {
            _currentFiguresCount = _desiredFiguresCount;
            SpawnFigures();
        }
    }

    private async void SpawnFigures()
    {
        for (_currentFiguresCount = 0; _currentFiguresCount < _desiredFiguresCount; _currentFiguresCount++)
        {
            _eventBus.Publish<string>(EventType.PlaySound, "Spawn");
            await Task.Delay(_spawnDelay);

            FigureDragHandler figureDragHandler = _figures[UnityEngine.Random.Range(0, _figures.Count)];
            var figure = Instantiate(figureDragHandler, Vector3.zero, Quaternion.identity);
            figure.Initialize(_eventBus, _figuresHolder, _grid, _colorsChanger.GetFiguresColors().Figures[figureDragHandler.FigureName]);
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

}
