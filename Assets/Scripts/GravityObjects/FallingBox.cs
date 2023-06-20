using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingBox : GravityObject
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float width;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckTransform;



    private void Start()
    {
        if (gravityChangedEvent == null)
            gravityChangedEvent = new UnityEvent();

        gravityChangedEvent.AddListener(ChangeGravity);

        width = transform.localScale.x;
        rb.mass = transform.localScale.x;
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
        else
        {
            rb.velocity = gravityDirection;
        }
    }

    void ChangeGravity()
    {
        currentState = State.drop;
        timeSinceStartedDropping = 0;

        float rotationZ = Vector2.SignedAngle(Vector2.down, gravityDirection);
        Vector3 rot = groundCheckTransform.eulerAngles;
        groundCheckTransform.eulerAngles = new Vector3(rot.x, rot.y, rotationZ);
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
        if(obj.tag == "Player")
        {
            obj.GetComponent<PlayerController>().Die();
        }
        else if(obj.tag != "Spikes")
        {
            rb.velocity = gravityDirection;
            currentState = State.idle;
        }
    }
}
