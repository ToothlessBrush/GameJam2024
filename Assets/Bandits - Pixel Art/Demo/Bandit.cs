using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{

    public GameLogicManager GameLogicManager;

    // Serialized fields allow you to set these values from the Unity Editor, 
    // and also keeps them private to the script.
    [SerializeField] float m_speed = 4.0f;            // Movement speed of the bandit
    [SerializeField] float m_jumpForce = 7.5f;        // Force applied when the bandit jumps

    // Private variables for internal use
    private Animator m_animator;                      // Animator component for handling animations
    private Rigidbody2D m_body2d;                     // Rigidbody2D component for physics
    private Sensor_Bandit m_groundSensor;             // Custom sensor for detecting if the bandit is grounded
    private bool m_grounded = false;                  // Flag to check if the bandit is on the ground
    private bool m_combatIdle = false;                // Flag for combat idle state
    private bool m_isDead = false;                    // Flag to check if the bandit is dead

    private bool movement = false;                    // Flag to control the movement of the bandit

    public int health = 3;

    public float backwardForce = 5.0f;

    private bool isInvolnerable = false;
    public float invulnerabilityDuration = 1.0f;
    private float invulnerabilityTimer = 0.0f;
    [SerializeField] public int jCount = 1;
    [SerializeField] public int jCountReset = 1;

    // Initialization
    void Start()
    {
        // Find all objects with the specified tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Ignore collision with each enemy
        foreach (GameObject enemy in enemies)
        {
            // Ignore collision between this object's collider and the enemy's collider
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>());
        }

        // Getting components from the GameObject to which this script is attached
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movement)
        {
            return;
        }

        if (isInvolnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;

            if (invulnerabilityTimer <= 0)
            {
                isInvolnerable = false;
            }
        }

        // Check if the bandit just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            jCount = jCountReset;
            Debug.Log("Grounded");
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // Check if the bandit just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // Handle input and movement
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move the bandit
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // Set AirSpeed in animator for jump/fall animations
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // Handle Animations based on user input
        // Death
        if (Input.GetKeyDown("e"))
        {
            if (!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }

        // Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        // Attack
        else if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Attack");
        }

        // Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        // Jump
        else if (Input.GetKeyDown("space") && jCount > 0)
        {
            if (jCount >= 1)
            {
                jCount--;
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }
        }

        // Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        // Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        // Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    // Public method to enable or disable movement externally
    public void setMovement(bool m)
    {
        movement = m;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isInvolnerable)
        {
            Debug.Log("hit Enemy");

            health--;

            Debug.Log("health: " + health);

            if (health <= 0)
            {
                killPlayer();
                return;
            }


            m_animator.SetTrigger("Hurt");

            isInvolnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;




            //collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
        }
    }

    private void killPlayer()
    {
        m_animator.SetTrigger("Death");
        movement = false;
        GameLogicManager.characterDeath();
    }
}