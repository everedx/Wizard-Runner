using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopSelector : MonoBehaviour
{

    [Serializable]
    struct ShopItemStruct
    {
        public string name;
        public Sprite image;
        public int price;
    }
   
    [SerializeField] ShopItemStruct[] items;
    [SerializeField] Image selectedItemImage;
    [SerializeField] Image prevItemImage;
    [SerializeField] Image nextItemImage;
    [SerializeField] Image SpareItemImage;
    [SerializeField] Marquee marquee;


    private Animator anim;
    private Vector3 originalLocationSelected;
    private Vector3 originalLocationPrev;
    private Vector3 originalLocationNext;
    private Vector2 originalSizeSelected;
    private Vector2 originalSizePrev;
    private Vector2 originalSizeNext;
    private Vector3 originalLocationSpare;
    private Vector2 originalSizeSpare;
    private ShopItemStruct selectedItem;
    private bool controlEnabled;
    private bool animationInCourse;
    // Start is called before the first frame update
    void Start()
    {
        if (items.Length < 4)
        {
            Debug.Log("Not enough items in the shop. Control disabled.");
            controlEnabled = false;
        }
        else
        {
            animationInCourse = false;
            controlEnabled = true;
            selectedItem = items[0];
            selectedItemImage.sprite = selectedItem.image;
            nextItemImage.sprite = items[1].image;
            prevItemImage.sprite = items[items.Length-1].image;
            originalLocationSelected = selectedItemImage.rectTransform.position;
            originalLocationPrev = prevItemImage.rectTransform.position;
            originalLocationNext = nextItemImage.rectTransform.position;
            originalSizeSelected = selectedItemImage.rectTransform.sizeDelta;
            originalSizePrev = prevItemImage.rectTransform.sizeDelta;
            originalSizeNext = nextItemImage.rectTransform.sizeDelta;
            originalSizeSpare = SpareItemImage.rectTransform.sizeDelta;
            originalLocationSpare = SpareItemImage.rectTransform.position;
            marquee.setTextToShow(selectedItem.name);
            anim = GetComponent<Animator>();
        }
    }


    public void nextButtonPressed()
    {
        if (controlEnabled && !animationInCourse)
        {
            int indexSelected = Array.IndexOf(items, selectedItem);
            if (indexSelected == 0)
            {
                indexSelected = items.Length - 1;
            }
            else
            {
                indexSelected--;
            }
            selectedItem = items[indexSelected];

            //updateVisuals(); DEBUG
            Debug.Log(selectedItem.name);

            //What to put in spare
            if (indexSelected == 0)
            {
                indexSelected = items.Length - 1;
            }
            else
                indexSelected--;
            SpareItemImage.sprite = items[indexSelected].image;

            //Animation next
            anim.SetTrigger("Next");
            animationInCourse = true;
        }

    }

    public void previousButtonPressed()
    {
        if (controlEnabled && !animationInCourse)
        {
            int indexSelected = Array.IndexOf(items, selectedItem);
            if (indexSelected == items.Length - 1)
            {
                indexSelected = 0;
            }
            else
            {
                indexSelected++;
            }
            selectedItem = items[indexSelected];
            //updateVisuals(); DEBUG
            Debug.Log(selectedItem.name);
            //What to put in spare
            if (indexSelected == items.Length - 1)
            {
                indexSelected = 0;
            }
            else
                indexSelected++;
            SpareItemImage.sprite = items[indexSelected].image;

            //Animation previous\
            anim.SetTrigger("Prev");
            animationInCourse = true;
        }
        
    }

    public void nextAnimEnded()
    {
        animationInCourse = false;
        //repositionImages
        selectedItemImage.rectTransform.position = originalLocationSelected;
        prevItemImage.rectTransform.position = originalLocationPrev;
        nextItemImage.rectTransform.position = originalLocationNext;
        selectedItemImage.rectTransform.sizeDelta = originalSizeSelected;
        prevItemImage.rectTransform.sizeDelta = originalSizePrev;
        nextItemImage.rectTransform.sizeDelta = originalSizeNext;
        SpareItemImage.rectTransform.sizeDelta = originalSizeSpare;
        SpareItemImage.rectTransform.position = originalLocationSpare;

        //updateVisuals
        updateVisuals();
    }

    public void prevAnimEnded()
    {
        animationInCourse = false;

        //repositionImages
        selectedItemImage.rectTransform.position = originalLocationSelected;
        prevItemImage.rectTransform.position = originalLocationPrev;
        nextItemImage.rectTransform.position = originalLocationNext;
        selectedItemImage.rectTransform.sizeDelta = originalSizeSelected;
        prevItemImage.rectTransform.sizeDelta = originalSizePrev;
        nextItemImage.rectTransform.sizeDelta = originalSizeNext;
        SpareItemImage.rectTransform.sizeDelta = originalSizeSpare;
        SpareItemImage.rectTransform.position = originalLocationSpare;

        //updateVisuals
        updateVisuals();
    }

    //Debug
    private void updateVisuals()
    {
        int indexSelected = Array.IndexOf(items, selectedItem);
        int indexNext = indexSelected + 1;
        int indexPrev= indexSelected -1;
        if (indexPrev < 0)
            indexPrev = items.Length - 1;
        if (indexNext >= items.Length)
            indexNext = 0;

        selectedItemImage.sprite = items[indexSelected].image;
        nextItemImage.sprite = items[indexNext].image;
        prevItemImage.sprite = items[indexPrev].image;
        marquee.setTextToShow(selectedItem.name);
    }
}
