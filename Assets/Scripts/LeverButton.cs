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
        Invoke("RevertToReleasedSprite", 2f);

        buttonImage.sprite = releasedAndHighlightedSprite;
    }

    private void RevertToReleasedSprite()
    {
        buttonImage.sprite = releasedSprite;
    }
}
