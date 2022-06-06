using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{

    public float _lifeTime;
    public float _speed;

    private void Awake()
    {
        //transform.rotation = Quaternion.identity;
    }


    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        transform.position += transform.forward * _speed;
    }
}
