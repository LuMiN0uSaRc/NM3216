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

        if (_ballRigidBody.velocity.x >= 0)
        {
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
        else
        {
            gameObject.transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
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
            //GameManager.Instance._numberOfBounces.text = "Bounces: " + bounceCount.ToString();
            //GameManager.Instance._speedOfCharacter.text = "Speed: " + BallSpeed.ToString();

            string difficulty = PlayerPrefs.GetString("Difficulty");
            if (difficulty == "Easy")
            {
                if (bounceCount == 5) SetBallSpeed(2.5f);
                if (bounceCount == 10) SetBallSpeed(3f);
                if (bounceCount == 15) SetBallSpeed(3.5f);
                if (bounceCount == 20) SetBallSpeed(4f);
                if (bounceCount == 25)
                {
                    SpawnSheep();
                    SetBallSpeed(1.75f);
                };

                if (bounceCount == 40) SetBallSpeed(2.1f);
                if (bounceCount == 55) SetBallSpeed(2.4f);
                if (bounceCount == 70) SetBallSpeed(2.8f);
                if (bounceCount == 90)
                {
                    SpawnSheep();
                    SetBallSpeed(1.65f);
                };
                if (bounceCount == 110) SetBallSpeed(1.85f);
                if (bounceCount == 125) SetBallSpeed(2.0f);
                if (bounceCount == 150) SetBallSpeed(2.2f);

            }

            // intiial speed 2.5f
            else if (difficulty == "Medium")

            {
                BallSpeed = 2.5f;

                if (bounceCount == 5) SetBallSpeed(3f);
                if (bounceCount == 10) SetBallSpeed(3.5f);
                if (bounceCount == 15) SetBallSpeed(4.15f);
                if (bounceCount == 20) SetBallSpeed(4.75f);
                if (bounceCount == 25)
                {
                    SpawnSheep();
                    SetBallSpeed(1.9f);
                };

                if (bounceCount == 40) SetBallSpeed(2.2f);
                if (bounceCount == 55) SetBallSpeed(2.55f);
                if (bounceCount == 70) SetBallSpeed(2.9f);
                if (bounceCount == 90)
                {
                    SpawnSheep();
                    SetBallSpeed(1.65f);
                };

                if (bounceCount == 110) SetBallSpeed(1.9f);
                if (bounceCount == 125) SetBallSpeed(2.15f);
                if (bounceCount == 150) SetBallSpeed(2.4f);
            }

            // initial speed 3f
            else if (difficulty == "Hard")
            {
                BallSpeed = 3f;

                if (bounceCount == 5) SetBallSpeed(3.8f);
                if (bounceCount == 10) SetBallSpeed(4.45f);
                if (bounceCount == 15) SetBallSpeed(5f);
                if (bounceCount == 25) SetBallSpeed(5.5f);
                if (bounceCount == 30)
                {

                    SpawnSheep();
                    SetBallSpeed(2.1f);
                }
                if (bounceCount == 40) SetBallSpeed(2.55f);
                if (bounceCount == 55) SetBallSpeed(2.9f);
                if (bounceCount == 70) SetBallSpeed(3.25f);
                if (bounceCount == 90)
                {

                    SpawnSheep();
                    SetBallSpeed(1.75f);
                }

                if (bounceCount == 110) SetBallSpeed(2.15f);
                if (bounceCount == 125) SetBallSpeed(2.35f);
                if (bounceCount == 150) SetBallSpeed(2.6f);
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

        if (_ballRigidBody.velocity.x >= 0)
        {
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
        else
        {
            gameObject.transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
        }
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
