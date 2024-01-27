using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextLevelScript : MonoBehaviour
{
    public GameLogicManager GameLogicManager;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameLogicManager == null)
        {
            Debug.LogError("GameLogicManager reference is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when trigger is entered, call nextLevel() from GameLogicManager
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameLogicManager.nextLevel();
        }
    }
}
