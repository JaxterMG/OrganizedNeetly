using System.Collections.Generic;
using UnityEngine;
using System;

public class FigureDragHandler : MonoBehaviour
{
    private EventBus _eventBus;
    public static event Action FigurePlaced;
    [SerializeField] private FiguresHolder _figuresHolder;
    public Dictionary<Vector2, Cell> FigureData = new Dictionary<Vector2, Cell>();
    public List<Vector2> Shape;
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging;

    private Grid _grid;
    private Collider2D _collider;

    [SerializeField] LayerMask _gridMask;

    private void OnEnable()
    {           
        _collider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
        for (int i = 0; i < transform.childCount; i++)
        {
            FigureData.TryAdd(Shape[i], transform.GetChild(i).GetComponent<Cell>());
        }
    }    
    public void SetShape(List<Vector2> shape)
    {
        Shape = shape;
    }
    public void Initialize(EventBus eventBus,FiguresHolder figuresHolder, Grid grid)
    {
        _eventBus = eventBus;
        _figuresHolder = figuresHolder;
        _grid = grid;
    }

    private void OnMouseDown()
    {
        _eventBus.Publish<string>(EventType.PlaySound, "Pickup");
        isDragging = true;
        offset = gameObject.transform.position - GetMouseWorldPos() + new Vector3(0, 0.5f, 0);
        _figuresHolder.ReleaseFigure(this);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseUp()
    {
        _eventBus.Publish<string>(EventType.PlaySound, "Place");
        isDragging = false;
        GridCell closestHit = FindClosestCellToFigure();
        //Debug.Log($"Closest hit {closestHit?.transform.position}");
        if (_grid.TryPlaceFigure(this, closestHit))
        {
            //TODO: Rework delete
            // Destroy(_collider);
            Destroy(gameObject, 0f);
            FigurePlaced?.Invoke();
            return;
        }

        _figuresHolder.AddFigure(this);
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }
    private GridCell FindClosestCellToFigure()
    {
        float shortestMagnitude = 999;
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.35f, 0.35f), 0, Vector2.zero, 1, _gridMask);

        if (hits.Length == 0) return null;

        RaycastHit2D closestHit = hits[0];
        foreach (var hit in hits)
        {
            var magnitude = (hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude;
            //Debug.Log($"Magnitude {magnitude}");

            if (magnitude < shortestMagnitude)
            {
                shortestMagnitude = magnitude;
                closestHit = hit;
            }
        }
        return closestHit.transform.GetComponent<GridCell>();
    }
}