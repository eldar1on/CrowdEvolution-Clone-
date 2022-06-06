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
    }

    void Shoot()
    {
        _animC.SetTrigger("Shoot");
        //print("Shoots fired!");
    }


    public void AnimShootEvent()
    {
        print("Shooted!");
        Quaternion faceDirection = transform.localRotation;
        Instantiate(_skull, handPos.position, faceDirection);
    }

    public void Die()
    {
        /*
        Agent clone = gameObject.GetComponent<Agent>();
        Agent _dyingCopy = Instantiate(clone, transform.position, Quaternion.identity);
        _dyingCopy.isDead = true;
        */
        //print(_ragdoll.name);

        GameObject newRagdoll = Instantiate(_ragdoll, transform.position, Quaternion.identity);


        _agentManager = transform.parent.GetComponent<AgentManager>();
        AgentPool._agentPool.agentAliveCount--;
        _agentManager.gameObject.SetActive(false);

        //_agentManager._isAlive = false;
        //gameObject.SetActive(false);


        //clone._animC.SetTrigger("Die");

    }
}
