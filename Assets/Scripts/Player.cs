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
    [SerializeField] private GameObject bigClosedMailBox;
    [SerializeField] private GameObject dropLetter;
    [SerializeField] private Director director;
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private float maxXOff;
    public float horizontalSpeed = 3f;
    private float realhorizontalSpeed = 3f;
    public bool timeInverse = false;
    [SerializeField] private GameObject inverseEffect;
    [SerializeField] private AudioClip menu;
    [SerializeField] private SoundEffectPlayer effects;
    [SerializeField] private GameObject letterCollect;
    [SerializeField] private GameObject letterDeliver;
    [SerializeField] private GameObject aura;
    [SerializeField] private float auraCooldown = 5f;

    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossDeath;
    public bool bossFight = false;

    [SerializeField] private GameObject birdDead;
    private bool dead = false;
    private float deathCountdown = 2f;

    private bool victory = false;
    private float victoryCountdown = 2f;

    private float currentAuraCooldown = 0f;
    private bool hasAura = false;

    private Rigidbody2D rigidbody;

    private int postcards = 0;
    private int score = 0;

    private bool boostActive = false;
    private bool slowActive = false;

    public int difficulty = -11;
    private float toNextDiff = 0f;
    private int difficultyCap = 4;
    private int level;
    private int goal;

    private bool paused = false;
    private AudioSource audioSource;

    private float speed = 1f;

    private GameObject bossspwnd;

    public void SetSpeed(float value)
    {
        speed = Mathf.Min(Mathf.Max(value, 0.25f), 2f);
        PlayerPrefs.SetFloat("speed", speed);
    }

    void Start()
    {
        speed = PlayerPrefs.GetFloat("speed");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = menu;
        audioSource.loop = true;
        audioSource.Play();

        level = PlayerPrefs.GetInt("level");
        if(level < 0)
        {
            PlayerPrefs.SetInt("level", 0);
            level = 0;
        }
        if(level == 0)
        {
            difficulty = -12;
            difficultyCap = 4;
            goal = 5;
        }
        else if(level == 1)
        {
            difficulty = 0;
            difficultyCap = 9;
            goal = 10;
            realhorizontalSpeed = 5f;
        }
        else if(level == 2)
        {
            difficulty = 5;
            difficultyCap = 15;
            goal = 15;
            realhorizontalSpeed = 6f;
        }
        else if(level == 3)
        {
            difficulty = 10;
            difficultyCap = 29;
            goal = 15;
            realhorizontalSpeed = 8f;
        }
        else if(level == 4)
        {
            difficulty = 20;
            difficultyCap = 35;
            goal = 30;
            realhorizontalSpeed = 10f;
        }
        else if(level == 5)
        {
            difficulty = 39;
            difficultyCap = 58;
            goal = -1;
            realhorizontalSpeed = 10f;
        }
        else
        {
            difficulty = 40;
            goal = -1;
            difficultyCap = -1;
            realhorizontalSpeed = 8f;
            hasAura = true;
        }
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        if(victory)
        {
            if(victoryCountdown < 0f)
                SceneManager.LoadScene(3);
            else
                victoryCountdown -= Time.deltaTime;
            
            return;
        }
        else if(dead)
        {
            if(deathCountdown <= 0f)
                SceneManager.LoadScene(level <= 5 ? 4 : 2);
            else
                deathCountdown -= Time.deltaTime;

            return;
        }
        currentAuraCooldown -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            paused = !paused;
        
        if(paused)
        {
            Time.timeScale = 0;
            pauseOverlay.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseOverlay.SetActive(false);
        }

        if(score >= goal && goal > 0)
        {
            Finish();
        }

        inverseEffect.SetActive(timeInverse);
        horizontalSpeed = (timeInverse ? -realhorizontalSpeed : realhorizontalSpeed) * speed;
        if(toNextDiff <= 0f && toNextDiff >= -2.0f && (difficulty < difficultyCap || difficultyCap < 0))
        {
            toNextDiff = -3.0f;
            difficulty += 1;
            if(difficulty == -11)
            {
                director.ShowText("Press Escape or P to pause the game or adjust the difficulty.", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -10)
            {
                director.ShowText("Press E to hide dialogue. Press R to skip the tutorial.", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -9)
            {
                director.ShowText("Welcome to your first day at the Storm Dove squadron!", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -8)
            {
                director.ShowText("Our squadron is specialized in recovering and delivering mail during the heaviest of storms!", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -7)
            {
                director.ShowText("The only thing that matters is that the post ends in the mailbox.", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -6)
            {
                director.ShowText("You will learn the different aspects of the job the deeper into the storm we get.", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -5)
            {
                director.ShowText("Use WASD or the arrow keys to adjust your position on the screen.", 8f);
                toNextDiff = 6f;
            }
            else if(difficulty == -4)
            {
                director.ShowText("You will have to avoid objects that the storm lifted from the ground.", 18f);
                toNextDiff = 16f;
            }
            else if(difficulty == -3)
            {
                director.ShowText("But now to your job. You will collect letters that got caught in the storm...", 18f);
                toNextDiff = 16f;
            }
            else if(difficulty == -2)
            {
                director.ShowText("...and deliver them to the mailboxes that got ripped from the ground!", 18f);
                toNextDiff = 16f;
            }
            else if(difficulty == -1)
            {
                director.ShowText("A letter can be used to protect you from obstacles, but you will lose the letter.", 18f);
                toNextDiff = 6f;
            }
            else if(difficulty == 0)
            {
                director.ShowText("You first mission is to deliver five letters to their mailboxes.", 18f);
                toNextDiff = 20f;
                realhorizontalSpeed = 5f;
            }
            else if(difficulty == 1)
            {
                if (level == 1)
                    director.ShowText("This time we will send you further into the storm, and you must deliver 10 letters.", 18f);
                toNextDiff = 10f;
            }
            else if(difficulty == 5)
            {
                director.ShowText("We've just gotten some lightning warnings. Avoid the sparks, as that is were lightning will appear!", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 6 && level == 2)
            {
                director.ShowText("You are getting familiar with this job, now you must deliver 15 more letters!", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 10)
            {
                director.ShowText("One of our other workers just got hit by a strong upwind, so keep an eye on whats below you.", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 11 && level == 3)
            {
                director.ShowText("With all your experience, we can send you deeper. Deliver another 15 letters.", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 16)
            {
                director.ShowText("The ground team is getting some strange readings, something is coming!", 5f);
                toNextDiff = 5f;
                realhorizontalSpeed += 0.25f;
            }
            else if(difficulty == 17)
            {
                director.ShowText("The strange readings intensify and our clocks have stopped working.", 5f);
                toNextDiff = 5f;
                realhorizontalSpeed += 0.25f;
            }
            else if(difficulty == 19)
            {
                director.ShowText("After a short analysis it looks like a temporal phenomenon, it could be dangerous.", 5f);
                toNextDiff = 5f;
                realhorizontalSpeed += 0.25f;
            }
            else if(difficulty == 20)
            {
                director.ShowText("The scientists have discovered the exit to the temporal rifts is round in shape.", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 21 && level == 4)
            {
                director.ShowText("You are a natural at this job! Deliver 30 letters in this stronger region of the storm.", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 30)
            {
                director.ShowText("The storm is now strong enough to lift up the big stuff, be careful!", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 31)
            {
                director.ShowText("Your mission today is to scout around the center of the storm, wait for further instructions", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 40)
            {
                director.ShowText("Predator birds have been sighted. Stay safe, they are really fast.", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 41 && level > 5)
            {
                director.ShowText("All missions have been completed, deliver as many letters as you can.", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 42 && level > 5)
            {
                director.ShowText("Based on the results of our research we fabricated a shield against time waves.", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 43 && level > 5)
            {
                director.ShowText("Activate it with Q, it takes half a second to boot up and holds for 3 seconds.", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 45 && level == 5)
            {
                director.ShowText("The origin of the storm is closing to you, keep close so we can act countermeassures.", 18f);
                bossspwnd = Instantiate(boss, new Vector3(12f, 12f, 0f), Quaternion.identity);
                bossspwnd.GetComponent<Boss>().player = gameObject;
                bossFight = true;
                toNextDiff = 5f;
            }
            else if(difficulty == 55 && level == 5)
            {
                Instantiate(bossDeath, bossspwnd.transform.position, Quaternion.identity);
                Destroy(bossspwnd);
                director.ShowText("Nice work, we were able to elimate what ever it was with a long range attack.", 18f);
                toNextDiff = 5f;
            }
            else if(difficulty == 56 && level == 5)
            {
                Finish();
            }
            else if(difficulty % 5 == 0)
            {
                toNextDiff = 5f;
                realhorizontalSpeed -= 0.50f;
            }
            else
            {
                toNextDiff = 5f;
                realhorizontalSpeed += 0.25f;
            }
        }
        else
            toNextDiff -= Time.deltaTime;


        letterCounter.GetComponent<TextMeshProUGUI>().text = postcards.ToString();
        mailBoxCounter.GetComponent<TextMeshProUGUI>().text = score.ToString() + (goal > 0 ? " / " + goal : "");

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

        if(Input.GetKey(KeyCode.E))
        {
            if(difficulty < 0)
            {
                toNextDiff = 0.2f;
            }
            director.End();
        }

        if(Input.GetKey(KeyCode.R) && difficulty < 0)
        {
            difficulty = 0;
            toNextDiff = 5f;
            director.End();
        }

        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.RightShift))
        {
            if(currentAuraCooldown < 0f && hasAura)
            {
                GameObject spwnd = Instantiate(aura, transform.position, Quaternion.identity);
                spwnd.transform.parent = transform;
                currentAuraCooldown = auraCooldown;
            }
        }

        if(boostActive && transform.position.x < maxXOff)
        {
            rigidbody.velocity = new Vector2(8.0f, rigidbody.velocity.y);
        }
        else if(slowActive && transform.position.x > -maxXOff)
        {
            rigidbody.velocity = new Vector2(-8.0f, rigidbody.velocity.y);
        }
        else
        {
            rigidbody.velocity = new Vector2(0.0f, rigidbody.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(victory)
            return;
        if(col.gameObject.tag == "Letter" && postcards < 10)
        {
            postcards += 1;
            Destroy(col.gameObject);
            GameObject letDel = Instantiate(letterCollect, col.gameObject.transform.position, Quaternion.identity);
            letDel.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            effects.Letter();
        }
        else if(col.gameObject.tag == "LetterBox" && postcards < 10)
        {
            postcards += 4;
            postcards = Mathf.Min(10, postcards);
            Destroy(col.gameObject);
            GameObject letDel = Instantiate(letterCollect, col.gameObject.transform.position, Quaternion.identity);
            letDel.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            effects.Letter();
        }
        else if(col.gameObject.tag == "MailBox" && postcards > 0)
        {
            postcards -= 1;
            score += 1;
            GameObject closedM = Instantiate(closedMailBox, col.gameObject.transform.position, Quaternion.identity);
            closedM.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            Destroy(col.gameObject);
            GameObject letDel = Instantiate(letterDeliver, col.gameObject.transform.position, Quaternion.identity);
            letDel.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            effects.MailBox();
        }
        else if(col.gameObject.tag == "BigMailBox" && postcards >= 4)
        {
            postcards -= 4;
            score += 5;
            GameObject closedM = Instantiate(bigClosedMailBox, col.gameObject.transform.position, Quaternion.identity);
            closedM.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            Destroy(col.gameObject);
            GameObject letDel = Instantiate(letterDeliver, col.gameObject.transform.position, Quaternion.identity);
            letDel.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            effects.MailBox();
        }
        else if (col.gameObject.tag == "TimeVortex")
        {
            timeInverse = !timeInverse;
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Obstacle")
        {
            if(postcards > 0)
            {
                effects.Collide();
                if(!col.gameObject.GetComponent<Boss>())
                    Destroy(col.gameObject);
                postcards -= 1;
                GameObject dl = Instantiate(dropLetter, transform.position, Quaternion.identity);
                dl.GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalSpeed, 1.0f);
            }
            else
                Die();
        }
        else if (col.gameObject.tag == "BigObstacle")
        {
            if(postcards > 0)
            {
                effects.Collide();
                Destroy(col.gameObject.GetComponent<Collider2D>());
                postcards -= 1;
                GameObject dl = Instantiate(dropLetter, transform.position, Quaternion.identity);
                dl.GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalSpeed, 1.0f);
            }
            else
                Die();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            if(postcards > 0)
            {
                effects.Collide();
                if(!col.gameObject.GetComponent<Boss>())
                    Destroy(col.gameObject);
                postcards -= 1;
                GameObject dl = Instantiate(dropLetter, transform.position, Quaternion.identity);
                dl.GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalSpeed, 1.0f);
            }
            else
                Die();
        }
        else if (col.gameObject.tag == "BigObstacle")
        {
            if(postcards > 0)
            {
                effects.Collide();
                Destroy(col.gameObject.GetComponent<Collider2D>());
                postcards -= 1;
                GameObject dl = Instantiate(dropLetter, transform.position, Quaternion.identity);
                dl.GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalSpeed, 1.0f);
            }
            else
                Die();
        }
    }

    public void Finish()
    {
        effects.Victory();
        PlayerPrefs.SetInt("level", level + 1);
        GameObject letDel = Instantiate(letterDeliver, new Vector3(0f, 0f, 0f), Quaternion.identity);
        letDel.transform.localScale = new Vector3(4f, 4f, 4f);
        victory = true;
    }

    public void Die()
    {
        effects.Death();
        dead = true;
        if(difficulty >= 0 && level >= 5)
        {
            PlayerPrefs.SetInt("lastScore", score);
            if(PlayerPrefs.GetInt("highscore") < score)
                PlayerPrefs.SetInt("highscore", score);
        }
        transform.localScale = new Vector3(0f, 0f, 0f);
        GameObject dl = Instantiate(birdDead, transform.position, Quaternion.identity);
        dl.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 1.0f);
    }
}
