using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;

public class Boss : Entity
{
    [SerializeField]
    private float m_HPCurrent;

    //private float ObjectifPositionZ = -1050;

    


    [SerializeField]
    private Camera m_Camera;
    [SerializeField]
    private float m_CameraMargin;

    private float useSpeed;
    public float directionSpeed = 9.0f;
    float origY;
    private float origZ;
    public float distance = 10.0f;
    private bool Charge = false;



    [Header("Bullet")]
    [SerializeField]
    private float m_FireRateDelay = 5000f;
    [SerializeField]
    private GameObject m_PrefabFire;
    private Stopwatch m_StopWatchBullet;

    public AudioClip tir;
    public AudioSource m_audio;

 
    

    void Awake()
    {
        DeplacementInitial();

        m_StopWatchBullet = new Stopwatch();
        m_StopWatchBullet.Start();

    }
    // Start is called before the first frame update
    void Start()
    {
        m_HPCurrent = m_HPMax;
        origY = transform.position.y;  // Y = de haut en bas  
        useSpeed = -directionSpeed;
        origZ = transform.position.z;
       
    }

    // Update is called once per frame
    void Update()
    {
        

        // Il faudrait faire des déplacements aléatoire pour le boss un peu comme dans le script du player. 
        // pour les tirs du Boss, tu peux recalibrer a la main l'endroit dôù le boss tire ou bien juste lui faire spawner des rangées d'ennemis devant lui pour qu'il soit dangeureux.
        // Je ne pense pas pouvoir me rendre dispo sur le reste de mon voyage a strasbourg, mais si t'as des questions hésite pas ! 
        if (GameManager.current.GetNiveau() != 0)
        {
            if (GameManager.current.GetNiveau() == 1)
            {
                if(Charge==false)
                {
                    if (origY - transform.position.y > distance) //Si la position est supérieur
                    {
                        useSpeed = directionSpeed; //flip direction
                    }
                    else if (origY - transform.position.y < -distance) // Si la position est inferieur 
                    {
                        useSpeed = -directionSpeed; //flip direction
                    }
                    transform.Translate(0, useSpeed * Time.deltaTime, 0);
                }
                if (m_HPCurrent == m_HPCurrent % 5 && m_HPCurrent != 0 && m_HPCurrent!=m_HPMax) 
                {
                    Charge = true;
                    if (Charge==true)
                    {
                       
                        if (origZ - transform.position.z > distance) //Si la position est supérieur
                        {
                            useSpeed = directionSpeed; //flip direction
                        }
                        else if (origY - transform.position.z < -distance) // Si la position est inferieur 
                        {
                            useSpeed = -directionSpeed; //flip direction
                        }
                        transform.Translate(0, 0, useSpeed * Time.deltaTime);
                    }
                   
                }
               
            }
            else if (GameManager.current.GetNiveau() == 2)
            {

            }
            else if (GameManager.current.GetNiveau() == 3)
            {

            }

            

        }

        //m_CurrentPosition = m_MainCamera.WorldToScreenPoint(transform.position);

        //transform.position += new Vector3(0,0, m_Speed * Time.deltaTime);

        if (m_StopWatchBullet.ElapsedMilliseconds >= m_FireRateDelay)
        {
            GameObject FireRight = Instantiate(m_PrefabFire, transform.position, new Quaternion(0, 90, 90, 1));
            m_StopWatchBullet.Restart();
            m_audio.PlayOneShot(tir, 0.5f);
        }
        
        //if (m_CurrentPosition.y > m_DeathDist)
        //    Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            
            if (m_HPCurrent > (4-GameManager.current.GetNiveau())/3)
            {
                m_HPCurrent -= 0.1f;
            }
            else 
            {
                GameManager.current.SetBossFighting(false);
                if (GameManager.current.GetNiveau() != 3)
                    GameManager.current.SetNiveau(GameManager.current.GetNiveau() + 1);
                else
                    GameManager.current.SetVictory(true);
                gameObject.SetActive(false);
            }
        }
    }

    private void DeplacementInitial()
    {

    }

}

