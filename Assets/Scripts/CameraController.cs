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

    private void FixedUpdate()
    {
        if (!followedTarget.IsSimulated())
            return;

        transform.position = Vector3.MoveTowards(transform.position,
            followedTarget.transform.position + originalPosition,
            followedTarget.PhysicsSpeed * Time.deltaTime);
    }

    public void ResetCamera()
    {
        transform.position = followedTarget.transform.position + originalPosition;
    }
}
