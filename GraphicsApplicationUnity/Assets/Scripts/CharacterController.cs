using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        // assigning animator
        m_animator = GetComponent<Animator>();
        m_healthHash = Animator.StringToHash("Health");
        setHealth();
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
    }

    void ResetTimer()
    {
        m_extraTimer = EXTRA_TIMER_MAX;
    }
    public void OhYeahAnim()
    {
        m_animator.SetInteger("OhIndex", Random.Range(0, 3));
        m_animator.SetTrigger("OHYEAH");
        ResetTimer();
    }

    public float ReduceHealth()
    {
        if (isDeadCheck()) return 0;
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

    public void Revive()
    {
        m_health += MAX_HEALTH / 2;
        StartCoroutine(wait4Revive());
    }

    void setHealth()
    {
        m_animator.SetFloat(m_healthHash, (m_health / MAX_HEALTH));
        // reset timer
        m_extraTimer = EXTRA_TIMER_MAX / 2;
    }

    IEnumerator wait4Revive()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(m_reviveTimer);
        m_animator.SetBool("Dead", false);

    }
}
