using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Need for the Image component in the icon

public class LifeContainer : MonoBehaviour
{
    [SerializeField] private HeartIcon heartIconPrefab;
    private GameController gc;
    private int startingLives;
    private List<HeartIcon> heartIcons = new List<HeartIcon>();

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        if(gc)
        {
            startingLives = gc.StartingLives;
            CreateHeartIcons();
        }
    }

    private void CreateHeartIcons()
    {
        for (int i = 0; i < startingLives; i++)
        {
            HeartIcon h = Instantiate(heartIconPrefab, transform);
            heartIcons.Add(h);
        }
    }

    public void LoseLife(int index)
    {
        // Use index to make one heart black
        heartIcons[index].GetComponent<Image>().color = Color.black;
    }

    public void AddLife(int index)
    {
        heartIcons[index].GetComponent<Image>().color = Color.red;
    }

  
}
