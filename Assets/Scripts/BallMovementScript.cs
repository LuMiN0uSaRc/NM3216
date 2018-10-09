using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementScript : MonoBehaviour {
    [SerializeField] float BallSpeed;

    private Rigidbody2D _ballRigidBody;
    private int _bounceCount;
    private Vector2 _initialDirection;

	// Use this for initialization
	void Start () {
        //Initial Velocity
        _ballRigidBody = GetComponent<Rigidbody2D>();

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
            if (_bounceCount == 10 ) SetBallSpeed(5);
            if (_bounceCount == 20 ) SetBallSpeed(8);
            if (_bounceCount == 30 ) SetBallSpeed(10);
            if (_bounceCount == 40 ) SetBallSpeed(14);
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
}
