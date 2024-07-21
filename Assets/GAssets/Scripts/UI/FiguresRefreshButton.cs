using System.Collections;
using Core.Grid.Figures;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class FiguresRefreshButton : MonoBehaviour
    {
        [SerializeField] Button _refreshButton;
        [SerializeField] Image _refreshImage;
        private FiguresSpawner _figuresSpawner;

        private float _defaultRefreshTime = 30;
        private float _refreshTime = 0;

        void OnEnable()
        {
            _refreshImage.fillAmount = 1;
            StartRefresh();
            //FindObjectOfType<SaveLoadHandler>().RegisterSavable(this);
            _figuresSpawner = FindAnyObjectByType<FiguresSpawner>();
            _refreshButton.onClick.AddListener(RefreshFigures);
            _refreshButton.onClick.AddListener(StartRefresh);
        }

        void OnDisable()
        {
            _refreshButton.onClick.RemoveListener(RefreshFigures);
            _refreshButton.onClick.RemoveListener(StartRefresh);
        }

        private void RefreshFigures()
        {
            if (_refreshTime > 0) return;

            _figuresSpawner.SpawnReviveFigures();
        }

        private void StartRefresh()
        {
            if (_refreshTime > 0) return;
            _refreshTime = _defaultRefreshTime;
            _refreshImage.fillAmount = 1;
            StartCoroutine(Refresh());
        }

        private IEnumerator Refresh()
        {
            while (_refreshTime > 0)
            {
                _refreshTime -= Time.deltaTime;
                _refreshImage.fillAmount -= Time.deltaTime / _defaultRefreshTime;
                yield return null;
            }

        }

        // private struct BonusesData
        // {
        //     public float RefreshTime;

        //     public BonusesData(float refreshTime)
        //     {
        //         RefreshTime = refreshTime;
        //     }
        // }
        // public string Save()
        // {
        //     BonusesData bonusesData = new BonusesData(_refreshTime);
        //     return JsonConvert.SerializeObject(bonusesData);
        // }
        // public void Load(string jsonData)
        // {
        //     StopCoroutine(Refresh());
        //     var data = JsonConvert.DeserializeObject<BonusesData>(jsonData);
        //     _refreshTime = data.RefreshTime;
        //     if(_refreshTime > 0)
        //         StartRefresh();
        //     else
        //     {
        //         _refreshImage.fillAmount = 1;
        //     }
        // }
    }
}