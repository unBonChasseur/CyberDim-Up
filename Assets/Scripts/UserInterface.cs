using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [Header("Main menu")]
    [SerializeField]
    private GameObject m_MainMenuGameObjects;

    [Header("Gameplay")]
    [SerializeField]
    private GameObject m_GameOverText;
    [SerializeField]
    private GameObject m_ScoreEndText;
    [SerializeField]
    private GameObject m_RestartButton;
    [SerializeField]
    private GameObject m_ScoreText;
    [SerializeField]
    private GameObject m_LifeGauge;

    private void Awake()
    {
      
    }

    // Start is called before the first frame update
    void Start()
    {
        m_MainMenuGameObjects.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.current.GetNiveau() == 0 && !m_MainMenuGameObjects.activeSelf)
        {
            Debug.Log("1");
            m_LifeGauge.SetActive(false);
            m_ScoreText.SetActive(false);

            m_GameOverText.SetActive(false);
            m_ScoreEndText.SetActive(false);
            m_RestartButton.SetActive(false);

            m_MainMenuGameObjects.SetActive(true);
        }
        if(GameManager.current.GetNiveau() >= 1 && m_MainMenuGameObjects.activeSelf)
        {
            Debug.Log("2");

            m_MainMenuGameObjects.SetActive(false);

            m_LifeGauge.SetActive(true);
            m_ScoreText.SetActive(true);


        }
        if (GameManager.current.GetGameOver() && !m_GameOverText.activeSelf)
        {
            Debug.Log("3");
            m_MainMenuGameObjects.SetActive(false);
            m_LifeGauge.SetActive(false);
            m_ScoreText.SetActive(false);


            m_GameOverText.SetActive(true);
            m_ScoreEndText.SetActive(true);
            m_RestartButton.SetActive(true);
        }
    }
   
}
