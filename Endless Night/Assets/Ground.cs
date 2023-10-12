using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float groundHeight;
    BoxCollider2D colliderG;

    private void Awake()
    {
        colliderG = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (colliderG.size.y / 2);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
