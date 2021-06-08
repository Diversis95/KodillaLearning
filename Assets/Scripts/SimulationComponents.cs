using System.Collections;
using UnityEngine;

public class SimulationComponents : InteractiveComponent
{
    private Rigidbody2D connectedBodies;
    private SpringJoint2D connectedJoints;
    private TrailRenderer trailRenderer;
    public CameraController cameraController;
    private Animator animator;
    public HiddenBallManager hiddenBall;
    public GameSettingsDatabase gameDatabase;

    public float slingStart = 0.5f;
    public float maxSpringDistance = 2.5f;

    public bool isFlying = false;
    public bool hittedTheGround = false;

    private Vector3 slingshotArms;

    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody2D>();
        connectedJoints = GetComponent<SpringJoint2D>();
        connectedBodies = connectedJoints.connectedBody;
        audioSource = GetComponent<AudioSource>();
        trailRenderer = GetComponent<TrailRenderer>();
        animator = GetComponentInChildren<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();

        slingshotArms = new Vector3(0.4f, 0, 0);

        startPosition = transform.position;
        startRotation = transform.rotation;

        StartCoroutine(CheckJointsConnection());
    }

    IEnumerator CheckJointsConnection()
    {
        while(true)
        {
            yield return null;
            yield return null;

            if (transform.position.x > connectedBodies.transform.position.x + slingStart)
            {
                connectedJoints.enabled = false;
            }

            trailRenderer.enabled = !hittedTheGround;
        }
    }

    private void OnMouseDown()
    {
        if (hittedTheGround || isFlying)
            return;

        audioSource.PlayOneShot(gameDatabase.ballPullSound);
    }

    void OnMouseDrag()
    {
        if (hittedTheGround || isFlying)
            return;

        rigidbody.simulated = false;
        hittedTheGround = false;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 newBallPos = new Vector3(worldPos.x, worldPos.y);

        float curJointDistance = Vector3.Distance(newBallPos, connectedBodies.transform.position);

        if (curJointDistance > maxSpringDistance)
        {
            Vector2 direction = (newBallPos - connectedBodies.position).normalized;
            transform.position = connectedBodies.position + direction * maxSpringDistance;
        }
        else
            transform.position = newBallPos;
    }

    void OnMouseUp()
    {
        if (hittedTheGround || isFlying)
            return;

        isFlying = true;

        rigidbody.simulated = true;

        audioSource.PlayOneShot(gameDatabase.ballShootSound);

        particle.Play();
    }

    public bool IsSimulated()
    {
        return rigidbody.simulated;
    }

    public float PhysicsSpeed => rigidbody.velocity.magnitude;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            hittedTheGround = true;
        }
        animator.enabled = true;
        animator.Play(0);

        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
            GameplayManager.Instance.Points += 1;
    }

    public override void DoRestart()
    {
        base.DoRestart();

        connectedJoints.enabled = true;
        trailRenderer.enabled = true;
        hittedTheGround = false;
        isFlying = false;
        hiddenBall.SetLineRendererPoints();

        cameraController.ResetCamera();
    }
}