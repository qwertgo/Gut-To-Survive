using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] Color deactivatedColor;
    [SerializeField] Color activatedColor;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float radius;

    [SerializeField] SpriteRenderer spriteRenderer;

    [HideInInspector] public Vector2 savedGravityDir;
    [HideInInspector] public float savedGravityAngle;

    bool activated = false;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Start()
    {
        spriteRenderer.color = deactivatedColor;

        Collider2D coll = Physics2D.OverlapCircle(transform.position, radius, groundLayer);
        Vector2 collisionPosition = Physics2D.ClosestPoint(transform.position, coll);

        savedGravityDir = collisionPosition - (Vector2)transform.position;
        savedGravityDir.Normalize();
        savedGravityAngle = Vector2.SignedAngle(Vector2.down, savedGravityDir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activated && collision.gameObject.tag.Equals("Player"))
        {
            spriteRenderer.color = activatedColor;
            audioSource.clip = audioClip;
            audioSource.Play();

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.lastSavePoint = this;

            GameEvents.HitSavePoint.Invoke();
            activated = true;
        }
    }
}
