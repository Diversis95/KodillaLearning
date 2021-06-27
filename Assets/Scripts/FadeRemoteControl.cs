using Kodilla.Module8.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoteControl : MonoBehaviour
{
    public FadingImage fading;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            fading.DoFade();
        }
    }
}
