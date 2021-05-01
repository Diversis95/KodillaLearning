using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Button pauseButton;
    public TMPro.TextMeshProUGUI pointsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdatePoints(int points)
    {
        pointsText.text = "Points: " + points;
    }
}
