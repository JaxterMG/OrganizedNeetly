using Core.UI;
using UnityEngine;
using DG.Tweening;

namespace Core.Shop.MoneyManager
{
    public class MoneyAttractorView : MonoBehaviour
    {
        [SerializeField] private GameObject _moneyObject;
        [SerializeField] float _transitionDuration = 0.5f;
        [SerializeField] private ObjectPooler _objectPooler;

        public void RequestViewUpdate(Vector3 position)
        {
            Vector3 canvasPosition = UnityEngine.Camera.main.WorldToScreenPoint(position);
            GameObject moneyObject = CreateMoneyImageElement(canvasPosition);
            AttractMoneyImageElement(moneyObject);
        }

        private GameObject CreateMoneyImageElement(Vector3 canvasPosition)
        {
            GameObject moneyImage = _objectPooler.RequestMoneyObject();
            moneyImage.SetActive(true);
            moneyImage.transform.SetParent(transform);
            moneyImage.transform.position = canvasPosition;

            return moneyImage;
        }

        public void AttractMoneyImageElement(GameObject moneyObject)
        {
            moneyObject.transform.DOMove(transform.position - new Vector3(0, 2.6f, 0), _transitionDuration)
                .OnComplete(() => moneyObject.transform.DOScale(1.3f, _transitionDuration / 4).SetEase(Ease.Linear)
                    .OnComplete(() => moneyObject.transform.DOScale(0f, _transitionDuration / 4).SetEase(Ease.Linear)
                        .OnComplete(() =>
                        {
                            _objectPooler.RemoveMoneyObject(moneyObject);
                            moneyObject.transform.localScale = Vector3.one;
                        })));
            moneyObject.transform.DORotate(new Vector3(0, 0, 360), _transitionDuration, RotateMode.FastBeyond360);
        }
    }
}
