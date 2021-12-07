using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_prefabEnemy;

    [SerializeField]
    private GameObject m_spawnDelay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
            StartCoroutine(EnnemySpawn());
    }

    private IEnumerator EnnemySpawn()
    {
        return null;
    }
}
