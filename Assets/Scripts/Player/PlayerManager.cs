using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public delegate void OnYearChange();
    public static event OnYearChange CheckNewAge;

    public delegate void OnStart();
    public static event OnStart Starting;

    public delegate void OnFire();
    public static event OnFire ShootSmt;

    public delegate void LevelFailed();
    public static event LevelFailed Fail;


    public delegate void LevelCompleted();
    public static event LevelCompleted Win;


    public enum gameState
    {
        NotStarted,
        Running,
        EndGameReach,
        Failed,
        Won
    }

    public gameState _state;

    public int _aliveAgents;
    public int _currentYear;

    public float _time;
    public float _shootInterval;

    public int endGameEnemyAlive;

    private void Awake()
    {
        instance = this;
        _aliveAgents = 0;
    }

    void Start()
    {
        //_state = gameState.Running;
        Application.targetFrameRate = 60;
    }

    void Update()
    {

        switch (_state)
        {
            case gameState.NotStarted:

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //Tapped, Lets start..
                    Starting();
                    _state = gameState.Running;
                }

                break;
            case gameState.Running:

                _time += Time.deltaTime;

                if (_time > _shootInterval)
                {
                    _time = 0;
                    ShootSmt();
                }
                
                if (AgentPool._agentPool.livingAgents == 0)
                {
                    print("EndGame");
                    Fail();
                    _state = gameState.Failed;
                }
                break;
            case gameState.EndGameReach:

                _time += Time.deltaTime;
                //Oyun sonunda daha seri atış.
                if (_time > _shootInterval/2)
                {
                    _time = 0;
                    ShootSmt();
                }
                if (AgentPool._agentPool.livingAgents == 0)
                {
                    print("EndGame");
                    Fail();
                    _state = gameState.Failed;
                }
                else if(endGameEnemyAlive <= 0)
                {
                    print("You win!");
                    _state = gameState.Won;
                    Win();
                }


                break;
            case gameState.Failed:
                print("Failed");
                //Activate End Game UI
                break;

            case gameState.Won:
                print("Won");
                //Activate End Game UI
                break;
            default:
                break;
        }


    }

    public void ChangeYear(int yearChange)
    {
        _currentYear += yearChange;
        if(_currentYear < 0)
        {
            _currentYear = 0;
        }
        else if(_currentYear > 2000)
        {
            _currentYear = 2000;
        }
        //print("New Current Year :" + _currentYear);
        CheckNewAge();
    }


}
