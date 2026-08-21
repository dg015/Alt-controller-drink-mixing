using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    ParticleSystem m_particleSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get variables
        m_particleSystem = gameObject.GetComponent<ParticleSystem>();
        float totalDuration = m_particleSystem.main.duration + m_particleSystem.main.startLifetime.constant;

        Destroy(gameObject, totalDuration);
    }
}
