using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_color : MonoBehaviour
{

    public float jumpForce = 10f;

    public GameObject particle;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private bool playStarted;
    public GameObject lostPrompt;
    public static int score;
    public static Text scoreBox;
    public static bool isAlive;
    public static int highScore;
    public Text highScoreBox;
    public Text scoreBoxPrompt;
    public GameObject highScoreCongratulate;

    public Color[] color;
    public string[] colorName;
    private int currentColor;
    public AudioSource audio;
    public AudioClip[] clip;

    // Start the game by waking up the player
    private void StartGame()
    {
        rb.WakeUp();
        rb.velocity = Vector2.up * jumpForce;
        playStarted = true;
    }

    // Share the score with other apps
    public void ShareScore()
    {
        ScoreSubmission.SendScore(score);
    }

    // Restart the game
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Set a random color for the player
    private void SetRandomColor()
    {
        currentColor = Random.Range(0, color.Length);
        sr.color = color[currentColor];
    }

    // Play the respective sound
    private void PlaySound(int clipIndex)
    {
        audio.clip = clip[clipIndex];
        audio.Play();
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("score_color", 0);
        isAlive = true;
        score = 0;

        scoreBox = GameObject.Find("ScoreBox").GetComponent<Text>();
        lostPrompt.SetActive(false);
        playStarted = false;

        SetRandomColor();
    }

    void Update()
    {
        // Wait for user input to start the game
        if (!playStarted)
        {
            rb.Sleep();
            if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
        }

        // Handle player input if alive
        if (isAlive && (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "MainCamera" && isAlive)
        {
            Lost();
            return;
        }

        if (col.tag == "Star")
        {
            PlaySound(0);
            return;
        }

        if (col.tag == "ColorChanger")
        {
            PlaySound(1);
            IncrementColor();
            Destroy(col.gameObject);
            return;
        }

        if (col.tag != colorName[currentColor] && isAlive)
        {
            Lost();
        }
        //When encountering a colorChange object, change color to next one
        void IncrementColor()
        {
            currentColor = (currentColor + 1) % 4;
            sr.color = color[currentColor];

        }
        //if player loses then show prompt, set values in textBoxes
        void Lost()
        {
            highScoreCongratulate.SetActive(false);

            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("score_color", score);
                highScoreCongratulate.SetActive(true);
            }

            highScoreBox.text = $"High Score : {highScore}";
            scoreBoxPrompt.text = $"Your score: {score}";

            // Instantiate the particle system and modify its color
            GameObject ps = Instantiate(particle, gameObject.transform.transform.position, Quaternion.identity);
            ParticleSystem particleSystem = ps.GetComponent<ParticleSystem>();

            // Get the main module and modify the start color
            ParticleSystem.MainModule mainModule = particleSystem.main;
            mainModule.startColor = color[currentColor];

            sr.enabled = false;
            rb.Sleep();
            isAlive = false;

            StartCoroutine(Prompt());
        }


        //show prompt after a short delay
        IEnumerator Prompt()
        {
            yield return new WaitForSeconds(.7f);
            lostPrompt.SetActive(true);
            //AdManager.instance.show_ads_ingames();

        }
    }
}