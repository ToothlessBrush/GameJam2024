using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogicManager : MonoBehaviour
{
    private bool pauseState;

    public float delay = 2.0f;

    public void nextLevel()
    {
        Debug.Log("next level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //load next scene in build order
        Debug.Log("loaded level 2");
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
