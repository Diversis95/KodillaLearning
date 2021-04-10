using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    private int frames;
    private float timer;
    Queue<int> tenFrames = new Queue<int>();
    private int secondsPassed;
    private int avarageFramerate;

    

    // Start is called before the first frame update
    void Start()
    {
        frames = 0;
        timer = 0;
        avarageFramerate = 0;
        secondsPassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        frames++;

        CheckIfSecondPass();
    }

    void CheckIfSecondPass()
    {
        if (timer > 1)
        {
            if (secondsPassed > 10) return;

            secondsPassed++;
            tenFrames.Enqueue(frames);

            avarageFramerate = (int)tenFrames.Average();

            Debug.Log(avarageFramerate);

            timer = 0;
            frames = 0;
        }
        
    }
}
