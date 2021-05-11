using UnityEngine;

public class Target : InteractiveComponent
{
    public GameSettingsDatabase gameDatabase;

    protected override void Awake()
    {
        base.Awake();

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
            audioSource.PlayOneShot(gameDatabase.targetHitSound);

            GameObject.Destroy(this.gameObject, 1.0f);
        }
    }
}
