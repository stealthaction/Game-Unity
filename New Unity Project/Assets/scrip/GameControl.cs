using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameControl : MonoBehaviour
{
    public static GameControl instance;         
    public Text scoreText;
   public static GameObject sS; 
    public GameObject gameOvertext;
    public int score = 0;
    private int currentNumberOfEnemies = 0;
  
    public float scrollSpeed = -1;
    public bool gameOver = false;
  

 

  
    void Awake()
    {
       
        if (instance == null)
          
            instance = this;
 
        else if (instance != this)
      
            Destroy(gameObject);
    }

    void Update()
    {
        string nick = NewBehaviourScript.nickname;
        int id_player = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT id_player FROM Player WHERE nickname = \"" + nick + "\";"));
        int skin_id = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT skin_id FROM Player WHERE id_player = \"" + id_player + "\";"));

        float speed = float.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT speed FROM Ability WHERE id_skins=" + skin_id + ";"));
        scrollSpeed = speed;
        /*if (scrollSpeed <= 0)

        {
            scrollSpeed = -10f;
        }
        else
        {
            scrollSpeed = float.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT speed FROM Ability WHERE id_skins=" + sS + ";"));
        }*/

        //scrollSpeed = 100;
        //hz = 100;
        if (gameOver && Input.GetMouseButtonDown(0)) {
            //string nick = NewBehaviourScript.nickname;
            //int id_player = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT id_player FROM Player WHERE nickname = \"" + nick + "\";"));
            if (MyDataBase.GetTable("SELECT * FROM Scores WHERE id_player = " + id_player + ";").Rows.Count == 0)
            //если у игрока до этго не было очков
            {
                MyDataBase.ExecuteQueryWithoutAnswer("INSERT INTO Scores(id_player, score) VALUES(" + id_player + "," + score + ");");
            }
            else
            {
                MyDataBase.ExecuteQueryWithoutAnswer("UPDATE Person_kab SET sc_die=sc_die+" + 1 + " WHERE player_id=" + id_player + ";");
                MyDataBase.ExecuteQueryWithoutAnswer("UPDATE Scores SET score=" + score + " WHERE id_player=" + id_player + ";");
            }
           
        }
        }

    
    void start()
    {
        string nick = NewBehaviourScript.nickname;
        int id_player = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT id_player FROM Player WHERE nickname = \"" + nick + "\";"));
        int skin_id = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT skin_id FROM Player WHERE id_player = \"" + id_player + "\";"));

        float speed = float.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT speed FROM Ability WHERE id_skins=" + skin_id + ";"));
        scrollSpeed = speed;
        /*if (scrollSpeed <= 0)

        {
            scrollSpeed = -10f;
        }
        else
        {
            scrollSpeed = float.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT speed FROM Ability WHERE id_skins=" + sS + ";"));
        }*/
        float hz = instance.GetComponentInChildren<float>();
        //scrollSpeed = 100;
        //hz = 100;

    }
    
  
    public void BirdScored()
    {

      
        score++;

        scoreText.text = "Score: " + score.ToString();
    }

    public void BirdDied()
    {
        gameOvertext.SetActive(true);
        gameOver = true;
    }
    

 
    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
    }
    public void IncreaseScore(int increase)
    {
        score += increase;
        scoreText.text = "Очки: " + score;
    }
    public void Quit()
    {
        //InputField[] btns = CustomPan.GetComponentsInChildren<UnityEngine.UI.InputField>();
        SceneManager.LoadScene(0);
    }
}