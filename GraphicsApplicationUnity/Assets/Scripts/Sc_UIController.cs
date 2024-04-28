using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sc_UIController : MonoBehaviour
{
    // Reference to on screen text
    [SerializeField] TMP_Text m_healthTMP;
    // Reference to the Character Controller Script
    [SerializeField] CharacterController m_characterController;

    // Start is called before the first frame update
    void Start() {   
        SetHealthText(m_characterController.GetCurrentHealth());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReduceHealth() {
        SetHealthText(m_characterController.ReduceHealth()); 
    }

    public void IncreaseHealth() {
        SetHealthText(m_characterController.IncreaseHealth());
    }

    void SetHealthText(float health) {
        if (m_healthTMP != null) {
            m_healthTMP.SetText(health.ToString());
        }
    }
}
