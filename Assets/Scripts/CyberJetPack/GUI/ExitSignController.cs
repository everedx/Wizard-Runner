using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSignController : MonoBehaviour
{

    [SerializeField] GameObject[] itemsToActivate;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in itemsToActivate)
        {
            item.SetActive(false);
        }
    }


    public void activateObjects()
    {
        foreach (GameObject item in itemsToActivate)
        {
            item.SetActive(true);
        }
    }
}
