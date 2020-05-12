using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : SimpleMainMenuPage
{
    [SerializeField] Text currentCoinsText;
    [SerializeField] Button buttonToBuy;
    [SerializeField] Image selectedItemImg;
    [SerializeField] Text priceOfCurrentlySelected;
    ShopController shopController;
    // Start is called before the first frame update
    void Start()
    {
        shopController= GetComponent<ShopController>();
        shopController.BoughtItem += updateGUI;
        shopController.selectedItem += updateGUISelection;
        currentCoinsText.text = GameManager.instance.getCurrentMoney().ToString();
        buttonToBuy.interactable = false;
        priceOfCurrentlySelected.text = "";
    }




    public void updateGUI(ShopItemStruct item)
    {
        Debug.Log("Bought. Update GUI");
        currentCoinsText.text = GameManager.instance.getCurrentMoney().ToString();
        
    }

    public void updateGUISelection(ShopItemStruct item)
    {
        Debug.Log("Selected. Update GUI");
        priceOfCurrentlySelected.text = item.price.ToString();
        //currentCoinsText.text = GameManager.instance.getCurrentMoney().ToString();
        selectedItemImg.sprite = item.image;
        if(item.price<= int.Parse(currentCoinsText.text))
            buttonToBuy.interactable = true;
        else
            buttonToBuy.interactable = false;
    }
}
