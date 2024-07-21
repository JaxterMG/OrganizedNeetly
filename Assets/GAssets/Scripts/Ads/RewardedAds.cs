using UnityEngine;
using System;

namespace Core.Ads
{
	public class RewardedAds : MonoBehaviour
	{
		public static String REWARDED_INSTANCE_ID = "0";

		// Use this for initialization
		void Start()
		{
			Debug.Log("unity-script: ShowRewardedVideoScript Start called");

			//Add Rewarded Video Events
			IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
			IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
			IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
			IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
			IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
			IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
			IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
			IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;

			//Add Rewarded Video DemandOnly Events
			IronSourceEvents.onRewardedVideoAdOpenedDemandOnlyEvent += RewardedVideoAdOpenedDemandOnlyEvent;
			IronSourceEvents.onRewardedVideoAdClosedDemandOnlyEvent += RewardedVideoAdClosedDemandOnlyEvent;
			IronSourceEvents.onRewardedVideoAdLoadedDemandOnlyEvent += this.RewardedVideoAdLoadedDemandOnlyEvent;
			IronSourceEvents.onRewardedVideoAdRewardedDemandOnlyEvent += RewardedVideoAdRewardedDemandOnlyEvent;
			IronSourceEvents.onRewardedVideoAdShowFailedDemandOnlyEvent += RewardedVideoAdShowFailedDemandOnlyEvent;
			IronSourceEvents.onRewardedVideoAdClickedDemandOnlyEvent += RewardedVideoAdClickedDemandOnlyEvent;
			IronSourceEvents.onRewardedVideoAdLoadFailedDemandOnlyEvent +=
				this.RewardedVideoAdLoadFailedDemandOnlyEvent;

		}

		/************* RewardedVideo API *************/
		public void ShowRewardedVideoButtonClicked()
		{
			Debug.Log("unity-script: ShowRewardedVideoButtonClicked");
			if (IronSource.Agent.isRewardedVideoAvailable())
			{
				IronSource.Agent.showRewardedVideo();
			}
			else
			{
				Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
			}

			// DemandOnly
			// ShowDemandOnlyRewardedVideo ();
		}

		void LoadDemandOnlyRewardedVideo()
		{
			Debug.Log("unity-script: LoadDemandOnlyRewardedVideoButtonClicked");
			IronSource.Agent.loadISDemandOnlyRewardedVideo(REWARDED_INSTANCE_ID);
		}

		void ShowDemandOnlyRewardedVideo()
		{
			Debug.Log("unity-script: ShowDemandOnlyRewardedVideoButtonClicked");
			if (IronSource.Agent.isISDemandOnlyRewardedVideoAvailable(REWARDED_INSTANCE_ID))
			{
				IronSource.Agent.showISDemandOnlyRewardedVideo(REWARDED_INSTANCE_ID);
			}
			else
			{
				Debug.Log("unity-script: IronSource.Agent.isISDemandOnlyRewardedVideoAvailable - False");
			}
		}

		/************* RewardedVideo Delegates *************/
		void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
		{
			Debug.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + canShowAd);
			if (canShowAd)
			{
			}
			else
			{
			}
		}

		void RewardedVideoAdOpenedEvent()
		{
			Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent");
		}

		void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
		{
			Debug.Log("unity-script: I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() +
			          " name = " + ssp.getRewardName());

		}

		void RewardedVideoAdClosedEvent()
		{
			Debug.Log("unity-script: I got RewardedVideoAdClosedEvent");
		}

		void RewardedVideoAdStartedEvent()
		{
			Debug.Log("unity-script: I got RewardedVideoAdStartedEvent");
		}

		void RewardedVideoAdEndedEvent()
		{
			Debug.Log("unity-script: I got RewardedVideoAdEndedEvent");
		}

		void RewardedVideoAdShowFailedEvent(IronSourceError error)
		{
			Debug.Log("unity-script: I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() +
			          ", description : " + error.getDescription());
		}

		void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
		{
			Debug.Log("unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
		}

		/************* RewardedVideo DemandOnly Delegates *************/

		void RewardedVideoAdLoadedDemandOnlyEvent(string instanceId)
		{
			Debug.Log("unity-script: I got RewardedVideoAdLoadedDemandOnlyEvent for instance: " + instanceId);
		}

		void RewardedVideoAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
		{
			Debug.Log("unity-script: I got RewardedVideoAdLoadFailedDemandOnlyEvent for instance: " + instanceId +
			          ", code :  " + error.getCode() + ", description : " + error.getDescription());
		}

		void RewardedVideoAdOpenedDemandOnlyEvent(string instanceId)
		{
			Debug.Log("unity-script: I got RewardedVideoAdOpenedDemandOnlyEvent for instance: " + instanceId);
		}

		void RewardedVideoAdRewardedDemandOnlyEvent(string instanceId)
		{
			Debug.Log("unity-script: I got RewardedVideoAdRewardedDemandOnlyEvent for instance: " + instanceId);
		}

		void RewardedVideoAdClosedDemandOnlyEvent(string instanceId)
		{
			Debug.Log("unity-script: I got RewardedVideoAdClosedDemandOnlyEvent for instance: " + instanceId);
		}

		void RewardedVideoAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
		{
			Debug.Log("unity-script: I got RewardedVideoAdShowFailedDemandOnlyEvent for instance: " + instanceId +
			          ", code :  " + error.getCode() + ", description : " + error.getDescription());
		}

		void RewardedVideoAdClickedDemandOnlyEvent(string instanceId)
		{
			Debug.Log("unity-script: I got RewardedVideoAdClickedDemandOnlyEvent for instance: " + instanceId);
		}
	}
}
