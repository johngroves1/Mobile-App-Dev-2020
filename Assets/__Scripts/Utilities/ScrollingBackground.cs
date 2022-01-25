using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1.0f;
    private Material myMaterial;
    private Vector2 offset;
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        // Swap 0f to get scrollSpeed of y axis
        offset = new Vector2(0f, scrollSpeed);
    }
    
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
