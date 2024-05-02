using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Controls the UI Elements attached to the players health.

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

    // Event Function - Calls SetHealthText while passing in the value from the Call to ReduceHealth within character controller.
    // Decreases the Characters health while also setting the GUI text
    public void ReduceHealth() {
        SetHealthText(m_characterController.ReduceHealth()); 
    }

    // Event Function - Calls SetHealthText while passing in the value from the Call to IncreaseHealth within character controller.
    // Increases the Characters health while also setting the GUI text
    public void IncreaseHealth() {
        SetHealthText(m_characterController.IncreaseHealth());
    }

    // Sets the GUI text to the parameter passed through
    void SetHealthText(float health) {
        if (m_healthTMP != null) {
            m_healthTMP.SetText(health.ToString());
        }
    }
}
