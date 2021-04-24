 using UnityEngine;

public class SimulationComponents : Singleton<GameplayManager>
{
    new Rigidbody2D rigidbody;
    private SpringJoint2D connectedJoints;
    private Rigidbody2D connectedBodies;
    private LineRenderer lineRenderer;
    private TrailRenderer trailRenderer;

    public float slingStart = 0.5f;
    public float maxSpringDistance = 2.5f;

    private bool hittedTheGround = false;

    private Vector3 slingshotArms;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        connectedJoints = GetComponent<SpringJoint2D>();
        connectedBodies = connectedJoints.connectedBody;
        lineRenderer = GetComponent<LineRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();

        slingshotArms = new Vector3(0.5f, 0 , 0);

        trailRenderer.enabled = false;
    }

    void Update()
    {
        if(transform.position.x > connectedBodies.transform.position.x + slingStart)
        {
            connectedJoints.enabled = false;
            lineRenderer.enabled = false;
        }
    }

    void OnMouseUp()
    {
        rigidbody.simulated = true;
        trailRenderer.enabled = true;
    }

    void OnMouseDrag()
    {
        rigidbody.simulated = false;
        hittedTheGround = false;

        if (GameplayManager.Instance.pause)
            return;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 newBallPos = new Vector3(worldPos.x, worldPos.y);

        Vector3 backgroundArm = connectedBodies.transform.position + slingshotArms;
        Vector3 foregroundArm = connectedBodies.transform.position - slingshotArms;

        float curJointDistance = Vector3.Distance(newBallPos, connectedBodies.transform.position);

        lineRenderer.positionCount = 3;
        lineRenderer.SetPositions(new Vector3[] { 
            backgroundArm, 
            transform.position,
            foregroundArm});

        if (curJointDistance > maxSpringDistance)
        {
            Vector2 direction = (newBallPos - connectedBodies.position).normalized;
            transform.position = connectedBodies.position + direction * maxSpringDistance;
        }
        else
            transform.position = newBallPos;
    }

    public bool IsSimulated()
    {
        return rigidbody.simulated;
    }

    public float PhysicsSpeed => rigidbody.velocity.magnitude;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            hittedTheGround = true;
        }
    }
}