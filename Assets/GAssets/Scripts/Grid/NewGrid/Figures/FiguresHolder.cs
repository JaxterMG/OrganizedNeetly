using DG.Tweening;
using UnityEngine;

public class FiguresHolder : MonoBehaviour
{
    public float spacing = 2f;
    public float totalWidth = 10;
    [SerializeField] private float _scale = 0.5f;
    [SerializeField] private float _scaleTime = 0.5f;
    [SerializeField] private float _moveTime = 0.5f;


    private void Start()
    {
        int figureCount = transform.childCount;

        float spacing = totalWidth / (figureCount + 1);

        for (int i = 0; i < figureCount; i++)
        {
            Transform figureTransform = transform.GetChild(i);
            AddFigure(figureTransform);
            float posX = -totalWidth / 2 + spacing * (i + 1);
            Vector3 newPosition = new Vector3(posX, 0, 0);

            figureTransform.localPosition = newPosition;

            figureTransform.SetSiblingIndex(i);
        }        
    }

    public void AddFigure(Transform figure)
    {
        figure.DOScale(_scale, _scaleTime);
        figure.SetParent(this.transform);
        ArrangeFigures();
    }
    public void ArrangeFigures()
    {
        int figureCount = transform.childCount;

        float spacing = totalWidth / (figureCount + 1);

        for (int i = 0; i < figureCount; i++)
        {
            Transform figureTransform = transform.GetChild(i);
            float posX = -totalWidth / 2 + spacing * (i + 1);
            Vector3 newPosition = new Vector3(posX, 0, 0);

            figureTransform.DOLocalMove(newPosition, _moveTime);

            figureTransform.SetSiblingIndex(i);
        }
    }
    public void ReleaseFigure(Transform figure)
    {
        figure.DOScale(Vector3.one, _scaleTime);
        figure.SetParent(null);
    }
}
