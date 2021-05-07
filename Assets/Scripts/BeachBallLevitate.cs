using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        StartCoroutine(LevitateBall());

    }

    IEnumerator LevitateBall()
    {
        while (true)
        {
            if (isMoving)
            {
                yield return null;

                curYPos = Mathf.PingPong(Time.time, amplitude) - amplitude * 0.5f;
                transform.position = new Vector3(startPosition.x,
                    startPosition.y + curYPos,
                    startPosition.z);

                curXSca = Mathf.PingPong(Time.time, scale);
                curYSca = Mathf.PingPong(Time.time, scale);

                transform.localScale = new Vector3(curXSca, curYSca, 0);

                curZRot += rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, curZRot);

                if(transform.position.y <= -1.99f)
                    isMoving = false;
            }
            else
            {
                yield return new WaitForSeconds(3);
                isMoving = true;
            }
        }
    }
}
