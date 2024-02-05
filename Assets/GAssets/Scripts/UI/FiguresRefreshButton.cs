using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FiguresRefreshButton : MonoBehaviour
{
    [SerializeField] Button _refreshButton;
    private FiguresSpawner _figuresSpawner;
    void OnEnable()
    {
        _figuresSpawner = FindAnyObjectByType<FiguresSpawner>();
        _refreshButton.onClick.AddListener(RefreshFigures);
    }
    void OnDisable()
    {
        _refreshButton.onClick.RemoveListener(RefreshFigures);
    }
    private void RefreshFigures()
    {
        _figuresSpawner.SpawnReviveFigures();
    }
 }
