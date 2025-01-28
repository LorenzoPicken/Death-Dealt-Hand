using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardRevealer : MonoBehaviour
{
    [SerializeField] List<CardInfo> cardWheel = new List<CardInfo>();
    public Button left;
    public Button right;
    [SerializeField] TextMeshProUGUI cardName;
    public Canvas thisCanvas;

    string direction;

    [SerializeField, Range(0f, 5f)] float transitionTime;
    float currentTime;

    [SerializeField] Transform leftOffScreenTransform;
    [SerializeField] Transform rightOffScreenTransform;
    [SerializeField] Transform CenterTransform;


    int index = 0;

    private void Awake()
    {
        thisCanvas.enabled = false;
        foreach(CardInfo card in cardWheel)
        {
            card.gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (index == 0)
        {
            left.enabled = false;
        }
        else
        {
            left.enabled = true;
        }
        if (index == cardWheel.Count - 1)
        {
            right.enabled = false;
        }
        else
        {
            right.enabled = true;
        }
    }


    public void ScrollLeft()
    {
        direction = "left";
        if(currentTime <  transitionTime)
        {
            return;
        }
        currentTime = 0;
        CardInfo currentCard = cardWheel[index];
        index++;
        
        CardInfo nextCard = cardWheel[index];
        cardName.text = nextCard.cardName;
        StartCoroutine(Scroll(currentCard, nextCard, direction));
    }

    public void ScrollRight()
    {
        direction = "right";
        if (currentTime < transitionTime)
        {
            return;
        }
        currentTime = 0;
        CardInfo currentCard = cardWheel[index];
        
        index--;
        
        CardInfo nextCard = cardWheel[index];
        cardName.text = nextCard.cardName;
        StartCoroutine(Scroll(currentCard, nextCard, direction));
    }


    public void Back()
    {
        thisCanvas.enabled = false;
        foreach(CardInfo card in cardWheel)
        {
            card.gameObject.SetActive(false);
        }
    }

    public void Begin()
    {
        thisCanvas.enabled = true;
        foreach (CardInfo card in cardWheel)
        {
            card.transform.position = rightOffScreenTransform.position;
            card.gameObject.SetActive(true);
        }
        index = 0;
        cardWheel[index].transform.position  =CenterTransform.position;
        cardWheel[index].transform.rotation = CenterTransform.rotation;
        cardWheel[index].gameObject.SetActive(true);
        cardName.text = cardWheel[index].cardName;
    }

    public IEnumerator Scroll(CardInfo currentCard, CardInfo nextCard, string direction)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime; // Calculate interpolation parameter

            if (direction == "left")
            {
                currentCard.transform.position = Vector3.Lerp(currentCard.transform.position, leftOffScreenTransform.position, t);
                currentCard.transform.rotation = Quaternion.Lerp(currentCard.transform.rotation, leftOffScreenTransform.rotation, t);

                nextCard.transform.position = Vector3.Lerp(nextCard.transform.position, CenterTransform.position, t);
                nextCard.transform.rotation = Quaternion.Lerp(nextCard.transform.rotation, CenterTransform.rotation, t);
            }
            else
            {
                currentCard.transform.position = Vector3.Lerp(currentCard.transform.position, rightOffScreenTransform.position, t);
                currentCard.transform.rotation = Quaternion.Lerp(currentCard.transform.rotation, rightOffScreenTransform.rotation, t);

                nextCard.transform.position = Vector3.Lerp(nextCard.transform.position, CenterTransform.position, t);
                nextCard.transform.rotation = Quaternion.Lerp(nextCard.transform.rotation, CenterTransform.rotation, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final positions
        currentCard.transform.position = (direction == "left") ? leftOffScreenTransform.position : rightOffScreenTransform.position;
        currentCard.transform.rotation = (direction == "left") ? leftOffScreenTransform.rotation : rightOffScreenTransform.rotation;
        nextCard.transform.position = CenterTransform.position;
        nextCard.transform.rotation = CenterTransform.rotation;
    }
}
