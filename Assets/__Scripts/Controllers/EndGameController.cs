using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start_OnClick()
    {
        SceneManager.LoadScene(0); // Load Menu
    }
}
