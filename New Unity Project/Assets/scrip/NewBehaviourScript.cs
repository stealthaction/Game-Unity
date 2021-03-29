using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject levelChanger;
    public GameObject ExitPanel;
    public static string nickname;
	public GameObject CustomPan;
    public InputField nick_input;
    public Text coins;


    void Start()
    {
        if (nick_input != null && nickname != null)
            nick_input.text = nickname;
    }
   
    void Update() {
       
        {
            
            if (levelChanger.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
            {
                levelChanger.SetActive(false);
            }

            if (CustomPan.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
            {
                CustomPan.SetActive(false);
            }
            if (ExitPanel.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
            {
                ExitPanel.SetActive(false);
            }

        }

    }
    public void StartGame()

    {
     

        if (nickname == null || nickname == "") {
            SceneManager.LoadScene(5);
        }//добавить действие, если ник не введен, а пользователь нажимает начать игру

        else
        {
            if (MyDataBase.GetTable("SELECT * FROM Player WHERE nickname = \"" + nickname + "\";").Rows.Count == 0)
            {//если пользователь не зареган. регаем
                MyDataBase.ExecuteQueryWithoutAnswer("INSERT INTO Player(nickname) VALUES(\"" + nickname + "\");");

            }
            levelChanger.SetActive(true);
        }
      
    }
    public void onClickQuit()
    {
        Application.Quit();
    }
    public void onClickExit()
    {
        ExitPanel.SetActive(true);
    }
	public void ChangeCust(){
		CustomPan.SetActive (true);
	}
    public void LevelBttns(int level)
    {
        int price = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT Coins FROM Level WHERE Id_lvl=" + level + ";"));


        if (nickname == null || nickname == "") ;//добавить действие, если ник не введен, а пользователь нажимает начать игру
        else
        {
            if (MyDataBase.GetTable("SELECT * FROM Player WHERE nickname = \"" + nickname + "\";").Rows.Count == 0)
            {//если пользователь не зареган. регаем
                MyDataBase.ExecuteQueryWithoutAnswer("INSERT INTO Player(nickname) VALUES(\"" + nickname + "\");");
            }


            int id_player = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT id_player FROM Player WHERE nickname = \"" + nickname + "\";"));

            int current_bayed_lvl = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT id_lvl FROM Player WHERE id_player = \"" + id_player + "\";"));
            if (level <= current_bayed_lvl)
            {
                SceneManager.LoadScene(level + 1);
                return;

            }
            else if (level != 1 + current_bayed_lvl)
            //пытается купить через уровень
            {
                return;
            }
            int how_much_coins_of_player = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT coins FROM Player WHERE id_player = " + id_player + ";"));

            if (how_much_coins_of_player <= price)

            //если у игрока до этго не было койнов или не достаточно
            {
                //добавить действие, если он бомж, и у него нет денег на уровень

            }
            else
            {
                MyDataBase.ExecuteQueryWithoutAnswer("UPDATE Player SET coins=coins-" + price + " WHERE id_player=" + id_player + ";");

                
                MyDataBase.ExecuteQueryWithoutAnswer("UPDATE Player SET id_lvl=" + level + " WHERE id_player=" + id_player + ";");
                
                SceneManager.LoadScene(level + 1); 
            }
        }
        
    }
    public void RaitingBttn()
    {
        SceneManager.LoadScene(1);

    }
    public void Menuagain()
    {
        SceneManager.LoadScene(0);

    }

    public void PlayerScore()
    {
        SceneManager.LoadScene(4);
    }
    
	public void BuySkin(int skin_id)
	{ 

        int price = int.Parse(MyDataBase.ExecuteQueryWithAnswer (("SELECT coins FROM Skins WHERE skin_id="+skin_id+";")));
    
  
        if (nickname == null || nickname == "") ;//добавить действие, если ник не введен, а пользователь нажимает начать игру
        else
        {
            if (MyDataBase.GetTable("SELECT * FROM Player WHERE nickname = \"" + nickname + "\";").Rows.Count == 0)
            {
                MyDataBase.ExecuteQueryWithoutAnswer("INSERT INTO Player(nickname) VALUES(\"" + nickname + "\");");
            }
            

            int id_player = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT id_player FROM Player WHERE nickname = \"" + nickname + "\";"));

            int how_much_coins_of_player =int.Parse (MyDataBase.ExecuteQueryWithAnswer("SELECT coins FROM Player WHERE id_player = " + id_player + ";"));

            if (how_much_coins_of_player <= price)
            //если у игрока до этго не было койнов или не достаточно
            {
                //добавить действие, если он бомж, и у него нет денег на скин
          
            }
            else
            {  
                MyDataBase.ExecuteQueryWithoutAnswer("UPDATE Player SET coins=coins-" + price + " WHERE id_player=" + id_player + ";");

                //изменить скина в таблице ----- НАЧАЛО
                /*if (MyDataBase.GetTable("SELECT * FROM Player WHERE skin_id = " + id_player + ";").Rows.Count == 0)
                {
                    MyDataBase.ExecuteQueryWithoutAnswer("INSERT INTO PLayer(id_player, skin_id) VALUES(" + id_player + "," + skin_id + ");");
                }*/
                MyDataBase.ExecuteQueryWithoutAnswer("UPDATE Player SET skin_id=" + skin_id + " WHERE id_player=" + id_player + ";");

                //изменить скина в таблице ----- КОНЕЦ

                //изменение кнопки ----- начало
                int btn_idx = skin_id - 1;
                Button[] btns = CustomPan.GetComponentsInChildren<UnityEngine.UI.Button>();
                btns[btn_idx].name = "Button";
                ColorBlock cb = new ColorBlock();
                cb.normalColor = new Color(0, 1.0f, 0, 1.0f);
                btns[btn_idx].colors = cb;
                

                Text[] txts = btns[btn_idx].GetComponentsInChildren<UnityEngine.UI.Text>();
                txts[0].text = "Куплено";
                PlayerPrefs.SetInt("b", skin_id);
                PlayerPrefs.Save();
                //изменение кнопки ----- конец


            }
        }
    }

}

