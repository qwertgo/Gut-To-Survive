using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] SpriteRenderer r;

    private void Start()
    {
        r.enabled = false;
    }
}
