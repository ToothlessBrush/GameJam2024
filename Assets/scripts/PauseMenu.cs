using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    //public GameObject pausePanel;
    public Canvas PauseCanvas;          // Reference to a PauseCanvas
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseCanvas.enabled = true;
        //pausePanel.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseCanvas.enabled = false;
        //pausePanel.SetActive(false);
        isPaused = false;
    }
}
