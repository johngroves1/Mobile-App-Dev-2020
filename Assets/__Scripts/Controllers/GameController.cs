using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    // == public fields ==
    public int playerScore = 0;
    // == private fields ==
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int startingLives = 3;
    private int livesLeft;
    [SerializeField] private int scoreCount = 0;
    private LifeContainer lifeContainer;

    // == properties ==
    public int StartingLives { get {return startingLives;}}
    public int LivesLeft { get {return livesLeft;}}
    public int PlayerScore { get {return playerScore;}}

      private void Awake()
    {
        SetUpSingleton();
    } 
 
    private void Start()
    {
        livesLeft = startingLives;
        lifeContainer = FindObjectOfType<LifeContainer>();
    }
    private void Update()
    {
       
    }

     private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }   

    // deal with the enemy dying
    // subscribe/listen for the EnemyKilledEvent
    private void OnEnable()
    {
        EnemyBehaviour.EnemyKilledEvent +=GameControllerHandlesEnemyDeath;   
        Astroid.AstroidKilledEvent += GameControllerHandlesAstroidDeath;
        EnemyS.EnemySKilledEvent += GameControllerHandlesEnemySDeath;
        EnemyTank.EnemyTankKilledEvent += GameControllerHandlesEnemyTankDeath;
        EnemyW.EnemyWKilledEvent += GameControllerHandlesEnemyWDeath;
        BossBehaviour.BossKilledEvent += GameControllerHandlesBossDeath;
    }

    private void OnDisable()
    {
        EnemyBehaviour.EnemyKilledEvent -=GameControllerHandlesEnemyDeath;
        Astroid.AstroidKilledEvent -= GameControllerHandlesAstroidDeath;
        EnemyS.EnemySKilledEvent -= GameControllerHandlesEnemySDeath;
        EnemyTank.EnemyTankKilledEvent -= GameControllerHandlesEnemyTankDeath;
        EnemyW.EnemyWKilledEvent -= GameControllerHandlesEnemyWDeath;
        BossBehaviour.BossKilledEvent -= GameControllerHandlesBossDeath;
    }


    // == Adds enemy score values to player score on enemy death ==
    private void GameControllerHandlesEnemyDeath(EnemyBehaviour enemy)
    {
        playerScore += enemy.ScoreValue;
        UpdateScreenText();
    }

    private void GameControllerHandlesAstroidDeath(Astroid enemy){
        playerScore += enemy.ScoreValue;
        UpdateScreenText();
    }

     private void GameControllerHandlesEnemySDeath(EnemyS enemy)
    {
        playerScore += enemy.ScoreValue;
        UpdateScreenText();
    } 

    private void GameControllerHandlesEnemyTankDeath(EnemyTank enemy)
    {
        playerScore += enemy.ScoreValue;
        UpdateScreenText();
    } 

    private void GameControllerHandlesEnemyWDeath(EnemyW enemy)
    {
        playerScore += enemy.ScoreValue;
        UpdateScreenText();
    } 

    private void GameControllerHandlesBossDeath(BossBehaviour enemy)
    {
        playerScore += enemy.ScoreValue;
        UpdateScreenText();
    } 

    public void ProcessPlayerDeath()
    {
        if(livesLeft > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void ProcessPlayerLife()
    {
        if(livesLeft < 3)
        {
            GiveLife();
        }
    }

    // Lose one life
    private void TakeLife()
    {
        livesLeft--;
        lifeContainer.LoseLife(livesLeft);
    }

    // Gain one life
    public void GiveLife()
    {
        livesLeft ++;
        lifeContainer.AddLife(livesLeft);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene("Game Over");
        Destroy(gameObject);
    }
    

    // Updates text to Screen and controls Waves by counters 
    private void UpdateScreenText()
    {

        if(playerScore > 1000 && scoreCount == 0) 
        {
           SceneManager.LoadScene("Wave 2");
           scoreCount ++;
           
        }
        else if(playerScore > 5000 && scoreCount == 1)
        {
            SceneManager.LoadScene("Wave 3");
            scoreCount ++;
        }
        else if(playerScore > 10000 && scoreCount == 2)
        {
            SceneManager.LoadScene("Wave 4");
            scoreCount ++;

        }
        else if(playerScore > 20000 && scoreCount == 3)
        {
            // Mutes Wave Music
            GameObject.Find("Wave Music Player").GetComponent<MusicPlayer>().Mute();
            SceneManager.LoadScene("Boss Wave");
            scoreCount ++;
        }
        
        // Sends score to UI
        scoreText.text = playerScore.ToString();
    }

}
