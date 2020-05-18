using Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressRoomMenu : SimpleMainMenuPage
{

    [SerializeField] TabDressRoom dressRoomControl;
    [SerializeField] Button buttonSelect;
    [SerializeField] Image selectedParticle;
    [SerializeField] GameObject selectedPowersControl;
    [SerializeField] Shader shaderHolo;

    DressRoomStruct selectedItemInDressRoom;
    // Start is called before the first frame update
    void Start()
    {
        dressRoomControl.ItemSelectedEvent += dressRoomItemClicked;
        updateHolograms();
    }


    private void dressRoomItemClicked(DressRoomStruct item)
    {
        
        if (string.IsNullOrEmpty(item.name))
        {
            Debug.Log("Event: No item selected in control");
            buttonSelect.interactable = false;
           // buttonSelect.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("");
        }
        else
        {
            buttonSelect.interactable = true;
            if (GameManager.instance.isItemBeingUsed(item.name))
            {
                if (item.type.Equals("POWER"))
                {
                    buttonSelect.interactable = true;
                    buttonSelect.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("REMOVE");
                }
                else
                {
                    buttonSelect.interactable = false;
                    buttonSelect.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("REMOVE");
                }
            }
            else
            {
                buttonSelect.interactable = true;
                buttonSelect.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("SELECT");
            }
            Debug.Log("Event: " + item.name + " selected in control");

        }
        selectedItemInDressRoom = item;
    }

    public void updateButtonSelect()
    {
        if (selectedItemInDressRoom.Equals(default(DressRoomStruct)))
        {
            buttonSelect.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("SELECT");
        }
        else
        {
            if (selectedItemInDressRoom.owned)
            {
                if (selectedItemInDressRoom.selected)
                {
                    buttonSelect.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("REMOVE");
                }
                else
                {
                    buttonSelect.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("SELECT");
                }
            }
            else
            {
                buttonSelect.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("SELECT");
            }
          
        }
    }

    public void selectedItemButton()
    {
        if (!GameManager.instance.isItemBeingUsed(selectedItemInDressRoom.name))
        {
            GameManager.instance.markItemToUse(selectedItemInDressRoom.name, true);
            buttonSelect.interactable = true;
            buttonSelect.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("REMOVE");
            if(!selectedItemInDressRoom.type.Equals("POWER"))
                buttonSelect.interactable = false;
        }
        else
        {
            GameManager.instance.markItemToUse(selectedItemInDressRoom.name, false);
            buttonSelect.interactable = true;
            buttonSelect.transform.GetChild(0).GetComponent<Text>().text = FindObjectOfType<LangResolver>().resolveText("SELECT");
        }
        updateHolograms();
    }

    private void updateHolograms()
    {
        foreach (Transform child in selectedPowersControl.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject objectHolder;
        List<ShopItemQuantityClass> powersList = GameManager.instance.getListOfMarkedItemsToUse();
        foreach (ShopItemQuantityClass power in powersList)
        {
            objectHolder = new GameObject(power.name, new Type[] { typeof(RectTransform), typeof(CanvasRenderer), typeof(Image) });
            objectHolder.transform.SetParent(selectedPowersControl.transform);
            Image image = objectHolder.GetComponent<Image>();
            image.sprite = dressRoomControl.getImagePower(power.name);
            image.material = new Material(shaderHolo);

        }
    }
}
