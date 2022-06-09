using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{

    public MoveSwipe _moveSwipe;


    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, 
            new Vector3(_moveSwipe.SumValue, transform.localPosition.y, transform.localPosition.z),
            10f * Time.deltaTime);
    }
}
