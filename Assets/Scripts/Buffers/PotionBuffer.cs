using System.Collections;
using UnityEngine;

public class PotionBuffer : MonoBehaviour
{
    public float boostDuration = 5f; // Hız artışı süresi
    private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer player bu alanla etkileşime girerse
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();  // PlayerController'a erişim sağla
            if (playerController != null)
            {
                // SpeedBoost fonksiyonunu kullanarak hız artışı yap
                playerController.IncreaseSpeed(2f, boostDuration); // Hızı iki katına çıkar
            }
            Destroy(gameObject); // PotionBuffer nesnesini yok et
        }
    }
}
