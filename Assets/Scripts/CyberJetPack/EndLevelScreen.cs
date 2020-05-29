using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelScreen : MonoBehaviour
{

    [SerializeField] Text textCoins;
    [SerializeField] Text textTotalCoins;
    [SerializeField] Text textDistance;
    PlayerController playerController;
    PlayerCollector playerCollector;


    public void gotoMenu()
    {
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().changeLevel("MainMenu");
    }

    public void repeatLevel()
    {
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().changeLevel(SceneManager.GetActiveScene().name);
    }

    public void seeAdvertisement()
    {
        GetComponent<RewardedAdsScript>().showAd();
    }


    public void setAllTexts()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerCollector = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollector>();
        textCoins.text = playerCollector.Score.ToString();
        textTotalCoins.text = GameManager.instance.getCurrentMoney().ToString();
        textDistance.text = playerController.Distance.ToString() + " M";
    }

    public void startAnimationToShow()
    {
        GetComponent<Animator>().SetTrigger("start");
        setAllTexts();
    }
}
