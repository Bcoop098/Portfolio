using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaristaAnim : MonoBehaviour
{
    public static BaristaAnim baristaAnimInstance;

    enum CurrentAnimation
    {
        WALKING,
        ATTACKING,
        IDLE
    }

    CurrentAnimation currentAnimation;
    public Animator BarAnimController;
    public AnimationClip BarAnimClip;
    private void Awake()
    {
        baristaAnimInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAnimation = CurrentAnimation.IDLE;
    }


    public void SetBarCurrentAnimations(int num)
    {
        if (num == 0)
        {
            //walking
            BarAnimController.SetBool("IdleState", false);
            BarAnimController.SetBool("Attacking", false);
        }
        else if (num == 1)
        {
            BarAnimController.SetBool("IdleState", false);
            BarAnimController.SetBool("Attacking", true);
        }

        else if(num == 2)
        {
            BarAnimController.SetBool("IdleState", true);
            BarAnimController.SetBool("Attacking", false);
        }
    }
}
