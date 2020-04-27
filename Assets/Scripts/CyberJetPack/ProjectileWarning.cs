using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWarning : MonoBehaviour
{

    Transform projectileToWarn;

    // Update is called once per frame
    void Update()
    {
        if (projectileToWarn != null)
        {
            transform.position = new Vector3(Camera.main.transform.position.x - 2 + Camera.main.orthographicSize * 2,projectileToWarn.transform.position.y,0);
            if (projectileToWarn.position.x - Camera.main.transform.position.x < (Camera.main.orthographicSize * 2) + 2)
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void setProjectile(Transform projectile)
    {
        projectileToWarn = projectile;
    }
}
