using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
     
    private bool isFadingIn;
    private bool isFadingOut;
    private string sceneToLoad;
    private Material matPanelLoader;
    private float masterVolume,sfx,music;
    [SerializeField] GameObject panelLoader;
    [SerializeField] private Shader dissolveShader;
    [SerializeField] AudioMixer gameMixer;
    [SerializeField] Animator[] triggerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        panelLoader.SetActive(true);
        matPanelLoader = new Material(dissolveShader);
        GetComponentInChildren<Image>().material = matPanelLoader;
        matPanelLoader.SetFloat("_Fade",1);
        matPanelLoader.SetFloat("_Scale", 200);
        //matPanelLoader.SetColor();
        isFadingIn = true;
        isFadingOut = false;
        GameManager.instance.GetVolumes(out masterVolume, out sfx, out music);
        GameManager.instance.SetVolumes(masterVolume,sfx,music,false);
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isFadingOut)
        {

           
        
            if (masterVolume > 0)
            {
                masterVolume -= Time.deltaTime / 2;
                gameMixer.SetFloat("Master", LogarithmicDbTransform(masterVolume ));
            }
            if (matPanelLoader.GetFloat("_Fade") < 1)
            {
                
                matPanelLoader.SetFloat("_Fade", matPanelLoader.GetFloat("_Fade") + Time.deltaTime / 2);
            }
            else
            {
                isFadingOut = false;
                SceneManager.LoadScene(sceneToLoad);
            }
        }
        else if(isFadingIn)
        {
            if (matPanelLoader.GetFloat("_Fade") > 0)
            {

                matPanelLoader.SetFloat("_Fade", matPanelLoader.GetFloat("_Fade") - Time.deltaTime / 2);
            }
            else
            {
                foreach (Animator animator in triggerAnimator)
                {
                        animator.SetTrigger("start");
                }
                 
                panelLoader.SetActive(false);
                isFadingIn = false;
            }
        }
    }

    public void changeLevel(string sceneToLoadArg)
    {
        GameManager.instance.GetVolumes(out masterVolume, out sfx, out music);
        sceneToLoad = sceneToLoadArg;
        isFadingOut = true;
        panelLoader.SetActive(true);
    }


    private float LogarithmicDbTransform(float volume)
    {
        volume = (Mathf.Log(89 * volume + 1) / Mathf.Log(90)) * 80;
        return volume - 80;
    }

}

