using System.Collections.Generic;
using UnityEngine;
using System;

public class FiguresSpawner : MonoBehaviour
{
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
            SpawnFigures();
        }
    }

    private void SpawnFigures()
    {
        for (_currentFiguresCount = 0; _currentFiguresCount < _desiredFiguresCount; _currentFiguresCount++)
        {
            var figure = Instantiate(_figures[UnityEngine.Random.Range(0, _figures.Count)], Vector3.zero, Quaternion.identity);
            figure.Initialize(_figuresHolder, _grid);
            _figuresHolder.AddFigure(figure.transform);
        }
    }

    void OnDisable()
    {
        FigureDragHandler.FigurePlaced -= OnFigurePlaced;
    }

}
