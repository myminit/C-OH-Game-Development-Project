// using UnityEditor;
// using UnityEngine;

// public class PlayerSaveSystem : MonoBehaviour
// {
//     public Transform player; // อ้างอิงถึง GameObject ของผู้เล่น

//     private void Awake()
//     {
//         if (player != null) // ตรวจสอบว่า player ไม่เป็น null ก่อน
//         {
//             LoadPlayerPosition(); // โหลดตำแหน่งผู้เล่นเมื่อเริ่มต้น Scene
//             SavePlayerPosition();
//         }
//         else
//         {
//             Debug.LogWarning("Player is not assigned!");
//             return;
//         }
//     }

//     private void OnDisable()
//     {
//         if (player != null && gameObject.activeSelf) // ตรวจสอบว่า player ไม่เป็น null ก่อน
//         {
//             SavePlayerPosition(); // บันทึกตำแหน่งเมื่อออกจาก Scene
//         }
//         else
//         {
//             Debug.LogWarning("Player is not assigned!2");
//         }
//     }

//     private void OnApplicationQuit()
//     {
//         if (player != null) // ตรวจสอบว่า player ไม่เป็น null ก่อน
//         {
//             PlayerPrefs.DeleteAll(); // ลบข้อมูลทั้งหมดใน PlayerPrefs
//             PlayerPrefs.Save();
//             ResetPlayerPosition();
//             Debug.LogWarning("Player is Delete!");
//         }
//         else
//         {
//             Debug.LogWarning("Player is not assigned!1");
//         }
//     }

//     public void SavePlayerPosition()
//     {
//         if (player != null) // ตรวจสอบว่า player ไม่เป็น null ก่อน
//         {
//             // บันทึกตำแหน่งของผู้เล่นใน PlayerPrefs
//             PlayerPrefs.SetFloat("PlayerX", player.position.x);
//             PlayerPrefs.SetFloat("PlayerY", player.position.y);
//             PlayerPrefs.SetFloat("PlayerZ", player.position.z);
//             PlayerPrefs.Save(); // บันทึกลงในดิสก์
//             Debug.Log("Player position saved: " + player.position);
//         }
//         else
//         {
//             Debug.LogWarning("Player is not assigned. Cannot save position.");
//         }
//     }

//     public void LoadPlayerPosition()
//     {
//         if (player != null) // ตรวจสอบว่า player ไม่เป็น null ก่อน
//         {
//             if (PlayerPrefs.HasKey("PlayerX"))
//             {
//                 // โหลดตำแหน่งที่บันทึกไว้
//                 float x = PlayerPrefs.GetFloat("PlayerX");
//                 float y = PlayerPrefs.GetFloat("PlayerY");
//                 float z = PlayerPrefs.GetFloat("PlayerZ");

//                 player.position = new Vector3(x, y, z); // ย้าย Player ไปยังตำแหน่งที่บันทึกไว้
//                 Debug.Log("Player position loaded: " + player.position);
//             }
//             else
//             {
//                 Debug.Log("No saved position found. Starting at default position.");
//             }
//         }
//         else
//         {
//             Debug.LogWarning("Player is not assigned. Cannot load position.");
//         }
//     }

//     public void ResetPlayerPosition()
//     {
//         if (player != null) // ตรวจสอบว่า player ไม่เป็น null ก่อน
//         {
//             player.position = new Vector3(-6.19f, -0.37f, 0);
//             PlayerPrefs.Save();
//             Debug.Log("Player position reset.");
//         }
//         else
//         {
//             Debug.LogWarning("Player is not assigned. Cannot reset position.");
//         }
//     }
// }
