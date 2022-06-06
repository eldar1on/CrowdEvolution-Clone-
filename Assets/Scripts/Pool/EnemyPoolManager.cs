using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public EnemyAgent enemyPrefab;

    public List<EnemyAgent> enemyPool;

    public int enemyAmountAtStart = 10;

    public List<Transform> enemyleCoordiantes;
    public GameObject CoordinatesParent;

    public GameObject coordinateParent;

    private void Awake()
    {
        enemyPool = new List<EnemyAgent>();

        for (int i = 0; i < enemyAmountAtStart; i++)
        {
            EnemyAgent newAgent = Instantiate(enemyPrefab);
            newAgent.transform.parent = transform;
            newAgent.gameObject.SetActive(false);
            enemyPool.Add(newAgent);
        }

        GetCoordinates();
    }

    EnemyAgent GetEnemyFromPool()
    {
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (!enemyPool[i].gameObject.activeSelf)
            {
                return enemyPool[i];
            }
        }
            return null;
    }

    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            CreateEnemy();
        }
        */
    }

    void CreateEnemy()
    {
        Vector3 worlPos = Vector3.zero;

        EnemyAgent newAgent = GetEnemyFromPool();

        if(newAgent == null)
        {
            newAgent = Instantiate(enemyPrefab);
            newAgent.transform.parent = transform;
            enemyPool.Add(newAgent);
            //return;
        }

        newAgent.gameObject.SetActive(true);
        newAgent.Init(worlPos);

        //Instantiate(enemyPrefab, worlPos, transform.rotation);
    }

    void GetCoordinates()
    {
        enemyleCoordiantes = new List<Transform>();

        int childCount = 0;

        childCount = coordinateParent.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            enemyleCoordiantes.Add(coordinateParent.transform.GetChild(i));
        }
    }
}
