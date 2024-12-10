//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyMovement : MonoBehaviour
//{
//    [SerializeField] private Transform[] patrolPoints;
//    [SerializeField] private float moveSpeed;
//    [SerializeField] private int patrolDestination;

//    private void Update()
//    {
//        if (patrolDestination == 0)
//        {
//            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
//            if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
//            {
//                transform.localScale = new Vector3(1, 1, 1);
//                patrolDestination = 1;
//            }
//        }

//        if (patrolDestination == 1)
//        {
//            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
//            if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
//            {
//                transform.localScale = new Vector3(-1, 1, 1);
//                patrolDestination = 0;
//            }
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// คลาส abstract หลักสำหรับการเคลื่อนที่
public abstract class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected Transform[] patrolPoints;
    [SerializeField] protected float moveSpeed;
    protected int currentPatrolIndex;

    protected virtual void Start()
    {
        currentPatrolIndex = 0;
        if (patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned to " + gameObject.name);
            enabled = false;
        }
    }

    protected virtual void Update()
    {
        Move();
        CheckPatrolPointReached();
    }

    protected abstract void Move();
    protected abstract void UpdateRotation();

    protected virtual void CheckPatrolPointReached()
    {
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            UpdateRotation();
        }
    }
}





