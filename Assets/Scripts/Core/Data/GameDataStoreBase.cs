using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;

public abstract class GameDataStoreBase : IDataStore
{
    public float sfxVolume = 1;
    public float masterVolume = 1;
    public float musicVolume = 1;


    public abstract void preSave();
    public abstract void postLoad();
}
