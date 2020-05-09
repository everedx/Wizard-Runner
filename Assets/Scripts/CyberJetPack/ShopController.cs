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

    private void Start()
    {
        currentMoney = GameManager.instance.getCurrentMoney();
    }


    public void buyItem(ShopItemStruct item)
    {
        if (currentMoney < item.price)
        {
            BuyAttemptFailed?.Invoke(item);
        }
        else
        {
            currentMoney -= item.price; 
            GameManager.instance.spendMoney(item.price);
            BoughtItem?.Invoke(item);
        }
    }

}
