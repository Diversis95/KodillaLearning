using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;

    private float fillSpeed = 0.5f;
    private float targetProgress = 0;
    public float timeToComplet = 10;
    private float actualPassed;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        IncrementProgress(1f);
    }

    void Update()
    {
        if(slider.value < targetProgress)
        {
            if(actualPassed < timeToComplet)
            {
                actualPassed += Time.deltaTime;
                slider.value = actualPassed/timeToComplet;
            }
        }
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
}
