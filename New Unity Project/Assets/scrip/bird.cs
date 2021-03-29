using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bird : MonoBehaviour {
    public float upForce = 200f;
    private bool IsDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    public int score = 0;
	public GameObject Fp;
    private bool isPaused = false;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		if(IsDead == false)
        {
            if(Input.GetMouseButtonDown (0))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Flap");
            }
        }

	}

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Finish")
        {
			Fp.SetActive (true);
            Time.timeScale = 0;
            isPaused = true;
            string nick = NewBehaviourScript.nickname;
            int id_player = int.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT id_player FROM Player WHERE nickname = \"" + nick + "\";"));
            if (MyDataBase.GetTable("SELECT * FROM Scores WHERE id_player = " + id_player + ";").Rows.Count == 0)
            //если у игрока до этго не было очков
            {
                MyDataBase.ExecuteQueryWithoutAnswer("INSERT INTO Scores(id_player, score) VALUES(" + id_player + "," + score + ");");


                if (MyDataBase.GetTable("SELECT coins FROM Player WHERE id_player = " + id_player + ";").Rows.Count == 0)
                //если у игрока до этго не было койнов
                {
                    MyDataBase.ExecuteQueryWithoutAnswer("INSERT INTO Player(id_player, coins) VALUES(" + id_player + "," + score + ");");
                }
                else
                {
                    MyDataBase.ExecuteQueryWithoutAnswer("UPDATE Player SET coins=coins+" + score + " WHERE id_player=" + id_player + ";");
                }
            }
            else
            {
                MyDataBase.ExecuteQueryWithoutAnswer("UPDATE Scores SET score=" + score + " WHERE id_player=" + id_player + ";");
                if (MyDataBase.GetTable("SELECT coins FROM Player WHERE id_player = " + id_player + ";").Rows.Count == 0)
                //если у игрока до этго не было койнов
                {
                    MyDataBase.ExecuteQueryWithoutAnswer("INSERT INTO Player(id_player, coins) VALUES(" + id_player + "," + score + ");");
                }
                else
                {
                    MyDataBase.ExecuteQueryWithoutAnswer("UPDATE Player SET coins=coins+" + score + " WHERE id_player=" + id_player + ";");
                }
            }
            MyDataBase.ExecuteQueryWithoutAnswer("UPDATE Person_kab SET sc_die=sc_die+" + 1 + " WHERE player_id=" + id_player + ";");


        }

    }


  void OnCollisionEnter2D(Collision2D myCollision) 
{
        if (myCollision.gameObject.name == "worm")
        {
            score++;
        }
        else
        {
            IsDead = true;
           
            GameControl.instance.BirdDied();
        }
    }
}

