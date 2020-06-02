using Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void ItemSelectedInDressRoom(DressRoomStruct item);

public class TabDressRoom : MonoBehaviour
{
    public event ItemSelectedInDressRoom ItemSelectedEvent;

    [SerializeField] ShopController shopController;
    [SerializeField] GameObject pageObject;
    [SerializeField] Shader shaderOutline;
    [SerializeField] Marquee marqueeText;
    [SerializeField] GameObject buttonTabPowers;
    [SerializeField] GameObject buttonTabBodies;
    [SerializeField] GameObject buttonTabParticles;
    [SerializeField] DressRoomStruct[] namesOfPowers;
    [SerializeField] DressRoomStruct[] namesOfBodies;
    [SerializeField] DressRoomStruct[] namesOfParticles;
    
    private string tabSelected;
    private DressRoomStruct itemSelected;
    public DressRoomStruct ItemSelected { get => itemSelected; }
    // Start is called before the first frame update
    private static TabDressRoom Me;


    void Start()
    {
        Me = this;
        shopController.BoughtItem += itemHasBeenBought;
        initializeCollections();
        tabSelected = "Powers";
        activatePowerTab();
        updateVisuals();
        marqueeText.setTextToShow(" ");
        itemSelected = default(DressRoomStruct);
        ItemSelectedEvent?.Invoke(default(DressRoomStruct));
    }

    private void initializeCollections()
    {
        for (int i = 0; i < namesOfPowers.Length; i++)
        {
            if (GameManager.instance.itemOwned(namesOfPowers[i].name))
            {
                namesOfPowers[i].owned = true;
            }
        }
        for (int i = 0; i < namesOfBodies.Length; i++)
        {
            if (GameManager.instance.itemOwned(namesOfBodies[i].name))
            {
                namesOfBodies[i].owned = true;
            }
        }
        for (int i = 0; i < namesOfParticles.Length; i++)
        {
            if (GameManager.instance.itemOwned(namesOfParticles[i].name))
            {
                namesOfParticles[i].owned = true;
            }
        }
    }

    //Update Visuals
    private void itemHasBeenBought(ShopItemStruct item)
    {
        int index = Array.FindIndex(namesOfPowers,c => c.name == item.name);
        if (index!=-1)
        {
            namesOfPowers[index].owned = true;
            updateVisuals();
            return;
        }
        index = Array.FindIndex(namesOfBodies, c => c.name == item.name);
        if (index != -1)
        {
            namesOfBodies[index].owned = true;
            updateVisuals();
            return;
        }
        index = Array.FindIndex(namesOfParticles, c => c.name == item.name);
        if (index != -1)
        {
            namesOfParticles[index].owned = true;
        }
        updateVisuals();

    }

    private void itemHasBeenSelected(string item)
    {
        itemSelected = default(DressRoomStruct);
        ItemSelectedEvent?.Invoke(default(DressRoomStruct));
        //First check if its owned
        int indexPowers = Array.FindIndex(namesOfPowers, c => c.name == item);
        int indexBodies = Array.FindIndex(namesOfBodies, c => c.name == item);
        int indexParticles = Array.FindIndex(namesOfParticles, c => c.name == item);
        if (indexPowers != -1)
        {
            if(!namesOfPowers[indexPowers].owned) 
                return;
        }
        if (indexBodies != -1)
        {
            if (!namesOfBodies[indexBodies].owned)
                return;
        }
        if (indexParticles != -1)
        {
            if (!namesOfParticles[indexParticles].owned)
                return;
        }

        //Check now
        for (int i = 0;i<namesOfPowers.Length;i++) 
        {
            namesOfPowers[i].selected = false;
        }
        for (int i = 0; i < namesOfBodies.Length; i++)
        {
            namesOfBodies[i].selected = false;
        }
        for (int i = 0; i < namesOfParticles.Length; i++)
        {
            namesOfParticles[i].selected = false;
        }

        int index = Array.FindIndex(namesOfPowers, c => c.name == item);
        if (index != -1)
        {
            namesOfPowers[index].selected = true;
            itemSelected = namesOfPowers[index];
            ItemSelectedEvent?.Invoke(itemSelected);
            return;
        }
        index = Array.FindIndex(namesOfBodies, c => c.name == item);
        if (index != -1)
        {
            namesOfBodies[index].selected = true;
            itemSelected = namesOfBodies[index];
            ItemSelectedEvent?.Invoke(itemSelected);
            return;
        }
        index = Array.FindIndex(namesOfParticles, c => c.name == item);
        if (index != -1)
        {
            itemSelected = namesOfParticles[index];
            namesOfParticles[index].selected = true;
            ItemSelectedEvent?.Invoke(itemSelected);
        }
       

    }

    public void selectTab(string idOfTab)
    {
        marqueeText.setTextToShow(" ");
        itemSelected = default(DressRoomStruct);
        ItemSelectedEvent?.Invoke(itemSelected);
        deselectAll();
        switch (idOfTab)
        {
            case "Powers":
                tabSelected = "Powers";
                activatePowerTab();
                break;
            case "Bodies":
                tabSelected = "Bodies";
                activateBodiesTab();
                break;
            case "Particles":
                tabSelected = "Particles";
                activateParticlesTab();
                break;
            default:
                break;
        }
        updateVisuals();
    }

    private void deselectAll()
    {
        for (int i = 0; i < namesOfPowers.Length; i++)
        {
            namesOfPowers[i].selected = false;
        }
        for (int i = 0; i < namesOfBodies.Length; i++)
        {
            namesOfPowers[i].selected = false;
        }
        for (int i = 0; i < namesOfParticles.Length; i++)
        {
            namesOfPowers[i].selected = false;
        }
    }

    private void activatePowerTab()
    {
        
        //disable everything else
        enableColors(buttonTabPowers.GetComponent<Button>());
        disableColors(buttonTabBodies.GetComponent<Button>());
        disableColors(buttonTabParticles.GetComponent<Button>());
        // load Powers and mark the button as Active
  

    }

    private void activateBodiesTab()
    {
        
        //disable everything else
        enableColors(buttonTabBodies.GetComponent<Button>());
        disableColors(buttonTabPowers.GetComponent<Button>());
        disableColors(buttonTabParticles.GetComponent<Button>());
        // load Bodies and mark the button as Active
    }

    private void activateParticlesTab()
    {

        //disable everything else
        enableColors(buttonTabParticles.GetComponent<Button>());
        disableColors(buttonTabBodies.GetComponent<Button>());
        disableColors(buttonTabPowers.GetComponent<Button>());
        // load Powers and mark the button as Active
    }

    private void enableColors(Button button)
    {
        ColorBlock colors = button.colors;
        colors.highlightedColor = Color.white;
        colors.normalColor = Color.white;
        colors.pressedColor = Color.white;
        colors.selectedColor = Color.white;
        button.colors = colors;
    }
    private void disableColors(Button button)
    {
        ColorBlock colors = button.colors;
        colors.highlightedColor = Color.gray;
        colors.normalColor = Color.gray;
        colors.pressedColor = Color.gray;
        colors.selectedColor = Color.gray;
        button.colors = colors;
    }

    public void updateVisuals()
    {
        foreach (Transform child in pageObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject objectHolder;
        switch (tabSelected)
        {
            case "Powers":
                foreach (DressRoomStruct str in namesOfPowers)
                {
                    objectHolder = new GameObject(str.name, new Type[] { typeof(RectTransform), typeof(CanvasRenderer), typeof(Image) });
                    objectHolder.transform.SetParent(pageObject.transform);
                    objectHolder.transform.localScale = new Vector3(1, 1, 1);
                    Image image = objectHolder.GetComponent<Image>();
                    image.sprite = str.image;
                    objectHolder.AddComponent<ClickAction>();
                    if (str.owned)
                        image.color = new Color(1, 1, 1, 1);
                    else
                        image.color = new Color(1, 1, 1, 0.1f);
                    if (str.selected && str.owned)
                    {
                        image.material = new Material(shaderOutline);
                        marqueeText.setTextToShow(FindObjectOfType<LangResolver>().resolveText(str.description));
                    }
                    else
                    {
                        image.material = null;
                    }
                }
                break;
            case "Bodies":
                foreach (DressRoomStruct str in namesOfBodies)
                {
                    objectHolder = new GameObject(str.name, new Type[] { typeof(RectTransform), typeof(CanvasRenderer), typeof(Image) });
                    objectHolder.transform.SetParent(pageObject.transform);
                    objectHolder.transform.localScale = new Vector3(1, 1, 1);
                    Image image = objectHolder.GetComponent<Image>();
                    image.sprite = str.image;
                    objectHolder.AddComponent<ClickAction>();
                    if (str.owned)
                        image.color = new Color(1, 1, 1, 1);
                    else
                        image.color = new Color(1, 1, 1, 0.1f);
                    if (str.selected && str.owned)
                    {
                        image.material = new Material(shaderOutline);
                        marqueeText.setTextToShow(FindObjectOfType<LangResolver>().resolveText(str.description));
                    }
                    else
                    {
                        image.material = null;
                    }
                }
                break;
            case "Particles":
                foreach (DressRoomStruct str in namesOfParticles)
                {
                    objectHolder = new GameObject(str.name, new Type[] { typeof(RectTransform), typeof(CanvasRenderer), typeof(Image) });
                    objectHolder.transform.SetParent(pageObject.transform);
                    objectHolder.transform.localScale = new Vector3(1, 1, 1);
                    Image image = objectHolder.GetComponent<Image>();
                    image.sprite = str.image;
                    objectHolder.AddComponent<ClickAction>();
                    if (str.owned)
                        image.color = new Color(1, 1, 1, 1);
                    else
                        image.color = new Color(1, 1, 1, 0.1f);
                    if (str.selected && str.owned)
                    {
                        image.material = new Material(shaderOutline);
                        marqueeText.setTextToShow(FindObjectOfType<LangResolver>().resolveText(str.description));
                    }
                    else
                    {
                        image.material = null;
                    }
                }
                break;
        }
    }


    public Sprite getImagePower(String key)
    {
        DressRoomStruct item;
        int index = Array.FindIndex(namesOfPowers, c => c.name == key);
        if (index != -1)
        {
            return namesOfPowers[index].image;
        }
        else
            return null;
    }


    private class ClickAction : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Me.itemHasBeenSelected(eventData.pointerCurrentRaycast.gameObject.name);
            Me.updateVisuals();
        }
    }
}




[Serializable]
public struct DressRoomStruct
{
    public string name;
    public Sprite image;
    public string description;
    public bool owned;
    public bool selected;
    public string type;
}

