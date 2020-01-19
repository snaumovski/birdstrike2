using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValueScript : MonoBehaviour
{
    


    public void SetTextOfCount(int value)
    {
        var percentageText = GetComponent<Text>();

        percentageText.text = Mathf.RoundToInt(value * 100).ToString();
    }
}
