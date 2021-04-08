using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    private int frame;
    private float timer;

    private int[] numberOfFrame = new int[10];
    private int thisFrame;
    private int avarageFramerate;

    

    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
        timer = 0;
        thisFrame = 0;
        avarageFramerate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        frame++;

        CheckIfSecondPass();

        
    }

    void CheckIfSecondPass()
    {
        if (timer > 1)
        {
            Debug.Log("Frames per seconds: " + frame);

            numberOfFrame[thisFrame] = frame;
            thisFrame++;

            if (thisFrame == 10)
            {
                AvarageCounter();
                Debug.Log("Avarage 10 sec framerate: " + avarageFramerate);

                thisFrame = 0;
            }

            frame = 0;
            timer = 0;
        }
    }

    void AvarageCounter()
    {
        for (int x = 0; x < numberOfFrame.Length; x++)
        {
            avarageFramerate += numberOfFrame[x];
        }
        avarageFramerate /= numberOfFrame.Length;
    }
}
