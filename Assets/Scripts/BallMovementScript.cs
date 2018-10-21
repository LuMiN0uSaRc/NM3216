using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallMovementScript : MonoBehaviour {
    [SerializeField] float BallSpeed;
    [SerializeField] TextMeshProUGUI _gameOverText;
    [SerializeField] AudioSource _bgm;

    public AudioClip GameOverBgm;

    private Rigidbody2D _ballRigidBody;
    private Vector2 _initialDirection;
    private bool _ifCollided = false;

	// Use this for initialization
	void Start () {
        //Initial Velocity
        _ballRigidBody = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_ifCollided)
        {
            if (collision.collider.CompareTag("Fence"))
            {
                GameManager.Instance.BallBounceCount++;
                int bounceCount = GameManager.Instance.BallBounceCount;

                //Update string 
                GameManager.Instance._numberOfBounces.text = "Bounces: " + bounceCount.ToString();
                GameManager.Instance._speedOfCharacter.text = "Speed: " + BallSpeed.ToString();

                string difficulty = PlayerPrefs.GetString("Difficulty");
                if (difficulty == "Easy")
                {
                    Debug.Log("EASY");
                    if (bounceCount == 1) SetBallSpeed(5);
                    if (bounceCount == 20) SetBallSpeed(7);
                    if (bounceCount == 30) SetBallSpeed(9);
                    if (bounceCount == 50)
                    {
                        SpawnSheep();
                        SetAllBallsSpeed(4);
                    }
                    if (bounceCount == 75) SetAllBallsSpeed(5);
                    if (bounceCount == 100) SetAllBallsSpeed(7);
                    if (bounceCount == 150) SetAllBallsSpeed(9);
                    if (bounceCount == 175)
                    {
                        SpawnSheep();
                        SetAllBallsSpeed(4);
                    }
                    if (bounceCount == 200) SetAllBallsSpeed(6);
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
        _ifCollided = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _ifCollided = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Time.timeScale = 0;
        _gameOverText.gameObject.SetActive(true);
        UIManagerScript._gameOverCheck = true;
        _bgm.GetComponent<AudioSource>().clip = GameOverBgm;
        _bgm.GetComponent<AudioSource>().Play();
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
        Debug.Log("OLD: " + BallSpeed);
        BallSpeed = inBallSpeed;
        Debug.Log("NEW: " + BallSpeed);
        _ballRigidBody.AddForce(_initialDirection * (BallSpeed - intialSpeed));
    }

    private void SetAllBallsSpeed(int inBallSpeed)
    {
        GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < allBalls.Length; i++)
        {
            allBalls[i].GetComponent<BallMovementScript>().SetBallSpeed(inBallSpeed);
        }
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
}
