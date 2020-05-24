using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsGraph : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (int.Parse(gameObject.name) < GameManager.instance.getBestMark())
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        }
    }

    
}
