using UnityEngine;

public class FitToCamera : MonoBehaviour
{
    // Private variables to store the size and the initial position of the GameObject
    private float length, startpos;

    // Serialized field to assign the camera from the Unity Editor
    [SerializeField] public GameObject cam;

    // The intensity of the parallax effect
    public float parallaxEffect;
//assdadesdfhnuijklo;vz
    void Start()
    {
        // Store the initial x position of the GameObject
        startpos = transform.position.x;

        // Get the width of the sprite attached to this GameObject
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Calculate the position in the parallax movement.
        // This is based on the camera's position and the parallax effect intensity.
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        // Set the new position of the GameObject based on the calculated distance.
        // The y position is hard-coded to 1.4f, which might be specific to your game's design.
        transform.position = new Vector3(startpos + dist, 1.4f, transform.position.z);

        // Check if the GameObject has moved a significant distance (more than its length)
        // and reset its start position. This creates an infinite scrolling effect.
        if (temp > startpos + length)
        {
            startpos += length * 2;
        }
        else if (temp < startpos - length)
        {
            startpos -= length * 2;
        }
    }
}