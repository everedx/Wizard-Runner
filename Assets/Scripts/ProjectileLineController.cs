using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLineController : MonoBehaviour
{
    [SerializeField] float speed;

    
    private Vector2 currentPos;
    // Start is called before the first frame update
    void Awake()
    {
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position + new Vector3(-speed*Time.deltaTime,0);
        transform.position = currentPos;
    }
}
