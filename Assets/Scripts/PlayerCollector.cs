using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{

    private int score;
    [SerializeField] int valueOfCoins;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("ScoringObject"))
        {
            updateScore(valueOfCoins);
            Destroy(collision.gameObject);
        }
    }


    public void updateScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }

}
