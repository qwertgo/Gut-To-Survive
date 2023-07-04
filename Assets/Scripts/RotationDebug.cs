using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDebug : MonoBehaviour
{
    [SerializeField] Transform child;


    // Update is called once per frame
    void Update()
    {
        Vector2 angleVec = child.position - transform.position;
        Debug.Log($"Angle: {Vector2.Angle(Vector2.down, angleVec)}, Signed Angle: {Vector2.SignedAngle(Vector2.down, angleVec)}");

        transform.eulerAngles = new Vector3(0, 0, Time.time * 100);
    }
}
