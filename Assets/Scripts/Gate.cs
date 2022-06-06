using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    public enum gateType
    {
        Year,
        People
    }

    public gateType _type;

    public string _msg;
    public TextMeshProUGUI _tmpText;

    public int value;

    void Start()
    {
        switch (_type)
        {
            case gateType.Year:
                _msg = value.ToString() + " Years";
                _tmpText.text = _msg;
                break;
            case gateType.People:
                _msg = "+ " +value.ToString() + " People";
                _tmpText.text = _msg;
                break;
            default:
                break;
        }
    }
}
