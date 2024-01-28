using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    //public GameObject pausePanel;
    public Canvas PauseCanvas;          // Reference to a PauseCanvas
    // Start is called before the first frame update
<<<<<<< Updated upstream
    //public AudioSource introAudioSource; // Assign this in the Unity Editor with the intro clip
=======
>>>>>>> Stashed changes
    

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
    void Start(){
        PauseCanvas.enabled = false;
<<<<<<< Updated upstream
        //introAudioSource.Play();
=======
>>>>>>> Stashed changes
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
