using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : SimpleMainMenuPage
{
    [SerializeField] Text masterValue;
    [SerializeField] Text musicValue;
    [SerializeField] Text sfxValue;
    [SerializeField] int valueModificator;
    // Start is called before the first frame update
    void Start()
    {
        setVolumeValuesInGUI();
    }

    public void setVolumeValuesInGUI()
    {
        float master, music, sfx;
        if (GameManager.instanceExists)
        {
            GameManager.instance.GetVolumes(out master, out sfx, out music);
            masterValue.text = ((float)master * 100f).ToString();
            musicValue.text = ((float)music * 100f).ToString();
            sfxValue.text = ((float)sfx * 100f).ToString();
        }
    }


    public void pressButtonVolumeAdd(string controlSection)
    {
      
        switch (controlSection)
        {
            case "Master":
                if (int.Parse(masterValue.text) > 100 - valueModificator)
                {
                    masterValue.text = "100";
                }
                else
                {
                    masterValue.text = (int.Parse(masterValue.text) + valueModificator).ToString();
                }
                break;
            case "Music":
                if (int.Parse(musicValue.text) > 100 - valueModificator)
                {
                    musicValue.text = "100";
                }
                else
                {
                    musicValue.text = (int.Parse(musicValue.text) + valueModificator).ToString();
                }
                break;
            case "SFX":
                if (int.Parse(sfxValue.text) > 100 - valueModificator)
                {
                    sfxValue.text = "100";
                }
                else
                {
                    sfxValue.text = (int.Parse(sfxValue.text)+valueModificator).ToString();
                }
                break;

        }
        GameManager.instance.SetVolumes(float.Parse(masterValue.text) / 100, float.Parse(sfxValue.text) / 100, float.Parse(musicValue.text) / 100, true);
    
    }

    public void pressButtonVolumeSubstract(string controlSection)
    {
        switch (controlSection)
        {
            case "Master":
                if (int.Parse(masterValue.text) < 0 + valueModificator)
                {
                    masterValue.text = "0";
                }
                else
                {
                    masterValue.text = (int.Parse(masterValue.text)-valueModificator).ToString();
                }
                break;
            case "Music":
                if (int.Parse(musicValue.text) < 0 + valueModificator)
                {
                    musicValue.text = "0";
                }
                else
                {
                    musicValue.text = (int.Parse(musicValue.text) - valueModificator).ToString();
                }
                break;
            case "SFX":
                if (int.Parse(sfxValue.text) < 0 + valueModificator)
                {
                    sfxValue.text = "0";
                }
                else
                {
                    sfxValue.text = (int.Parse(sfxValue.text) - valueModificator).ToString();
                }
                break;

        }
        GameManager.instance.SetVolumes(float.Parse(masterValue.text)/100, float.Parse(sfxValue.text) / 100, float.Parse(musicValue.text) / 100, true);

    }
}
