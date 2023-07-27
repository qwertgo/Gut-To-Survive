using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    [SerializeField] ParticleSystem particles;
    ParticleSystem.EmissionModule em;

    private void Start()
    {
        //particles.Pause();
        em = particles.emission;
        em.rateOverTime = 0;
        //em.SetBursts(new List<ParticleSystem.Burst>().ToArray());
        trail.enabled = false;
    }

    public void StartMe(Vector2 gravityDir)
    {
        Physics.gravity = -gravityDir * 9.81f;
        trail.enabled = true;
        em.rateOverTime = 10;
    }

    public void StopMe()
    {
        trail.enabled = false;
        em.rateOverTime = 0;
    }

}
