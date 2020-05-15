using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;
using System;


public class GameDataStore : GameDataStoreBase
{
    public int bestMark = 0;
    public int money = 0;
    public string language;

    public List<ShopItemQuantityClass> itemsList = new List<ShopItemQuantityClass>();
   


    public GameDataStore() : base()
    {
        language = "English";

        itemsList.Add(new ShopItemQuantityClass("BasicShield", 0));
        itemsList.Add(new ShopItemQuantityClass("AdvancedShield", 0));
        itemsList.Add(new ShopItemQuantityClass("DoubleCoins", 0));
        itemsList.Add(new ShopItemQuantityClass("NoDynamicObstacles", 0));
        itemsList.Add(new ShopItemQuantityClass("NoWaveProjectiles", 0));
        itemsList.Add(new ShopItemQuantityClass("LessProjectiles", 0));
        itemsList.Add(new ShopItemQuantityClass("SmallerObstacles", 0));
    }


    public int getBestMark()
    {
        return bestMark;
    }

    public void setLanguage(string language)
    {
        this.language = language;
    }
    public string getLanguage()
    {
        return language;
    }

    public void addMoney(int money)
    {
        this.money += money;
    }
    public void spendMoney(int money)
    {
        this.money -= money;
    }

    public int getCurrentMoney()
    {
        return money;
    }


    public override void preSave()
    {
        Debug.Log("[GAME] Saving Game");
    }
    public override void postLoad()
    {
        Debug.Log("[GAME] Loaded Game");
    }

    public void buyItem(string keyOfItem)
    {
        int index = itemsList.FindLastIndex(c => c.name == keyOfItem);
        if (index != -1)
        {
            itemsList[index] = new ShopItemQuantityClass(keyOfItem,itemsList[index].quantity+1, itemsList[index].useNow);
        }
    }

    public void useItem(string keyOfItem)
    {
        int index = itemsList.FindLastIndex(c => c.name == keyOfItem);
        if (index != -1)
        {
            itemsList[index] = new ShopItemQuantityClass(keyOfItem, itemsList[index].quantity-1, false);
        }
    }

    public void restartUsesStatusForAllItems()
    {
        ShopItemQuantityClass item;
        for (int index = 0; index < itemsList.Count;index++)
        {
            item = itemsList[index];
            itemsList[index] = new ShopItemQuantityClass(item.name, item.quantity,false);
        }
    }

    public void useMarkedItems()
    {
        ShopItemQuantityClass item;
        for (int index = 0; index < itemsList.Count; index++)
        {
            item = itemsList[index];
            if(item.useNow)
                itemsList[index] = new ShopItemQuantityClass(item.name, item.quantity-1, false);
        }
    }

    public void markItemForUse(string key, bool use)
    {
        ShopItemQuantityClass item;
        int index = itemsList.FindLastIndex(c => c.name == key);
        if (index != -1)
        {
            item = itemsList[index];
            itemsList[index] = new ShopItemQuantityClass(item.name, item.quantity, use);
        }

        
    }

    public bool itemOwned(string key) 
    {
        ShopItemQuantityClass item;
        int index = itemsList.FindLastIndex(c => c.name == key);
        if (index != -1)
        {
            item = itemsList[index];
            if (item.quantity > 0)
                return true;
        }
        return false;
    }

    public bool isItemBeingUsed(String key)
    {
        ShopItemQuantityClass item;
        int index = itemsList.FindLastIndex(c => c.name == key);
        if (index != -1)
        {
            item = itemsList[index];
            if (item.useNow == true)
                return true;
        }
        return false;
    }
    

    public List<ShopItemQuantityClass> getListOfMarkedItemsToUse()
    {
        List<ShopItemQuantityClass> listItemsToUse = new List<ShopItemQuantityClass>(); 
        foreach (ShopItemQuantityClass item in itemsList)
        {
            if (item.useNow)
            {
                listItemsToUse.Add(item);
            }
        }

        return listItemsToUse;
    }



}

[Serializable]
public class ShopItemQuantityClass
{
    public string name;
    public int quantity;
    public bool useNow;
    public ShopItemQuantityClass(string name, int quantity)
    {
        this.name = name;
        this.quantity = quantity;
        useNow = false;
    }
    public ShopItemQuantityClass(string name, int quantity, bool useNow)
    {
        this.name = name;
        this.quantity = quantity;
        this.useNow = useNow;
    }
}