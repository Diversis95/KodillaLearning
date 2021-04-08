using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponentTransform : MonoBehaviour
{
    public float transformingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one;

        transformingSpeed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x < 3.0f && transform.localScale.y < 3.0f && transform.localScale.z < 3.0f)
        {
            transform.localScale += Vector3.right * Time.deltaTime * transformingSpeed;
            transform.localScale += Vector3.up * Time.deltaTime * transformingSpeed;
            transform.localScale += Vector3.forward * Time.deltaTime * transformingSpeed;
        }
    }
}
