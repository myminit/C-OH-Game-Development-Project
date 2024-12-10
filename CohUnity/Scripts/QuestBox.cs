using System.Collections;
using System.Collections.Generic;
using MyGameNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestBox : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; 
    [SerializeField] private Sprite boxSprite;        
    // [SerializeField] private Transform player;  

    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
            SpawnBoxes(DataManager.BoxNum);
    }

    void SpawnBoxes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnBox();
        }
    }

    void SpawnBox()
    {
        // สุ่มตำแหน่งสำหรับสร้างกล่อง
        Vector3 position = GetRandomPosition();
        if (position != Vector3.zero) // ตรวจสอบว่าตำแหน่งที่ได้ไม่ใช่ Vector3.zero
        {
            // สร้าง GameObject ใหม่สำหรับกล่อง
            GameObject box = new GameObject("QuestBox");
            box.transform.position = position; // กำหนดตำแหน่งของกล่อง

            // เพิ่ม SpriteRenderer เพื่อแสดงภาพ Sprite บนกล่อง
            SpriteRenderer spriteRenderer = box.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = boxSprite; // ตั้งค่า Sprite ที่ใช้แสดงกล่อง

            // เพิ่ม BoxCollider2D เพื่อให้กล่องมีคอลลิชัน
            BoxCollider2D boxCollider = box.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true; // ตั้งค่าคอลลิชันเป็น Trigger (ไม่มีการชน)

            // เพิ่มสคริปต์ Checkpoint เพื่อให้กล่องมีพฤติกรรมเฉพาะ
            Checkpoint checkpoint = box.AddComponent<Checkpoint>();

            box.AddComponent<QuestBoxCollider>();

            // กำหนดเลเยอร์การเรนเดอร์ของ SpriteRenderer
            spriteRenderer.sortingLayerName = "Foreground"; // เลเยอร์ที่ใช้ในการแสดงผล
            spriteRenderer.sortingOrder = 0; // ลำดับการแสดงผลในเลเยอร์นี้

            // บันทึกตำแหน่งที่ใช้งานแล้ว เพื่อหลีกเลี่ยงการสร้างกล่องในตำแหน่งซ้ำ
            usedPositions.Add(position);

            // เพิ่มฟังก์ชันตรวจจับการชน
            box.AddComponent<QuestBoxCollider>();
        }
    }

    Vector3 GetRandomPosition()
    {
        if (usedPositions.Count >= spawnPoints.Length)
        {
            Debug.LogWarning("All spawn points are used!");
            return Vector3.zero;
        }

        int attempts = 0;
        int maxAttempts = 100;
        while (attempts < maxAttempts)
        {
            int randomIndex = Random.Range(DataManager.IndexBox, spawnPoints.Length);
            Vector3 position = spawnPoints[randomIndex].position;
            // Debug.Log(player.position);
            if (!usedPositions.Contains(position))
            {
                DataManager.IndexBox = randomIndex;
                return position;
            }
            attempts++;
        }

        return Vector3.zero;
    }
}

public class QuestBoxCollider : MonoBehaviour
{
    private bool status = true;

    IEnumerator ResetStatus()
    {
        yield return new WaitForSeconds(2f); 
        status = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && status) 
        {
            status = false; 
            Destroy(gameObject);
            SceneManager.LoadScene("EDITOR C-OH GAME");
            StartCoroutine(ResetStatus());
        }
    }

}

