using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// smoothly follows the target
public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    void Update()
    {
        //follow the target's x and y position smoothly
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }
}
