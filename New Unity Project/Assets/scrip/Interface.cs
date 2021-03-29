using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interface : MonoBehaviour {
    public bird bs;
    public bird bs2;
    public Text coinsCount;
    public int CoinsTg3 = 10; public int CoinsTg2 = 6; public int CoinsTg1 = 3;
    public Image[] stars;
    public GameObject[] birds;
	public string KeyName = "S";
    public GameObject pp;
    private bool isPaused = false;

    void Start()
    {
        string nick = NewBehaviourScript.nickname;
        int id_player = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT id_player FROM Player WHERE nickname = \"" + nick + "\";"));
        if (MyDataBase.GetTable("SELECT * FROM Skins WHERE skin_id = " + id_player + ";").Rows.Count == 0)
        //если не установлен скин
        {
            birds[0].SetActive(true);
        }
        else
        {
            int skin_id = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT skin_id FROM Player WHERE id_player = \"" + id_player + "\";"));
            birds[skin_id].SetActive(true);
        }
        
    }
    // Update is called once per frame
    void Update ()
	{
        {
            
                //PlayerPrefs.SetInt(KeyName, bs.score);
                //PlayerPrefs.Save();
                coinsCount.text = "Собрано монет: " + GameControl.instance.score;
            
            if (GameControl.instance.score == CoinsTg3)
            {
                stars[0].color = new Color(stars[0].color.r, stars[0].color.g, stars[0].color.b, 255);
                stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255);
                stars[2].color = new Color(stars[2].color.r, stars[2].color.g, stars[2].color.b, 255);
                PlayerPrefs.SetInt(KeyName, 3);
                PlayerPrefs.Save();
            }
            else if (GameControl.instance.score >= CoinsTg2 && GameControl.instance.score != CoinsTg3)
            {
                stars[0].color = new Color(stars[0].color.r, stars[0].color.g, stars[0].color.b, 255);
                stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255);
            }
            else if (GameControl.instance.score >= CoinsTg1 && GameControl.instance.score != CoinsTg2)
                PlayerPrefs.SetInt(KeyName, 2);
            PlayerPrefs.Save();
            {
                stars[0].color = new Color(stars[0].color.r, stars[0].color.g, stars[0].color.b, 255);
                PlayerPrefs.SetInt(KeyName, 1);
                PlayerPrefs.Save();
            }
        }
      
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pp.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pp.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
	}
    public void pauseOn()
    {
        pp.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void _continue(){
        pp.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
    public void GameAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void gotomenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }   
}