using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class InterstitialAd : MonoBehaviour
{
    string gameId = "3539462";

    [SerializeField] bool testMode;
    [SerializeField] int secondsToWaitUntilNextAd;
    private string levelToLoad;
    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        levelToLoad = null;
    }


    //PC
    public void showAd(string ltl)
    {
        levelToLoad = ltl;
        if (!ltl.Equals("MainMenu"))
        {
            //first time game executed.
            if (GameManager.instance.getDateLastCommercial() == 0)
            {
                GameManager.instance.setDateLastCommercial();
                changeLevel();
            }

            TimeSpan t = DateTime.UtcNow - new DateTime(1970,1,1).AddSeconds(GameManager.instance.getDateLastCommercial());

            Debug.Log(t.TotalSeconds);
            if (t.TotalSeconds > secondsToWaitUntilNextAd)
            {

                ShowOptions showOptions = new ShowOptions();
                showOptions.resultCallback = result =>
                {
                    GameManager.instance.setDateLastCommercial();
                    changeLevel();
                };
                if (Advertisement.IsReady())
                {
                    Advertisement.Show(showOptions);
                }
            }
            else
            {
                changeLevel();
            }
        }
        else
        {
            changeLevel();
        }

    }

    private void changeLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
