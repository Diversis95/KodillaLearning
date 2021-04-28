using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private ParticleSystem particle;
    private AudioSource audioSource;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponentInChildren<AudioSource>();
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
