using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class raitingController : MonoBehaviour {
    public Text raitingText;


    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
  

    // Use this for initialization
    void Start () {
        // Получаем отсортированную таблицу лидеров
        DataTable scoreboard = MyDataBase.GetTable("SELECT * FROM Scores ORDER BY score DESC LIMIT 10;");
        /*// Получаем id лучшего игрока
        int idBestPlayer = int.Parse(scoreboard.Rows[0][1].ToString());
        // Получаем ник лучшего игрока
        string nickname = MyDataBase.ExecuteQueryWithAnswer("SELECT nickname FROM Player WHERE id_player = "+idBestPlayer+";");
        raitingText.text = "Лучший игрок " +nickname+" набрал "+scoreboard.Rows[0][2].ToString()+" очков.";
        */
        raitingText.text = "ТОП 10 ИГРОКОВ\n";
        for (int i=0; i<scoreboard.Rows.Count; i++)
        {
            raitingText.text += (i+1) + " место: " + MyDataBase.ExecuteQueryWithAnswer("SELECT nickname FROM Player WHERE id_player = " + scoreboard.Rows[i][1].ToString() + ";") +"  " + scoreboard.Rows[i][2].ToString() + " очков\n";
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
		
	}
}
