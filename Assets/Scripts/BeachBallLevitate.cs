using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BeachBallLevitate : MonoBehaviour
{
    private Vector3 startPosition;

    private float curYPos = 0.0f;
    private float curZRot = 0.0f;
    private float curXSca = 0.0f;
    private float curYSca = 0.0f;

    public float scale = 1.0f;
    public float amplitude = 1.0f;
    public float rotationSpeed = 50;

    public bool isMoving = true;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        StartCoroutine(LevitateBall());

    }

    private void Update()
    {
        if (isMoving == true)
            timer += Time.deltaTime;
    }

    IEnumerator LevitateBall()
    {
        while (true)
        {
            if (isMoving)
            {

                yield return null;

                curYPos = Mathf.PingPong(timer, amplitude) - amplitude * 0.5f;
                transform.position = new Vector3(startPosition.x,
                    startPosition.y + curYPos,
                    startPosition.z);

                curXSca = Mathf.PingPong(timer, scale);
                curYSca = Mathf.PingPong(timer, scale);

                transform.localScale = new Vector3(curXSca, curYSca, 0);

                curZRot += rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, curZRot);

                if (timer >= 2f)
                {
                    isMoving = false;
                    timer = 0;
                }
                    
            }
            else
            {
                yield return new WaitForSeconds(3);
                isMoving = true;
            }
        }
    }
}
