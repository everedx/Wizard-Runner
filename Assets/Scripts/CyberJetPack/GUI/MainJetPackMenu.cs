using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainJetPackMenu : MenuController
{

    public OptionsMenu optionsMenu;
    public ShopMenu shopMenu;
    public SimpleMainMenuPage titleMenu;

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
}
