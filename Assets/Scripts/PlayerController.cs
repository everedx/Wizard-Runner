using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using everedxCode;

public class PlayerController : MonoBehaviour
{
    

    Rigidbody2D rBody;
    [SerializeField] int impulseUp;
    [SerializeField] int initialSpeed;
    [SerializeField] int speedIncrement;
    [SerializeField] int incrementInterval;
    [SerializeField] ParticleSystem ps;
    [SerializeField] GameObject stepObject;
    [SerializeField] int particlesPerSecond;
    [SerializeField] Text textDistance;
    [SerializeField] DynamicParallaxController dynamicParallax;
    [SerializeField] bool debugMode;

    private Vector2 iniPos;
    private int distance;
    private int speed;
    private bool floorDetected;

    public int Distance { get => distance; }
    public bool FloorDetected { get => floorDetected; }

    // Start is called before the first frame update
    void Start()
    {

        rBody = GetComponent<Rigidbody2D>();
        speed = initialSpeed;
        iniPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = (int)transform.position.x - (int)iniPos.x;
        textDistance.text = distance.ToString() + " m";
        rBody.velocity = new Vector3(speed,rBody.velocity.y,0);
        
        if (Input.touchCount == 0)
        {
            changeEmissionOverTime(0);
            if (transform.position.y < iniPos.y)
            {
                floorDetected = true;
            }
            else
                floorDetected = false;
        }
        else
        {
            changeEmissionOverTime(particlesPerSecond);
            rBody.AddForce(new Vector3(0, impulseUp, 0));
            floorDetected = false;
        }

        if (distance > incrementInterval)
        {
            if(debugMode)
                Utils.showFloatingText("Speed Up!", transform.position,transform,speed+ speedIncrement,4f);
            speed += speedIncrement;
            incrementInterval = incrementInterval + distance + speedIncrement * 20;
            //Change background
            dynamicParallax.ChangeBackGround();
        }

    }

 

    private void changeEmissionOverTime(float value)
    {
     
        var emission = ps.emission;
        emission.rateOverTime = value;
    }

    public void createStep()
    {
        if (floorDetected)
        {
            Instantiate(stepObject,new Vector3(transform.position.x,-7.8f,0),Quaternion.identity);
        }
            
    }
}
