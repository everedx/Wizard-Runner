using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int initialHealth;
    [SerializeField] float timeInvulAfterHit;
    private float invulCounter;
    private int currentHealth;
    SpriteRenderer sr;

    public int CurrentHealth { get => currentHealth; }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHealth = initialHealth;
        invulCounter = timeInvulAfterHit;
    }

    private void Update()
    {
        invulCounter += Time.deltaTime;
        if (invulCounter <= timeInvulAfterHit)
        {
            if(sr.color.a == 255)
                sr.color = new Color(255, 255, 255, 0);
            else
                sr.color = new Color(255, 255, 255, 255);
        }
        else
        {
            sr.color = new Color(255, 255, 255, 255);
        }
    }

    public void updateHealth(int value)
    {
        currentHealth += value;
        //Debug.Log("Health: " + currentHealth);
        if (value > 0)
        {
            GameObject.Find("HeartContainer").GetComponent<HeartContainer>().addHP();
        }
        if (value < 0)
        {
            GameObject.Find("HeartContainer").GetComponent<HeartContainer>().lostHP();
        }
                

        if (getHealth() <= 0)
        {
            bool markBeated = false;
            if (GameManager.instanceExists)
            {
                markBeated= GameManager.instance.registerMark(GetComponent<PlayerController>().Distance);
            }

            if (markBeated)
            {
                //SHOW REWARD
                Debug.Log(markBeated);
                
            }

           // Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private int getHealth()
    {
        return currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag.Equals("Obstacle") || collision.tag.Equals("Projectile")) && invulCounter > timeInvulAfterHit)
        {
            updateHealth(-1);
            invulCounter = 0;


        }
    }

}
