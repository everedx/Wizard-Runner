using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollector : MonoBehaviour
{

    private int score;
    private float timerAccumulator;
    private float alphaValueAccumulator;

    [SerializeField] int valueOfCoins;
    [SerializeField] Text textCoins;
    [SerializeField] Text textAcumulator;

    public int Score { get => score; }



    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    private void Update()
    {
        timerAccumulator += Time.deltaTime;
        int intText = int.Parse(textCoins.text);
        if (score != intText)
        {
            intText += 2;
            textCoins.text = intText.ToString();
            timerAccumulator = 0;
        }


        if (timerAccumulator > 1)
        {
            if (textAcumulator.color.a > 0)
            {
                alphaValueAccumulator = textAcumulator.color.a - 0.01f;
                textAcumulator.color = new Color(1, 1, 1, alphaValueAccumulator);

            }
            else
            {
                textAcumulator.text = "+0";
            }
        }


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
        int valueInAccumulator = int.Parse(textAcumulator.text.Remove(0, 1));
        valueInAccumulator += value;
        textAcumulator.color = new Color(1, 1, 1, 1);
        textAcumulator.text = "+" + valueInAccumulator;
   
    }

}
