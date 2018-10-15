using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementScript : MonoBehaviour {
    [SerializeField] float BallSpeed;

    private Rigidbody2D _ballRigidBody;
    private Vector2 _initialDirection;

	// Use this for initialization
	void Start () {
        //Initial Velocity
        _ballRigidBody = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        //for (int i = 0; i < balls.Length; i++)
        //{
        //    Physics2D.IgnoreCollision(balls[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //}

        if (collision.collider.CompareTag("Fence"))
        {
            GameManager.Instance.BallBounceCount++;
            int bounceCount = GameManager.Instance.BallBounceCount;
            string difficulty = PlayerPrefs.GetString("Difficulty");
            if (difficulty == "Easy")
            {
                if (bounceCount == 10) SetBallSpeed(5);
                if (bounceCount == 20) SetBallSpeed(6);
                if (bounceCount == 30) SetBallSpeed(8);
                if (bounceCount == 40) SetBallSpeed(10);
                if (bounceCount == 100) SpawnSheep();
                if (bounceCount == 200) SpawnSheep();
                if (bounceCount == 240) SetBallSpeed(11);
                if (bounceCount == 280) SetBallSpeed(12);
                if (bounceCount == 350) SetBallSpeed(15);
            } 
            else if (difficulty == "Medium")
            {
                if (bounceCount == 10) SetBallSpeed(5);
                if (bounceCount == 20) SetBallSpeed(6);
                if (bounceCount == 30) SetBallSpeed(8);
                if (bounceCount == 40) SetBallSpeed(10);
                if (bounceCount == 50) SpawnSheep();
                if (bounceCount == 80) SpawnSheep();
                if (bounceCount == 120) SetBallSpeed(11);
                if (bounceCount == 160) SetBallSpeed(12);
                if (bounceCount == 200) SetBallSpeed(15);
            }
            else if (difficulty == "Hard")
            {
                if (bounceCount == 10) SetBallSpeed(5);
                if (bounceCount == 20) SpawnSheep();
                if (bounceCount == 30) SetBallSpeed(8);
                if (bounceCount == 40) SetBallSpeed(10);
                if (bounceCount == 50) SetBallSpeed(12);
                if (bounceCount == 80) SetBallSpeed(15);
                if (bounceCount == 120) SpawnSheep();
                if (bounceCount == 160) SetBallSpeed(18);
                if (bounceCount == 200) SetBallSpeed(20);
            }
        }

        if (collision.collider.CompareTag("Ball"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    private void GoBall()
    {
        float xDirection = Random.Range(-1f, 1f);
        float yDirection = Random.Range(-1f, 1f);

        _initialDirection = new Vector2(xDirection, yDirection).normalized;
        _ballRigidBody.AddForce(_initialDirection * BallSpeed);
    }

    private void ResetBall()
    {
        _ballRigidBody.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    private void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }

    private void SetBallSpeed(int inBallSpeed)
    {
        float intialSpeed = BallSpeed;
        BallSpeed = inBallSpeed;
        _ballRigidBody.AddForce(_initialDirection * (BallSpeed - intialSpeed));
    }

    private void SpawnSheep()
    {
        GameObject sheepObj = Instantiate(GameManager.Instance.SheepPrefab);
        sheepObj.transform.SetParent(gameObject.transform.parent);
        sheepObj.transform.position = new Vector2();
        sheepObj.transform.localScale = gameObject.transform.localScale;
    }
}
