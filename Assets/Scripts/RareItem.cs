using UnityEngine;

public class RareItem : MonoBehaviour
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
            ScoreManager.instance.AddScore(50);
            GameManager.instance.AddLife();

            Destroy(gameObject);
        }
    }
}