using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsForLevel : MonoBehaviour
{

    List<ShopItemQuantityClass> itemsList;

    // Start is called before the first frame update
    void Start()
    {
        itemsList = GameManager.instance.getListOfMarkedItemsToUse();


        Debug.Log("Items: " + itemsList.Count);
        foreach (ShopItemQuantityClass item in itemsList)
        {
            Debug.Log(item.name); ;
        }
        //All items have been loaded in their correspondant scripts. Use them.
        GameManager.instance.useMarkedItems();
    }

    
   


}
