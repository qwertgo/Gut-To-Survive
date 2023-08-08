using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGravityOnLevelEnd : MonoBehaviour
{

    [SerializeField] GravityHandler handler;
    [SerializeField] PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(11))
        {
            Vector2 gravityDir = new Vector2(1, 0);
            float angle = Vector2.SignedAngle(Vector2.down, gravityDir);
            handler.StartGravityChange(angle, gravityDir);
            ControllerRumbleManager.StopRumble();
            playerController.Disable(true);
        }
    }
}
