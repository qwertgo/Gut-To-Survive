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


    public void StartGravityChange(PlayerController player, float gravityAngle, UnityEvent gravityChangedEvent, Rigidbody2D playerRb)
    {
        float cameraRotation = camTransform.eulerAngles.z;


        cameraRotation = Modulo(cameraRotation, 360);
        gravityAngle = Modulo(gravityAngle, 360);

        //
        //if (cameraRotation < 0)
        //    cameraRotation = -(-cameraRotation % 360 - 360);
        //else
        //    cameraRotation %= 360;

        //if (gravityAngle < 0)
        //    gravityAngle = -(-gravityAngle % 360 - 360);
        //else
        //    gravityAngle %= 360;


        if (Mathf.RoundToInt(gravityAngle) == Mathf.RoundToInt(cameraRotation))
            return;


        GravityObject.gravityAngle = gravityAngle;

        if (!motionSicknessSafeMode)
        {
            player.enabled = false;
            player.isSleeping = true;
            playerRb.velocity = Vector2.zero;

            this.gravityChangedEvent = gravityChangedEvent;
            this.player = player;

            StartCoroutine(cam.ChangeRotationOverTime(gravityAngle));
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

    int Modulo(int a, int n)
    {
        return ((a % n) + n) % n;
    }

    float Modulo(float a, float n)
    {
        return ((a % n) + n) % n;
    }
}
