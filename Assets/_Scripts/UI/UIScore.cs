using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;
    public void UpdateScore(int points)
    {
        if (points == 0)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
        text.SetText(points.ToString());
    }
}
