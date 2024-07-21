using System.Collections.Generic;
using UnityEngine;
using System;
using Core.EventBus;

namespace Core.Grid.Figures
{
    public class FigureDragHandler : MonoBehaviour
    {
        private EventBus.EventBus _eventBus;
        public string FigureName;
        public int FigureIndex;
        public static event Action FigurePlaced;
        [SerializeField] private FiguresHolder _figuresHolder;
        public Dictionary<Vector2, Cell> FigureData = new Dictionary<Vector2, Cell>();
        public List<Vector2> Shape;
        private Vector3 offset;
        private UnityEngine.Camera mainCamera;
        private bool isDragging;

        private Grid _grid;
        private Collider2D _collider;
        public int FigureId;

        [SerializeField] LayerMask _gridMask;

        private void OnEnable()
        {
            _collider = GetComponent<Collider2D>();
            mainCamera = UnityEngine.Camera.main;
            for (int i = 0; i < transform.childCount; i++)
            {
                FigureData.TryAdd(Shape[i], transform.GetChild(i).GetComponent<Cell>());
            }
        }

        public void SetShape(List<Vector2> shape)
        {
            Shape = shape;
        }

        public void Initialize(EventBus.EventBus eventBus, FiguresHolder figuresHolder, Grid grid, Color figureColor,
            int figureId)
        {
            _eventBus = eventBus;
            _figuresHolder = figuresHolder;
            _grid = grid;
            SetColorTheme(figureColor);
            FigureId = figureId;
        }

        private void SetColorTheme(Color figureColor)
        {
            foreach (var cell in GetComponentsInChildren<Cell>())
            {
                cell.SetColor(figureColor);
                cell.FigureName = FigureName;
            }
        }

        private void OnMouseDown()
        {
            _eventBus.Publish<string>(BusEventType.PlaySound, "Pickup");
            isDragging = true;
            offset = gameObject.transform.position - GetMouseWorldPos() + new Vector3(0, 0.5f, 0);
            OnFigureDrag();
        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
            return mainCamera.ScreenToWorldPoint(mousePoint);
        }

        private void OnFigureDrag()
        {
            _figuresHolder.OnFigureDrag(this);
        }


        private void OnMouseUp()
        {
            _eventBus.Publish<string>(BusEventType.PlaySound, "Place");
            isDragging = false;
            GridCell closestHit = FindClosestCellToFigure();
            //Debug.Log($"Closest hit {closestHit?.transform.position}");
            if (_grid.TryPlaceFigure(this, closestHit))
            {
                //TODO: Rework delete
                // Destroy(_collider);
                OnFigureRelease(false);
                Destroy(gameObject, 0f);
                FigurePlaced?.Invoke();
                if (_figuresHolder.GetFigures().Count > 0)
                {
                    _grid.CheckAvailableSpaceForFigures(_figuresHolder.GetFigures());
                }

                return;
            }

            OnFigureRelease(true);
        }

        private void OnFigureRelease(bool backToHolder)
        {
            _figuresHolder.OnFigureRelease(this, backToHolder);
        }

        private void Update()
        {
            if (isDragging)
            {
                Vector3 mousePos = GetMouseWorldPos();
                transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, -0.01f);
            }
        }

        private GridCell FindClosestCellToFigure()
        {
            float shortestMagnitude = 999;
            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.35f, 0.35f), 0, Vector2.zero,
                1, _gridMask);

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
}