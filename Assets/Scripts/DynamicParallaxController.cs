using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DynamicParallaxController : MonoBehaviour
{
    //This will manage the change between environments. MUST HAVE a public read only property for the globalLightController to have the info.
    [SerializeField] Material animationMaterial;
    [SerializeField] float timeOfAnimation;

    [SerializeField] SpriteRenderer backgroundRenderer;
    [SerializeField] SpriteRenderer parallaxBackRenderer;
    [SerializeField] SpriteRenderer parallaxMiddleBackRenderer;
    [SerializeField] SpriteRenderer parallaxMiddlefrontRenderer;
    [SerializeField] SpriteRenderer parallaxfrontRenderer;


    [SerializeField] Sprite[] spritesRound1;
    [SerializeField] Sprite[] spritesRound2;
    [SerializeField] Sprite[] spritesRound3;
    [SerializeField] Sprite[] spritesRound4;
    [SerializeField] Sprite[] spritesRound5;
    [SerializeField] Light2D lightObject;
    [SerializeField] Color[] colorsLighting;

    private int i;
    private float timer;
    private bool hasToGoDown;
    private bool hasToGoUp;
    private bool changingScenario;

    private void Start()
    {
        i = 0;
        timer = 0;
        hasToGoDown = false;
        hasToGoUp = false;
        changingScenario = false;

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!changingScenario)
        {
            if (!hasToGoUp && !hasToGoDown)
                animationMaterial.SetFloat("_Fade", 1);
            else if (hasToGoDown && animationMaterial.GetFloat("_Fade") > 0.15f)
            {
                Debug.Log(animationMaterial.GetFloat("_Fade"));
                animationMaterial.SetFloat("_Fade", animationMaterial.GetFloat("_Fade") - Time.deltaTime/2 );
                Debug.Log(animationMaterial.GetFloat("_Fade"));
                if (animationMaterial.GetFloat("_Fade") <= 0.15f)
                {
                    changingScenario = true;
                    hasToGoDown = false;
                    timer = 0;
                    //Change scenario
                    switch (i)
                    {
                        case 0:
                            backgroundRenderer.sprite = spritesRound1[0];
                            parallaxBackRenderer.sprite = spritesRound1[1];
                            parallaxMiddleBackRenderer.sprite = spritesRound1[2];
                            parallaxMiddlefrontRenderer.sprite = spritesRound1[3];
                            parallaxfrontRenderer.sprite = spritesRound1[4];
                            
                            break;
                        case 1:
                            backgroundRenderer.sprite = spritesRound2[0];
                            parallaxBackRenderer.sprite = spritesRound2[1];
                            parallaxMiddleBackRenderer.sprite = spritesRound2[2];
                            parallaxMiddlefrontRenderer.sprite = spritesRound2[3];
                            parallaxfrontRenderer.sprite = spritesRound2[4];
                            break;
                        case 2:
                            backgroundRenderer.sprite = spritesRound3[0];
                            parallaxBackRenderer.sprite = spritesRound3[1];
                            parallaxMiddleBackRenderer.sprite = spritesRound3[2];
                            parallaxMiddlefrontRenderer.sprite = spritesRound3[3];
                            parallaxfrontRenderer.sprite = spritesRound3[4];
                            break;
                        case 3:
                            backgroundRenderer.sprite = spritesRound4[0];
                            parallaxBackRenderer.sprite = spritesRound4[1];
                            parallaxMiddleBackRenderer.sprite = spritesRound4[2];
                            parallaxMiddlefrontRenderer.sprite = spritesRound4[3];
                            parallaxfrontRenderer.sprite = spritesRound4[4];
                            break;
                        case 4:
                            backgroundRenderer.sprite = spritesRound5[0];
                            parallaxBackRenderer.sprite = spritesRound5[1];
                            parallaxMiddleBackRenderer.sprite = spritesRound5[2];
                            parallaxMiddlefrontRenderer.sprite = spritesRound5[3];
                            parallaxfrontRenderer.sprite = spritesRound5[4];
                            break;


                    }
                    lightObject.color = colorsLighting[i];
                }

            }

            if (hasToGoUp && animationMaterial.GetFloat("_Fade") < 1)
            {
                animationMaterial.SetFloat("_Fade", animationMaterial.GetFloat("_Fade") + Time.deltaTime/2 );
                if (animationMaterial.GetFloat("_Fade") >= 1)
                {
                    hasToGoUp = false;
                }
            }
        




        }
        else
        {
            if (timer > timeOfAnimation)
            {
                hasToGoUp = true;
                changingScenario = false;
            }
        }



    }

    public void ChangeBackGround()
    {
        i++;
        if (i > 4)
            i = 0;
        //take sprites from index i and place them
        hasToGoDown = true;
    }
}
