using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Include this - load, unload scenes

public class SceneController : MonoBehaviour
{
    
     public int ScoreValue
    {
        set { scoreValue = value; }
        get { return scoreValue; }
    }

    private int scoreValue;
    private GameController gc;

    public void Start()
    {
        gc = GetComponent<GameController>();
    }

    // Start is called before the first frame update
    // == onClick Event Handlers ==
    public void Start_OnClick()
    {
        SceneManager.LoadScene(2); // Make sure the scene is added in the Build Settings
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Options_OnClick()
    {
        SceneManager.LoadScene(5);
    }

    public void Update()
    {
        
    }
}
