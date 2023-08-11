using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] bool playRunningSounds;
    [SerializeField] List<AudioClip> walk;
    [SerializeField] List<AudioClip> die;
    [SerializeField] List<AudioClip> jump;
    [SerializeField] List<AudioClip> landing;
    [SerializeField] AudioClip P_Polarity;
    [SerializeField] AudioClip N_Polarity;

    

    public enum PlayerSound {Walk, Die, Jump, Landing, P_Polarity, N_Polarity};
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(PlayerSound sound, float volume, bool playOneShot)
    {
        if (enabled == false || sound <= PlayerSound.Landing)
            return;

        StopAllCoroutines();
        switch (sound)
        {
            case PlayerSound.Walk:
                StartCoroutine(PlayOnLoop(walk, .5f, .1f,volume, .25f));
                break;

            case PlayerSound.Die:
                Play(die, 1,.1f, volume, .2f, playOneShot);
                break;

            case PlayerSound.Jump:
                Play(jump, 1, .1f, volume, .2f, playOneShot);
                break;

            case PlayerSound.Landing:
                Play(landing, 1, .1f, volume, .2f, playOneShot);
                break;

            case PlayerSound.P_Polarity:
                source.clip = P_Polarity;
                source.pitch = 1;
                source.volume = volume;
                source.Play();
                break;

            case PlayerSound.N_Polarity:
                source.clip = N_Polarity;
                source.pitch = 1;
                source.volume = volume;
                source.Play();
                break;
        }
    }

    void Play(List<AudioClip> list, float pitchBase, float pitchRandom, float volumeBase, float volumeRandom, bool PlayOneShot)
    {
        

        if (PlayOneShot)
        {
            AudioClip clip = list[Random.Range(0, list.Count)];
            float volume = Random.Range(volumeBase - volumeRandom, volumeBase);
            source.PlayOneShot(clip, volume);
        }
        else
        {
            source.clip = list[Random.Range(0, list.Count)];
            source.pitch = Random.Range(pitchBase - pitchRandom, pitchBase + pitchRandom);
            source.volume = Random.Range(volumeBase - volumeRandom, volumeBase);
            source.Play();
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
        if (enabled == false)
            return;

        StopAllCoroutines();
        source.Stop();
    }

   
}
