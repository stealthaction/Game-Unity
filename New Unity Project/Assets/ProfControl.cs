using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfControl : MonoBehaviour {

    public InputField nickField;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        NewBehaviourScript.nickname = nickField.text;
    }


    public void MyUpdate()
    {
        NewBehaviourScript.nickname = nickField.text;
    }
}
