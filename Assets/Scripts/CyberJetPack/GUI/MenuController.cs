using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuController
{

    private static MenuController instance= null;

    public static MenuController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MenuController();
            }
            return instance;
        }
    }
    
    private Stack<IMenuPage> menuPages;

    private MenuController()
    {
        
    }

    public void addPageToList(IMenuPage page)
    {
        menuPages.Push(page);
    }

    public void removePageFromList()
    {
        menuPages.Pop();
    }

}
