using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rBody;
    [SerializeField] int impulseUp;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(0, 0);
        if (Input.touchCount == 0)
        {
            
        }
        else
        {
            rBody.AddForce(new Vector3(0, impulseUp, 0));
        }

        
    }
}
