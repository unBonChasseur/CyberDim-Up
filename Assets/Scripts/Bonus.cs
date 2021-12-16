using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Bonus : Entity
{
    [SerializeField]
    private float vitesse = 10f;

    [SerializeField]
    private Camera m_mainCamera;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(0,0, vitesse * Time.deltaTime);

        if (transform.position.z > 1000)
            Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
