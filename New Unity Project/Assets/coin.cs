using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour
{

    public int Coins;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "coin")
        {
            Coins++;
            Destroy(other.gameObject);
        }
    }

    void OnGUI()
    {
        GUILayout.Label("coins = " + Coins);
    }
}