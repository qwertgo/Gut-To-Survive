using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walkSound;

    public void WalkSound()
    {
        audioSource.clip = walkSound;
        audioSource.volume = Random.Range(0.7f, 0.9f);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }
}
