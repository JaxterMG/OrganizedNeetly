using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FigureDragHandler : MonoBehaviour
{
    [SerializeField] private FiguresHolder _figuresHolder;
    public List<Vector2> Shape;

    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging;

    // Grid reference
    public Grid gridManager;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    public void Initialize(List<Vector2> shape)
    {
        Shape = shape;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        offset = gameObject.transform.position - GetMouseWorldPos();
        _figuresHolder.ReleaseFigure(transform);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (!gridManager.TryPlaceFigure(this))
        {
            _figuresHolder.AddFigure(this.transform);
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }
}