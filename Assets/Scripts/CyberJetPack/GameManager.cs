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

    public void buyItem(string item)
    {
       m_DataStore.itemsDictionary[item] = m_DataStore.itemsDictionary[item] + 1;
    }

    public void sellItem(string item)
    {
        m_DataStore.itemsDictionary[item] = m_DataStore.itemsDictionary[item] - 1;
    }
}
