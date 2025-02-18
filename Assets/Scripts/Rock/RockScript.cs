using System.Collections;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public Animator rockAnim;
    public Animator playerAnim; // Player'ın animasyonunu da kontrol edebilmek için
    public GameObject reaper;
    //public GameObject[] reapers;
    public GameObject potionBufferPrefab;
    private GameManager gameManager;

 
    private bool isDestroyed = false;  // Kayanın yok olup olmadığına dair bir flag

    void Start()
    {
        reaper.SetActive(false);
        rockAnim.SetBool("isDamaged", false); // Başlangıçta hasar almamış
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDestroyed)
        {
            // Player kaya ile çarpıştığında 'playerMining' animasyonunu başlat
            playerAnim.Play("playerMining");  // Mining animasyonunu başlat

            rockAnim.SetBool("isDamaged", true); // Kaya hasar aldı
            rockAnim.Play("rockDamaged"); // Hasar animasyonunu oynat

            isDestroyed = true;  // Kaya yok oldu, tekrar yok edilmesine izin verilmez
            StartCoroutine(DestroyRockAfterAnimation()); // Animasyon bitince kayayı yok et
        }
    }

    private IEnumerator DestroyRockAfterAnimation()
    {
        // Animasyon süresi 4 saniye
        float animLength = 4f;  // 4 saniye olarak ayarlandı

        // Animasyonun bitmesini bekleyelim
        yield return new WaitForSeconds(animLength);

        // Animasyon tamamlandıktan sonra kayayı yok et
        gameObject.SetActive(false);  // Kayayı yok et veya görünmez yap
        reaper.SetActive(true);  // Reaper'ı aktif et

       

        // PotionBuffer prefab'ını kayanın pozisyonunda spawn et
        if (potionBufferPrefab != null)
        {
            GameObject potionBuffer = Instantiate(potionBufferPrefab, transform.position, Quaternion.identity); // Pozisyonu doğru ayarla

            // Eğer spawn edilen nesne PotionBuffer tag'ine sahipse bir işlem yapabilirsin
            if (potionBuffer.CompareTag("PotionBuffer"))
            {
                // PotionBuffer tag'ine sahip nesne üzerinde işlem yapabilirsin
                Debug.Log("PotionBuffer spawn edildi!");
            }
        }
    }

    

}
