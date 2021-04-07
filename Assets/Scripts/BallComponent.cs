using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    private int frame; //variable that shows counted frames
    private float timer; //variable that count seconds

    // Start is called before the first frame update
    void Start()
    {
        frame = 0; //setting frames to zero
        timer = 0; //setting timer to zero
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; //timer counting every second
        frame++; //adding every frame
        if(timer > 1) //if timer exceed one second
        {
            Debug.Log("Frames per seconds: " + frame); //shows FPS
            frame = 0; //setting frames to zero again
            timer = 0; //setting timer to zero again
        }
        
    }
}
