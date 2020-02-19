using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public static PlayerAnim playerAnimInstance;
    enum CurrentAnimation
    {
        IDLING,
        WALKING,
        SWINGING,
        DAMAGED,
        DEATH
    };


   // public GameObject player;

    [SerializeField] CurrentAnimation currentAnimation;

    public Animator animController;
    public AnimationClip animClip;

    private void Awake()
    {
        playerAnimInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       // playerAnimInstance = this;

        currentAnimation = CurrentAnimation.IDLING;
    }

    public void SetCurrentAnimations(int num)
    {

        if (num < 4)
        {
            animController.SetBool("IdleState", false);   
            animController.SetBool("AttackInitiated", false);
            currentAnimation = CurrentAnimation.WALKING;
        }

        else if (num == 5)
        {
            animController.SetBool("IdleState", false);
            animController.SetBool("AttackInitiated", true);
            currentAnimation = CurrentAnimation.SWINGING;
        }

        else 
        {
            animController.SetBool("IdleState", true);
            animController.SetBool("AttackInitiated", false);
            currentAnimation = CurrentAnimation.IDLING;
        }
    }
}
