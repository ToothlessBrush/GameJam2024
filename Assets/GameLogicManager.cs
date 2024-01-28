using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogicManager : MonoBehaviour
{
    private int currentLevel;
    private bool pauseState;

    public float delay = 2.0f;

    
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
        StartCoroutine(RestartLevelAfterDelay(delay));
    }

    public IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        restartLevel();
    }

    public void restartLevel()
    {
        //restart level
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
