using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardedTypes
{
    Revive,
    Multiplier
}

public class Rewarded : MonoBehaviour
{
    //ca-app-pub-2195788793554518/4288577297 - actual
    //ca-app-pub-3940256099942544/5224354917 Test
    private RewardedAd rewardedAd;
    private string adUnitId;
    RewardedTypes _rewardedType;
    AdRequest request;
    public void Start()
    {
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-2195788793554518/4288577297";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544~3347511713";
#else
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#endif

        RequestNewAd();
    }

    public void RequestNewAd()
    {
        // this.rewardedAd = new RewardedAd(adUnitId);
        // request = new AdRequest.Builder().Build();
        // this.rewardedAd.LoadAd(request);
        //
    }

    public void RequestRewarded(RewardedTypes rewardedType, bool isAdsDisabled)
    {
        // this.rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        // this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedType = rewardedType;
        if (isAdsDisabled)
        {
            //GameLoopController.Instance.Revive?.Invoke();
            return;
        }
        
        HandleRewardedAdLoaded();

    }

    public void HandleRewardedAdLoaded()
    {
        // if (rewardedAd.IsLoaded())
        //     this.rewardedAd.Show();

    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("Shown");
        if (_rewardedType == RewardedTypes.Revive)
        {
            //GameLoopController.Instance.Revive?.Invoke();
        }
        if (_rewardedType == RewardedTypes.Multiplier)
        {
            AdsManager.Instance.RewardedMultiplierAdWatched?.Invoke(3);
        }
        RequestNewAd();
    }

}
