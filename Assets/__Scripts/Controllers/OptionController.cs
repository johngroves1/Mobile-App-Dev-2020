using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionController : MonoBehaviour
{
    public void Back_OnClick()
    {
        SceneManager.LoadScene(0); 
    }
}
