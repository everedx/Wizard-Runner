using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacle : MonoBehaviour
{

    [SerializeField] int rotationSpeedMin;
    [SerializeField] int rotationSpeedMax;

    private int rotationSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        rotationSpeed = Random.Range(rotationSpeedMin,rotationSpeedMax+1);
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + rotationSpeed*0.1f); ;

    }
}
