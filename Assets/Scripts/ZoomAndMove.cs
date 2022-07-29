using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndMove : MonoBehaviour
{
    [SerializeField] float cameraSpeed = 12f;
    [SerializeField] float cameraMovementZone = 50f;
    [SerializeField] float scrollZoomMagnitude = 20f;

    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        if (Input.GetAxisRaw("Horizontal") < 0 || mousePos.x <= cameraMovementZone) transform.Translate(Vector3.left * Time.deltaTime * cameraSpeed);
        if (Input.GetAxisRaw("Vertical") < 0 || mousePos.y <= cameraMovementZone) transform.Translate(Vector3.down * Time.deltaTime * cameraSpeed);
        if (Input.GetAxisRaw("Horizontal") > 0 || mousePos.x >= Screen.width - cameraMovementZone) transform.Translate(Vector3.right * Time.deltaTime * cameraSpeed);
        if (Input.GetAxisRaw("Vertical") > 0 || mousePos.y >= Screen.height - cameraMovementZone) transform.Translate(Vector3.up * Time.deltaTime * cameraSpeed);

        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollZoomMagnitude;

    }
}
