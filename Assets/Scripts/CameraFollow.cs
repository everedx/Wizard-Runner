using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform player;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] float smoothTime;
    Vector3 goalPos;
    Camera cam; 
    [SerializeField] float distanceToEdge;
    // Start is called before the first frame update
    void Start()
    {
        goalPos = new Vector3();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        goalPos.x = player.transform.position.x + 2*cam.orthographicSize - distanceToEdge;
   
        goalPos.z = -10;
        goalPos.y = 0;

        transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
    }
}
