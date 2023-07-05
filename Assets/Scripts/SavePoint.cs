using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] Color deactivatedColor;
    [SerializeField] Color activatedColor;

    [SerializeField] SpriteRenderer spriteRenderer;

    [HideInInspector] public Vector2 savedGravityDir;
    [HideInInspector] public float savedGravityAngle;
    [HideInInspector] public float  savedRotation;

    private void Start()
    {
        spriteRenderer.color = deactivatedColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            spriteRenderer.color = activatedColor;

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.lastSavePoint = this;
            savedRotation = player.transform.eulerAngles.z;

            savedGravityDir = Quaternion.Euler(0f, 0f, GravityObject.gravityAngle) * Vector2.down;
            savedGravityAngle = GravityObject.gravityAngle;

            GameEvents.HitSavePoint.Invoke();
        }
    }
}
