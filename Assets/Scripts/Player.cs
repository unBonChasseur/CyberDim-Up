using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Player : Entity
{
    [Header("Level")]
    [SerializeField]
    private int m_LevelNumber;

    [Header("Movements")]
    [SerializeField]
    private float m_movementSpeed;
    [SerializeField]
    private float m_smooth;
    [SerializeField]
    private float m_tiltAngle;

    [Header("Camera")]
    [SerializeField]
    private GameObject[] m_Cinemachines;
    private int m_NbCinemachines;
    private Stopwatch m_CamStopWatch;

    [SerializeField]
    private Camera m_mainCamera;
    [SerializeField]
    private float m_marginCamera;
    [SerializeField]
    private float m_camTransitionTime;
    private Vector3 m_initialPosition;
    private Vector3 m_currentPosition;

    [Header("Bullet")]
    [SerializeField]
    private float m_fireRateDelay;
    [SerializeField]
    private GameObject m_PrefabFire;
    private Stopwatch m_FireStopWatch;


    public bool m_defaite;

    private float m_score;

    void Awake()
    {
        m_CamStopWatch = new Stopwatch();
        m_CamStopWatch.Start();

        m_FireStopWatch = new Stopwatch();
        m_FireStopWatch.Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_NbCinemachines = m_Cinemachines.Length;
        m_camTransitionTime = 2000;
        m_defaite = false;
        m_score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentPosition = m_mainCamera.WorldToScreenPoint(transform.position);
        //UnityEngine.Debug.Log("x : " + m_currentPosition.x + " < " + (m_initialPosition.x - m_marginCamera));
        //UnityEngine.Debug.Log("y : " + m_currentPosition.y + " < " + (m_initialPosition.y * 2 - m_marginCamera));
        
        // Changement de caméra en fonction du niveau sélectionné
        if (!m_Cinemachines[m_LevelNumber % m_NbCinemachines].activeSelf)
        {
            for (int i = 0; i < m_NbCinemachines; i++)
                m_Cinemachines[i].SetActive(false);

            transform.position = new Vector3(0, -1000, -1000);
            m_Cinemachines[m_LevelNumber % m_NbCinemachines].SetActive(true);

            m_CamStopWatch.Restart();
        }

        if(m_CamStopWatch.ElapsedMilliseconds >= m_camTransitionTime)
        {
            if (m_LevelNumber % m_NbCinemachines == 1)
            {

                // Gestion des déplacements
                if (Input.GetKey(KeyCode.UpArrow) && m_currentPosition.y < (m_initialPosition.y * 2) - m_marginCamera)
                    transform.position += new Vector3(0, m_movementSpeed * Time.deltaTime, 0);

                if (Input.GetKey(KeyCode.DownArrow) && m_currentPosition.y > m_marginCamera)
                    transform.position -= new Vector3(0, m_movementSpeed * Time.deltaTime, 0);

                // Gestion des tirs
                if (Input.GetKey(KeyCode.Space))
                {
                    if (m_FireStopWatch.ElapsedMilliseconds >= m_fireRateDelay)
                    {

                        UnityEngine.Debug.Log("Player : " + m_initialPosition + "<-- init||current -->" + m_currentPosition);

                        // On créé les tirs pour qu'ils partent exactement des canons
                        GameObject FireLeft = Instantiate(m_PrefabFire, transform.position + new Vector3(-0.305f, 0.08f, -3.6f), new Quaternion(0, 90, 90, 1));
                        GameObject FireRight = Instantiate(m_PrefabFire, transform.position + new Vector3(0.305f, 0.08f, -3.6f), new Quaternion(0, 90, 90, 1));
                        Bullet.OnHit += OnBulletHit;
                        m_FireStopWatch.Restart();
                    }
                }
            }
            else if (m_LevelNumber % m_NbCinemachines == 2)
            {
                // Gestion des déplacements
                if (Input.GetKey(KeyCode.LeftArrow) && m_currentPosition.x > m_marginCamera)
                    transform.position += new Vector3(m_movementSpeed * Time.deltaTime, 0, 0);

                if (Input.GetKey(KeyCode.RightArrow) && m_currentPosition.x < (m_initialPosition.x*2) - m_marginCamera)
                    transform.position -= new Vector3(m_movementSpeed * Time.deltaTime, 0, 0);

                if (Input.GetKey(KeyCode.DownArrow) && m_currentPosition.y > m_marginCamera)
                    transform.position += new Vector3(0, 0, m_movementSpeed * Time.deltaTime);

                if (Input.GetKey(KeyCode.UpArrow) && m_currentPosition.y < (m_initialPosition.y * 2) - m_marginCamera)
                    transform.position -= new Vector3(0, 0, m_movementSpeed * Time.deltaTime);

                // Gestion des tirs
                if (Input.GetKey(KeyCode.Space))
                {
                    if (m_FireStopWatch.ElapsedMilliseconds >= m_fireRateDelay)
                    {
                        // On créé les tirs pour qu'ils partent exactement des canons
                        GameObject FireLeft = Instantiate(m_PrefabFire, transform.position + new Vector3(-0.305f, 0.08f, -3.6f), new Quaternion(0, 90, 90, 1));
                        GameObject FireRight = Instantiate(m_PrefabFire, transform.position + new Vector3(0.305f, 0.08f, -3.6f), new Quaternion(0, 90, 90, 1));
                        Bullet.OnHit += OnBulletHit;
                        m_FireStopWatch.Restart();
                    }
                }
            }
            else if (m_LevelNumber % m_NbCinemachines == 3)
            {
                // Gestion des Rotations
                float tiltAroundZ = Input.GetAxis("Horizontal") * -m_tiltAngle;
                float tiltAroundX = Input.GetAxis("Vertical") * -m_tiltAngle;

                Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * m_smooth);

                // Gestion des déplacements
                if (Input.GetKey(KeyCode.LeftArrow) && m_currentPosition.x > m_marginCamera)
                    transform.position += new Vector3(m_movementSpeed * Time.deltaTime, 0, 0);

                if (Input.GetKey(KeyCode.RightArrow) && m_currentPosition.x < (m_initialPosition.x*2) - m_marginCamera)
                    transform.position -= new Vector3(m_movementSpeed * Time.deltaTime, 0, 0);

                if (Input.GetKey(KeyCode.UpArrow) && m_currentPosition.y < (m_initialPosition.y * 2) - m_marginCamera)
                    transform.position += new Vector3(0, m_movementSpeed * Time.deltaTime, 0);

                if (Input.GetKey(KeyCode.DownArrow) && m_currentPosition.y > m_marginCamera)
                    transform.position -= new Vector3(0, m_movementSpeed * Time.deltaTime, 0);

                // Gestion des tirs
                if (Input.GetKey(KeyCode.Space))
                {
                    if (m_FireStopWatch.ElapsedMilliseconds >= m_fireRateDelay)
                    {
                        // On créé les tirs pour qu'ils partent exactement des canons
                        GameObject FireLeft = Instantiate(m_PrefabFire, transform.position + new Vector3(-0.305f, 0.08f, -3.6f), new Quaternion(0, 90, 90, 1));
                        GameObject FireRight = Instantiate(m_PrefabFire, transform.position + new Vector3(0.305f, 0.08f, -3.6f), new Quaternion(0, 90, 90, 1));
                        Bullet.OnHit += OnBulletHit;
                        m_FireStopWatch.Restart();
                    }
                }
            }
        }
        else
        {
            m_initialPosition = m_mainCamera.WorldToScreenPoint(transform.position);
            //UnityEngine.Debug.Log(m_initialPosition + "<- Init || Current ->" + m_currentPosition);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        float hp_max = current_hp;
        if (collision.gameObject.tag == "Enemy")
        {

            for (float i = hp_max; i > 0; i--)
            {
                if (current_hp != 0)
                {
                    current_hp -= 5;
                }
                else
                {
                    Destroy(gameObject);
                    m_defaite = true;
                }


            }


        }
        if (collision.gameObject.tag == "Bullet_enemy")
        {
            for (float i = hp_max; i > 0; i--)
            {
                if (current_hp != 0)
                {
                    current_hp -= 1;
                }
                else
                {
                    Destroy(gameObject);
                    m_defaite = true;
                }

            }
        }
    }
    void OnBulletHit()
    {
        m_score += 1;
    }


}


