using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Animator _animC;
    public int weaponID;
    public int agentID;
    public bool isDead;

    public GameObject _skull;
    public Transform handPos;

    AgentManager _agentManager;

    public GameObject _ragdoll;

    void Awake()
    {
        _animC = GetComponent<Animator>();
    }

    void Start()
    {
        if (isDead == true)
        {
            print("Dieee");
            _animC.SetTrigger("Die");
        }
        else
        {
            //print("Not ded");
        }
    }

    void OnEnable()
    {
        PlayerManager.ShootSmt += Shoot;
        PlayerManager.Starting += StartRunning;
        AgentManager.Complete += PathCompleted;

        if (PlayerManager.instance._state == PlayerManager.gameState.Running)
        {
            StartRunning();
        }
    }

    private void PathCompleted()
    {
        StopRunning();
    }

    void StopRunning()
    {
        _animC.SetTrigger("Idle");
    }

    void StartRunning()
    {
        _animC.SetTrigger("Run");
    }

    void OnDisable()
    {
        PlayerManager.ShootSmt -= Shoot;
        PlayerManager.Starting -= StartRunning;
        AgentManager.Complete -= PathCompleted;

        AgentPool._agentPool.agentAliveCount--;
        
    }

    void Shoot()
    {
        _animC.SetTrigger("Shoot");
        //print("Shoots fired!");
    }


    public void AnimShootEvent()
    {
        print("Shooted!");
        Quaternion faceDirection = transform.rotation;
        Instantiate(_skull, handPos.position, faceDirection);
    }

    public void Die()
    {
        //**
        AgentPool._agentPool.livingAgents--;
        GameObject newRagdoll = Instantiate(_ragdoll, transform.position, Quaternion.identity);



        _agentManager = transform.parent.GetComponent<AgentManager>();

        _agentManager.gameObject.SetActive(false);

    }


    
}
