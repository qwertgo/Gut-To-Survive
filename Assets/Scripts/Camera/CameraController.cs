using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [Range(0,20)] public float upwardOffset;


    Vector2 gravityDirection = Vector2.down;
    float savedRotation;

    private void Start()
    {
        GameEvents.HitSavePoint.AddListener(SaveCurrentState);
        GameEvents.Respawn.AddListener(Respawn);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = -gravityDirection * upwardOffset;

        Vector3 playerPos = playerTransform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z) + offset;
    }

    void SaveCurrentState()
    {
        savedRotation = GravityObject.gravityAngle;
    }

    void Respawn()
    {
        transform.eulerAngles = new Vector3(0, 0, savedRotation);
        gravityDirection = Quaternion.Euler(0,0, savedRotation) * Vector2.down;

        Update();
    }


}
