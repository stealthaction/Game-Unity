﻿using UnityEngine;
using System.Collections;

public class Column : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<bird>() != null)
        {
            
            GameControl.instance.BirdScored();
        }
    }
}