using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


    public int health;
    public PlayerAnimation playerAnim;
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();  // Sahnedeki GameManager'ı bul ve referansı al
    }
    public void PlayerGetsHurt()
    {
        health--;  // Oyuncu sağlığı azalır
        playerAnim.PlayHurtAnimation(); // Zarar aldığında animasyonu çağır

        gameManager.UpdateHealth(health);  // GameManager'daki health değerini güncelle
    }

    public void UpdateHealth(int newHealth)
    {
        health = newHealth;
    }
}