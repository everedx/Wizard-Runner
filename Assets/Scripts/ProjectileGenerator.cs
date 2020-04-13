using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : MonoBehaviour
{

    [SerializeField] int distanceToStart;
    [SerializeField] GameObject lineProjectile;
    [SerializeField] GameObject waveProjectile;
    [SerializeField] GameObject lineProjectileWarning;
    [SerializeField] GameObject waveProjectileWarning;
    [SerializeField] PlayerController playerController;
    [SerializeField] float minTimeToGenerate;
    [SerializeField] float maxTimeToGenerate;
    [SerializeField] float distanceToGenerate;
    [SerializeField] int minLineProjectiles;
    [SerializeField] int maxLineProjectiles;

    private float distanceBetweenSimultProjectiles = 2.5f;
    private float timerToGenerate;
    private float timeToGenerate;
    private int typeOfProjectile; //1 = Line , 2 = wave

    // Start is called before the first frame update
    void Start()
    {
        timeToGenerate = Random.Range(minTimeToGenerate, maxTimeToGenerate);
        typeOfProjectile = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timerToGenerate += Time.deltaTime;
        if (playerController.Distance > distanceToStart)
        {
            if (timerToGenerate > timeToGenerate)
            {
                timeToGenerate = Random.Range(minTimeToGenerate, maxTimeToGenerate);
                timerToGenerate = 0;
                typeOfProjectile = Random.Range(1, 3);

                if (typeOfProjectile == 1)
                {
                    generateLineProjectile();
                }
                else if (typeOfProjectile == 2)
                {
                    generateWaveProjectile();
                }

            }
        }
    }

    private void generateLineProjectile()
    {
        int numberOfProjectiles = Random.Range(minLineProjectiles,maxLineProjectiles+1);
        float lowestPos = -7 + distanceBetweenSimultProjectiles * (numberOfProjectiles - 1);
        lowestPos = Random.Range(-7f, lowestPos);
        //int[] locations = new int[numberOfProjectiles];
        
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            ProjectileWarning warning= Instantiate(lineProjectileWarning,new Vector3(0,40,0),Quaternion.identity).GetComponent<ProjectileWarning>();
            warning.setProjectile(Instantiate(lineProjectile, new Vector3(distanceToGenerate + playerController.Distance, lowestPos + i*distanceBetweenSimultProjectiles, 0), Quaternion.identity).transform);
        }

        
    }

    private void generateWaveProjectile()
    {
        ProjectileWarning warning = Instantiate(waveProjectileWarning, new Vector3(0, 40, 0), Quaternion.identity).GetComponent<ProjectileWarning>();
        warning.setProjectile(Instantiate(waveProjectile,new Vector3(distanceToGenerate+ playerController.Distance, Random.Range(-3,4),0),Quaternion.identity).transform);
    }
}
