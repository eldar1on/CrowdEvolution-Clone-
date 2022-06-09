using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int _index;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            SceneManager.LoadScene(_index);
        }
    }
}
