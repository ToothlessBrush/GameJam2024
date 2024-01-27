using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testCamera : MonoBehaviour
{
    // Public variables are accessible from the Unity Editor. They can be used to assign references to other objects in the scene.
    public FollowCamera followCamera; // Reference to the FollowCamera script component.
    public Button button;             // Reference to a UI Button component.
    public Canvas gameCanvas;         // Reference to a Canvas component.

    // playGame is a public method which can be linked to UI events (like button clicks) in the Unity Editor.
    public void playGame()
    {
        // Logs a message to the console when the method is called. Useful for debugging to confirm that this method is being triggered.
        Debug.Log("game started test should be gone");

        // Calls a method on the followCamera script to potentially switch the camera's target.
        followCamera.switchTarget();

        // Check if the button reference is not null (i.e., has been assigned in the Unity Editor).
        if (button != null)
        {
            // If the button is valid, make it non-interactable to prevent further clicks.
            button.interactable = false;

            // Also, disable the Canvas component, which will hide it and all of its child UI elements in the game.
            gameCanvas.enabled = false;
        }
        else
        {
            // If the button reference is null (not assigned), logs a warning to the console. This is helpful for debugging.
            Debug.LogWarning("Canvas reference not set in testCamera");
        }
    }
}