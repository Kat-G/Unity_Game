using UnityEngine;

public class BadItem : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           GameManager.instance.TakeDamage();
           Destroy(gameObject);
        }
    }
}