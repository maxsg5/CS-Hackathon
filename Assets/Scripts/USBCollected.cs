using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class USBCollected : MonoBehaviour
{
    public TMP_Text MyText;
    private int score;

    void Start()
    {
        score = 0;
        MyText.text = " " + score;
    }

    void Update()
    {
        if (score < 10)
        {
            MyText.text = " " + score;
        }
        else
        {
            MyText.text = "" + score;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AddScore();
        }
    }

    public void AddScore()
    {
        score++;
    }
}
