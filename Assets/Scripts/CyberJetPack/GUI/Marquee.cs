using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marquee : MonoBehaviour
{

    private char[] textToShow;
   // private string textShowing;
    [SerializeField] float speedScroll;
    [SerializeField] int numberOfCharactersFitting;
    private float timer;
    [SerializeField] private bool marqueeWorking;
    char[] charactersToFill;
    int indexTextToShow;
    Text text;
    bool finishMarquee;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        timer = 0;
        
        //marqueeWorking = false;
        //textToShow = text;
        charactersToFill = new char[numberOfCharactersFitting];
        for (int i = 0; i < charactersToFill.Length; i++)
        {
            charactersToFill[i] = ' ';
        }

        //debug
        //setTextToShow("   ");
        finishMarquee = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (marqueeWorking)
        {
            if (timer > speedScroll)
            {
                //move all the characters to the left
                charactersToFill = moveCharactersToLeft(charactersToFill);

                //put the last character

                if (!finishMarquee)
                {
                    charactersToFill[charactersToFill.Length - 1] = textToShow[indexTextToShow];
                    indexTextToShow++;
                }
                else
                {
                    charactersToFill[charactersToFill.Length - 1] = ' ';
                }
               

                //check if it was the last chatarter;
                if (indexTextToShow >= textToShow.Length)
                {
                    indexTextToShow = 0;
                    finishMarquee = true;
                }

                text.text =new string (charactersToFill);
                if (string.IsNullOrWhiteSpace(text.text))
                {
                    finishMarquee = false;
                }
                timer = 0;   
            }
        }
    }

    public void setTextToShow(string text)
    {
        if (charactersToFill==null)
        {
            charactersToFill = new char[numberOfCharactersFitting];
        }

        indexTextToShow = 0;
        textToShow = text.ToCharArray();
        for(int i=0;i<charactersToFill.Length;i++) 
        {
            charactersToFill[i] = ' ';
        }
    }

    private char[] moveCharactersToLeft(char[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            array[i] = array[i + 1];
        }
        return array;
    }


}
