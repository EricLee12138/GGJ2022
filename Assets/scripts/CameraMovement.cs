using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Bounds cameraBounds;

    [SerializeField]
    Vector2 triggerDistance;

    bool disabled = false;
    float k = .15f;
    float speed = 2f;
    Vector3 targetPosition;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        targetPosition = mainCamera.transform.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(cameraBounds.center, cameraBounds.size);
    }

    void Update()
    {
        if (!disabled) {
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(mousePosition.x - cameraPosition.x) > triggerDistance.x ||
            Mathf.Abs(mousePosition.y - cameraPosition.y) > triggerDistance.y) {
                RaycastHit2D hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit) {
                    targetPosition = hit.point;
                    targetPosition = targetPosition * k + cameraPosition * (1-k);
                    targetPosition.x = Mathf.Clamp(targetPosition.x , cameraBounds.min.x, cameraBounds.max.x);
                    targetPosition.y = Mathf.Clamp(targetPosition.y , cameraBounds.min.y, cameraBounds.max.y);
                    targetPosition.z = mainCamera.transform.position.z;
                }
            }
        }
        
        float distance = Vector3.Distance(mainCamera.transform.position, targetPosition);
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, speed * distance * Time.deltaTime);
    }

    public void DisableMovement()
    {
        disabled = true;
        targetPosition.x = 0;
        targetPosition.y = 0;
        targetPosition.z = mainCamera.transform.position.z;
    }
}
