using System;
using MyGameNamespace;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Health PlayerController;
    private Collider2D Collider;

    private void Awake()
    {
        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DataManager.X = PlayerController.transform.position.x;
            DataManager.Y = PlayerController.transform.position.y;

            Debug.Log($"Checkpoint saved at position: X = {DataManager.X}, Y = {DataManager.Y}");

            Collider.enabled = false;
        }
    }
}
