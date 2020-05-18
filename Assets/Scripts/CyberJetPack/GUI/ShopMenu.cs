using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Utils;
using System.Threading;
using Core.Data;
public class ShopMenu : SimpleMainMenuPage
{
    [SerializeField] Text currentCoinsText;
    [SerializeField] Button buttonToBuy;
    [SerializeField] Image selectedItemImg;
    [SerializeField] Text priceOfCurrentlySelected;
    [SerializeField] Text priceSubstractor;
    ShopController shopController;
    float timerBuyAdvice;
    // Start is called before the first frame update
    void Start()
    {
        timerBuyAdvice = 0;
        shopController = GetComponent<ShopController>();
        shopController.BoughtItem += updateGUI;
        shopController.selectedItem += updateGUISelection;
        currentCoinsText.text = GameManager.instance.getCurrentMoney().ToString();
        buttonToBuy.interactable = false;
        priceOfCurrentlySelected.text = "";
        buttonToBuy.interactable = false;
        buttonToBuy.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("BUY");
    }


    private void Update()
    {
        timerBuyAdvice += Time.deltaTime;
        if (timerBuyAdvice > 2)
        {
            if (priceSubstractor.color.a > 0f)
            {
                priceSubstractor.color = new Color(1, 1, 1, priceSubstractor.color.a - 0.01f);
            }
            else
            {
                priceSubstractor.color = new Color(1, 1, 1, 0);
            }
        }
    
    }

    public void updateGUI(ShopItemStruct item)
    {
        Debug.Log("Bought. Update GUI");
        currentCoinsText.text = GameManager.instance.getCurrentMoney().ToString();
        priceSubstractor.text = "-"+item.price.ToString();
        priceSubstractor.color = new Color(1,1,1,1);
        timerBuyAdvice = 0;

        buttonToBuy.interactable = false;
        buttonToBuy.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("OWNED");
    }

    public void updateGUISelection(ShopItemStruct item)
    {
        Debug.Log("Selected. Update GUI");
        priceOfCurrentlySelected.text = item.price.ToString();
        //currentCoinsText.text = GameManager.instance.getCurrentMoney().ToString();
        selectedItemImg.sprite = item.image;
        if (item.price <= int.Parse(currentCoinsText.text) && !GameManager.instance.itemOwned(item.name))
        {
            buttonToBuy.interactable = true;
            buttonToBuy.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("BUY");
        }
        else
        {
            buttonToBuy.interactable = false;
            if (GameManager.instance.itemOwned(item.name))
            {
                buttonToBuy.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("OWNED");
            }
            else
            {
                buttonToBuy.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("BUY");
            }
        }
            
    }

    public void updateButtonShop()
    {
        buttonToBuy.interactable = false;
        buttonToBuy.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("BUY");
    }
}
