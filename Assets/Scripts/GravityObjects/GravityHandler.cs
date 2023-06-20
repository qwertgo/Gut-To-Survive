using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityHandler : MonoBehaviour
{
    public bool motionSicknessSafeMode = false;
    [SerializeField] CameraController cam;
    [SerializeField] Transform camTransform;

    UnityEvent gravityChangedEvent;
    PlayerController player;


    public void StartGravityChange(PlayerController player, float camRotation, UnityEvent gravityChangedEvent, Rigidbody2D playerRb )
    {
        if (camRotation == camTransform.eulerAngles.z)
            return;


       

        if (!motionSicknessSafeMode)
        {
            player.enabled = false;
            player.isSleeping = true;
            playerRb.velocity = Vector2.zero;

            this.gravityChangedEvent = gravityChangedEvent;
            this.player = player;

            StartCoroutine(cam.ChangeRotationOverTime(camRotation));
        }
        else
        {
            gravityChangedEvent.Invoke();
        }
    }

    public void CameraFinishedRotating()
    {
        player.enabled = true;
        gravityChangedEvent.Invoke();
    }
}
