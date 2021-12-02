using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePlatform : MonoBehaviour
{
    public float force = 20f;
    public bool zeroOutVelocity = true;
    public bool singleUse = false;

    [Header("TriggerAudio")]
    public AudioSource aud;
    public AudioClip forceClip;
    [Range(0f,1f)]
    public float endVolume = .5f;

    void OnDrawGizmoSelected()
    {
        Gizmos.color = Color.green;
        Vector3 direction = transform.TransformDirection(Vector3.up) * force * .5f;
        Gizmos.DrawRay(transform.position, direction);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            aud.PlayOneShot(forceClip);

            // get the rigidbody component of the player
            Rigidbody rb = other.GetComponent<Rigidbody>();

            // stop the players movement
            if(zeroOutVelocity) rb.velocity = Vector3.zero;

            // push the player up relative to the direction of the platform
            rb.AddForce(transform.up * force, ForceMode.Impulse);

            // destroy this after use
            if(singleUse) Destroy(this.gameObject);
        }
    }
}
