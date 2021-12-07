using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_movementSpeed;

    [SerializeField]
    private Camera m_mainCamera;
    private Vector3 m_currentPosition;

    [SerializeField]
    private float m_deathDist;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_currentPosition = m_mainCamera.WorldToScreenPoint(transform.position);

        transform.position += new Vector3(0, m_movementSpeed * Time.deltaTime, 0);

        if (m_currentPosition.y > m_deathDist)
            Destroy(gameObject, 0);
    }
}
