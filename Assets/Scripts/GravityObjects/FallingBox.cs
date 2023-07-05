using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingBox : GravityObject
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] BoxCollider2D mainCollider;
    [SerializeField] BoxCollider2D groundCollider;

    Vector2 savedPosition;

    bool isSleeping = false;

    private void Start()
    {

        GameEvents.gravityChangedEvent.AddListener(ChangeGravity);
        GameEvents.prepareGravityChangeEvent.AddListener(PrepareGravityChange);
        GameEvents.HitSavePoint.AddListener(SaveCurrentState);
        GameEvents.Respawn.AddListener(Respawn);

        rb.mass = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            rb.velocity = Vector2.zero;
            timeSinceStartedDropping = 0;
        }

        if (isSleeping)
            return;

        Drop();

    }

    void PrepareGravityChange()
    {
        isSleeping = true;
    }

    void ChangeGravity()
    {
        timeSinceStartedDropping = 0;
        float newRotation = Vector2.SignedAngle(Vector2.down, gravityDirection);

        Vector3 rot = groundCheckTransform.eulerAngles;
        groundCheckTransform.eulerAngles = new Vector3(rot.x, rot.y, newRotation);
        isSleeping = false;
    }

    void Drop()
    {
        float t = timeSinceStartedDropping + standartJumpLength;
        float dropVelocity = CalculateVelocityGraph(t);
        rb.velocity = dropVelocity * gravityDirection;

        timeSinceStartedDropping += Time.fixedDeltaTime;
    }

    bool IsGrounded()
    {
        return mainCollider.IsTouchingLayers(groundLayer) && groundCollider.IsTouchingLayers(groundLayer);

    }

    void SaveCurrentState()
    {
        savedPosition = transform.position;
    }

    void Respawn()
    {
        transform.position = savedPosition;
        isSleeping = false;
        timeSinceStartedDropping = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag != "Player")
            return;

        PlayerController p = obj.GetComponent<PlayerController>();
        if (p.isGrounded)
            p.Die();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(LayerIsInMask(collision.gameObject.layer, groundLayer))
        {
            rb.velocity = Vector2.zero;
            currentState = State.idle;
        }
    }
}
