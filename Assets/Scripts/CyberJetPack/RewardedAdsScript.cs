using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdsScript : MonoBehaviour
{

    [SerializeField] Button buttonAd;
    [SerializeField] Text textMoney;
    string gameId = "3539462";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;
    private int reward;
    // Initialize the Ads listener and service:
    void Start()
    {
        reward = 500;
        if (!Advertisement.isInitialized)
            Advertisement.Initialize(gameId, testMode);
    }

    public void showAd()
    {
        ShowOptions showOptions = new ShowOptions();

        showOptions.resultCallback = result =>
        {
            // Define conditional logic for each ad completion status:
            if (result == ShowResult.Finished)
            {
                // Reward the user for watching the ad to completion.
                // let the user continue!
                GameManager.instance.addMoney(reward);
                textMoney.text = GameManager.instance.getCurrentMoney().ToString();
                buttonAd.gameObject.SetActive(false);
            }
            else if (result == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
                //nothing to do.
                buttonAd.gameObject.SetActive(false);
            }
            else if (result == ShowResult.Failed)
            {
                buttonAd.gameObject.SetActive(false);
                Debug.LogWarning("The ad did not finish due to an error.");
            }
        };
        if (Advertisement.IsReady())
        {

            Advertisement.Show(myPlacementId, showOptions);
        }



    }
}