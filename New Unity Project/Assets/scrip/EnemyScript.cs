using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D theCollision)
    {
            Destroy(this.gameObject);
        GameControl controller = GameObject.FindGameObjectWithTag("GameControl").GetComponent("GameControl") as GameControl;
        controller.KilledEnemy();
        controller.IncreaseScore(1);
    }
    }
