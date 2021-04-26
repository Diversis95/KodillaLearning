using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBallManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Rigidbody2D connectedBodies;
    private SpringJoint2D connectedJoints;
    public GameObject ball;
    public SimulationComponents ballComponents;

    public float maxSpringDistance = 2.5f;

    private Vector3 slingshotArms;
    Vector2 currentPosition;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        connectedJoints = GetComponent<SpringJoint2D>();
        connectedBodies = connectedJoints.connectedBody;

        slingshotArms = new Vector3(0.4f, 0, 0);
    }

    private void Update()
    {
        if (!ballComponents.isFlying)
            currentPosition = ball.transform.position;
        else
            currentPosition = transform.position;

        SetMaxDistance();
        SetLineRendererPoints();
    }

    public void SetLineRendererPoints()
    {
        Vector3 backgroundArm = connectedBodies.transform.position + slingshotArms;
        Vector3 foregroundArm = connectedBodies.transform.position - slingshotArms;

        lineRenderer.positionCount = 3;
        lineRenderer.SetPositions(new Vector3[] {
            backgroundArm,
            currentPosition,
            foregroundArm});
    }

    void SetMaxDistance()
    {
        float curJointDistance = Vector3.Distance(transform.position, connectedBodies.transform.position);

        if (curJointDistance > maxSpringDistance)
        {
            Vector2 direction = (currentPosition - connectedBodies.position).normalized;
            transform.position = connectedBodies.position + direction * maxSpringDistance;
        }
        else
            transform.position = currentPosition;
    }
}
