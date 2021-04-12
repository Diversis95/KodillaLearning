using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BallComponentTransform : MonoBehaviour
{
    Vector3 parametersToScale = Vector3.one;
    private float scalingSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x >= 3.0f)
            return;

        ScaleObject();
    }

    void ScaleObject()
    {
        transform.localScale += parametersToScale * Time.deltaTime * scalingSpeed;
    }
}
