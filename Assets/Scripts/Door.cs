using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   Animator animator;
    public float waitTime = 20f;
    void Awake()
   {
        animator = GetComponent<Animator>();
   }

   public void OpenDoor()
   {
     StartCoroutine(Opening());
   }

   private IEnumerator Opening()
   {
          yield return new WaitForSeconds(waitTime);
          animator.SetTrigger("Open");
     }
}
