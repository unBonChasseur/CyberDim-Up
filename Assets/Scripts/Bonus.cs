using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Bonus : Entity
{
    [SerializeField]
    private float m_movementSpeed;

    [SerializeField]
    private Camera m_mainCamera;

    [SerializeField]
    private float m_maxRange = 1075;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, m_movementSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.z) >= m_maxRange)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
