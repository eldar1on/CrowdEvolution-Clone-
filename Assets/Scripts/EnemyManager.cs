using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public delegate void OnEnemyShoot();
    public static event OnEnemyShoot EnemyShoot;

    public static EnemyManager instance;
    public Transform endGameTarget;

    public float timer;
    public float _shootInterval;
    public bool _onAir;

    private void OnEnable()
    {
        EnemyShoot += Shoot;
        PlayerManager.Starting += MakeItRain;
    }

    private void OnDisable()
    {
        EnemyShoot -= Shoot;
        PlayerManager.Starting -= MakeItRain;
    }


    void MakeItRain()
    {
        //Oyun başladı, sıkabiliriz.
        _onAir = true;
    }

    void Awake()
    {
        instance = this;       
    }

    void Shoot()
    {
        //If there is no enemy, dont throw an error msg...
        print("Enemy shooting...");
    }

    void Update()
    {
        if (_onAir)
        {
            timer += Time.deltaTime;

            if( timer > _shootInterval)
            {
                timer = 0;
                EnemyShoot();
            }
        }
    }
    

}
