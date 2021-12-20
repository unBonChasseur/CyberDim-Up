using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_movementSpeed;

    [SerializeField]
    private Camera m_mainCamera;

    [SerializeField]
    private float m_maxRange = 1075;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, 0, m_movementSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.z) >= m_maxRange)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.current.AddScore(10);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bullet_enemy")
        {
            GameManager.current.AddScore(2);
        }
        if (collision.gameObject.tag == "Boss")
        {
            GameManager.current.AddScore(20);
            Destroy(gameObject);
        }
    }
}
