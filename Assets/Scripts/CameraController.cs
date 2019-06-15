using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private PlayerController thePlayer;
    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    // Use this for initialization
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, transform.position.z);
        theCamera = GetComponentInChildren<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, thePlayer.transform.position.x, ref velocity.x, SmoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, thePlayer.transform.position.y, ref velocity.y, SmoothTime);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}