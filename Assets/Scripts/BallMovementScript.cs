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
        Debug.Log(_ballRigidBody.velocity);
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
            if (bounceCount == 10 ) SpawnSheep();
            if (bounceCount == 20 ) SpawnSheep();
            if (bounceCount == 30 ) SetBallSpeed(10);
            if (bounceCount == 40 ) SetBallSpeed(14);
            if (bounceCount == 50 ) SpawnSheep();
            if (bounceCount == 80 ) SpawnSheep();
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
        BallSpeed = inBallSpeed;
        _ballRigidBody.AddForce(_initialDirection * (BallSpeed - inBallSpeed));
    }

    private void SpawnSheep()
    {
        GameObject sheepObj = Instantiate(GameManager.Instance.SheepPrefab);
        sheepObj.transform.SetParent(gameObject.transform.parent);
        sheepObj.transform.position = new Vector2();
        sheepObj.transform.localScale = gameObject.transform.localScale;
    }
}
