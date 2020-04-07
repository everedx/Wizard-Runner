using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] prefabsStaticObstacles;
    [SerializeField] GameObject[] prefabsCoins;

    private bool generateNext;

    // Start is called before the first frame update
    void Start()
    {
        generateNext = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
