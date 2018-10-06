using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementScript : MonoBehaviour {
    [SerializeField] float BallSpeed;

    private Rigidbody2D _ballRigidBody;
    private int _bounceCount;

	// Use this for initialization
	void Start () {
        //Initial Velocity
        _ballRigidBody = GetComponent<Rigidbody2D>();
        //_ballRigidBody.velocity = Vector2.right * BallSpeed;

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < balls.Length; i++)
        {
            Physics2D.IgnoreCollision(balls[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        Invoke("GoBall", 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fence"))
        {
            _bounceCount++;
            //_ballRigidBody.AddForce(Vector2.left * 0.001f, ForceMode2D.Impulse);
        }
    }

    private void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            _ballRigidBody.AddForce(new Vector2(0.01f, -0.03f));
        }
        else
        {
            _ballRigidBody.AddForce(new Vector2(-0.01f, -0.03f));
        }
    }

    void ResetBall()
    {
        _ballRigidBody.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }
}
