using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{

    [SerializeField] Sprite heartSprite;
    [SerializeField] Sprite lostHeartSprite;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Shader matHearts;

    private Image[] hearthRendererArray;
    private int health;
    private float width;
    private float height;
    
    // Start is called before the first frame update
    void Start()
    {
        health = playerHealth.CurrentHealth;
        hearthRendererArray = new Image[health];
        width = GetComponent<RectTransform>().sizeDelta.x;
        height = GetComponent<RectTransform>().sizeDelta.y;
        float spaceInContainerPerHeart = ((width-40) / health);
        Image img;
        GameObject gm;
        for (int i = 0; i < health; i++)
        {
            gm = new GameObject("Heart " + i+1,typeof(RectTransform));
            gm.transform.parent = transform;
            img = gm.AddComponent<Image>();
            img.sprite = heartSprite;
            img.material = new Material(matHearts);
            img.material.SetFloat("_Fade", 1);
            img.material.SetFloat("_Scale", 10);
            img.rectTransform.localScale = new Vector3(1, 1, 1);
            hearthRendererArray[i] = img;
            float xPos = (-width/2)+(gm.GetComponent<RectTransform>().sizeDelta.x/2)+ (gm.GetComponent<RectTransform>().sizeDelta.x / 2)/5+20 + (i*(spaceInContainerPerHeart + ((width / health) / 5) - 2*40/health));
            gm.transform.localPosition = new Vector2(xPos, 0);
            
        }


    }

    public void lostHP()
    {
        health -= 1;
        //hearthRendererArray[hearthRendererArray.Length - health -1].sprite = lostHeartSprite;
    }
    public void addHP()
    {
        health += 1;
        //hearthRendererArray[hearthRendererArray.Length - health - 1].sprite = heartSprite;

    }

    private void Update()
    {
        for (int i = 0; i < hearthRendererArray.Length; i++)
        {
            if (i > hearthRendererArray.Length - health - 1)
            {
                if (hearthRendererArray[i].material.GetFloat("_Fade") < 1)
                {
                   hearthRendererArray[i].material.SetFloat("_Fade", hearthRendererArray[i].material.GetFloat("_Fade") + Time.deltaTime/2);
                }
            }
            else
            {
                if (hearthRendererArray[i].material.GetFloat("_Fade") > 0)
                {
                   
                    hearthRendererArray[i].material.SetFloat("_Fade", hearthRendererArray[i].material.GetFloat("_Fade") - Time.deltaTime/2);
                }
            }
        }
    }
}
