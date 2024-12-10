using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// คลาสลูกสำหรับการเคลื่อนที่แนวตั้ง
public class VerticalEnemyMovement : EnemyMovement
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
        // หันหน้าขึ้นหรือลงตามทิศทางการเคลื่อนที่
        float direction = patrolPoints[currentPatrolIndex].position.y > transform.position.y ? 1 : -1;
        transform.localScale = new Vector3(1, direction, 1);
    }
}
