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
    private Vector3 m_initialPosition; 
    private Vector3 m_currentPosition;

    public static event Action OnHit;


    // Start is called before the first frame update
    void Start()
    {
        m_initialPosition = m_mainCamera.WorldToScreenPoint(transform.position);

        //Debug.Log("Bullet : " + m_initialPosition + "<-- init||current -->" + m_currentPosition);
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Bullet : " + m_initialPosition + "<-- init||current -->" + m_currentPosition);

        m_currentPosition = m_mainCamera.WorldToScreenPoint(transform.position);

        transform.position -= new Vector3(0, 0, m_movementSpeed * Time.deltaTime);

        if (Mathf.Abs(m_currentPosition.x) > Mathf.Abs(2 * m_initialPosition.x))
        {
            Debug.Log("Bullet : " + m_initialPosition + "<-- init||current -->" + m_currentPosition);
            Destroy(gameObject, 0);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //if(collision.gameObject.GetComponent<Enemy>(hp_max))
            OnHit();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bullet_enemy")
        {
            Destroy(gameObject);
        }
    }
}
