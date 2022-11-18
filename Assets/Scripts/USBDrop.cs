using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBDrop : MonoBehaviour
{
    public Transform transform;
    public GameObject[] Prefabs;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    public void Drop()
    {
        GameObject usb = Instantiate(Prefabs[0], transform.position, Quaternion.identity);
    }

    public void Destroy()
    {
        Destroy(Prefabs[0]);
    }
}