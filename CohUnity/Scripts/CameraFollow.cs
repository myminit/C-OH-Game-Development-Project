using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float FollowSpeed = 2f;
    [SerializeField] private float minX; 
    private float fixedY = -2.3f; 
    private float fixedZ = -10f; 

    void Update()
    {
        float targetX = Mathf.Max(target.position.x, minX);

        Vector3 newPos = new Vector3(targetX, fixedY, fixedZ);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}

