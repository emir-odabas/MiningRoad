using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public static bool moving;
    public GameObject Player; // Oyuncu GameObject referansı.
    public float speed = 2.0f;
    public float knockbackForce;
    public Rigidbody2D rb;
    public int health;

    public Animator reaperAnim;

    // Oyuncunun scriptlerine referans
    private PlayerController playerController;
    private PlayerHealth playerHealth;

    void Start()
    {
        // Player GameObject'i sahneden bul.
        Player = GameObject.Find("Player");

        // Player'daki ilgili scriptlere eriş.
        if (Player != null)
        {
            playerController = Player.GetComponent<PlayerController>();
            playerHealth = Player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (playerHealth != null && playerController.gameManager.health > 0) // Oyuncunun sağlığı kontrol edilir.
        {
            moving = true; //Düşmanın hareket etmeye başladığını belirtmek için bir durumu aktif hale getiriyor.
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime); //Düşmanı, şu anki pozisyonundan oyuncunun pozisyonuna doğru belirli bir hızda hareket ettiriyor.
        }

        if (health <= 0) // Düşman sağlığı sıfırlandıysa yok edilir.
        {
            Debug.Log("Reaper öldü!");  // Debug log ile kontrol et
            gameObject.SetActive(false);
            GameManager.instance.ActivateNextRock();  // ActivateNextRock çağrıl
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("knife"))
        {
            health--; // Düşmanın sağlığı azalır.
            reaperAnim.Play("reaperHurt"); //reaperHurt adlı animasyonu oynatarak düşmanın hasar aldığını görsel olarak gösteriyor.
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, -100 * Time.deltaTime); //Düşmanı, oyuncunun pozisyonundan uzaklaşacak şekilde (eksi yönde) belirli bir hızla hareket ettiriyor.
        }

        if (collision.gameObject.CompareTag("Player") && playerHealth != null)
        {
            playerHealth.PlayerGetsHurt(); // Oyuncunun hasar alması tetiklenir.
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, -100 * Time.deltaTime);
        }
    }
}
