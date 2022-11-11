using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   Animator animator;
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
          yield return new WaitForSeconds(20f);
          animator.SetTrigger("Open");
     }
}
