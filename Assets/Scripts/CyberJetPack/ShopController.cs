using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void BoughtItem(ShopItemStruct item);
public delegate void BuyAttemptFailed(ShopItemStruct item);

public class ShopController : MonoBehaviour
{
    int currentMoney;



    public event BoughtItem BoughtItem;
    public event BuyAttemptFailed BuyAttemptFailed;
    public event SelectedItem selectedItem;

    [SerializeField] ShopSelector[] shopSelectors;
    private ShopItemStruct item;


    private void Start()
    {
        currentMoney = GameManager.instance.getCurrentMoney();
        foreach (ShopSelector shopSelector in shopSelectors)
        {
            shopSelector.selectedItemEvent += SelectedItem;
        }
    }


    public void SelectedItem(ShopItemStruct item)
    {
        this.item = item;
        //Show price and enable/disable button to buy
        //event
        selectedItem?.Invoke(item);
    }

    public void buySelectedItem()
    {
        if (!item.Equals(default(ShopItemStruct)))
        {
            if (currentMoney < item.price)
            {
                BuyAttemptFailed?.Invoke(item);
            }
            else
            {
                currentMoney -= item.price;
                GameManager.instance.spendMoney(item.price);
                GameManager.instance.buyItem(item.name);
                BoughtItem?.Invoke(item);
            }
        }

    }

    public void buyItem(ShopItemStruct item)
    {
        if (!item.Equals(default(ShopItemStruct)))
        {
            if (currentMoney < item.price)
            {
                BuyAttemptFailed?.Invoke(item);
            }
            else
            {
                currentMoney -= item.price;
                GameManager.instance.spendMoney(item.price);
                GameManager.instance.buyItem(item.name);
                BoughtItem?.Invoke(item);
            }
        }
  
    }

}
