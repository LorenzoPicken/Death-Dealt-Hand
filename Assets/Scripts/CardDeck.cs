using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnEnable()
    {
        EventManager.onShuffleDeck += ShuffleAnimation;
    }

    private void OnDisable()
    {
        EventManager.onShuffleDeck -= ShuffleAnimation;
    }

    private void ShuffleAnimation()
    {
        animator.SetTrigger("ActivateShuffle");
    }
}
