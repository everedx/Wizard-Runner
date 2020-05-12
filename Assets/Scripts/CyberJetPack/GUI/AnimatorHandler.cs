using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{

    [SerializeField] Animator[] animators;


    public void startAnimations()
    {
        foreach (Animator anim in animators)
        {
            anim.SetTrigger("start");
        }
    }

}
