using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// smoothly follows the target
public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float horizontalOffset = 0f;
    [SerializeField] private float smoothSpeed = 0.125f;
    
    void Update()
    {
        //follow the target's x and y position smoothly
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 cameraPosition = new Vector3(transform.position.x + horizontalOffset, transform.position.y + verticalOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}
