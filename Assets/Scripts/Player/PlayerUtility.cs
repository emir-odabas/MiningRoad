using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUtility : MonoBehaviour
{
   
    public void ResetPlayerPosition()
    {
        transform.position = new Vector3(-2, 0, 0); // Başlangıç pozisyonu
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1); // Oyun sıfırlama
    }
}