using System.Collections;
using System.Collections.Generic;
using ColourRun.Managers;
using UnityEngine.UI;
using UnityEngine;

public class TutorialTouch : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    private TutorialManager tm;

    public void ActivateAnimation()
    {
        if(!animator.GetBool("Tutorial"))
            animator.SetBool("Tutorial", true);
    }

    public void DeActivateAnimation()
    {
        if(animator.isActiveAndEnabled)
            animator.SetBool("Tutorial", false);
    }
}
