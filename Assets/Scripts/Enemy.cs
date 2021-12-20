using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    private float m_Speed = 2f;
    [SerializeField]
    private float m_DeathDist = 450f;
    private float m_HPCurrent;

    [SerializeField]
    private Camera m_MainCamera;
    private Vector3 m_CurrentPosition;


    [Header("Bullet")]
    [SerializeField]
    private float m_FireRateDelay = 2000f;
    [SerializeField]
    private GameObject m_PrefabFire;
    private Stopwatch m_StopWatchBullet;

    [Header("Bonus")]
    [SerializeField]
    private GameObject m_prefabBonus;

    void Awake()
    {
        m_StopWatchBullet = new Stopwatch();
        m_StopWatchBullet.Start();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_HPCurrent = m_HPMax;
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentPosition = m_MainCamera.WorldToScreenPoint(transform.position);

        transform.position += new Vector3(0,0, m_Speed * Time.deltaTime);

        if (m_StopWatchBullet.ElapsedMilliseconds >= m_FireRateDelay)
        {
            GameObject FireRight = Instantiate(m_PrefabFire, transform.position, new Quaternion(0, 90, 90, 1));
            m_StopWatchBullet.Restart();
        }

        if (m_CurrentPosition.y > m_DeathDist)
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
            
            if (m_HPCurrent != 0)
            {
                m_HPCurrent -= 5.0f;
            }
            else
            {
                if (Random.Range(0f, 10f) < 1f)
                {
                    GameObject Bonus = Instantiate(m_prefabBonus, transform.position, new Quaternion(0, 90, 90, 1));
                }

                Destroy(gameObject);
            }
        }
    }

}
