using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public static Manager _instance;

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public GameObject _winUI;
    public GameObject _loseUI;

    void OnEnable()
    {
        _instance = this;
        PlayerManager.Fail += Failed;
        PlayerManager.Win += Win;
    }

    void OnDisable()
    {
        PlayerManager.Fail -= Failed;
        PlayerManager.Win -= Win;
    }

    public void Failed()
    {
        _loseUI.SetActive(true);
    }

    public void Win()
    {
        _winUI.SetActive(true);
    }

    

}
