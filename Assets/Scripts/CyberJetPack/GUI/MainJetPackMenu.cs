using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainJetPackMenu : MenuController
{

    public OptionsMenu optionsMenu;
    public ShopMenu shopMenu;
    public DressRoomMenu dressRoomMenu;
    public SimpleMainMenuPage titleMenu;
    [SerializeField] GameObject achievementsPanel;


    // Start is called before the first frame update
    public void startGame()
    {
        SceneManager.LoadScene("DevScene", LoadSceneMode.Single);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void ShowOptionsMenu()
    {
        ChangePage(optionsMenu);
    }

    public void ShowDressRoomMenu()
    {
        ChangePage(dressRoomMenu);
    }

    public void ShowShopMenu()
    {
        ChangePage(shopMenu);
    }

    public void ShowTitleScreen()
    {
        Back(titleMenu);
    }

    protected virtual void Awake()
    {
        ShowTitleScreen();
    }


    public void showAchievements()
    {
        achievementsPanel.SetActive(true);
    }

    public void hideAchievements()
    {
        achievementsPanel.SetActive(false);
    }
}
