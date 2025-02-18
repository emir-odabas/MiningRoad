using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject potionBufferPrefab;
    public int health = 3;
    public float boostDuration = 5f; // Hız artışı süresi
    public float boostMultiplier = 2f; // Hız artışı kat sayısı


    public GameObject[] rocks;  // Tüm kaya nesnelerini tutacak dizi
    public GameObject[] reapers;  // Tüm reaper nesnelerini tutacak dizi
    private int currentRockIndex = 0;  // Hangi kayanın aktif olduğunu takip eden index

    public GameObject gameOverPanel;


    void Start()
    {
        rocks[0].SetActive(true);
        reapers[0].SetActive(false);
    }

    void Awake()
    {
        if (instance == null) //Eğer instance değişkeni boşsa (henüz bir GameManager atanmadıysa), bu nesne instance olarak atanır.
        {
            instance = this; //Bu nesneyi GameManager sınıfının örneği olarak belirler (singleton yapısı).
        }
        else //Eğer instance zaten atanmışsa (birden fazla GameManager oluşturulmuşsa), aşağıdaki blok çalışır.
        {
            Debug.LogError("Birden fazla GameManager var!");
            Destroy(gameObject); //Fazladan oluşturulmuş olan GameManager nesnesini sahneden silerek yalnızca bir örnek olmasını sağlar.
        }
    }

    public void UpdateHealth(int newHealth)
    {
        health = newHealth; //Sağlık değeri, newHealth parametresi ile güncelleniyor. Yani mevcut sağlık, yeni değere eşitleniyor.
        UpdateHearts(); //Sağlık güncellendikten sonra, sağlık göstergesini (örneğin kalpler veya sağlık çubuğu) güncellemek için başka bir metod çağrılıyor.
    }

    public void UpdateHearts()
    {
        // Öncelikle tüm kalpleri boş olarak ayarla
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }

        // Ardından, health değeri kadar kalbi dolu yap
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

    // Bir kaya kesildiğinde çağrılır, bir sonraki kaya aktif edilir
    public void ActivateNextRock()
    {
        if (currentRockIndex < rocks.Length - 1) //currentRockIndex değişkeni, kaya listesindeki (rocks) son indeksin altında bir değere sahipse,
                                                 //yeni bir kaya aktifleştirilir. Eğer tüm kayalar aktifleştirilmişse, else bloğuna geçer.
        {
            Debug.Log($"Aktif edilen kaya indexi: {currentRockIndex + 1}"); //Konsola, sıradaki aktif olacak kayanın indeksini yazdırır.
            rocks[currentRockIndex].SetActive(false); //Şu anda aktif olan kayayı devre dışı bırakır.
            reapers[currentRockIndex].SetActive(false); //Şu anda aktif olan reaper'ı (düşman nesnesi) devre dışı bırakır.

            currentRockIndex++; //Mevcut kaya indeksini bir artırır, yani bir sonraki kaya için hazırlanır.
            rocks[currentRockIndex].SetActive(true); //Bir sonraki kayayı aktif hale getirir.
            reapers[currentRockIndex].SetActive(true); //Bir sonraki reaper'ı aktif hale getirir.
        }
        else
        {
            Debug.Log("Tüm kayalar kesildi!");
            GameOver();
        }
    }
    public void ActivateSpeedBoost(GameObject player)
    {
        PlayerController playerMove = player.GetComponent<PlayerController>(); //player nesnesinin üzerinde bir PlayerController bileşeni
                                                                               //olup olmadığı kontrol edilir. Eğer varsa, bu bileşen playerMove değişkenine atanır.
        if (playerMove != null) //PlayerController bileşeni mevcutsa (null değilse), aşağıdaki hız artırma işlemleri gerçekleştirilir.
        {
            playerMove.IncreaseSpeed(boostMultiplier, boostDuration); //PlayerController içinde tanımlanmış olan IncreaseSpeed metoduna,
                                                                    //hız çarpanı (boostMultiplier) ve süre (boostDuration) parametreleri gönderilir. Bu, oyuncunun hızını belirtilen süre boyunca artırır.
        }
    }


    public void GameOver()
    {
        Debug.Log("Oyun Bitti!");

        gameOverPanel.SetActive(true); // Game Over panelini aktif et

        Time.timeScale = 0; // Oyunu durdur
    }


    public void RestartGame()
    {
        Time.timeScale = 1; // Oyunu tekrar başlat
        SceneManager.LoadScene(0); // İlk sahneyi yükle
    }

}
