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

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        curYPos = Mathf.PingPong(Time.time, amplitude) - amplitude * 0.5f;
        transform.position = new Vector3(startPosition.x,
            startPosition.y + curYPos,
            startPosition.z);

        curXSca = Mathf.PingPong(Time.time, scale);
        curYSca = Mathf.PingPong(Time.time, scale);

        transform.localScale = new Vector3(curXSca, curYSca, 0);

        curZRot += Time.deltaTime + rotationSpeed;
        transform.rotation = Quaternion.Euler(0, 0, curZRot);
    }
}
