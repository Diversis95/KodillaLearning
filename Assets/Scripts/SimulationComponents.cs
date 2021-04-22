 using UnityEngine;

public class SimulationComponents : Singleton<GameplayManager>
{
    new Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    void OnMouseUp()
    {
        rigidbody.simulated = true;
    }

    void OnMouseDrag()
    {
        rigidbody.simulated = false;

        if (GameplayManager.Instance.pause)
            return;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPos.x, worldPos.y, 0);
    }

    public bool IsSimulated()
    {
        return rigidbody.simulated;
    }

    public float Speed()
    {
        float physicsSpeed = rigidbody.velocity.magnitude;

        return physicsSpeed;
    }
}