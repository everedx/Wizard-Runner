using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Linq;

public class PlayerItemsForLevel : MonoBehaviour
{

    List<ShopItemQuantityClass> itemsList;

    [SerializeField] List<imageHolder> imagesOfPowers;
    [SerializeField] GameObject panelItems;
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

        //show them in GUI
        GameObject objectHolder;

        foreach (ShopItemQuantityClass item in itemsList)
        {
            objectHolder = new GameObject(item.name, new Type[] { typeof(RectTransform), typeof(CanvasRenderer), typeof(Image) });
            objectHolder.transform.SetParent(panelItems.transform);
            Image image = objectHolder.GetComponent<Image>();
            image.sprite = imagesOfPowers.Find(x=> x.name.Equals(item.name)).image;
            image.color = new Color(1, 1, 1, 0.5f);

            //Import Items to the controllers of the game to change their characteristics.
            switch (item.name)
            {
                case "BasicShield":
                    GetComponent<PlayerHealth>().setBasicShieldPlusOne();
                    break;
                case "AdvancedShield":
                    GetComponent<PlayerHealth>().setAdvancedShield();
                    break;
                case "DoubleCoins":
                    GetComponent<PlayerCollector>().setDoubleCoins();
                    break;
                case "NoDynamicObstacles":
                    GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().setPowerNoDynamic();
                    break;
                case "NoWaveProjectiles":
                    GameObject.Find("ProjectileGenerator").GetComponent<ProjectileGenerator>().setNoWavePower();
                    break;
                case "LessProjectiles":
                    GameObject.Find("ProjectileGenerator").GetComponent<ProjectileGenerator>().setLessProjectilesPower();
                    break;
                case "SmallerObstacles":
                    GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().setPowerSmallObstacles();
                    break;

            }
        }

        //Get body to use

        ShopItemQuantityClass body= GameManager.instance.getBodyToUse();

        switch (body.name)
        {
            case "DefaultBody":
                //GetComponent<Animator>().runtimeAnimatorController =(RuntimeAnimatorController) Resources.Load("path_to_your_controller");
                break;
            case "RedBody":
                GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Red");
                break;
            case "PurpleBody":
                GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Purple");
                break;
        }

        //Get particle to use




    }


    [Serializable]
    public class imageHolder 
    {
        public string name;
        public Sprite image;
    }




}
