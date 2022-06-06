using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAgent : MonoBehaviour
{
    public Vector3 _worldPos;

    [Header("Shoot")]
    public Animator _animC;
    public Transform handPos;
    public Skull _skull;

    [Header("EndGame")]
    public NavMeshAgent _navAgent;
    public Transform _endGameTarget;

    private void OnEnable()
    {
        EnemyManager.EnemyShoot += EnemyManager_EnemyShoot;
        AgentManager.Complete += StartEndGame;
    }

    private void OnDisable()
    {
        EnemyManager.EnemyShoot -= EnemyManager_EnemyShoot;
        AgentManager.Complete -= StartEndGame;
    }

    void Awake()
    {
        _animC = GetComponent<Animator>();
    }

    void EnemyManager_EnemyShoot()
    {
        _animC.SetTrigger("Throw");
    }

    public void Throw()
    {
        Quaternion faceDirection = transform.localRotation;
        Instantiate(_skull, handPos.position, faceDirection);
    }

    public void Init(Vector3 _coordiate)
    {
        transform.position = _coordiate;
        //_inflate = true;
    }

    public void KillSelf()
    {
        gameObject.SetActive(false);
    }

    public void StartEndGame()
    {
        //_navAgent = gameObject.AddComponent<NavMeshAgent>();
        //_navAgent.radius = .3f;
        _navAgent = GetComponent<NavMeshAgent>();
        _animC.SetTrigger("Run");
        //_navAgent.enabled = true;
        _endGameTarget = EnemyManager.instance.endGameTarget;
        _navAgent.SetDestination(_endGameTarget.position);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!_navAgent)
            {
                _navAgent.enabled = false;
            }
            _animC.enabled = false;
            //transform.position = new Vector3(0f, 30f, 0f);
        }    
    }
}
