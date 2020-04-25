using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] Transform[] terrainObjects;
    [SerializeField] float offsetTerrain;
    [SerializeField] float distanceToCreateNewTerrain;

    [SerializeField] Transform[] cellarObjects;
    [SerializeField] float offsetCellar;
    [SerializeField] float distanceToCreateNewCellar;

    private Transform rightTerrain;
    private Transform leftTerrain;

    private Transform rightCellar;
    private Transform leftCellar;


    // Update is called once per frame
    void Update()
    {
        rightTerrain = terrainObjects.Aggregate((i1, i2) => i1.position.x > i2.position.x ? i1 : i2);
        if (rightTerrain.position.x - Camera.main.transform.position.x < distanceToCreateNewTerrain)
        {
            leftTerrain = terrainObjects.Aggregate((i1, i2) => i1.position.x < i2.position.x ? i1 : i2);
            leftTerrain.position = new Vector3(rightTerrain.position.x + offsetTerrain, leftTerrain.position.y);
        }

        rightCellar = cellarObjects.Aggregate((i1, i2) => i1.position.x > i2.position.x ? i1 : i2);
        if (rightCellar.position.x - Camera.main.transform.position.x < distanceToCreateNewCellar)
        {
            leftCellar = cellarObjects.Aggregate((i1, i2) => i1.position.x < i2.position.x ? i1 : i2);
            leftCellar.position = new Vector3(rightCellar.position.x + offsetCellar, leftCellar.position.y);
        }
    }
}
