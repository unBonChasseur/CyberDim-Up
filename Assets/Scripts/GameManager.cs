using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager current = null;
    private int m_Level = 0;
    private bool m_GameOver = false;
    private bool m_Victory = false;
    private bool m_BossFighting = false;
    [SerializeField]
    private GameObject m_Boss;

    [Header("Score")]
    private int m_Score;
    [SerializeField]
    private TMP_Text m_ScoreText;
    [SerializeField]
    private TMP_Text m_ScoreEndText;


    private void Awake()
    {
        if (current == null)
        {
            current = this as GameManager;
        }
        else if (current != this)
            DestroySelf();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Score = 0;
        AddScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Score >= 2000 * m_Level && m_Level != 0)
            m_BossFighting = true;
    }

    public void AddScore(int score)
    {
        if (score < 0 && m_Score < Mathf.Abs(score))
        {
            m_Score = 0;
        }
        else
            m_Score += score;
        m_ScoreText.text = "Score:" + m_Score;
        m_ScoreEndText.text = "Your score:" + m_Score;
    }

    public int GetScore()
    {
        return this.m_Score;
    }

    public void SetNiveau(int niveau)
    {
        this.m_Level = niveau;
    }

    public int GetNiveau()
    {
        return this.m_Level;
    }
    
    public void SetGameOver(bool gameover)
    {
        this.m_GameOver = gameover;
    }

    public bool GetGameOver()
    {
        return this.m_GameOver;
    }

    public void SetBossFighting(bool bossfighting)
    {
        if (bossfighting)
            m_Boss.SetActive(true);
        this.m_BossFighting = bossfighting;
    }

    public bool GetBossFighting()
    {
        return this.m_BossFighting;
    }

    public void SetVictory(bool victory)
    {

        this.m_Victory = victory;
    }

    public bool GetVictory()
    {
        return this.m_Victory;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    /// <summary>
    /// Destroys the instance.
    /// </summary>
    private void DestroySelf()
    {
        if (Application.isPlaying)
            Destroy(this);
        else
            DestroyImmediate(this);
    }
}
