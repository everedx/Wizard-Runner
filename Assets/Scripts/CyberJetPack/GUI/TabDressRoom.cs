using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabDressRoom : MonoBehaviour
{

    [SerializeField] GameObject buttonTabPowers;
    [SerializeField] GameObject buttonTabBodies;
    [SerializeField] GameObject buttonTabParticles;
    [SerializeField] DressRoomStruct[] namesOfPowers;
    [SerializeField] DressRoomStruct[] namesOfBodies;
    [SerializeField] DressRoomStruct[] namesOfParticles;

    // Start is called before the first frame update
    void Start()
    {
        loadPowers();
    }



    public void selectTab(string idOfTab)
    {
        switch (idOfTab)
        {
            case "Powers":
                loadPowers();
                break;
            case "Bodies":
                loadBodies();
                break;
            case "Particles":
                loadParticles();
                break;
            default:
                break;
        }
    }

    private void loadPowers()
    {
        
        //disable everything else
        enableColors(buttonTabPowers.GetComponent<Button>());
        disableColors(buttonTabBodies.GetComponent<Button>());
        disableColors(buttonTabParticles.GetComponent<Button>());
        // load Powers and mark the button as Active
    }

    private void loadBodies()
    {
        
        //disable everything else
        enableColors(buttonTabBodies.GetComponent<Button>());
        disableColors(buttonTabPowers.GetComponent<Button>());
        disableColors(buttonTabParticles.GetComponent<Button>());
        // load Bodies and mark the button as Active
    }

    private void loadParticles()
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

}


[Serializable]
public struct DressRoomStruct
{
    public string name;
    public Sprite image;
    public string description;
    public bool owned;
}

