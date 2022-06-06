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

    public enum gameState
    {
        NotStarted,
        Running,
        EndGameReach
    }

    public gameState _state;

    public int _aliveAgents;
    public int _currentYear;

    public float _time;
    public float _shootInterval;

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

                break;
            case gameState.EndGameReach:

                _time += Time.deltaTime;

                if (_time > _shootInterval/2)
                {
                    _time = 0;
                    ShootSmt();
                }
                break;
            default:
                break;
        }
    }

    public void ChangeYear(int yearChange)
    {
        _currentYear += yearChange;
        //print("New Current Year :" + _currentYear);
        CheckNewAge();
    }


}
