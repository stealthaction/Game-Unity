using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using UnityEngine.SceneManagement;
public class Resultcontrol : MonoBehaviour {
    public static Resultcontrol instance;
    public Text Diedscore;
    public Text nametext;
    public Text lvlup;
    public Text count;
    public GameObject[] birds;

    public void QUITMENU()
    {
        SceneManager.LoadScene(0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        string nick = NewBehaviourScript.nickname;
        int id_player = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT id_player FROM Player WHERE nickname = \"" + nick + "\";"));
        if (MyDataBase.GetTable("SELECT sc_die FROM Person_kab WHERE player_id = " + id_player + ";").Rows.Count == 0)
        //если у игрока до этго не было коинов
         
        {
            Diedscore.text = "0";
            nametext.text = MyDataBase.ExecuteQueryWithAnswer("SELECT nickname FROM PLayer WHERE nickname = \"" + nick + "\";");
        }
        else
        {
            count.text = MyDataBase.ExecuteQueryWithAnswer("SELECT coins FROM Player WHERE id_player=\"" + id_player + "\";");
            Diedscore.text = MyDataBase.ExecuteQueryWithAnswer("SELECT sc_die FROM Person_kab WHERE player_id = \"" + id_player + "\";");
            nametext.text = MyDataBase.ExecuteQueryWithAnswer("SELECT nickname FROM PLayer WHERE nickname = \"" + nick + "\";");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        /*if (MyDataBase.GetTable("SELECT * FROM Player WHERE id_lvl = " + id_player + ";").Rows.Count == 0)
        //
        {
            lvlup.text ="0";
        }
        else*/
        {
            string levels = "";
            int id_lvl = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT id_lvl FROM Player WHERE id_player = \"" + id_player + "\";"));

            for (int lvl = 1; lvl <= id_lvl; )
            {
                levels += lvl;
                if (++lvl <= id_lvl)
                    levels += " - ";
            }

            lvlup.text = levels;
            // int lvlup = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT id_lvl FROM Player WHERE id_player = \"" + id_player + "\";"));


        }

    }
}
