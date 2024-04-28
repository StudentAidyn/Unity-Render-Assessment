using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_SliderRotation : MonoBehaviour
{
    // references to the sliders
    [SerializeField] Slider m_xSlider;
    [SerializeField] Slider m_ySlider;
    // the max limits of the slider, works since the slider value is from 0 to 1. 
    [SerializeField] float m_xLimit = 360f;
    [SerializeField] float m_yLimit = 360f;


    // Start is called before the first frame update
    void Start()
    {
        // sets the sliders' current value to the rotation the light is starting in,
        // dividing by 360 to match the sliders 0 to 1 value set.
        m_xSlider.value = transform.localEulerAngles.x / 360f;
        m_ySlider.value = transform.localEulerAngles.y / 360f;

        // Adds listeners attached to a single function that acts whenever a change in the sliders' value occurs ---
        m_xSlider.onValueChanged.AddListener(delegate
        {
            UpdateRotation();
        });

        m_ySlider.onValueChanged.AddListener(delegate
        {
            UpdateRotation();
        });
        //--------------------------------------------------------------------------------------
    }

    // UpdateRotation updates the current rotation of an object on its X and Y axis
    void UpdateRotation()
    {
        // updates the rotation of the gameobject the script is attached to.
        transform.localEulerAngles = new Vector3(m_xSlider.value * m_xLimit, m_ySlider.value * m_yLimit, 0f);
    }
}
