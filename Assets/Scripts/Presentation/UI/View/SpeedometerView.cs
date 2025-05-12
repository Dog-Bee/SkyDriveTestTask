using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedometerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;

    public void UpdateSpeed(int speed)
    {
        speedText.text = speed.ToString();
    }

    public void Show()
    {
        
    }

    public void Hide()
    {
        
    }
}
