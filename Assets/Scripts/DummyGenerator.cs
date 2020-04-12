using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyGenerator : MonoBehaviour
{

    [SerializeField] float minTimeGeneration;
    [SerializeField] float maxTimeGeneration;
    [SerializeField] GameObject dummyObject;
    [SerializeField] float yPositionToGenerateDummies;
    [SerializeField] float xDistanceToGenerateDummies;

    private float timeToGenerate;
    private float timerGeneration;

    // Start is called before the first frame update
    void Start()
    {
        timerGeneration = 0;
        timeToGenerate = Random.Range(minTimeGeneration, maxTimeGeneration+1);
    }

    // Update is called once per frame
    void Update()
    {
        timerGeneration += Time.deltaTime;

        if (timerGeneration > timeToGenerate)
        {
            timerGeneration = 0;
            timeToGenerate= Random.Range(minTimeGeneration, maxTimeGeneration + 1);
            Instantiate(dummyObject,new Vector3(Camera.main.transform.position.x + xDistanceToGenerateDummies, yPositionToGenerateDummies, 0),Quaternion.identity);
        }

    }
}
