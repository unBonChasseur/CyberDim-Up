using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    private float vitesse = 2f;

    [SerializeField]
    private Camera m_mainCamera;

    private Vector3 m_currentPosition;

    [SerializeField]
    private float m_deathDist;

    [Header("Bullet")]
    [SerializeField]
    private float m_fireRateDelay;
    [SerializeField]
    private GameObject m_PrefabFire;
    private Stopwatch m_FireStopWatch;

    public float hp_max;

    void Awake()
    {
        m_FireStopWatch = new Stopwatch();
        m_FireStopWatch.Start();
        current_hp = 4;
    }
    // Start is called before the first frame update
    void Start()
    {
        hp_max = current_hp;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentPosition = m_mainCamera.WorldToScreenPoint(transform.position);

        transform.position += new Vector3(0,0, vitesse * Time.deltaTime);

        if (m_FireStopWatch.ElapsedMilliseconds >= m_fireRateDelay)
        {
            GameObject FireRight = Instantiate(m_PrefabFire, transform.position, new Quaternion(90, 0, 0, 1));
            m_FireStopWatch.Restart();
        }

        if (m_currentPosition.y > m_deathDist)
            Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            for (float i = hp_max; i > 0; i--)
            {
                if (current_hp != 0)
                {
                    current_hp -= 1;
                }
                else
                    Destroy(gameObject);

            }
        }
    }

}
