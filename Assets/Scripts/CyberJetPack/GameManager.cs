using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;
public class GameManager : GameManagerBase<GameManager, GameDataStore>
{
    protected override void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        base.Awake();
        m_DataStore.restartUsesStatusForAllItems();
    }

    public bool registerMark(int newMark)
    {
        if (newMark > m_DataStore.bestMark)
        {
            m_DataStore.bestMark = newMark;
            initializeCollections();
            SaveData();
            return true;
        }
        else
        {
            return false;
        }
    }

    public int getBestMark()
    {
        return m_DataStore.getBestMark();
    }

    public string getLanguage()
    {
        return m_DataStore.getLanguage();
    }
    public void setLanguage(string lang)
    {
        m_DataStore.setLanguage(lang);
        initializeCollections();
        SaveData();
    }

    public void buyItem(string item)
    {
       m_DataStore.buyItem(item);
        initializeCollections();
        SaveData();
    }

    public void sellItem(string item)
    {
        m_DataStore.useItem(item);
        initializeCollections();
        SaveData();
    }

    public void addMoney(int money)
    {
        m_DataStore.addMoney(money);
        initializeCollections();
        SaveData();
    }
    public void spendMoney(int money)
    {
        m_DataStore.spendMoney(money);
        initializeCollections();
        SaveData();
    }

    public int getCurrentMoney()
    {
        return m_DataStore.getCurrentMoney();
    }

    public void useMarkedItems()
    {
        m_DataStore.useMarkedItems();
        initializeCollections();
        SaveData();
    }

    public void markItemToUse(string key,bool use = true)
    {
        m_DataStore.markItemForUse(key, use);
    }

    public List<ShopItemQuantityClass> getListOfMarkedItemsToUse()
    {

        return m_DataStore.getListOfMarkedItemsToUse();
    }

    public ShopItemQuantityClass getBodyToUse()
    {

        return m_DataStore.getBodyToUse();
    }


    public bool itemOwned(string key)
    {
        return m_DataStore.itemOwned(key);
    }

    public bool isItemBeingUsed(string key)
    {
        return m_DataStore.isItemBeingUsed(key);
    }

    public void setDateLastCommercial()
    {
        m_DataStore.setDateLastCommercial();
    }

    public int getDateLastCommercial()
    {
        return m_DataStore.getDateLastCommercial();
    }

    private void initializeCollections()
    {
        if (m_DataStore.itemsList.Count == 0)
        {
            m_DataStore.itemsList.Add(new ShopItemQuantityClass("BasicShield", 0));
            m_DataStore.itemsList.Add(new ShopItemQuantityClass("AdvancedShield", 0));
            m_DataStore.itemsList.Add(new ShopItemQuantityClass("DoubleCoins", 0));
            m_DataStore.itemsList.Add(new ShopItemQuantityClass("NoDynamicObstacles", 0));
            m_DataStore.itemsList.Add(new ShopItemQuantityClass("NoWaveProjectiles", 0));
            m_DataStore.itemsList.Add(new ShopItemQuantityClass("LessProjectiles", 0));
            m_DataStore.itemsList.Add(new ShopItemQuantityClass("SmallerObstacles", 0));
        }
        if (m_DataStore.bodiesList.Count == 0)
        {
            m_DataStore.bodiesList.Add(new ShopItemQuantityClass("DefaultBody", 1,true));
            m_DataStore.bodiesList.Add(new ShopItemQuantityClass("RedBody", 0));
            m_DataStore.bodiesList.Add(new ShopItemQuantityClass("PurpleBody", 0));
        }
        if (m_DataStore.particlesList.Count == 0)
        {
            m_DataStore.particlesList.Add(new ShopItemQuantityClass("DefaultParticle", 1,true));
           
        }

    }
}
