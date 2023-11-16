using System;
using DG.Tweening;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
public class AdsManager : MonoBehaviour
{
    [SerializeField] Rewarded _rewarded;
    [SerializeField] Interstitial _interstitial;
    [SerializeField] GameObject _noAdsButton;
    [SerializeField] GameObject _noAdsButtonInFailWindow;
    int _failCount = 0;

    [SerializeField] int _showAdAfterNFails;
    public UnityEvent<int> RewardedMultiplierAdWatched;
    public static AdsManager Instance;
    int _adsDisabled;
    public int AdsDisabled => _adsDisabled;


    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        _adsDisabled = PlayerPrefs.GetInt("AdsDisabled", 0);
        if(_adsDisabled == 1)
        {
            _noAdsButton.SetActive(false);
            _noAdsButtonInFailWindow.SetActive(false);
        }
        _failCount = PlayerPrefs.GetInt("Fails", 0);
       
       // GameLoopController.Instance.Fail.AddListener(OnFail);
        MobileAds.Initialize(initStatus => {});
        Instance = this;
    }

    private void OnFail()
    {
        Debug.Log($"Fails {_failCount}");
        if (_failCount != 0 && _failCount % _showAdAfterNFails == 0 && _adsDisabled == 0)
        {
            RequestInterstitial();
        }
        _failCount += 1;
        PlayerPrefs.SetInt("Fails", _failCount);
    }

    public void RequestInterstitial()
    {
        _interstitial.RequestInterstitial();
    }

    public void RequestRewardedRevive()
    {
        _rewarded.RequestRewarded(RewardedTypes.Revive, Convert.ToBoolean(_adsDisabled));
    }

    public void RequestRewardedMultiplier()
    {
        _rewarded.RequestRewarded(RewardedTypes.Multiplier, Convert.ToBoolean(_adsDisabled));
    }

    public void RemoveAds()
    {
        _adsDisabled = 1;
        PlayerPrefs.SetInt("AdsDisabled", _adsDisabled);
        _noAdsButton.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => _noAdsButton.SetActive(false));
    }
    private void OnDestroy()
    {
        //GameLoopController.Instance.Fail.RemoveListener(OnFail);
    }
}
