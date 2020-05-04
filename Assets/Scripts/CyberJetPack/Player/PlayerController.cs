using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Utils;

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
    [SerializeField] Text bestMarkText;
    [SerializeField] DynamicParallaxController dynamicParallax;
    [SerializeField] bool debugMode;

    private Animator animator;
    private Vector2 iniPos;
    private int distance;
    private int speed;
    private bool floorDetected;
    bool touching;
    private bool movementEnabled;
    public int Distance { get => distance; }
    public bool FloorDetected { get => floorDetected; }

    // Start is called before the first frame update
    void Start()
    {
        movementEnabled = true;
        rBody = GetComponent<Rigidbody2D>();
        speed = initialSpeed;
        iniPos = transform.position;
        animator = GetComponent<Animator>();
        animator.SetBool("Pressing",false);
        if (GameManager.instanceExists)
        {
           bestMarkText.text= bestMarkText.text.Split(':')[0] + ": "  + GameManager.instance.getBestMark();
        }
        else
            bestMarkText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = (int)transform.position.x - (int)iniPos.x;
        textDistance.text = distance.ToString() + " m";
        if (movementEnabled)
        {
            rBody.velocity = new Vector3(speed, rBody.velocity.y, 0);
            if (Application.isMobilePlatform)
                touching = Input.touchCount > 0;
            else
                touching = Input.GetMouseButton(0);
            if (!touching)
            {
                animator.SetBool("Pressing", false);
                changeEmissionOverTime(0);
                if (transform.position.y < iniPos.y)
                {
                    floorDetected = true;
                    animator.SetBool("InFloor", true);
                }
                else
                {
                    animator.SetBool("InFloor", false);
                    floorDetected = false;
                }
            }
            else
            {
                animator.SetBool("Pressing", true);
                changeEmissionOverTime(particlesPerSecond);
                rBody.AddForce(new Vector3(0, impulseUp, 0));
                floorDetected = false;
            }
        }
        else
        {
            changeEmissionOverTime(0);
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

    public void disableMovement()
    {
        movementEnabled = false;
    }
}
