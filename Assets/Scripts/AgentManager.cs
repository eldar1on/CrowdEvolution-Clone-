using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public enum playerAge
    {
        Primitive,
        Modern,
        Futuristic
    }

    public playerAge _age;

    public Agent[] _model;
    public bool _isAlive;

    Gate _gate;
    int gateYear;

    public int _activeYear;
    public int _activeModel;

    public delegate void OnComplete();
    public static event OnComplete Complete;


    void OnEnable()
    {
        PlayerManager.CheckNewAge += CheckYear;
        CheckYear();
    }

    void OnDisable()
    {
        PlayerManager.CheckNewAge -= CheckYear;
    }

    void Awake()
    {
        CheckYear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            print("Gate");
            _gate = other.GetComponent<Gate>();

            if(_gate._type == Gate.gateType.Year)
            {
                gateYear = _gate.value;
                PlayerManager.instance.ChangeYear(gateYear);
            }
            else if(_gate._type == Gate.gateType.People)
            {
                int newPeople = _gate.value;
                for (int i = 0; i < newPeople; i++)
                {
                    //print(" i : " + i);
                    AgentPool._agentPool.CreateEnemy();
                }
            }

            Destroy(other.gameObject);
        }        
        else if (other.CompareTag("Obstacle"))
        {

            _model[_activeModel].Die();
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("EndGame"))
        {
            print(" End Game Reached!");
            Destroy(other);
            Complete();
            PlayerManager.instance._state = PlayerManager.gameState.EndGameReach;
        }
    }

    void CheckYear()
    {
        _activeYear = PlayerManager.instance._currentYear;

        playerAge currentAge = _age;

        if(_activeYear <= 1300)
        {
            _age = playerAge.Primitive;
            _activeModel = 0;
        }
        else if(_activeYear <= 1900)
        {
            _age = playerAge.Modern;
            _activeModel = 1;
        }
        else
        {
            _age = playerAge.Futuristic;
            _activeModel = 2;
        }

        if(currentAge != _age)
        {
            print("New Age : " + _age);
            UpdateModel(_age);
        }
        else
        {
            //print("Check Year called N nuttin happened!");
        }

        
    }

    void UpdateModel(playerAge _newAge)
    {
        for (int i = 0; i < _model.Length; i++)
        {
            _model[i].gameObject.SetActive(false);
        }


        switch (_newAge)
        {
            case playerAge.Primitive:
                _model[0].gameObject.SetActive(true);
                break;
            case playerAge.Modern:
                _model[1].gameObject.SetActive(true);
                break;
            case playerAge.Futuristic:
                _model[2].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void Init(Vector3 _coordiate)
    {
        transform.position = _coordiate;
        //_inflate = true;
    }

    /*
    public void Init(int openPos)
    {
        transform.position = AgentPool._agentPool.agentCoordinates[openPos].position;
        print(AgentPool._agentPool.agentCoordinates[openPos].position);
        //_inflate = true;
    }*/

    private void Update()
    {
        
    }

    public void GetTarget(Vector3 _position)
    {
        print("Target position msg arrived.");
        _targetPos = _position;
        reached = false;
        MoveLinear();
    }

    [Header("FinalRun")]
    public Vector3 _targetPos;
    public float distanceThreshold;
    public float totalDistance;
    public bool reached;

    public void MoveLinear()
    {
        while (!reached)
        {
            distanceThreshold = Vector3.Distance(transform.position, _targetPos);
            if(distanceThreshold < 0.1f)
            {
                print("Reached");
                reached = true;
                return;
            }
            transform.position += (_targetPos - transform.position) / 100f;
        }
    }

}
