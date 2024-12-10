// using UnityEngine;
// using UnityEngine.SceneManagement;
// using MyGameNamespace;
// using Unity.VisualScripting;

// public class SetStart : MonoBehaviour
// {
//     public Transform player;
    
//     private void Start(){
//         if(DataManager.StatusReset){
//             Restart();
//             DataManager.StatusReset = false;
//         }
//     }
//     public void Restart()
//     {

//         ResetPlayerPosition();
//         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//         DataManager.IndexBox = 0;
//         Time.timeScale = 1;
//     }

//     public void ResetPlayerPosition()
//     {
//         player.position = new Vector3(-6.19f, -0.37f, 0);
//         float x = PlayerPrefs.GetFloat("PlayerX");
//         float y = PlayerPrefs.GetFloat("PlayerY");
//         float z = PlayerPrefs.GetFloat("PlayerZ");
//         PlayerPrefs.Save();
//         player.position = new Vector3(x, y, z);
//         Debug.Log("Player position reset.");
//     }
// }

