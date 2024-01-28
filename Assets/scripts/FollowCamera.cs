using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Serialized fields are visible and editable in the Unity Editor. 
    // They allow you to easily adjust parameters without changing the script.

    // 'offset' determines the relative position of the camera to the target.
    [SerializeField] private Vector3 offset = new Vector3(0f, 2f, -10f);

    // 'smoothTime' controls the smoothness of the camera movement.
    private float smoothTime = .25f;

    // 'velocity' is used internally by SmoothDamp function.
    private Vector3 velocity = Vector3.zero;

    // References to the targets for the camera: the player and the main menu.
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Transform mainMenuTarget;

    // Reference to the Bandit script for controlling the character's movement.
    [SerializeField] private Bandit bandit;

    // 'currentTarget' keeps track of the camera's current target.
    private Transform currentTarget;

    public bool startTargetOnPlayer = false;
    public testCamera testCamera;

    public bool RestrictCameraHeight = false;
    public float RestrictCameraHeightValue = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (startTargetOnPlayer)
        {
            Debug.Log("start Target On Player");
            currentTarget = playerTarget;
            bandit.setMovement(true);
            testCamera.playGame();
        }
        else
        {
            currentTarget = mainMenuTarget;
            bandit.setMovement(false);
        }
        
        transform.position = mainMenuTarget.position + offset;

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the desired position of the camera by adding the offset to the current target's position.
        Vector3 targetPosition = currentTarget.position + offset;

        if (RestrictCameraHeight)
        {
            if (targetPosition.y > RestrictCameraHeightValue)
            {
                targetPosition.y = RestrictCameraHeightValue;
            }
        }

        // Smoothly move the camera towards the target position.
        // 'transform.position' is the current position of the camera.
        // 'targetPosition' is where we want the camera to go.
        // 'velocity' is a reference variable for the SmoothDamp function to store the current velocity.
        // 'smoothTime' determines how quickly the camera moves to the target position.
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    // Public method to switch the camera's target.
    public void switchTarget()
    {
        // If the current target is the player, switch to the main menu.
        if (currentTarget == playerTarget)
        {
            currentTarget = mainMenuTarget;

            // Disable the bandit's movement when focusing on the main menu.
            bandit.setMovement(false);
        }
        // If the current target is not the player (i.e., the main menu), switch to the player.
        else
        {
            currentTarget = playerTarget;

            // Enable the bandit's movement when focusing on the player.
            bandit.setMovement(true);
        }
    }
}