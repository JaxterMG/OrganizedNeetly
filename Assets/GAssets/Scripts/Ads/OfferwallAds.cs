using UnityEngine;
using System.Collections.Generic;

public class OfferwallAds : MonoBehaviour
{
	void Start ()
	{
		Debug.Log ("ShowOfferwallScript Start called");

		// Add Offerwall Events
		IronSourceEvents.onOfferwallClosedEvent += OfferwallClosedEvent;
		IronSourceEvents.onOfferwallOpenedEvent += OfferwallOpenedEvent;
		IronSourceEvents.onOfferwallShowFailedEvent += OfferwallShowFailedEvent;
		IronSourceEvents.onOfferwallAdCreditedEvent += OfferwallAdCreditedEvent;
		IronSourceEvents.onGetOfferwallCreditsFailedEvent += GetOfferwallCreditsFailedEvent;
		IronSourceEvents.onOfferwallAvailableEvent += OfferwallAvailableEvent;
	}

	public void ShowOfferwallButtonClicked ()
	{
		if (IronSource.Agent.isOfferwallAvailable ()) {
			IronSource.Agent.showOfferwall ();
		} else {
			Debug.Log ("IronSource.Agent.isOfferwallAvailable - False");
		}
	}
		
	void OfferwallOpenedEvent ()
	{
		Debug.Log ("I got OfferwallOpenedEvent");
	}

	void OfferwallClosedEvent ()
	{
		Debug.Log ("I got OfferwallClosedEvent");
	}

	void OfferwallShowFailedEvent (IronSourceError error)
	{
		Debug.Log ("I got OfferwallShowFailedEvent, code :  " + error.getCode () + ", description : " + error.getDescription ());
	}

	void OfferwallAdCreditedEvent (Dictionary<string, object> dict)
	{
		Debug.Log ("I got OfferwallAdCreditedEvent, current credits = " + dict ["credits"] + " totalCredits = " + dict ["totalCredits"]);
	}

	void GetOfferwallCreditsFailedEvent (IronSourceError error)
	{
		Debug.Log ("I got GetOfferwallCreditsFailedEvent, code :  " + error.getCode () + ", description : " + error.getDescription ());
	}

	void OfferwallAvailableEvent (bool canShowOfferwal)
	{
		Debug.Log ("I got OfferwallAvailableEvent, value = " + canShowOfferwal);
	}
}
