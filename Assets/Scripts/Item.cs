using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private AudioSource audioSource;
    private MeshRenderer meshRenderer;

    private float rotateSpeed = 120f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) 
        {
            GameManager.Instance.AddScore();
            audioSource.Play();
            meshRenderer.enabled = false;
            Destroy(gameObject, 0.2f);
        }
    }
}
