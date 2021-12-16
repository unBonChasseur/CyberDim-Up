using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current = null;
    public int m_level = 0;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNiveau(int niveau)
    {
        this.m_level = niveau;
    }

    public int GetNiveau()
    {
        return this.m_level;
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
