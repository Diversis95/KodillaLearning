using UnityEngine;

public class Target : InteractiveComponent
{
    public AudioClip hitSound;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        particle = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponentInChildren<AudioSource>();

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            particle.Play();
            audioSource.PlayOneShot(hitSound);
        }
    }

    public override void DoRestart()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0.0f;
    }
}
