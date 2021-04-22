using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private SimulationComponents followedTarget;

    private Vector3 originalPosition;

    private void Start()
    {
        followedTarget = FindObjectOfType<SimulationComponents>();

        originalPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (!followedTarget.IsSimulated())
            return;

        /*transform.position = followedTarget.transform.position + originalPosition;*/


        transform.position = Vector3.MoveTowards(transform.position,
            followedTarget.transform.position + originalPosition,
            followedTarget.Speed() * Time.deltaTime);
    }
}
