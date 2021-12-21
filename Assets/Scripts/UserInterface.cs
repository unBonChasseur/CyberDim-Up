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
    private GameObject m_VictoryText;
    [SerializeField]
    private GameObject m_ScoreEndText;
    [SerializeField]
    private GameObject m_RestartButton;
    [SerializeField]
    private GameObject m_ScoreText;
    [SerializeField]
    private GameObject m_LifeGauge;
    [SerializeField]
    private GameObject m_PauseText;
    [SerializeField]
    private GameObject m_instructions;
   

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
            m_LifeGauge.SetActive(false);
            m_ScoreText.SetActive(false);

            m_VictoryText.SetActive(false);
            m_GameOverText.SetActive(false);
            m_ScoreEndText.SetActive(false);
            m_RestartButton.SetActive(false);

            m_MainMenuGameObjects.SetActive(true);
        }
        if(GameManager.current.GetNiveau() >= 1 && m_MainMenuGameObjects.activeSelf)
        {
            m_MainMenuGameObjects.SetActive(false);


            m_LifeGauge.SetActive(true);
            m_ScoreText.SetActive(true);
            m_instructions.SetActive(true);
        }
        if (GameManager.current.GetGameOver() && !m_GameOverText.activeSelf)
        {
            m_MainMenuGameObjects.SetActive(false);
            m_LifeGauge.SetActive(false);
            m_ScoreText.SetActive(false);
            m_VictoryText.SetActive(false);

            m_GameOverText.SetActive(true);
            m_ScoreEndText.SetActive(true);
            m_RestartButton.SetActive(true);
        }
        if(GameManager.current.GetVictory() && !m_VictoryText.activeSelf)
        {
            m_MainMenuGameObjects.SetActive(false);
            m_LifeGauge.SetActive(false);
            m_ScoreText.SetActive(false);
            m_GameOverText.SetActive(false);

            m_VictoryText.SetActive(true);
            m_ScoreEndText.SetActive(true);
            m_RestartButton.SetActive(true);
        }
        if (GameManager.current.IsPaused() && GameManager.current.GetNiveau() >= 1 && !m_MainMenuGameObjects.activeSelf && !m_GameOverText.activeSelf && !m_VictoryText.activeSelf)
        {
            m_LifeGauge.SetActive(false);
            m_ScoreText.SetActive(false);
            m_instructions.SetActive(false);

            m_PauseText.SetActive(true);
            m_RestartButton.SetActive(true);
           
           
        }
        if(!GameManager.current.IsPaused() && GameManager.current.GetNiveau() >= 1 && !m_MainMenuGameObjects.activeSelf && !m_GameOverText.activeSelf && !m_VictoryText.activeSelf)
        {
            m_LifeGauge.SetActive(true);
            m_ScoreText.SetActive(true);
            m_instructions.SetActive(true);

            m_PauseText.SetActive(false);
            m_RestartButton.SetActive(false);
           
        }
    }
   
}
