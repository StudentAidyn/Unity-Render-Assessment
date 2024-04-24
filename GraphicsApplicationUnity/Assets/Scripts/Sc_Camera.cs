using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Camera : MonoBehaviour
{ 
    // the main target the camera will move with
    public Transform camTarget;
    public float pLerp = .01f;
    public float rLerp = .02f;

    [SerializeField] float m_scrollSpeed = 1.0f;
    private Camera m_cam;

    private void Start()
    {
        // Set Camera to the camera component
        m_cam = GetComponent<Camera>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.rotation, rLerp);

        m_cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * m_scrollSpeed;
    }
}
