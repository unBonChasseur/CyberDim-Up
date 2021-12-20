using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Boss : Entity
{
    [SerializeField]
    private float m_Speed = 5f;
    [SerializeField]
    private float m_HPCurrent;

    [SerializeField]
    private Camera m_MainCamera;
    private Vector3 m_CurrentPosition;


    [Header("Bullet")]
    [SerializeField]
    private float m_FireRateDelay = 5000f;
    [SerializeField]
    private GameObject m_PrefabFire;
    private Stopwatch m_StopWatchBullet;

    void Awake()
    {
        DeplacementInitial();

        m_StopWatchBullet = new Stopwatch();
        m_StopWatchBullet.Start();

        if(GameManager.current.GetNiveau()>=1)
            m_HPCurrent = m_HPMax;

    }
    // Start is called before the first frame update
    void Start()
    {
        m_HPCurrent = m_HPMax;
    }

    // Update is called once per frame
    void Update()
    {
        //m_CurrentPosition = m_MainCamera.WorldToScreenPoint(transform.position);

        //transform.position += new Vector3(0,0, m_Speed * Time.deltaTime);

        if (m_StopWatchBullet.ElapsedMilliseconds >= m_FireRateDelay)
        {
            GameObject FireRight = Instantiate(m_PrefabFire, transform.position, new Quaternion(0, 90, 90, 1));
            m_StopWatchBullet.Restart();
        }

        //if (m_CurrentPosition.y > m_DeathDist)
        //    Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            
            if (m_HPCurrent > 0)
            {
                m_HPCurrent -= 0.1f;
            }
            else 
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void DeplacementInitial()
    {

    }

}
