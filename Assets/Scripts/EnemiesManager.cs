using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_prefabEnemy;

    [SerializeField]
    private float m_spawnDelay = 2f;

   

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnnemySpawn());
    }

    // Update is called once per frame
    void Update()
    { 
               

    }

    private IEnumerator EnnemySpawn()
    {
        while(Application.isPlaying)
        {
            yield return new WaitForSeconds(m_spawnDelay);
            Vector3 spawnPos = new Vector3(0,-1000,-1075);
            spawnPos.x = Random.Range(-50f, 50f);
            GameObject enemy = Instantiate(m_prefabEnemy, spawnPos, new Quaternion(0, 0, 90, 1));
           
        }
       
    }
}
