using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test : MonoBehaviour
{
    private int[] values;
    private bool[] keys;
    private void Start()
    {
        Debug.Log(Convert.ToBoolean("true"));
        Debug.Log(Convert.ToBoolean("false"));
    }

    private void Update()
    {
       

    }
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

