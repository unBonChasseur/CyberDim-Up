using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int m_LevelNumber;

    [SerializeField]
    private float m_movementSpeed;

    [SerializeField]
    private Camera m_mainCamera;
    private Vector3 m_initialPosition;
    private Vector3 m_currentPosition;

    [Header("Bullet")]
    [SerializeField]
    private float m_fireRateDelay;
    [SerializeField]
    private GameObject m_PrefabFire;

    private Stopwatch m_StopWatch;

    void Awake()
    {
        m_StopWatch = new Stopwatch();
        m_StopWatch.Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_initialPosition = m_mainCamera.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        m_currentPosition = m_mainCamera.WorldToScreenPoint(transform.position);

        if (m_LevelNumber == 1)
        {
            if (Input.GetKey(KeyCode.UpArrow) && m_currentPosition.y < m_initialPosition.y * 2)
                transform.position += new Vector3(0, m_movementSpeed * Time.deltaTime, 0);

            if (Input.GetKey(KeyCode.DownArrow) && m_currentPosition.y > 0)
                transform.position -= new Vector3(0, m_movementSpeed * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.Space))
            {
                if (m_StopWatch.ElapsedMilliseconds >= m_fireRateDelay)
                {
                    GameObject Fire = Instantiate(m_PrefabFire, transform.position, new Quaternion(0,0,0,1));
                    m_StopWatch.Restart();
                }
            }
        }
        else if (m_LevelNumber == 2)
        {
            if (Input.GetKey(KeyCode.RightArrow) && m_currentPosition.x < m_initialPosition.x * 2)
                transform.position += new Vector3(m_movementSpeed * Time.deltaTime, 0, 0);

            if (Input.GetKey(KeyCode.LeftArrow) && m_currentPosition.x > 0)
                transform.position -= new Vector3(m_movementSpeed * Time.deltaTime, 0, 0);

            if (Input.GetKey(KeyCode.UpArrow) && m_currentPosition.y < m_initialPosition.y * 2)
                transform.position += new Vector3(0, m_movementSpeed * Time.deltaTime, 0);

            if (Input.GetKey(KeyCode.DownArrow) && m_currentPosition.y > 0)
                transform.position -= new Vector3(0, m_movementSpeed * Time.deltaTime, 0);

            if (Input.GetKey(KeyCode.Space))
            {
                if (m_StopWatch.ElapsedMilliseconds >= m_fireRateDelay)
                {
                    GameObject Fire = Instantiate(m_PrefabFire, transform.position, new Quaternion(0, 0, 0, 1));
                    m_StopWatch.Restart();
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow) && m_currentPosition.x < m_initialPosition.x * 2)
                transform.position += new Vector3(m_movementSpeed * Time.deltaTime, 0, 0);

            if (Input.GetKey(KeyCode.LeftArrow) && m_currentPosition.x > 0)
                transform.position -= new Vector3(m_movementSpeed * Time.deltaTime, 0, 0);

            if (Input.GetKey(KeyCode.UpArrow) && m_currentPosition.y < m_initialPosition.y * 2)
                transform.position += new Vector3(0, 0, m_movementSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.DownArrow) && m_currentPosition.y > 0)
                transform.position -= new Vector3(0, 0, m_movementSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Space))
            {
                if (m_StopWatch.ElapsedMilliseconds >= m_fireRateDelay)
                {
                    GameObject Fire = Instantiate(m_PrefabFire, transform.position, new Quaternion(0, 0, 0, 1));
                    m_StopWatch.Restart();
                }
            }
        }
    }
   
       
    
}


