using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Camera : MonoBehaviour
{
    // reference to the camera
    [SerializeField] Camera m_cam;

    // the main target the camera will move with
    [SerializeField] Transform m_camTarget;


    // the positional & rotationallerping values
    [SerializeField] float m_pLerp = .01f;
    [SerializeField] float m_rLerp = .02f;

    // the power and speed of each input on the zoom.
    [SerializeField] float m_scrollSpeed = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        // Set Camera to the local game object camera component
        m_cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Updates the position & rotation of the game object lerping between the current and updated positions and rotations
        transform.position = Vector3.Lerp(transform.position, m_camTarget.position, m_pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, m_camTarget.rotation, m_rLerp);

        // sets the field of view values within the camera using the scroll wheel multiplied by the scroll speed variable.
        m_cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * m_scrollSpeed;
    }
}
