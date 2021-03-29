using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Noscriptstar : MonoBehaviour {
	public Image[] stars;
	public string keyName = "S";

	// Use this for initialization
	void Awake () {
		stars = GetComponentsInChildren<Image>();
	}
	
	// Update is called once per frame
	void Start ()
	{
		if (PlayerPrefs.GetInt (keyName) == 3) {
			stars [1].color = new Color (stars [1].color.r, stars [1].color.g, stars [1].color.b, 0);
			stars [2].color = new Color (stars [2].color.r, stars [2].color.g, stars [2].color.b, 0);
			stars [3].color = new Color (stars [3].color.r, stars [3].color.g, stars [3].color.b, 0);	
		} else if (PlayerPrefs.GetInt (keyName) == 2) {
			stars [1].color = new Color (stars [1].color.r, stars [1].color.g, stars [1].color.b, 0);
			stars [2].color = new Color (stars [2].color.r, stars [2].color.g, stars [2].color.b, 0);
		} else if (PlayerPrefs.GetInt (keyName) == 1) {
			stars [1].color = new Color (stars [1].color.r, stars [1].color.g, stars [1].color.b, 0);

		}
	}
}
