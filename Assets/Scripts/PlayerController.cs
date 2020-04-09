using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rBody;
    [SerializeField] int impulseUp;
    [SerializeField] int initialSpeed;
    [SerializeField] int speedIncrement;
    [SerializeField] int incrementInterval;
    [SerializeField] ParticleSystem ps;
    [SerializeField] int particlesPerSecond;
    [SerializeField] Text textDistance;

    private Vector2 iniPos;
    private int distance;
    private int speed;

    public int Distance { get => distance; }

    // Start is called before the first frame update
    void Start()
    {

        rBody = GetComponent<Rigidbody2D>();
        speed = initialSpeed;
        iniPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = (int)transform.position.x - (int)iniPos.x;
        textDistance.text = distance.ToString() + " m";
        rBody.velocity = new Vector3(speed,rBody.velocity.y,0);
        
        if (Input.touchCount == 0)
        {
            changeEmissionOverTime(0);
        }
        else
        {
            changeEmissionOverTime(particlesPerSecond);
            rBody.AddForce(new Vector3(0, impulseUp, 0));
        }



    }


    private void changeEmissionOverTime(float value)
    {
        var emission = ps.emission;
        emission.rateOverTime = value;
    }
}
