using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_movementSpeed;

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
        Debug.Log(m_currentPosition + "||" + m_initialPosition);
        transform.position += new Vector3(0, m_movementSpeed * Time.deltaTime, 0);

        if (m_currentPosition.y > 450.0f)
            Destroy(this, 0);
    }
}
