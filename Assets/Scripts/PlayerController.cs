using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rBody;
    [SerializeField] int impulseUp;
    [SerializeField] int initialSpeed;
    [SerializeField] int speedIncrement;
    [SerializeField] int incrementInterval;
    [SerializeField] ParticleSystem ps;
    [SerializeField] int particlesPerSecond;
    private int speed;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        speed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
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
