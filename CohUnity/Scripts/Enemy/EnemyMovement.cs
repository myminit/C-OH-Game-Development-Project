using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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





