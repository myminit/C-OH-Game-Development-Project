using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// คลาสลูกสำหรับการเคลื่อนที่แนวนอน
public class HorizontalEnemyMovement : EnemyMovement
{
    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            patrolPoints[currentPatrolIndex].position,
            moveSpeed * Time.deltaTime
        );
    }

    protected override void UpdateRotation()
    {
        // หันหน้าไปทางที่กำลังเคลื่อนที่
        float direction = patrolPoints[currentPatrolIndex].position.x > transform.position.x ? 1 : -1;
        transform.localScale = new Vector3(direction, 1, 1);
    }
}
