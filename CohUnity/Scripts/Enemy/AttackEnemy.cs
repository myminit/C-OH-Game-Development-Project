using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Weak Point")
        {
            Destroy(collision.gameObject);
            soundManager.PlaySFX(soundManager.attack);
        }
    }
}
