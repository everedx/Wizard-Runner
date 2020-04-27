using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utils;
using UnityEngine.Audio;
using System;

namespace Core.Data
{
    public abstract class GameManagerBase<TGameManager, TDataStore> : PersistentSingleton<TGameManager>
        where TDataStore : GameDataStoreBase, new()
        where TGameManager : GameManagerBase<TGameManager, TDataStore>
    {
        /// <summary>
		/// File name of saved game
		/// </summary>
		const string k_SavedGameFile = "save";

        //Audio
        public AudioMixer gameMixer;
        public string masterVolumeParameter;
        public string sfxVolumeParameter;
        public string musicVolumeParameter;

        protected JsonSaver<TDataStore> m_DataSaver;
        protected TDataStore m_DataStore;

        public virtual void GetVolumes(out float master, out float sfx, out float music)
        {
            master = m_DataStore.masterVolume;
            sfx = m_DataStore.sfxVolume;
            music = m_DataStore.musicVolume;
        }

        public virtual void SetVolumes(float master, float sfx, float music, bool save)
        {
            // Early out if no mixer set
            if (gameMixer == null)
            {
                return;
            }

            // Transform 0-1 into logarithmic -80-0
            if (masterVolumeParameter != null)
            {
                gameMixer.SetFloat(masterVolumeParameter, LogarithmicDbTransform(Mathf.Clamp01(master)));
            }
            if (sfxVolumeParameter != null)
            {
                gameMixer.SetFloat(sfxVolumeParameter, LogarithmicDbTransform(Mathf.Clamp01(sfx)));
            }
            if (musicVolumeParameter != null)
            {
                gameMixer.SetFloat(musicVolumeParameter, LogarithmicDbTransform(Mathf.Clamp01(music)));
            }

            if (save)
            {
                // Apply to save data too
                m_DataStore.masterVolume = master;
                m_DataStore.sfxVolume = sfx;
                m_DataStore.musicVolume = music;
                SaveData();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            LoadData();
        }
        protected virtual void Start()
        {
            SetVolumes(m_DataStore.masterVolume, m_DataStore.sfxVolume, m_DataStore.musicVolume, false);
        }


        protected void LoadData()
        {
            // If it is in Unity Editor use the standard JSON (human readable for debugging) otherwise encrypt it for deployed version
#if UNITY_EDITOR
            m_DataSaver = new JsonSaver<TDataStore>(k_SavedGameFile);
#else
			m_DataSaver = new EncryptedJsonSaver<TDataStore>(k_SavedGameFile);
#endif

            try
            {
                if (!m_DataSaver.Load(out m_DataStore))
                {
                    m_DataStore = new TDataStore();
                    SaveData();
                }
            }
            catch (Exception)
            {
                Debug.Log("Failed to load data, resetting");
                m_DataStore = new TDataStore();
                SaveData();
            }
        }

        protected virtual void SaveData()
        {
            m_DataSaver.Save(m_DataStore);
        }


        protected static float LogarithmicDbTransform(float volume)
        {
            volume = (Mathf.Log(89 * volume + 1) / Mathf.Log(90)) * 80;
            return volume - 80;
        }
    }
}

