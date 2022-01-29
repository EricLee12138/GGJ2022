using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Bounds cameraBounds;

    float k = .25f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(cameraBounds.center, cameraBounds.size);
    }

    void Update()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit) {
            print("hit");
            Vector3 targetPosition = hit.point;
            targetPosition = targetPosition * k + cameraPosition * (1-k);
            targetPosition.x = Mathf.Clamp(targetPosition.x , cameraBounds.min.x, cameraBounds.max.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y , cameraBounds.min.y, cameraBounds.max.y);
            targetPosition.z = mainCamera.transform.position.z;

            float distance = Vector3.Distance(cameraPosition, targetPosition);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, 5f * distance * Time.deltaTime);
        }

    }

    void OnMouseOver()
    {
        // Vector3 cameraPosition = mainCamera.transform.position;
        // Vector3 targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // targetPosition = targetPosition * k + cameraPosition * (1-k);
        // targetPosition.x = Mathf.Clamp(targetPosition.x , cameraBounds.min.x, cameraBounds.max.x);
        // targetPosition.y = Mathf.Clamp(targetPosition.y , cameraBounds.min.y, cameraBounds.max.y);
        // targetPosition.z = mainCamera.transform.position.z;
        // Debug.DrawLine(Vector3.zero, targetPosition);

        // float distance = Vector3.Distance(cameraPosition, targetPosition);
        // mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, 5f * distance * Time.deltaTime);
    }
}
