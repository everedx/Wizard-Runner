using Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    [Serializable]
     struct LanguageStruct
    {
        public string name;
        public Sprite image;
    }

    [SerializeField] GameObject buttonLanguagePf;
    [SerializeField] LanguageStruct[] pictures;
 

    private GameObject panelLanguages;

    private void Start()
    {
        float ratioWidth = (float)Screen.width / (float)Screen.currentResolution.width;
        float ratioHeight = (float)Screen.height / (float)Screen.currentResolution.height;

        panelLanguages = new GameObject("PanelLanguages",new Type[] {typeof(RectTransform), typeof(CanvasRenderer), typeof(Image)});
        panelLanguages.transform.SetParent(gameObject.transform);
        panelLanguages.GetComponent<Image>().color = gameObject.GetComponent<Button>().colors.normalColor;
        RectTransform rect = panelLanguages.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2( gameObject.GetComponent<RectTransform>().rect.width * ratioWidth, gameObject.GetComponent<RectTransform>().rect.height*5*ratioHeight);
        //rect.position = gameObject.transform.position + new Vector3(0, gameObject.GetComponent<RectTransform>().rect.height/2 + gameObject.GetComponent<RectTransform>().rect.height / 10 + rect.rect.height / 2, 0);
        rect.position = gameObject.transform.position + new Vector3(0, rect.sizeDelta.y/2 + ratioHeight*gameObject.GetComponent<RectTransform>().rect.height / 10 + ratioHeight* gameObject.GetComponent<RectTransform>().rect.height / 2, 0);
        Image imHolder;
        for ( int i =0; i<pictures.Length;i++)
        {
            GameObject button = Instantiate(buttonLanguagePf, panelLanguages.transform);
            RectTransform rectButton = button.GetComponent<RectTransform>();
            rectButton.sizeDelta = new Vector2(rectButton.sizeDelta.x * ratioWidth, rectButton.sizeDelta.y * ratioHeight);
            imHolder = button.transform.GetChild(0).GetComponent<Image>();
            imHolder.GetComponent<RectTransform>().anchoredPosition = new Vector2(imHolder.GetComponent<RectTransform>().anchoredPosition.x * ratioWidth, imHolder.GetComponent<RectTransform>().anchoredPosition.y);
            imHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(imHolder.GetComponent<RectTransform>().sizeDelta.x * ratioWidth, imHolder.GetComponent<RectTransform>().sizeDelta.y * ratioHeight);
            button.name = pictures[i].name;
            button.transform.GetChild(0).GetComponent<Image>().sprite = pictures[i].image;
            button.transform.position = panelLanguages.transform.position + new Vector3(0, rect.rect.height/2 - button.GetComponent<RectTransform>().rect.height/2 - i* button.GetComponent<RectTransform>().rect.height, 0);
            button.GetComponent<Button>().onClick.AddListener(() => { childButtonClicked(button.transform.GetChild(0).GetComponent<Image>().sprite, button.name); });
        }
        panelLanguages.SetActive(false);
        
        foreach (LanguageStruct st in pictures)
        {
            if (st.name.Equals(GameManager.instance.getLanguage()))
            {
                imHolder = gameObject.transform.GetChild(0).GetComponent<Image>();
                imHolder.sprite = st.image;
            }
        }
    }

    public void clickDone()
    {
        panelLanguages.SetActive(!panelLanguages.activeSelf);
    }

    private void childButtonClicked(Sprite image,string name)
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = image;
        panelLanguages.SetActive(false);
        if(GameManager.instanceExists)
             GameManager.instance.setLanguage(name);
        FindObjectOfType<LangResolver>().ReadProperties();
        FindObjectOfType<LangResolver>().ResolveTexts();
    }

}
