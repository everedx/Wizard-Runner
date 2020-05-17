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
    private int[] numbersLotoCoins;
    private int[] numbersLotoObstacles;
    private int[] numbersLotoCoinsCopy;
    private int[] numbersLotoObstaclesCopy;
    private float positionToCheck;
    private int numberOfItemsInSequence;
    private int lastSequenceMode; //0 = 1 obstacles , 2 = coins


    //powers
    private bool smallObstaclesActivated;
    private bool noDynamicActivated;
    


    // Start is called before the first frame update
    void Start()
    {
        
        int y = calculateMaxValueValuesRatio(ratioObstaclesRoundCoinsRound);
        int x = 1;
        smallObstaclesActivated = false;
        noDynamicActivated = false;
        lastSequenceMode = 0;
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
        numbersLotoCoinsCopy = (int[])numbersLotoCoins.Clone();
        numbersLotoObstaclesCopy = (int[])numbersLotoObstacles.Clone();
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
                if (lastSequenceMode == 2)
                {
                    numbersLotoObstacles = (int[])numbersLotoObstaclesCopy.Clone();
                }
                generateCoins();
                removeHalfValues(ref numbersLotoCoins);
            }
            else if (Array.IndexOf(numbersLotoObstacles, randomModeChooser) != -1)
            {
                if (lastSequenceMode == 1)
                {
                    numbersLotoCoins = (int[])numbersLotoCoinsCopy.Clone();
                }
                generateObstacles();
                removeHalfValues(ref numbersLotoObstacles);
            }
            else 
            {
                if (lastSequenceMode == 1)
                {
                    generateCoins();
                    numbersLotoObstacles =(int[])numbersLotoObstaclesCopy.Clone();
                }
                else if (lastSequenceMode == 2)
                {
                    generateObstacles();
                    numbersLotoCoins = (int[])numbersLotoCoinsCopy.Clone();
                }
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

    private void removeHalfValues(ref int[] array)
    {
        int count = 0;
        for (int i = 0; i < array.Length; ++i)
        {
            if (array[i] != 0)
                count++;
        }
        count = count / 2;
        for (int i = 0;i<array.Length; i++)
        {
            if (array[i] != 0)
            {
                array[i] = 0;
                count--;
                if (count <= 0)
                {
                    break;
                }
            }
        }
       
    }



    private void generateObstacles()
    {
        GameObject obj;
        lastSequenceMode = 1;
        numberOfItemsInSequence = UnityEngine.Random.Range(minNunberOfObstaclesPerGenation, maxNunberOfObstaclesPerGenation + 1);
        for (int i = 0; i < numberOfItemsInSequence; i++)
        {
            if (noDynamicActivated)
            {
                obj = Instantiate(prefabsStaticObstacles[0], new Vector3(Camera.main.transform.position.x + spaceBetweenRounds + distanceBetweenGenerations * i, UnityEngine.Random.Range(-6, 6 + 1), 10), Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
            }
            else
                obj= Instantiate(prefabsStaticObstacles[UnityEngine.Random.Range(0, prefabsStaticObstacles.Length)], new Vector3(Camera.main.transform.position.x + spaceBetweenRounds + distanceBetweenGenerations * i, UnityEngine.Random.Range(-6, 6+1), 10), Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
            if (smallObstaclesActivated)
            {
                obj.transform.localScale = obj.transform.localScale / 2;
            }
        }
        positionToCheck = Camera.main.transform.position.x + spaceBetweenRounds * 2 + distanceBetweenGenerations * (numberOfItemsInSequence);
    }

    private void generateCoins()
    {
        lastSequenceMode = 2;
        numberOfItemsInSequence = UnityEngine.Random.Range(minNunberOfCoinsPerGenation, maxNunberOfCoinsPerGenation + 1);
        for (int i = 0; i < numberOfItemsInSequence; i++)
        {
            Instantiate(prefabsCoins[UnityEngine.Random.Range(0, prefabsCoins.Length)], new Vector3(Camera.main.transform.position.x + spaceBetweenRounds + distanceBetweenGenerations * i, 0, 10), Quaternion.identity);
        }
        positionToCheck = Camera.main.transform.position.x + spaceBetweenRounds * 2 + distanceBetweenGenerations * (numberOfItemsInSequence);
    }

    public void setPowerSmallObstacles()
    {
        smallObstaclesActivated = true;
    }

    public void setPowerNoDynamic()
    {
        noDynamicActivated = true;
    }

}
