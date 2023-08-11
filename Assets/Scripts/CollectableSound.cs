using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CollectableSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    public void Play(Vector3 position)
    {
        transform.position = position;
        audioSource.PlayOneShot(audioClip);
    }
}
