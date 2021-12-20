using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;

public class Player : Entity
{
    [Header("Movements")]
    [SerializeField]
    private float m_MovementSpeed;
    [SerializeField]
    private float m_MovementSmooth;
    [SerializeField]
    private float m_MovementRotationAngle;

    [Header("Camera")]
    [SerializeField]
    private GameObject[] m_Cinemachines;
    private int m_CinemachinesNb;
    private Stopwatch m_StopWatchCamera;

    [SerializeField]
    private Camera m_Camera;
    [SerializeField]
    private float m_CameraMargin;
    [SerializeField]
    private float m_CameraTransitionTime;
    private Vector3 m_CurrentPosition;

    [Header("Bullet")]
    [SerializeField]
    private float m_BulletDelay;
    [SerializeField]
    private GameObject m_PrefabFire;
    private Stopwatch m_StopWatchBullets;

    public MeshRenderer m_Renderer;

    [SerializeField]
    public Slider m_LifeGauge;
    private float m_HPCurrent;

    public AudioClip shot;
    public AudioClip bonus;
    public AudioClip degat;
    public AudioSource m_audio;

    void Awake()
    {
        m_StopWatchCamera = new Stopwatch();
        m_StopWatchCamera.Start();

        m_StopWatchBullets = new Stopwatch();
        m_StopWatchBullets.Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_CinemachinesNb = m_Cinemachines.Length;
        m_CameraTransitionTime = 2000;
        m_HPCurrent = m_HPMax;

        m_LifeGauge.value = m_HPMax;
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentPosition = m_Camera.WorldToScreenPoint(transform.position);


        // Changement de caméra en fonction du niveau sélectionné
        if (!m_Cinemachines[GameManager.current.GetNiveau() % m_CinemachinesNb].activeSelf)
        {
            for (int i = 0; i < m_CinemachinesNb; i++)
                m_Cinemachines[i].SetActive(false);

            transform.position = new Vector3(0, -1000, -1000);
            m_Cinemachines[GameManager.current.GetNiveau() % m_CinemachinesNb].SetActive(true);

            m_StopWatchCamera.Restart();
        }

        if (m_StopWatchCamera.ElapsedMilliseconds >= m_CameraTransitionTime)
        {
            if (GameManager.current.GetNiveau() % m_CinemachinesNb == 1)
            {

                // Gestion des déplacements
                if (Input.GetKey(KeyCode.UpArrow) && m_CurrentPosition.y < m_Camera.pixelHeight - m_CameraMargin)
                    transform.position += new Vector3(0, m_MovementSpeed * Time.deltaTime, 0);

                if (Input.GetKey(KeyCode.DownArrow) && m_CurrentPosition.y > m_CameraMargin)
                    transform.position -= new Vector3(0, m_MovementSpeed * Time.deltaTime, 0);

                if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && transform.position.y != -1000)
                {
                    // si le vaisseau est vers le bas
                    if(transform.position.y < -1000)
                    {
                        if (Mathf.Abs(transform.position.y) - 1000 < m_MovementSpeed * Time.deltaTime)
                            transform.position = new Vector3(0, -1000, -1000);
                        else
                            transform.position += new Vector3(0, m_MovementSpeed * Time.deltaTime, 0);
                        
                    }
                    else if (transform.position.y > -1000)
                    {
                        if (1000 - Mathf.Abs(transform.position.y) < m_MovementSpeed * Time.deltaTime)
                            transform.position = new Vector3(0, -1000, -1000);
                        else
                            transform.position -= new Vector3(0, m_MovementSpeed * Time.deltaTime, 0);
                    }
                }

            }
            else if (GameManager.current.GetNiveau() % m_CinemachinesNb == 2)
            {
                // Gestion des déplacements
                if (Input.GetKey(KeyCode.LeftArrow) && m_CurrentPosition.x > m_CameraMargin)
                    transform.position += new Vector3(m_MovementSpeed * Time.deltaTime, 0, 0);

                if (Input.GetKey(KeyCode.RightArrow) && m_CurrentPosition.x < m_Camera.pixelWidth - m_CameraMargin)
                    transform.position -= new Vector3(m_MovementSpeed * Time.deltaTime, 0, 0);

                if (Input.GetKey(KeyCode.DownArrow) && m_CurrentPosition.y > m_CameraMargin)
                    transform.position += new Vector3(0, 0, m_MovementSpeed * Time.deltaTime);

                if (Input.GetKey(KeyCode.UpArrow) && m_CurrentPosition.y < m_Camera.pixelHeight - m_CameraMargin)
                    transform.position -= new Vector3(0, 0, m_MovementSpeed * Time.deltaTime);

            }
            else if (GameManager.current.GetNiveau() % m_CinemachinesNb == 3)
            {
                // Gestion des Rotations
                float tiltAroundZ = Input.GetAxis("Horizontal") * -m_MovementRotationAngle;
                float tiltAroundX = Input.GetAxis("Vertical") * -m_MovementRotationAngle;

                Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * m_MovementSmooth);

                // Gestion des déplacements
                if (Input.GetKey(KeyCode.LeftArrow) && m_CurrentPosition.x > m_CameraMargin)
                    transform.position += new Vector3(m_MovementSpeed * Time.deltaTime, 0, 0);

                if (Input.GetKey(KeyCode.RightArrow) && m_CurrentPosition.x < m_Camera.pixelWidth - m_CameraMargin)
                    transform.position -= new Vector3(m_MovementSpeed * Time.deltaTime, 0, 0);

                if (Input.GetKey(KeyCode.UpArrow) && m_CurrentPosition.y < m_Camera.pixelHeight - m_CameraMargin)
                    transform.position += new Vector3(0, m_MovementSpeed * Time.deltaTime, 0);

                if (Input.GetKey(KeyCode.DownArrow) && m_CurrentPosition.y > m_CameraMargin)
                    transform.position -= new Vector3(0, m_MovementSpeed * Time.deltaTime, 0);

            }

            if (Input.GetKey(KeyCode.Space) && GameManager.current.GetNiveau() % m_CinemachinesNb != 0)
            {
                if (m_StopWatchBullets.ElapsedMilliseconds >= m_BulletDelay)
                {
                    // On créé les tirs pour qu'ils partent exactement des canons
                    GameObject FireLeft = Instantiate(m_PrefabFire, transform.position + new Vector3(-0.305f, 0.08f, -3.6f), new Quaternion(0, 90, 90, 1));
                    GameObject FireRight = Instantiate(m_PrefabFire, transform.position + new Vector3(0.305f, 0.08f, -3.6f), new Quaternion(0, 90, 90, 1));
                    m_StopWatchBullets.Restart();
                    m_audio.PlayOneShot(shot, 1f);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet")
        {

            if (collision.gameObject.tag == "Enemy")
            {
                StartCoroutine(ClignotementDegats());
                GameManager.current.AddScore(-10);
                m_HPCurrent -= 5;

                if (m_HPCurrent > 0)
                    m_LifeGauge.value = m_HPCurrent;

                else
                {
                    GameManager.current.SetGameOver(true);
                    Destroy(gameObject);
                }
                m_audio.PlayOneShot(degat, 1f);
            }
            if (collision.gameObject.tag == "Bullet_enemy")
            {
                StartCoroutine(ClignotementDegats());
                GameManager.current.AddScore(-2);
                m_HPCurrent -= 1;
                if (m_HPCurrent > 0)
                    m_LifeGauge.value = m_HPCurrent;
                else
                {
                    GameManager.current.SetGameOver(true);
                    Destroy(gameObject);
                }
                m_audio.PlayOneShot(degat, 1f);
            }
            if (collision.gameObject.tag == "Bonus")
            {
                StartCoroutine(ClignotementBonus());
                GameManager.current.AddScore(4);
                m_HPCurrent = m_HPMax;
                m_LifeGauge.value = m_HPCurrent;
                m_audio.PlayOneShot(bonus, 1f);
            }
            if (collision.gameObject.tag == "Boss")
            {
                GameManager.current.AddScore(-20);
                m_HPCurrent -= 10;
                GameManager.current.SetGameOver(true);
                Destroy(gameObject);
                m_audio.PlayOneShot(degat, 1f);

            }
        }
    }


    private IEnumerator ClignotementDegats()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.3f);
            m_Renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            m_Renderer.material.color = Color.white;
        }
    }

    private IEnumerator ClignotementBonus()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.3f);
            m_Renderer.material.color = Color.green;
            yield return new WaitForSeconds(0.3f);
            m_Renderer.material.color = Color.white;
        }
    }
}


