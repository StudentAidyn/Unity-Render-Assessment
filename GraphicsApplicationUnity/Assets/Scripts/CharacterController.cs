using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CharacterController : MonoBehaviour
{
    // Health will always be set to the max health on start unless altered otherwise
    [SerializeField] const float MAX_HEALTH = 100f;
    float m_health = MAX_HEALTH;
    int m_healthHash;
    public float m_difference = 5f;

    public float GetCurrentHealth() { return m_health; }

    // Animation Timers
    const float EXTRA_TIMER_MAX = 20f;
    [SerializeField] float m_extraTimer;
    [SerializeField] float m_reviveTimer = 5f;

    // Animator Component Reference
    Animator m_animator;

    // post processing controls
    [SerializeField] PostProcessVolume m_volume;
    private Vignette m_vignette;

    [SerializeField] float m_vignetteIntensity;
    [SerializeField] float m_vignetteThreshold;
    [SerializeField] float m_speed;
    [SerializeField] float m_frequency = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // assigning animator
        m_animator = GetComponent<Animator>();
        m_healthHash = Animator.StringToHash("Health");
        setHealth();

        // Post processing settings
        m_volume.profile.TryGetSettings(out m_vignette);
        m_vignette.intensity.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_extraTimer -= 1 * Time.deltaTime;
        if (m_extraTimer <= 0) {
            // play animation
            // reset timer
            m_animator.SetTrigger("Extra");
            ResetTimer();
        }
        if(m_health <= m_vignetteThreshold) // creates vignette low health effect when health drops below a threshold
        {
            // sets a heart beat effect (using Cos waves) using vignette screen effect, being based on the health + clamp
            float intensity = m_vignetteIntensity - (m_health / MAX_HEALTH);
            float frequency = m_frequency * (intensity * 10f);
            m_vignette.intensity.value = (m_health <= 0) ? intensity : Mathf.Clamp(intensity * (0.5f - (Mathf.Cos(Time.timeSinceLevelLoad * frequency + Mathf.Cos(Time.timeSinceLevelLoad * frequency * m_speed))) * 0.5f) , intensity * 0.8f, m_vignetteIntensity);
        }
        else { m_vignette.intensity.value = 0f; } // resets the vignette when health returns above the threshold
    }

    // simple function to reset the timer
    void ResetTimer()
    {
        m_extraTimer = EXTRA_TIMER_MAX;
    }

    // Event Function - When a GUI button is pressed this button triggers a random animation within the 'oh yeah' animations, reseting the extra timer
    public void OhYeahAnim()
    {
        m_animator.SetInteger("OhIndex", Random.Range(0, 3));
        m_animator.SetTrigger("OHYEAH");
        ResetTimer();
    }

    // Event Function - Reduces the Health by the difference variable
    // checks if the character is 'dead'
    public float ReduceHealth()
    {
        if (isDeadCheck()) return m_health;
        m_health -= m_difference;
        if (m_health <= 0)
        {
            m_health = 0;
            m_animator.ResetTrigger("Damaged");
            m_animator.SetBool("Dead", true);
        }
        else
        {
            m_animator.SetTrigger("Damaged");
        }
        setHealth();
        return m_health;
    }


    // Event Function - Increases the Health by the difference variable
    // checks if the character is 'dead'
    public float IncreaseHealth()
    {
        if (m_health <= 0) Revive();
        else
        {
            if (isDeadCheck()) return m_health;
            m_health += m_difference;
            
        }

        if (m_health > MAX_HEALTH)
        {
            m_health = MAX_HEALTH;
        }
        else if (!isDeadCheck())
        {
            m_animator.SetTrigger("Healed");
        }
        setHealth();
        return m_health;
    }

    // compares if the 'dead' boolean in the animator is true or false
    bool isDeadCheck()
    {
        if (m_animator.GetBool("Dead"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // an extra function specifically when the player is revived, if they have 0 or lower health then health increased (healed)
    public void Revive()
    {
        m_health += MAX_HEALTH / 2;
        StartCoroutine(wait4Revive());
    }

    // sets the float based on an integer hash value comparing the actual health of the character
    void setHealth()
    {
        m_animator.SetFloat(m_healthHash, (m_health / MAX_HEALTH));
        // reset timer
        m_extraTimer = EXTRA_TIMER_MAX / 2;
    }


    // A Timer function that waits seconds for to set the animator dead state to false
    IEnumerator wait4Revive()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(m_reviveTimer);
        m_animator.SetBool("Dead", false);

    }
}
