using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D r2bd;
    public GameManager gameManager;
    public PlayerAnimation playerAnim;

    public float movementSpeed;
    private bool facingRight = true;
    public bool gameIsOver;
    public GameObject gameOverScene;
    private Vector2 moveInput;

    private float originalSpeed; // Başlangıç hızını saklamak için


    private void Start()
    {
        // Başlangıç hızını sakla
        originalSpeed = movementSpeed;
    }

    private void FixedUpdate()
    {

        if (gameIsOver == false)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            moveInput.x = h;
            moveInput.y = v;
            moveInput = moveInput.normalized;
            r2bd.velocity = moveInput * movementSpeed * Time.deltaTime;
            FlipCharacter(h);

            // Animasyonları tetikle
            if (Input.GetKey(KeyCode.Space))
            {
                playerAnim.PlayMiningAnimation();
            }

            if (Input.GetKey(KeyCode.E))
            {
                playerAnim.PlayKnifeAnimation();
            }
        }
        if (gameManager.health <= 0) // Oyun bittiğinde(gameManager.health <= 0), hareket hızı sıfırlanır ve oyun bitiş ekranı(gameOverScene) aktif hale gelir.
        {
            gameIsOver = true;
            movementSpeed = 0;
            moveInput = Vector2.zero;
            r2bd.velocity = moveInput * movementSpeed;
            gameOverScene.SetActive(true);
        }
    }

    //Oyuncunun yatay eksendeki yönünü (sağa/sola bakma) değiştirir.
    private void FlipCharacter(float horizontal)
    {
        if (horizontal > 0 && !facingRight)
        {
            facingRight = true;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x); // Pozitif yap
            transform.localScale = scale;
        }
        else if (horizontal < 0 && facingRight)
        {
            facingRight = false;
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x); // Negatif yap
            transform.localScale = scale;
        }
    }

    //Oyuncunun hızını belirli bir çarpan ile artırır ve bu artırmayı belirli bir süre için uygular.
    public void IncreaseSpeed(float multiplier, float duration)
    {
        StartCoroutine(SpeedBoost(multiplier, duration)); //SpeedBoost adında bir coroutine çağrılarak hız geçici olarak artırılır.
    }

    //Oyuncunun hızını artıran coroutine metodu.
    private IEnumerator SpeedBoost(float multiplier, float duration)
    {
        movementSpeed *= multiplier; // Hızı arttır
        yield return new WaitForSeconds(duration);
        movementSpeed = originalSpeed; // Hızı eski haline getir
    }
}