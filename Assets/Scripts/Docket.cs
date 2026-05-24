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
                    docketOrder.orderScore = 10;
                    activeImages[0].sprite = slopAIcon;
                    break;

                case Order.SlopStates.Slop_B_Full:
                    activeImages = onePrepMethodLocations;
                    docketOrder.orderScore = 10;
                    activeImages[0].sprite = slopBIcon;
                    break;

                case Order.SlopStates.Slop_C_Full:
                    activeImages = onePrepMethodLocations;
                    docketOrder.orderScore = 10;
                    activeImages[0].sprite = slopCIcon;
                    break;

                default:
                    activeImages = onePrepMethodLocations;
                    docketOrder.orderScore = 10;
                    activeImages[0].sprite = slopAIcon;
                    docketOrder.ingredient.associatedObject.currentSlopState = IngredientLogic.slopStates.Slop_A_Full;
                    break;
            }
        }
        else
        {
            switch (docketOrder.requiredPrepMethod)
            {
                case Order.PrepMethod.Clean_Sliced_Cooked:
                    activeImages = threePrepMethodLocations;
                    docketOrder.orderScore = 30;
                    activeImages[0].sprite = washIcon;
                    activeImages[1].sprite = sliceIcon;
                    activeImages[2].sprite = cookIcon;
                    break;

                case Order.PrepMethod.Clean_Sliced_Raw:
                    activeImages = twoPrepMethodLocations;
                    docketOrder.orderScore = 20;
                    activeImages[0].sprite = washIcon;
                    activeImages[1].sprite = sliceIcon;
                    break;

                case Order.PrepMethod.Clean_Unsliced_Cooked:
                    activeImages = twoPrepMethodLocations;
                    docketOrder.orderScore = 20;
                    activeImages[0].sprite = washIcon;
                    activeImages[1].sprite = cookIcon;
                    break;

                case Order.PrepMethod.Clean_Unsliced_Raw:
                    activeImages = onePrepMethodLocations;
                    docketOrder.orderScore = 10;
                    activeImages[0].sprite = washIcon;
                    break;

                case Order.PrepMethod.Dirty_Sliced_Cooked:
                    activeImages = twoPrepMethodLocations;
                    docketOrder.orderScore = 10;
                    activeImages[0].sprite = sliceIcon;
                    activeImages[1].sprite = cookIcon;
                    break;

                case Order.PrepMethod.Dirty_Sliced_Raw:
                    activeImages = onePrepMethodLocations;
                    docketOrder.orderScore = 10;
                    activeImages[0].sprite = sliceIcon;
                    break;

                case Order.PrepMethod.Dirty_Unsliced_Cooked:
                    activeImages = onePrepMethodLocations;
                    docketOrder.orderScore = 10;
                    activeImages[0].sprite = cookIcon;
                    break;

                case Order.PrepMethod.Dirty_Unsliced_Raw:
                    docketOrder.orderScore = 5;
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