using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BallComponentTransform : MonoBehaviour
{
    Vector3 newScale = new Vector3(1, 1, 1);
    private float scallingSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        ScallingObject();
    }

    void ScallingObject()
    {
        if (transform.localScale.x >= 3.0f)
            return;

        transform.localScale += newScale * Time.deltaTime * scallingSpeed;
    }
}
