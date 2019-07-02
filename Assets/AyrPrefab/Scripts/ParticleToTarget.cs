using UnityEngine;
using System.Collections;

public class ParticleToTarget : MonoBehaviour
{
    public Vector3 particleHitLocation;

    private ParticleSystem wallParticleSystem;

    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];

    int count;

    void Start()
    {
        if (wallParticleSystem == null)
            wallParticleSystem = GetComponent<ParticleSystem>();

        if (wallParticleSystem == null)
        {
            this.enabled = false;
        }
        else
        {
            wallParticleSystem.Stop();
        }
    }
    void Update()
    {

        count = wallParticleSystem.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            ParticleSystem.Particle particle = particles[i];

            Vector3 v1 = wallParticleSystem.transform.TransformPoint(particle.position);
            Vector3 v2 = particleHitLocation;

            Vector3 tarPosi = (v2 - v1) * (particle.remainingLifetime / particle.startLifetime);
            particle.position = wallParticleSystem.transform.InverseTransformPoint(v2 - tarPosi);
            particles[i] = particle;
        }

        wallParticleSystem.SetParticles(particles, count);
    }

    public void StartParticleEffects(Vector3 target)
    {
        particleHitLocation = target;
        wallParticleSystem.Play();
    }

    public void StopParticleEffects()
    {
        wallParticleSystem.Stop();
    }

    public void UpdateParticleTargetLocation(Vector3 location)
    {
        particleHitLocation = location;
    }
}