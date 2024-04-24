using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    const float MAX_HEALTH = 100f;
    float m_health;
    int m_healthHash;
    public float m_difference = 5f;
    const float EXTRA_TIMER = 20f;
    public float m_timer;
    public float m_reviveTimer = 5f;
    Animator m_animator;
    // Start is called before the first frame update
    void Start()
    {
        // assigning animator
        m_animator = GetComponent<Animator>();
        m_healthHash = Animator.StringToHash("Health");
        m_health = MAX_HEALTH;
        setHealth();
    }

    // Update is called once per frame
    void Update()
    {
        m_timer -= 1 * Time.deltaTime;
        if (m_timer <= 0) {
            // play animation
            // reset timer
            m_animator.SetTrigger("Extra");
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        m_timer = EXTRA_TIMER;
    }
    public void OhYeahAnim()
    {
        m_animator.SetInteger("OhIndex", Random.Range(0, 3));
        m_animator.SetTrigger("OHYEAH");
        ResetTimer();
    }

    public void ReduceHealth()
    {
        if (isDeadCheck()) return;
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
    }

    public void IncreaseHealth()
    {
        if (m_health <= 0) Revive();
        else
        {
            if (isDeadCheck()) return;
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
        m_timer = EXTRA_TIMER / 2;
    }

    IEnumerator wait4Revive()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(m_reviveTimer);
        m_animator.SetBool("Dead", false);

    }
}
