using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    // Update is called once per frame
    private float orthographicSize;
    private float targetOrthographicSize;
    void Update()
    {
       HandleMovement();
       HandleZoom();
       
    }

    private void HandleMovement()
    {
        float x =  Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(x, y).normalized;
        float moveSpeed = 35f;
        transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime; 
    }

    private void HandleZoom()
    {
        float zoomAmount = 2f;
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;
        float minOrthoGraphicSize = 10f;
        float maxOrthoGraphicSize = 40f;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthoGraphicSize, maxOrthoGraphicSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize; 
    }
}
