using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{

    [SerializeField] float dummyMoveSpeed;
    [SerializeField] int minTimeChangeDirection;
    [SerializeField] int maxTimeChangeDirection;
    

    private Rigidbody2D rBody;
    private SpriteRenderer sRenderer;
    private float timer;
    private float speed;
    private float timeToChange;
    // Start is called before the first frame update
    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        int randomSpeed=Random.Range(0, 2);
        if (randomSpeed == 0)
            speed = dummyMoveSpeed;
        else
            speed = -dummyMoveSpeed;

        if (speed < 0)
            sRenderer.flipX = true;

        timeToChange = Random.Range(minTimeChangeDirection,maxTimeChangeDirection+1);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (speed < 0)
            sRenderer.flipX = true;
        else
            sRenderer.flipX = false;
        rBody.velocity = new Vector2(speed, 0);

        if (timer > timeToChange)
        {
            timer = 0;
            timeToChange = Random.Range(minTimeChangeDirection, maxTimeChangeDirection + 1);
            speed = -speed;
        }

    }

}
