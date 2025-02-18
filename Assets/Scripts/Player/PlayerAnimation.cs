using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator playerAnim;

    //Cagirilacak class lar
    public void PlayMiningAnimation()
    {
        playerAnim.Play("playerMining");
    }

    public void PlayKnifeAnimation()
    {
        playerAnim.Play("playerKnife");
    }

    public void PlayHurtAnimation()
    {
        playerAnim.Play("playerHurt");
    }
}