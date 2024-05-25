using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector2 m_turn;
    [SerializeField] float m_sensitivity = .5f;


    [SerializeField] List<Transform> m_focuses;
    [SerializeField] int m_index;
    [SerializeField] Vector3 m_offset;

    //not locking mouse 
    private void Start()
    {
        if(m_focuses.Count == 0)
        {
            Debug.Log("EMPTY_ERROR"); 
        }
        else
        {
            UpdateFocus();
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Quits application when pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        // Get mouse input to move the camera around
        if (Input.GetMouseButton(1))
        {
            m_turn.x += Input.GetAxis("Mouse X") * m_sensitivity;
            m_turn.y += Input.GetAxis("Mouse Y") * m_sensitivity;
            // Fix to the camera angle, can look up and down but not swing and turn the angle 'upside down'
            m_turn.y = Mathf.Clamp(m_turn.y, -90, 90);
            transform.localRotation = Quaternion.Euler(-m_turn.y, m_turn.x, 0);
        }
    }

    public void NextFocus()
    {
        // next available object to focus on
        m_index = (m_index + 1) % m_focuses.Count;
        UpdateFocus();
    }

    public void PrevFocus()
    {
        // previous available object to focus on
        m_index--;
        if (m_index < 0) m_index = m_focuses.Count - 1;
        UpdateFocus();
    }

    // change the current position to the position of the next object
    private void UpdateFocus()
    {
        if (m_focuses[m_index] != null)
        {
            gameObject.transform.position = m_focuses[m_index].position + m_offset;
        }

    }

}
