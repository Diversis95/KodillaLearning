using UnityEngine;

public class InteractiveComponent : MonoBehaviour, IRestartableObject
{
    protected Vector3 startPosition;
    protected Quaternion startRotation;
    protected new Rigidbody2D rigidbody;
    protected AudioSource audioSource;
    protected ParticleSystem particle;

    protected virtual void Awake()
    {
        GameplayManager.onGamePaused += DoPause;
        GameplayManager.onGamePlaying += DoPlay;
    }

    public virtual void DoRestart()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        rigidbody.simulated = true;

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0.0f;
    }

    protected virtual void DoPlay()
    {
        rigidbody.simulated = true;
    }

    protected virtual void DoPause()
    {
        rigidbody.simulated = false;
    }

    protected virtual void OnDestroy()
    {
        GameplayManager.onGamePaused -= DoPause;
        GameplayManager.onGamePlaying -= DoPlay;
    }
}
