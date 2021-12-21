using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;

public class Boss : Entity
{
    [SerializeField]
    private float m_HPCurrent;

    [SerializeField]
    private Camera m_Camera;
    [SerializeField]
    private float m_CameraMargin;

    private float useSpeed;
    public float directionSpeed = 9.0f;
    private float origY;
    private float origZ;
    private float origX;
    public float distancey = 14.2f;
    public float distancez = 55f;
    public float distancex = 50f;
    private bool Charge = false;



    [Header("Bullet")]
    [SerializeField]
    private float m_FireRateDelay = 5000f;
    [SerializeField]
    private GameObject m_PrefabFire;
    private Stopwatch m_StopWatchBullet;

    public AudioClip tir;
    public AudioSource m_audio;
    private bool dirvalide=true;

    void Awake()
    {
        //DeplacementInitial();

        m_StopWatchBullet = new Stopwatch();
        m_StopWatchBullet.Start();

    }
    // Start is called before the first frame update
    void Start()
    {
        m_HPCurrent = m_HPMax;
        origY = transform.position.y;  // Y = de haut en bas  
        origZ = transform.position.z;
        origX = transform.position.x;
        useSpeed = -directionSpeed;
        
        


    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.current.GetNiveau() != 0)
        {
            if (GameManager.current.GetNiveau() == 1)
            {
                if(Charge==false)
                {
                    if (origY - transform.position.y > distancey) //Si la position est supérieur
                    {
                        useSpeed = directionSpeed; //flip direction
                    }
                    else if (origY - transform.position.y < -distancey) // Si la position est inferieur 
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

                        if (origZ - transform.position.z > distancez) //Si la position est supérieur
                        {
                            useSpeed = directionSpeed; //flip direction
                        }
                        else if (origZ - transform.position.z < -distancez) // Si la position est inferieur 
                        {
                            useSpeed = -directionSpeed; //flip direction
                        }
                        transform.Translate(0, 0, useSpeed * Time.deltaTime);
                    }
                   
                }
               
            }
            else if (GameManager.current.GetNiveau() == 2)
            {
                if (Charge == false)
                {
                    if (origX-transform.position.x > distancex )
                    {


                        useSpeed = -directionSpeed;
                        
                    }
                    else if (origX - transform.position.x < -distancex)
                    {


                        useSpeed = directionSpeed;
                        
                    }
                    transform.Translate(useSpeed * Time.deltaTime, 0, 0);
                }
                if (m_HPCurrent == m_HPCurrent % 5 && m_HPCurrent != 0 && m_HPCurrent != m_HPMax)
                {
                    Charge = true;
                    if (Charge == true)
                    {

                        if (origZ - transform.position.z > distancez) //Si la position est supérieur
                        {
                            useSpeed = directionSpeed; //flip direction
                        }
                        else if (origZ - transform.position.z < -distancez) // Si la position est inferieur 
                        {
                            useSpeed = -directionSpeed; //flip direction
                        }
                        transform.Translate(0, 0, useSpeed * Time.deltaTime);
                    }

                }
            }
            else if (GameManager.current.GetNiveau() == 3)
            {
                if (Charge == false)
                {
                    if (origY - transform.position.y > distancey) //Si la position est supérieur
                    {
                        useSpeed = directionSpeed; //flip direction
                    }
                    else if (origY - transform.position.y < -distancey) // Si la position est inferieur 
                    {
                        useSpeed = -directionSpeed; //flip direction
                    }
                    transform.Translate(0, useSpeed * Time.deltaTime, 0);
                }
                if (m_HPCurrent == m_HPCurrent % 5 && m_HPCurrent != 0 && m_HPCurrent != m_HPMax)
                {
                    Charge = true;
                    if (Charge == true)
                    {

                        if (origZ - transform.position.z > distancez/2) //Si la position est supérieur
                        {
                            useSpeed = directionSpeed; //flip direction
                        }
                        else if (origY - transform.position.z < -distancez) // Si la position est inferieur 
                        {
                            useSpeed = -directionSpeed; //flip direction
                        }
                        transform.Translate(0, 0, -useSpeed * Time.deltaTime);
                    }

                }

            }

            

        }

        if (m_StopWatchBullet.ElapsedMilliseconds >= m_FireRateDelay && !GameManager.current.IsPaused())
        {
            GameObject FireRight = Instantiate(m_PrefabFire, transform.position, new Quaternion(0, 90, 90, 1));
            m_StopWatchBullet.Restart();
            m_audio.PlayOneShot(tir, 0.5f);
        }
        
        
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

    /*private void DeplacementInitial()
    {
        transform.Rotate(new Vector3(0,0,360));
    }*/

}

