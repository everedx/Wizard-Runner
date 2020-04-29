using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMainMenuPage : MonoBehaviour, IMenuPage
{
    public Canvas canvas;

    public virtual void hide()
    {
        if (canvas != null)
        {
            canvas.enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void show()
    {
        if (canvas != null)
        {
            canvas.enabled = true;
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
