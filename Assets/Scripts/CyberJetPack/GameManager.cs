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
        SaveData();
    }

    public void buyItem(string item)
    {
       m_DataStore.buyItem(item);
        SaveData();
    }

    public void sellItem(string item)
    {
        m_DataStore.useItem(item);
        SaveData();
    }

    public void addMoney(int money)
    {
        m_DataStore.addMoney(money);
        SaveData();
    }
    public void spendMoney(int money)
    {
        m_DataStore.spendMoney(money);
        SaveData();
    }

    public int getCurrentMoney()
    {
        return m_DataStore.getCurrentMoney();
    }

    public void useMarkedItems()
    {
        m_DataStore.useMarkedItems();
        SaveData();
    }

    public void markItemToUse(string key)
    {
        m_DataStore.markItemForUse(key);
    }

    public List<ShopItemQuantityClass> getListOfMarkedItemsToUse()
    {

        return m_DataStore.getListOfMarkedItemsToUse();
    }

}
