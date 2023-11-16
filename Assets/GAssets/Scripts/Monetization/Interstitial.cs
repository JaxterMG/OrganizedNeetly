using GoogleMobileAds.Api;
using UnityEngine;

public class Interstitial : MonoBehaviour
{
    private InterstitialAd _interstitial;
    //ca-app-pub-3940256099942544~3347511713 Test
    AdRequest request;
    private string adUnitId;

    public void Start()
    {
#if UNITY_EDITOR
        adUnitId = "ca-app-pub-3940256099942544~3347511713";
#endif
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544~3347511713";
#endif
        RequestNewAd();

    }
    public void RequestNewAd()
    {
        // this._interstitial = new InterstitialAd();
        // request = new AdRequest.Builder.Build();
        //  this._interstitialLoadAd(request);
    }
    public void RequestInterstitial()
    {
       
        // if (_interstitial.IsLoaded())
        // {
        //     _interstitial.Show();
        //     RequestNewAd();
        // }


    }
}
