using UnityEngine;
using DG.Tweening;

public class MoneyAttractorView : MonoBehaviour
{
    [SerializeField] private GameObject _moneyObject;
    [SerializeField] float _transitionDuration = 0.5f;
    public void RequestViewUpdate(Vector3 position)
    {
        Vector3 canvasPosition = Camera.main.WorldToScreenPoint(position);
        GameObject moneyObject = CreateMoneyImageElement(canvasPosition);
        AttractMoneyImageElement(moneyObject);
    }
    private GameObject CreateMoneyImageElement(Vector3 canvasPosition)
    {
        GameObject moneyImage = Instantiate(_moneyObject, transform);
        moneyImage.transform.position = canvasPosition;
        return moneyImage;
    }
    public void AttractMoneyImageElement(GameObject moneyObject)
    {
        moneyObject.transform.DOMove(transform.position - new Vector3(0, 2.6f, 0), _transitionDuration)
        .OnComplete(() => moneyObject.transform.DOScale(1.3f, _transitionDuration / 4).SetEase(Ease.Linear)
        .OnComplete(() => moneyObject.transform.DOScale(0f, _transitionDuration / 4).SetEase(Ease.Linear)
        .OnComplete(() => Destroy(moneyObject))));
        moneyObject.transform.DORotate(new Vector3(0,0, 360), _transitionDuration, RotateMode.FastBeyond360);
    }
}
