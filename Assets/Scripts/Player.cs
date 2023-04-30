using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 2.0f;
    [SerializeField] private GameObject letterCounter;
    [SerializeField] private GameObject mailBoxCounter;
    [SerializeField] private GameObject closedMailBox;
    [SerializeField] private GameObject dropLetter;
    [SerializeField] private Director director;
    [SerializeField] private float maxXOff;
    public float horizontalSpeed = 3f;
    private float realhorizontalSpeed = 3f;
    public bool timeInverse = false;
    private bool first = true;
    [SerializeField] private GameObject inverseEffect;

    private Rigidbody2D rigidbody;

    private int postcards = 0;
    private int score = 0;

    private bool boostActive = false;
    private bool slowActive = false;

    public int difficulty = -10;
    private float toNextDiff = 0f;

    void Start()
    {
        if(PlayerPrefs.GetInt("Runs") <= 0)
        {
            PlayerPrefs.SetInt("Runs", 0);
        }
        else
        {
            difficulty = 0;
            realhorizontalSpeed = 5f;
            toNextDiff = 20f;
        }
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inverseEffect.SetActive(timeInverse);
        horizontalSpeed = timeInverse ? -realhorizontalSpeed : realhorizontalSpeed;
        if(toNextDiff <= 0f && toNextDiff >= -2.0f)
        {
            toNextDiff = -3.0f;
            difficulty += 1;
            if(difficulty == -9)
            {
                director.ShowText("Welcome to your first day at the storm dove squadron!", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -8)
            {
                director.ShowText("Our squadron is specialized in recovering and delivering mail during the heaviest storms!", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -7)
            {
                director.ShowText("The only thing that matters is that the post ends in the mailbox.", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -6)
            {
                director.ShowText("Now, dont worry on how to do this job I will tell you on the flight!", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -5)
            {
                director.ShowText("Use WASD or the arrow keys to adjust your position in the storm.", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -4)
            {
                director.ShowText("You will have to use this movement when avoiding objects that the storm lifted from the ground.", 18f);
                toNextDiff = 16f;
            }
            else if(difficulty == -3)
            {
                director.ShowText("But now to your job, you will collect letters that got caught in the storm...", 18f);
                toNextDiff = 16f;
            }
            else if(difficulty == -2)
            {
                director.ShowText("...and deliver them to the mailboxes that got ripped from the ground.", 18f);
                toNextDiff = 16f;
            }
            else if(difficulty == -1)
            {
                director.ShowText("A letter can work protective against an obstacle but you will loose it afterwards.", 18f);
                toNextDiff = 6f;
            }
            else if(difficulty == 0)
            {
                director.ShowText("The storm is going to get stronger so be careful out there.", 18f);
                toNextDiff = 20f;
                realhorizontalSpeed = 5f;
            }
            else if(difficulty == 1)
            {
                toNextDiff = 10f;
            }
            else if(difficulty > 1 && difficulty < 5)
            {
                toNextDiff = 5f;
                realhorizontalSpeed += 0.25f;
            }
            else if(difficulty == 5)
            {
                director.ShowText("We just got some lightning warnings. You better change height when you see some sparks!", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty > 5 && difficulty < 10)
            {
                toNextDiff = 5f;
                realhorizontalSpeed += 0.25f;
            }
            else if(difficulty == 10)
            {
                director.ShowText("One of our other workers just got hit by a strong upwind, sp have an eye on whats below you.", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty > 10 && difficulty < 20)
            {
                toNextDiff = 5f;
                realhorizontalSpeed += 0.25f;
            }
            else if(difficulty == 20)
            {
                director.ShowText("Our ground team has meassured temporal waves, if you get hit by one, the exit should be a round shape.", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty > 20)
            {
                toNextDiff = 5f;
                realhorizontalSpeed += 0.25f;
            }
        }
        else
            toNextDiff -= Time.deltaTime;


        letterCounter.GetComponent<TextMeshProUGUI>().text = postcards.ToString();
        mailBoxCounter.GetComponent<TextMeshProUGUI>().text = score.ToString();

        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, (verticalSpeed - Mathf.Min(1.5f, postcards / 6.7f)));
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -(verticalSpeed - Mathf.Min(1.5f, postcards / 6.7f)));
        }
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            boostActive = true;
        }
        else
        {
            boostActive = false;
        }
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            slowActive = true;
        }
        else
        {
            slowActive = false;
        }
        if(transform.position.x <= (slowActive ? -maxXOff : -0.1f)|| (boostActive && transform.position.x < maxXOff))
        {
            rigidbody.velocity = new Vector2(boostActive ? 6.0f: 3.0f, rigidbody.velocity.y);
        }
        else if(transform.position.x >= 0.1f || (slowActive && transform.position.x > -maxXOff))
        {
            rigidbody.velocity = new Vector2(slowActive ? -6.0f : -3.0f, rigidbody.velocity.y);
        }
        else
        {
            rigidbody.velocity = new Vector2(0.0f, rigidbody.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Letter" && postcards < 10)
        {
            postcards += 1;
            Destroy(col.gameObject);
        }
        else if(col.gameObject.tag == "MailBox" && postcards > 0)
        {
            postcards -= 1;
            score += 1;
            GameObject closedM = Instantiate(closedMailBox, col.gameObject.transform.position, Quaternion.identity);
            closedM.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "TimeVortex")
        {
            if(timeInverse && first)
            {
                director.ShowText("What do you say? Evil mailboxes? We will have to investigate this, stay careful.", 18f);
                first = false;
            }
            timeInverse = !timeInverse;
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Obstacle")
        {
            if(postcards > 0)
            {
                Destroy(col.gameObject);
                postcards -= 1;
                GameObject dl = Instantiate(dropLetter, transform.position, Quaternion.identity);
                dl.GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalSpeed, 1.0f);
            }
            else
                Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            if(postcards > 0)
            {
                Destroy(collision.gameObject);
                postcards -= 1;
                GameObject dl = Instantiate(dropLetter, transform.position, Quaternion.identity);
                dl.GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalSpeed, 1.0f);
            }
            else
                Die();
        }
    }

    public void Die()
    {
        if(difficulty >= 0)
        {
            int run = PlayerPrefs.GetInt("Runs") + 1;
            PlayerPrefs.SetInt("Score"+run, score);
            PlayerPrefs.SetInt("Runs", run);
        }
        SceneManager.LoadScene(2);
    }
}
