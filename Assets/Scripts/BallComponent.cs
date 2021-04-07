using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    private int frame;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        frame++;
        if(timer > 1)
        {
            Debug.Log("Frames per seconds: " + frame);
            frame = 0;
            timer = 0;
        }
        
    }
}
