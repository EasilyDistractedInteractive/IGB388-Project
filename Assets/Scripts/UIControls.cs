using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// UI Element starts hidden and at 0 scale. When the player triggers via the button, the UI element will be unhidden and will scale up to its normal size. When the player triggers the hide, the UI element will scale down to 0 and then be hidden.
/// </summary>

public class UIControls : MonoBehaviour
{
    [Header("UI Elements")]

    public float animationDuration = 0.5f;
    
    public Ease showEaseType = Ease.OutBack;
    public Ease hideEaseType = Ease.InBack;

    private Vector3 originalScale;
    private bool isVisible = false;
    private bool isCooldown = false;


    [Header("Toggle Settings")]

    public float toggleCooldown = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;

        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);

        transform.DOKill();

        transform.localScale = Vector3.zero;
        transform.DOScale(originalScale, animationDuration).SetEase(showEaseType);

        isVisible = true;
    }

    public void HideUI()
    {
        transform.DOKill();

        transform.DOScale(Vector3.zero, animationDuration)
            .SetEase(hideEaseType).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });

        isVisible = false;
    }

    public void ToggleUI()
    {
        if (isCooldown) return;

        isCooldown = true;

        if (isVisible)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }

        Invoke(nameof(ResetCooldown), toggleCooldown);

    }
    private void ResetCooldown()
    {
        isCooldown = false;
    }
}
