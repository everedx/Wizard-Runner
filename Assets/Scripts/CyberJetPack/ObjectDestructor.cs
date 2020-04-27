using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestructor : MonoBehaviour
{

    private void Update()
    {
        if (gameObject.transform.position.x < Camera.main.transform.position.x - 50)
            Destroy(gameObject);
    }
}
  
