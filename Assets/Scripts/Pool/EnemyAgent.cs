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
    public GameObject _ragdoll;

    [Header("EndGame")]
    public NavMeshAgent _navAgent;
    public Transform _endGameTarget;
    public bool _endTriggered;

    private void OnEnable()
    {
        EnemyManager.EnemyShoot += EnemyManager_EnemyShoot;
        AgentManager.Complete += StartEndGame;

        if (!_animC.isActiveAndEnabled)
        {
            _animC.enabled = true;
        }
    }

    private void OnDisable()
    {
        EnemyManager.EnemyShoot -= EnemyManager_EnemyShoot;
        AgentManager.Complete -= StartEndGame;
        //AgentPool._agentPool.livingAgents--;
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
        Quaternion faceDirection = transform.rotation;
        Instantiate(_skull, handPos.position, faceDirection);
    }

    public void Init(Vector3 _coordiate, Vector3 _rotation)
    {
        transform.position = _coordiate;
        transform.eulerAngles = _rotation;
        //_inflate = true;
    }

    public void KillSelf()
    {
        if (_endTriggered)
        {
            PlayerManager.instance.endGameEnemyAlive--;
        }
        gameObject.SetActive(false);
    }

    public void StartEndGame()
    {
        _endTriggered = true;
        _animC.SetTrigger("Run");
        PlayerManager.instance.endGameEnemyAlive++;
        _navAgent = GetComponent<NavMeshAgent>();
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
            Instantiate(_ragdoll, transform.position, Quaternion.identity, null);
            other.gameObject.GetComponent<AgentManager>().KillAgent();
            KillSelf();
        }    
        else if ( other.gameObject.tag == "Sweeper")
        {
            //olaysız süpür, poola dönsün.
            KillSelf();
        }
        else if(other.CompareTag("PlayerBullet"))
        {
            //Ragdoll yap, bunu kapat onu aç... Yukarda da öyle. Bi tek sweeper'da yapma.
            Instantiate(_ragdoll, transform.position, Quaternion.identity, null);
            Destroy(other.gameObject);
            KillSelf();
        }
    }
}
