using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeIn : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;


    public void Start()
    {
        StartCoroutine(appear());
    }
    IEnumerator appear()
    {
        //Play Animation
        transition.SetTrigger("beginFade");
        yield return new WaitForSeconds(transitionTime);
    }
    public void fadeBegin()
    {
        StartCoroutine(appear());
    }

}
