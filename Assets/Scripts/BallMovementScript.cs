using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallMovementScript : MonoBehaviour {
    [SerializeField] float BallSpeed;

    private Rigidbody2D _ballRigidBody;
    private Vector2 _initialDirection;
    private AudioSource _audioSource;
    private Animator _animator;

    //when fence appears, push the sheep back in
    //when sheep exits the area, gameover!

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        //Initial Velocity
        _ballRigidBody = GetComponent<Rigidbody2D>();
        float xDirection = Random.Range(-1f, 1f);
        float yDirection = Random.Range(-1f, 1f);

        _initialDirection = new Vector2(xDirection, yDirection).normalized;

        Invoke("GoBall", 3);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fence"))
        {
            //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider, true);
            float xDirection = Random.Range(-1f, 1f);
            float yDirection = Random.Range(-1f, 1f);
            Vector2 _randomDirection = new Vector2(xDirection, yDirection).normalized;
            _ballRigidBody.velocity = _randomDirection.normalized * BallSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            //Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            _ballRigidBody.velocity = _initialDirection.normalized * BallSpeed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //_ifStuck = false;

        if (collision.collider.CompareTag("Fence"))
        {
            float randomValue = Random.value;
            if (randomValue < 0.3)
            {
                _animator.SetTrigger("Stun");
            }
            else if (randomValue >= 0.3 && randomValue < 0.6)
            {
                _animator.SetTrigger("Sad");
            }
            else
            {
                _animator.SetTrigger("Angry");
            }
            GameManager.Instance.BallBounceCount++;
            if (GameManager.Instance.numOfSoundPlaying < 2)
            {
                Invoke("IfAudioFinished", _audioSource.clip.length);
                _audioSource.Play();
                GameManager.Instance.numOfSoundPlaying++;
            }
            int bounceCount = GameManager.Instance.BallBounceCount;

            //Update string 
            GameManager.Instance._numberOfBounces.text = "Bounces: " + bounceCount.ToString();
            GameManager.Instance._speedOfCharacter.text = "Speed: " + BallSpeed.ToString();

            string difficulty = PlayerPrefs.GetString("Difficulty");
            if (difficulty == "Easy")
            {
                if (bounceCount == 5) SetBallSpeed(2.5f);
                if (bounceCount == 10) SetBallSpeed(3);
                if (bounceCount == 15) SetBallSpeed(3.5f);
                if (bounceCount == 20) SetBallSpeed(4);
                if (bounceCount == 25)
                {
                    SpawnSheep();
                    SetBallSpeed(2);
                };

                if (bounceCount == 45) SetBallSpeed(3);
                if (bounceCount == 70) SetBallSpeed(5);
                if (bounceCount == 90)
                {
                    SpawnSheep();
                    SetBallSpeed(2);
                };
                if (bounceCount == 110) SetBallSpeed(3);
                if (bounceCount == 150) SetBallSpeed(5);

            }

            else if (difficulty == "Medium")
            {
                if (bounceCount == 5) SetBallSpeed(3);
                if (bounceCount == 10) SetBallSpeed(3.5f);
                if (bounceCount == 20) SetBallSpeed(4);
                if (bounceCount == 25)
                {

                    SpawnSheep();
                    SetBallSpeed(3);
                }
                if (bounceCount == 40) SetBallSpeed(3);
                if (bounceCount == 60) SetBallSpeed(4);
                if (bounceCount == 80) SetBallSpeed(5);
                if (bounceCount == 100)
                {

                    SpawnSheep();
                    SetBallSpeed(2);
                }

            }
            else if (difficulty == "Hard")
            {
                if (bounceCount == 5) SetBallSpeed(4);
                if (bounceCount == 10) SetBallSpeed(5);
                if (bounceCount == 15) SetBallSpeed(6);
                if (bounceCount == 25) SetBallSpeed(7);
                if (bounceCount == 30)
                {

                    SpawnSheep();
                    SetBallSpeed(3);
                }
                if (bounceCount == 40) SetBallSpeed(4);
                if (bounceCount == 50) SetBallSpeed(6);
                if (bounceCount == 75) SetBallSpeed(8);
                if (bounceCount == 100)
                {

                    SpawnSheep();
                    SetBallSpeed(3);
                }
            }
        }
        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider, false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Time.timeScale = 0;
        UIManagerScript._gameOverCheck = true;
        UIManagerScript.Instance.OpenGameOverPanel();
    }

    private void GoBall()
    {
        //_ballRigidBody.AddForce(_initialDirection * BallSpeed);
        _ballRigidBody.velocity = _initialDirection.normalized * BallSpeed;
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

    private void SetBallSpeed(float inBallSpeed)
    {
        float initialSpeed = BallSpeed;
        BallSpeed = inBallSpeed;
        //_ballRigidBody.AddForce(_initialDirection * (BallSpeed - initialSpeed));
        _ballRigidBody.velocity = _initialDirection.normalized * inBallSpeed;
        //_ballRigidBody.velocity = _ballRigidBody.velocity.normalized * (BallSpeed - initialSpeed);
    }

    private void SpawnSheep()
    {
        if (GameManager.Instance.NumOfSheeps < 3)
        {
            GameObject sheepObj = Instantiate(GameManager.Instance.SheepPrefab);
            sheepObj.transform.SetParent(gameObject.transform.parent);
            sheepObj.transform.position = new Vector2();
            sheepObj.transform.localScale = gameObject.transform.localScale;
            GameManager.Instance.NumOfSheeps++;
        }
    }

    private void IfAudioFinished()
    {
        GameManager.Instance.numOfSoundPlaying--;
    }
}
