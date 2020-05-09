using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : SimpleMainMenuPage
{
    [SerializeField] Text currentCoinsText;
    ShopController shopController;
    // Start is called before the first frame update
    void Start()
    {
        shopController= GetComponent<ShopController>();
        shopController.BoughtItem += updateGUI;
        currentCoinsText.text = GameManager.instance.getCurrentMoney().ToString();
    }




    public void updateGUI(ShopItemStruct item)
    {
        Debug.Log("Bought. Update GUI");
        currentCoinsText.text = GameManager.instance.getCurrentMoney().ToString();
    }
}
