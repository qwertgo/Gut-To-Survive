using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static MathExtention;

public class Lights : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] Light2D l;

    [SerializeField] bool turnedOff = false;

    private void Start()
    {
        GameEvents.gravityChangedEvent.AddListener(ChangedGravity);
        GameEvents.Respawn.AddListener(ChangedGravity);
    }

    void ChangedGravity()
    {
        if (turnedOff)
            return;

        float ownAgnle = GetOwnAngle();
        float gravAngle = Modulo(GravityObject.gravityAngle, 360);

        if (AreCloseTogether(ownAgnle, gravAngle, 5))
            l.intensity = 1;
        else
            l.intensity = 0;
    }

    float GetOwnAngle()
    {
        float angle = Vector2.SignedAngle(Vector2.down, direction);
        return Modulo(angle, 360);
    }

    public void TurnOn(float fadeSpeed)
    {
        StopAllCoroutines();
        StartCoroutine(TournOnCoroutine(fadeSpeed));
    }


    IEnumerator TournOnCoroutine(float speed)
    {
        float ownAngle = GetOwnAngle();
        float gravAngle = Modulo(GravityObject.gravityAngle, 360);

        if (!AreCloseTogether(ownAngle, gravAngle, 5))
            yield break;

        while (l.intensity < 1)
        {
            l.intensity += speed * Time.deltaTime;

                yield return null;
        }

        l.intensity = 1;
        turnedOff = false;
    }

    public void TurnOff(float speed)
    {
        StopAllCoroutines();
        StartCoroutine(TurnOffCoroutine(speed));
    }

    IEnumerator TurnOffCoroutine(float speed)
    {
        while (l.intensity > 0)
        {
            l.intensity -= speed * Time.deltaTime;

            yield return null;
        }

        l.intensity = 0;
        turnedOff = true;
    }

    
}
