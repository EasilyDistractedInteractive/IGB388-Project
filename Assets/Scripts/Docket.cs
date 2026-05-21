using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Docket : MonoBehaviour
{
    public Image[] onePrepMethodLocations;
    public Image[] twoPrepMethodLocations;
    public Image[] threePrepMethodLocations;

    public Image[] activeImages;

    public Order docketOrder;

    public Image ingredientIcon;

    public TMP_Text orderNumberText;

    [Header("Prep Method Icons")]
    [SerializeField] private Sprite sliceIcon;
    [SerializeField] private Sprite washIcon;
    [SerializeField] private Sprite cookIcon;
    [SerializeField] private Sprite slopAIcon;
    [SerializeField] private Sprite slopBIcon;
    [SerializeField] private Sprite slopCIcon;

    public void DocketSetup()
    {
        if (docketOrder.isSlopBowl)
        {
            switch (docketOrder.requiredSlopState)
            {
                case Order.SlopStates.Slop_A_Full:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = slopAIcon;
                    break;

                case Order.SlopStates.Slop_B_Full:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = slopBIcon;
                    break;

                case Order.SlopStates.Slop_C_Full:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = slopCIcon;
                    break;

                default:
                    break;
            }
        }
        else
        {
            switch (docketOrder.requiredPrepMethod)
            {
                case Order.PrepMethod.Clean_Sliced_Cooked:
                    activeImages = threePrepMethodLocations;
                    activeImages[0].sprite = washIcon;
                    activeImages[1].sprite = sliceIcon;
                    activeImages[2].sprite = cookIcon;
                    break;

                case Order.PrepMethod.Clean_Sliced_Raw:
                    activeImages = twoPrepMethodLocations;
                    activeImages[0].sprite = washIcon;
                    activeImages[1].sprite = sliceIcon;
                    break;

                case Order.PrepMethod.Clean_Unsliced_Cooked:
                    activeImages = twoPrepMethodLocations;
                    activeImages[0].sprite = washIcon;
                    activeImages[1].sprite = cookIcon;
                    break;

                case Order.PrepMethod.Clean_Unsliced_Raw:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = washIcon;
                    break;

                case Order.PrepMethod.Dirty_Sliced_Cooked:
                    activeImages = twoPrepMethodLocations;
                    activeImages[0].sprite = sliceIcon;
                    activeImages[1].sprite = cookIcon;
                    break;

                case Order.PrepMethod.Dirty_Sliced_Raw:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = sliceIcon;
                    break;

                case Order.PrepMethod.Dirty_Unsliced_Cooked:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = cookIcon;
                    break;

                case Order.PrepMethod.Dirty_Unsliced_Raw:
                    break;

                default:
                    break;
            }
        }

        ingredientIcon.gameObject.SetActive(true);
        ingredientIcon.sprite = docketOrder.ingredient.associatedObject.ingredientIcon;

        foreach (Image activeImage in activeImages)
        {
            activeImage.gameObject.SetActive(true);
        }
    }
}