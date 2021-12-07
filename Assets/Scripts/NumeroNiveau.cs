using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumeroNiveau : MonoBehaviour
{
    private static NumeroNiveau m_NumeroNiveau;
    private NumeroNiveau() {}

    // Start is called before the first frame update
    public static NumeroNiveau Instance{ get; private set; }

    void Awake()
    {
        if (m_NumeroNiveau != null && m_NumeroNiveau != this) 
            Destroy(gameObject);
        m_NumeroNiveau = this;
    }

    public void loadFile()
    {
    }
    
}
