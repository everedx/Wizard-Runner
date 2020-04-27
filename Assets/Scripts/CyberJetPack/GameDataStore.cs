﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;

public class GameDataStore : GameDataStoreBase
{
    public int bestMark = 0;

    public Dictionary<string, int> itemsDictionary = new Dictionary<string, int>();

    public GameDataStore() : base()
    {
        itemsDictionary.Add("doubleCoin",0);
        itemsDictionary.Add("smallShield",0);
        itemsDictionary.Add("bigShield",0);
        itemsDictionary.Add("halfSizedObstacles",0);
        itemsDictionary.Add("onlyStaticObstacles",0);
        itemsDictionary.Add("onlyStaticMissiles", 0);
        itemsDictionary.Add("halfQuantityMissiles", 0);
    }


    public int getBestMark()
    {
        return bestMark;
    }

    public override void preSave()
    {
        Debug.Log("[GAME] Saving Game");
    }
    public override void postLoad()
    {
        Debug.Log("[GAME] Loaded Game");
    }


}