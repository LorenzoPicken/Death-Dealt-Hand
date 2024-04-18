using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardRevealer : MonoBehaviour
{
    [SerializeField] List<Card> cardWheel = new List<Card>();
    [SerializeField] private Button left;
    [SerializeField] private Button right;

    [SerializeField, Range(0f, 5f)] float transitionTime;
    float currentTime;

    [SerializeField] Transform leftOffScrrenTransform;
    [SerializeField] Transform rightOffScrrenTransform;
    [SerializeField] Transform CenterTransform;

    int index = 0;

    private void OnAwake()
    {
        left.SetEnabled(false);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }


    public void ScrollLeft()
    {
        if(currentTime <  transitionTime)
        {
            return;
        }
        index++;
        if(index == 0)
        {
            left.SetEnabled(false);
        }
        else
        {
            left.SetEnabled(true);
        }
    }

    public void ScrollRight()
    {
        if (currentTime < transitionTime)
        {
            return;
        }
        
        index++;
        if(index == cardWheel.Count - 1)
        {
            right.SetEnabled(false);
        }
        else
        {
            right.SetEnabled(true);
        }
    }


    public void Back()
    {

    }

    public IEnumerator ScrollLeft(Card currentCard, Card nextCard)
    {
        
        yield return null;
    }
}
