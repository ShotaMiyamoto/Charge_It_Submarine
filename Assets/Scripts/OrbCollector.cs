using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(Rigidbody))]
public class OrbCollector : MonoBehaviour
{
    [SerializeField] Transform m_target;
    [SerializeField] float m_forceMultiplier = 10f;
    [SerializeField] float m_distanceForDestruction = 0.1f;
    ParticleSystem m_particle;
    Rigidbody m_rb;
    float m_timer;

    private void Awake()
    {
        m_particle = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();

        if (!m_target)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player)
            {
                m_target = player.transform;
            }
            else
            { 
                Debug.LogWarning("target for orb is not found.");
            }
        }
    }
    
    void Update()
    {
        if (m_particle.isPlaying)
        {
            m_timer += Time.deltaTime;
        }
        else
        {
            return;
        }

        if (m_timer > m_particle.main.duration / 2)
        {
            if (m_target)
            {
                Vector3 v = m_target.transform.position - transform.position;
                v.Normalize();
                float distance = Vector3.Distance(m_target.transform.position, transform.position);

                if (distance < m_distanceForDestruction)
                {
                    Destroy(gameObject);
                }
                else
                {
                    m_rb.AddForce(v * distance * distance * m_forceMultiplier);
                }
            }
        }
    }

    public void Emit(int countOfOrbs)
    {
        ParticleSystem.Burst burst = m_particle.emission.GetBurst(0);
        burst.count = new ParticleSystem.MinMaxCurve((float)countOfOrbs);
        m_particle.emission.SetBurst(0, burst);
        m_particle.Play();
    }
}
