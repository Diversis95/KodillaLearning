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
}
