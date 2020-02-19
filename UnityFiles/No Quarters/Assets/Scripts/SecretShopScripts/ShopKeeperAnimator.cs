using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeperAnimator : MonoBehaviour
{
    [SerializeField]
    RuntimeAnimatorController animCont;

    Animator animator;
    Image imageCanvas;
    SpriteRenderer hiddenRender;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        hiddenRender = gameObject.GetComponent<SpriteRenderer>();
        imageCanvas = gameObject.GetComponent<Image>();

        animator.runtimeAnimatorController = animCont;
        hiddenRender.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.runtimeAnimatorController != null)
        {
            imageCanvas.sprite = hiddenRender.sprite; 
        }
    }
}
