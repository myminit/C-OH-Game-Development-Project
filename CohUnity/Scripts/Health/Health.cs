using System.Collections;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour 
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    Vector2 checkpointPos;

    SoundManager soundManager;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    private void Start()
    {
        checkpointPos = transform.position;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            soundManager.PlaySFX(soundManager.hurt);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                soundManager.PlaySFX(soundManager.die);

                StartCoroutine(Respawns(3f));
            }
        }
    }

    public void UpdateCheckpoint(Vector2 Pos)
    {
        checkpointPos = Pos;
    }

    IEnumerator Respawns(float duration)
    {
        yield return new WaitForSeconds(duration);
        dead = false;
        transform.position = checkpointPos;
        currentHealth = startingHealth;
        soundManager.PlaySFX(soundManager.respawn);
        anim.ResetTrigger("die");
        anim.Play("Idle"); // ÃÕà«çµ¡ÅÑºä» animation Idle
        GetComponent<PlayerMovement>().enabled = true;
    }
}
