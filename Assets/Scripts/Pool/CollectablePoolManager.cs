using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePoolManager : MonoBehaviour
{
    public EnemyAgent collectablePrefab;

    public List<EnemyAgent> collectablePool;

    public int collectableAmounthInitial;

    public List<Transform> collectableCoordiantes;
    public GameObject coordinateParent;

    public int _openCollectableCount;

    public GameObject _Player;
    public bool checkPosition;
    public float distanceTreshold;
    public float distanceToNext;


    



    private void Awake()
    {
        //Oyun henüz başlamadan, belirttiğimiz kadar enemy spawnlıyoruz.

        _openCollectableCount = 0;

        collectablePool = new List<EnemyAgent>();

        for (int i = 0; i < collectableAmounthInitial; i++)
        {
            EnemyAgent newCollectable = Instantiate(collectablePrefab);

            newCollectable.gameObject.SetActive(false);
            newCollectable.transform.parent = transform;
            collectablePool.Add(newCollectable);
        }

        GetCoordinates();
    }

    void GetCoordinates()
    {
        //enemylerin yerleşeceği bütün pozisyonları listeliyoruz.

        collectableCoordiantes = new List<Transform>();

        int childCount = 0;
        childCount = coordinateParent.transform.childCount;
        //print("Coordiantes count : " + childCount);

        for (int i = 0; i < childCount; i++)
        {
            collectableCoordiantes.Add(coordinateParent.transform.GetChild(i));
        }
    }



    EnemyAgent GetCollectableFromPool()
    {
        //Boştaki collectablelardan almaya çalışıyoruz.

        for (int i = 0; i < collectablePool.Count; i++)
        {
            if (!collectablePool[i].gameObject.activeSelf)
            {
                return collectablePool[i];
            }
        }

        return null;
    }


    void PositionCollectable()
    {
        EnemyAgent newCollectable = GetCollectableFromPool();

        //print(collectableCoordiantes[_openCollectableCount].position);

        if(newCollectable == null)
        {
            return;
        }

        newCollectable.gameObject.SetActive(true);
        newCollectable.Init(collectableCoordiantes[_openCollectableCount].position, collectableCoordiantes[_openCollectableCount].eulerAngles);

        _openCollectableCount++;



    }



    void Update()
    {
        if (checkPosition)
        {
            FurtherSpawn();
        }

    }

    void FurtherSpawn()
    {
        distanceToNext = Vector3.Distance(_Player.transform.position, collectableCoordiantes[ _openCollectableCount ].position);

        if(distanceToNext < distanceTreshold)
        {
            if(_openCollectableCount + 1 < collectableCoordiantes.Count)

            PositionCollectable();
        }
    }
}
