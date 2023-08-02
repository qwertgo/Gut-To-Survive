using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraViewFinder : GravityObject
{
    public float upwardOffset;
    public float horizontalOffset = 1;

    public bool isInMenuScene = false;

    [SerializeField] Transform player;

    [SerializeField] Material materialFront;
    [SerializeField] Material materialMiddle;
    [SerializeField] Transform lights;

    [SerializeField, Range(0, 1)] float offsetAmountFront;
    [SerializeField, Range(0, 1)] float offsetAmountMiddle;
    [SerializeField, Range(0, 1)] float offsetAmountLights;

    Vector3 startPos;

    private void Start()
    {
        startPos = (Vector2)player.transform.position + -gravityDirection * upwardOffset; 
    }


    void Update()
    {
        if(isInMenuScene)
            transform.position = new Vector2(horizontalOffset, transform.position.y);
        else
            transform.position = (Vector2)player.transform.position + -gravityDirection * upwardOffset;


        materialFront.SetVector("_Offset", new Vector4(-transform.position.x * offsetAmountFront, -transform.position.y * offsetAmountFront));
        materialMiddle.SetVector("_Offset", new Vector4(-transform.position.x * offsetAmountMiddle, -transform.position.y * offsetAmountMiddle));

        Vector2 tmp = transform.position - startPos;
        lights.position = tmp * offsetAmountLights;

    }

    private void OnDestroy()
    {
        materialFront.SetVector("_Offset", new Vector4(0, 0, 0, 0));
        materialMiddle.SetVector("_Offset", new Vector4(0, 0, 0, 0));
    }
}
