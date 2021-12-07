using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementsVaisseau : MonoBehaviour
{
    [SerializeField]
    private int m_numeroNiveau = 1;
    [SerializeField]
    private float movementSpeed = 2f;
    [SerializeField]
    private Camera m_mainCamera;
    private Vector3 m_initialPosition;
    private Vector3 m_currentPosition;


    // Start is called before the first frame update
    void Start()
    {
        m_initialPosition = m_mainCamera.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        m_currentPosition = m_mainCamera.WorldToScreenPoint(transform.position);

        if (m_numeroNiveau == 1)
        {
            if (Input.GetKey(KeyCode.UpArrow) && m_currentPosition.y < m_initialPosition.y * 2)
                transform.position += new Vector3(0, movementSpeed * Time.deltaTime, 0);

            if (Input.GetKey(KeyCode.DownArrow) && m_currentPosition.y > 0)
                transform.position -= new Vector3(0, movementSpeed * Time.deltaTime, 0);
        }
        else
        {
            if (m_numeroNiveau == 2)
            {
                if (Input.GetKey(KeyCode.RightArrow) && m_currentPosition.x < m_initialPosition.x * 2)
                    transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
                
                if (Input.GetKey(KeyCode.LeftArrow) && m_currentPosition.x > 0)
                    transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0, 0);
                   
                if (Input.GetKey(KeyCode.UpArrow) && m_currentPosition.y < m_initialPosition.y * 2)
                    transform.position += new Vector3(0, movementSpeed * Time.deltaTime, 0);
                        
                if (Input.GetKey(KeyCode.DownArrow) && m_currentPosition.y > 0)
                    transform.position -= new Vector3(0, movementSpeed * Time.deltaTime, 0);

            }
            else
            {

            }
        }
    }
}
