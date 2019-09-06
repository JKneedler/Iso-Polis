using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 mouseDownStartPos;
    public float zoomSpeed;
    public int zoomMin;
    public int zoomMax;


    void Start()
    {
        
    }


    void Update()
    {
        if(Camera.main.orthographicSize > zoomMin && Input.mouseScrollDelta.y > 0) {
            Camera.main.orthographicSize += (-Input.mouseScrollDelta.y * zoomSpeed);
        }
        if(Camera.main.orthographicSize < zoomMax && Input.mouseScrollDelta.y < 0) {
            Camera.main.orthographicSize += (-Input.mouseScrollDelta.y * zoomSpeed);
        }

        if(Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
        if(Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        }
        if(Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        if(Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }

        if(Input.GetMouseButtonDown(2)) {
            mouseDownStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(2)) {
            var curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mousePosDifference = mouseDownStartPos - curMousePos;
            mousePosDifference = new Vector3(mousePosDifference.x, mousePosDifference.y, 0);
            transform.position += mousePosDifference;
        }
    }
}
