using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverButton : MonoBehaviour
{
    public Sprite releasedSprite;
    public Sprite pressedSprite;
    public Sprite releasedAndHighlightedSprite;

    private Image buttonImage;
    private Button button;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();

        button.onClick.AddListener(OnButtonClicked);
        button.onClick.AddListener(OnButtonReleased);
    }

    private void OnButtonClicked()
    {
        buttonImage.sprite = pressedSprite;
    }

    private void OnButtonReleased()
    {
        buttonImage.sprite = releasedAndHighlightedSprite;

        Invoke("RevertToReleasedSprite", 0.2f);
    }

    private void RevertToReleasedSprite()
    {
        buttonImage.sprite = releasedSprite;
    }
}
