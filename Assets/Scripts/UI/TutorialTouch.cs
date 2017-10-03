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
        animator.SetBool("Tutorial", true);
    }

    public void DeActivateAnimation()
    {
        animator.SetBool("Tutorial", false);
    }
}
