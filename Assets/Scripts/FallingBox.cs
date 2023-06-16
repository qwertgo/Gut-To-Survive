using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingBox : GravityObject
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float width;
    [SerializeField] LayerMask groundLayer;


    private void Start()
    {
        if (gravityChangedEvent == null)
            gravityChangedEvent = new UnityEvent();

        gravityChangedEvent.AddListener(ChangeGravity);
    }

    private void Awake()
    {
        currentState = State.drop;

    }

    private void FixedUpdate()
    {
        if(currentState == State.drop)
        {
            Drop();
        }
    }

    void ChangeGravity()
    {
        currentState = State.drop;
        timeSinceStartedDropping = 0;
    }

    void Drop()
    {
        float t = timeSinceStartedDropping + standartJumpLength;
        float dropVelocity = CalculateVelocityGraph(t);
        rb.velocity = dropVelocity * gravityDirection;
        timeSinceStartedDropping += Time.fixedDeltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 pos = transform.position;
        Vector2 downwards = pos + gravityDirection * width;

        bool hasWallBelow = Physics2D.OverlapCircle(downwards, .25f, groundLayer);
        if (collision.gameObject.layer == 3 && hasWallBelow) 
        {
            rb.velocity = Vector2.zero;
            currentState = State.idle;
        }
    }
}
