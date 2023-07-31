using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] List<AudioClip> walk;
    [SerializeField] List<AudioClip> die;

    public enum PlayerSound {Walk, Die, Jump  };
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(PlayerSound sound, float volume)
    {
        StopAllCoroutines();
        switch (sound)
        {
            case PlayerSound.Walk:
                StartCoroutine(PlayOnLoop(walk, .5f, .1f,volume, .25f));
                break;
            case PlayerSound.Die:
                source.clip = die[Random.Range(0, die.Count)];
                source.pitch = Random.Range(0.9f, 1.1f);
                source.volume = Random.Range(volume - .2f, volume);
                source.Play();
                break;
        }
    }

    IEnumerator PlayOnLoop(List<AudioClip> soundclips, float delay, float pitchVariation, float volumeBase, float volumeVaritation)
    {
        while (true)
        {
            if (source.isPlaying)
                yield return null;

            source.clip = soundclips[Random.Range(0, soundclips.Count)];
            source.pitch = Random.Range(1 - pitchVariation, 1 + pitchVariation);
            source.volume = Random.Range(volumeBase - volumeVaritation, volumeBase);
            source.Play();
            yield return new WaitForSeconds(delay);
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
        source.Stop();
    }

   
}
