using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicManager : MonoBehaviour
{
    private int currentLevel;
    private bool pauseState;
    private bool isDead;
    public Canvas deathCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void nextLevel()
    {
        Debug.Log("next level");
        currentLevel++;
        //Unity.sceneManagement.loadScene(currentLevel);
    }

    public void characterDeath()
    {
        isDead = true;
        deathCanvas.enabled = true;
    }

    public void restartLevel()
    {
        //restart level
    }
}
