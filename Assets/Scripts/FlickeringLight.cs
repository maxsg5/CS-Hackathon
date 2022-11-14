using System.Collections;
using UnityEngine;


public class FlickeringLight : MonoBehaviour
{

    UnityEngine.Rendering.Universal.Light2D testLight;
    public float minWaitTime;
    public float maxWaitTime;
    void Start()
    {
        testLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing ()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime,maxWaitTime));
            testLight.enabled = !testLight.enabled;
        }
    }

}
