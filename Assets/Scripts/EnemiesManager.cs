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
        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    private IEnumerator EnemySpawn()
    {
        while(Application.isPlaying && !GameManager.current.GetBossFighting())
        {
            yield return new WaitForSeconds(m_spawnDelay);
            Vector3 spawnPos = new Vector3(0,-1000,-1075);
            if (GameManager.current.GetNiveau() != 0)
            {
                if (GameManager.current.GetNiveau() == 1)
                {
                    spawnPos.y += Random.Range(-14.2f, 14.2f);
                }
                else if (GameManager.current.GetNiveau() == 2)
                {
                    spawnPos.x = Random.Range(-50f, 50f);
                }
                else if (GameManager.current.GetNiveau() == 3)
                {
                    spawnPos.y += Random.Range(-4.6f, 7.6f);
                    spawnPos.x = Random.Range(-13f, 13f);
                }

                GameObject enemy = Instantiate(m_prefabEnemy, spawnPos, new Quaternion(0, 0, 90, 1));
               

            }
        }
       
    }
}
