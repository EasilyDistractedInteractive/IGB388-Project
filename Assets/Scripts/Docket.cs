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
    [SerializeField] private Image sliceIcon;
    [SerializeField] private Image washIcon;
    [SerializeField] private Image cookIcon;
    [SerializeField] private Image slopAIcon;
    [SerializeField] private Image slopBIcon;
    [SerializeField] private Image slopCIcon;

    public void DocketSetup()
    {
        if (docketOrder.isSlopBowl)
        {
            switch (docketOrder.requiredSlopState)
            {
                case Order.SlopStates.Slop_A_Full:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = slopAIcon.sprite;
                    break;

                case Order.SlopStates.Slop_B_Full:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = slopBIcon.sprite;
                    break;

                case Order.SlopStates.Slop_C_Full:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = slopCIcon.sprite;
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
                    activeImages[0].sprite = washIcon.sprite;
                    activeImages[1].sprite = sliceIcon.sprite;
                    activeImages[2].sprite = cookIcon.sprite;
                    break;

                case Order.PrepMethod.Clean_Sliced_Raw:
                    activeImages = twoPrepMethodLocations;
                    activeImages[0].sprite = washIcon.sprite;
                   activeImages[1].sprite = sliceIcon.sprite;
                    break;

                case Order.PrepMethod.Clean_Unsliced_Cooked:
                    activeImages = twoPrepMethodLocations;
                    activeImages[0].sprite = washIcon.sprite;
                    activeImages[1].sprite = cookIcon.sprite;
                    break;

                case Order.PrepMethod.Clean_Unsliced_Raw:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = washIcon.sprite;
                    break;

                case Order.PrepMethod.Dirty_Sliced_Cooked:
                    activeImages = twoPrepMethodLocations;
                    activeImages[0].sprite = sliceIcon.sprite;
                    activeImages[1].sprite = cookIcon.sprite;
                    break;

                case Order.PrepMethod.Dirty_Sliced_Raw:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = sliceIcon.sprite;
                    break;

                case Order.PrepMethod.Dirty_Unsliced_Cooked:
                    activeImages = onePrepMethodLocations;
                    activeImages[0].sprite = cookIcon.sprite;
                    break;

                case Order.PrepMethod.Dirty_Unsliced_Raw:
                    break;

                default:
                    break;
            }
        }

        ingredientIcon.gameObject.SetActive(true);
        ingredientIcon.sprite = docketOrder.ingredient.associatedObject.ingredientIcon;

        foreach (Image activeImage  in activeImages)
        {
            activeImage.gameObject.SetActive(true);
        }
    }
}