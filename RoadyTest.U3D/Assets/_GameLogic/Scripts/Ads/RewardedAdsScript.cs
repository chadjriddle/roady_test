using UnityEngine;
using UnityEngine.Advertisements;
using WildbotLabs.Scriptables.GameEvents;
using WildbotLabs.Scriptables.References;

public class RewardedAdsScript : MonoBehaviour, IUnityAdsListener { 

    string gameId = "3481798";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;

    public int RewardAmount = 100;
    public IntGameEvent AddCashEvent;
    public BoolReference IsAddReady;

    // Initialize the Ads listener and service:
    void Start () {
        Advertisement.AddListener(this);
        Advertisement.Initialize (gameId, testMode);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            // Reward the user for watching the ad to completion.
            Debug.Log("Finished Rewarded Add");
            AddCashEvent.Raise(RewardAmount);
        } else if (showResult == ShowResult.Skipped) {
            // Do not reward the user for skipping the ad.
            Debug.Log("Skipped Rewarded Add");
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId) {
            IsAddReady.SetValue(true);
        }
    }

    public void ShowAd()
    {
        if (IsAddReady.Value)
        {
            Advertisement.Show(myPlacementId);
        }
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 
}