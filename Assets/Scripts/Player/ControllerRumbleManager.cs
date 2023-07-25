using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerRumbleManager : MonoBehaviour
{
    static bool stopCurrentRumble = false;
    static bool limitedRumble;
    static bool unlimitedRumble;

    static ControllerRumbleManager rumbleManager;

    private void Start()
    {
        if (rumbleManager == null)
            rumbleManager = this;
    }

    private void OnDestroy()
    {
        if(Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
    }

    public static void StartRumble(float lowFrequency, float highFrequency)
    {
        if (Gamepad.current == null)
            return;

        Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
        stopCurrentRumble = true;

        rumbleManager.StartCoroutine(rumbleManager.UnTimedRumble(lowFrequency, highFrequency));
    }

    public static void StopRumble()
    {
        if (Gamepad.current == null)
            return;

        rumbleManager.StopAllCoroutines();
        Gamepad.current.SetMotorSpeeds(0, 0);
        stopCurrentRumble = true;
    }

    public static void StartTimedRumble(float lowFrequency, float highFrequency, float duration)
    {
        if (Gamepad.current == null)
            return;

        stopCurrentRumble = false;
        rumbleManager.StartCoroutine(rumbleManager.TimedRumble(lowFrequency, highFrequency, duration));
    }

    IEnumerator TimedRumble(float lowFrequency, float highFrequency, float duration)
    {
        Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
        float elapsedTime = 0;
        while(elapsedTime < duration)
        {
            if (stopCurrentRumble)
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return 0;
        }
        Gamepad.current.SetMotorSpeeds(0, 0);
    }

    IEnumerator UnTimedRumble(float lowFrequency,float highFrequency)
    {
        yield return 0;
        stopCurrentRumble = false;

        while (!stopCurrentRumble)
        {
            if (Gamepad.current == null)
                yield break;

            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
            yield return 0;
        }
        Gamepad.current.SetMotorSpeeds(0, 0);
    }
}
