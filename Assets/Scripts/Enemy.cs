using System.Collections;
using System.Collections.Generic;
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

    void Awake()
    {
        current_hp = 4;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_currentPosition = m_mainCamera.WorldToScreenPoint(transform.position);

        transform.position += new Vector3(0,-vitesse * Time.deltaTime, 0);

        if (m_currentPosition.y < m_deathDist)
            Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        float hp_max = current_hp;
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
