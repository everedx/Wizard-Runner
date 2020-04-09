using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] prefabsStaticObstacles;
    [SerializeField] GameObject[] prefabsCoins;
    [SerializeField] int distanceBetweenGenerations;
    [SerializeField] int minNunberOfCoinsPerGenation;
    [SerializeField] int maxNunberOfCoinsPerGenation;
    [SerializeField] int minNunberOfObstaclesPerGenation;
    [SerializeField] int maxNunberOfObstaclesPerGenation;
    [SerializeField] int ratioObstaclesRoundCoinsRound;
    [SerializeField] int spaceBetweenRounds;

    private bool generateNext;
    private int randomModeChooser;
   // private int[] numbersOfLoto;
    private int[] numbersLotoCoins;
    private int[] numbersLotoObstacles;
    private float positionToCheck;
    private int numberOfItemsInSequence;

    // Start is called before the first frame update
    void Start()
    {
        
        int y = calculateMaxValueValuesRatio(ratioObstaclesRoundCoinsRound);
        int x = 1;
        positionToCheck = 0;
        generateNext = true;
        numbersLotoCoins = new int[y];
        numbersLotoObstacles = new int[100 - y];
        for (int i = 0; i < numbersLotoCoins.Length; i++) 
        {
            numbersLotoCoins[i] = x;
            x++;
        }
        for (int i = 0; i < numbersLotoObstacles.Length; i++)
        {
            numbersLotoObstacles[i] = x;
            x++;
        }
        positionToCheck = Camera.main.transform.position.x + spaceBetweenRounds + 50;
    }

    // Update is called once per frame
    void Update()
    {
        ///check it we need to generate again
        if ((positionToCheck - Camera.main.transform.position.x) < spaceBetweenRounds && !generateNext)
        {
            generateNext = true;
        }

        if (generateNext)
        {
            generateNext = false;
            randomModeChooser = UnityEngine.Random.Range(1, 100+1);
            if (Array.IndexOf(numbersLotoCoins, randomModeChooser) != -1)
            {
                numberOfItemsInSequence = UnityEngine.Random.Range(minNunberOfCoinsPerGenation, maxNunberOfCoinsPerGenation+1);
                for (int i = 0; i < numberOfItemsInSequence; i++)
                {
                    Instantiate(prefabsCoins[UnityEngine.Random.Range(0,prefabsCoins.Length)],new Vector3(Camera.main.transform.position.x+spaceBetweenRounds + distanceBetweenGenerations*i,0,10),Quaternion.identity);
                }
                positionToCheck = Camera.main.transform.position.x+spaceBetweenRounds*2 + distanceBetweenGenerations * (numberOfItemsInSequence);
            }
            else if(Array.IndexOf(numbersLotoObstacles, randomModeChooser) != -1)
            {
                numberOfItemsInSequence = UnityEngine.Random.Range(minNunberOfObstaclesPerGenation, maxNunberOfObstaclesPerGenation+1);
                for (int i = 0; i < numberOfItemsInSequence; i++)
                {
                    Instantiate(prefabsStaticObstacles[UnityEngine.Random.Range(0, prefabsStaticObstacles.Length)], new Vector3(Camera.main.transform.position.x+spaceBetweenRounds + distanceBetweenGenerations * i, UnityEngine.Random.Range(-5,5), 10), Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
                }
                positionToCheck = Camera.main.transform.position.x+spaceBetweenRounds*2 + distanceBetweenGenerations * (numberOfItemsInSequence);
            }


        }



    }


    private int calculateMaxValueValuesRatio(int ratio)
    {
        float  x;

        x = 100f / (ratio+1);
        x = Mathf.FloorToInt(x) + 1;

        return (int)x;
    }
}
