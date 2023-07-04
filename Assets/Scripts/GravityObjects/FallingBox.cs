using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingBox : GravityObject
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckTransform;

    private void Start()
    {
        if (gravityChangedEvent == null)
            gravityChangedEvent = new UnityEvent();

        if (prepareGravityChangeEvent == null)
            prepareGravityChangeEvent = new UnityEvent();

        gravityChangedEvent.AddListener(ChangeGravity);
        prepareGravityChangeEvent.AddListener(PrepareGravityChange);

        rb.mass = transform.localScale.x;
        currentState = State.drop;
    }

    private void FixedUpdate()
    {
        if(currentState == State.drop)
        {
            Drop();
        }
    }

    void PrepareGravityChange()
    {
        rb.velocity = Vector2.zero;
    }

    void ChangeGravity()
    {
        timeSinceStartedDropping = 0;
        float newRotation = Vector2.SignedAngle(Vector2.down, gravityDirection);

        Vector2 colliderPos = (Vector2)transform.position + gravityDirection * (transform.localScale.x / 2 + .3f);
        Collider2D collidedWith = Physics2D.OverlapBox(colliderPos, new Vector2(transform.lossyScale.x - .1f, .25f), newRotation);

        if (collidedWith != null && LayerIsInMask(collidedWith.gameObject.layer, groundLayer) && !LayerIsInMask(collidedWith.gameObject.layer, 8))
            currentState = State.idle;
        else
            currentState = State.drop;

        Vector3 rot = groundCheckTransform.eulerAngles;
        groundCheckTransform.eulerAngles = new Vector3(rot.x, rot.y, newRotation);
    }

    void Drop()
    {
        float t = timeSinceStartedDropping + standartJumpLength;
        float dropVelocity = CalculateVelocityGraph(t);
        rb.velocity = dropVelocity * gravityDirection;

        timeSinceStartedDropping += Time.fixedDeltaTime;
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
