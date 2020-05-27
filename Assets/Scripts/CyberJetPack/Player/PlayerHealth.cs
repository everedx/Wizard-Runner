using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int initialHealth;
    [SerializeField] float timeInvulAfterHit;
    [SerializeField] Material materialShield;
    private float invulCounter;
    private int currentHealth;
    private int shieldCurrentHealth;
    [SerializeField] private int shieldInitialHeath;
    [SerializeField] GameObject gameLevelObj;
    SpriteRenderer sr;

    bool shieldActivated;
    public int CurrentHealth { get => currentHealth; }
    public bool ShieldActivated { get => shieldActivated; set => shieldActivated = value; }

    // Start is called before the first frame update
    void Start()
    {
        shieldActivated = false;
        shieldCurrentHealth = 0;
        materialShield.SetFloat("_Alpha", 0f);
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
        if (currentHealth > 0)
        {
            currentHealth += value;
            //Debug.Log("Health: " + currentHealth);
            if (value > 0)
            {
                GameObject.Find("HeartContainer").GetComponent<HeartContainer>().addHP();
            }
            if (value < 0)
            {
                Handheld.Vibrate(); //PC
                GameObject.Find("HeartContainer").GetComponent<HeartContainer>().lostHP();
            }


            if (getHealth() <= 0)
            {
                bool markBeated = false;
                if (GameManager.instanceExists)
                {
                    markBeated = GameManager.instance.registerMark(GetComponent<PlayerController>().Distance);
                }

                if (markBeated)
                {
                    //SHOW REWARD
                    Debug.Log(markBeated);

                }

                // Debug.Log("Game Over");
                GetComponent<PlayerController>().disableMovement();
                GameManager.instance.addMoney(GetComponent<PlayerCollector>().Score);
                StartCoroutine("delayLevelLoad");
            }
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
            if (shieldCurrentHealth <= 0)
            {
                updateHealth(-1);
                invulCounter = 0;
            }
            else
            {
                Handheld.Vibrate(); //PC
                shieldCurrentHealth -= 1;
                float alphaValue = materialShield.GetFloat("_Alpha");
                alphaValue -= 0.3f;
                if (alphaValue < 0)
                    alphaValue = 0;
                materialShield.SetFloat("_Alpha", alphaValue);
                invulCounter = 0;
            }
            


        }
    }


    public void setBasicShieldPlusOne()
    {
        initialHealth += 1;
        currentHealth = initialHealth;
    }

    public void setAdvancedShield()
    {
        shieldCurrentHealth = shieldInitialHeath;
        //materialShield.SetFloat("_Alpha",0.8f);
        shieldActivated = true;
        
    }

    public void enableShieldVisual()
    {
        materialShield.SetFloat("_Alpha", 0.8f);
    }

    IEnumerator delayLevelLoad()
    {
        yield return new WaitForSeconds(2f);
       // GameObject.Find("LevelChanger").GetComponent<LevelChanger>().changeLevel(SceneManager.GetActiveScene().name);

        //SHOW END LEVEL

        gameLevelObj.SetActive(true);
        gameLevelObj.GetComponent<EndLevelScreen>().startAnimationToShow();

    }



}
