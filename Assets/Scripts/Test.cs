using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test : MonoBehaviour
{
    public void RebindKey(string LineName)
    {
        foreach (KeyCode kCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kCode))
            {

                Debug.Log("Key pressed: " + kCode);

            }
        }
    }
}


