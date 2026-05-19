using UnityEngine;
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

    public Image[] prepImages;

    public void DocketSetup()
    {
        if (docketOrder.isSlopBowl)
        {
            activeImages = onePrepMethodLocations;
        }

        else
        {
            switch (docketOrder.requiredPrepMethod)
            {
                case Order.PrepMethod.Dirty_Sliced_Raw or Order.PrepMethod.Dirty_Unsliced_Cooked or Order.PrepMethod.Clean_Unsliced_Raw:
                    activeImages = onePrepMethodLocations;
                    break;

                case Order.PrepMethod.Clean_Sliced_Raw or Order.PrepMethod.Clean_Unsliced_Cooked or Order.PrepMethod.Dirty_Sliced_Cooked:
                    activeImages = twoPrepMethodLocations;
                    break;

                case Order.PrepMethod.Clean_Sliced_Cooked:
                    activeImages = threePrepMethodLocations;
                    break;

                default:
                    break;
            }
        }

        ingredientIcon.sprite = docketOrder.ingredient.associatedObject.ingredientIcon;

        for (int i = 0; i < prepImages.Length; i++)
        {
            activeImages[i].sprite = prepImages[i].sprite;
        }
    }
}