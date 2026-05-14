using UnityEngine;
using UnityEngine.UI;

public class Docket : MonoBehaviour
{
    public Image[] onePrepMethodLocations;
    public Image[] twoPrepMethodLocations;
    public Image[] threePrepMethodLocations;

    public Image[] activeImages;

    public Order docketOrder;

    public Image ingredientIcon;

    public void DocketSetup()
    {
        switch (docketOrder.requiredPrepMethod)
        {
            case Order.PrepMethod.Dirty_Sliced or Order.PrepMethod.Dirty_Cooked or Order.PrepMethod.Clean_Unsliced:
                activeImages = onePrepMethodLocations;
                break;

            case Order.PrepMethod.Clean_Sliced or Order.PrepMethod.Clean_Cooked or Order.PrepMethod.Sliced_Cooked:
                activeImages = twoPrepMethodLocations;
                break;

            case Order.PrepMethod.Clean_Sliced_Cooked:
                activeImages= threePrepMethodLocations;
                break;

            default:
                break;
        }

        ingredientIcon.sprite = docketOrder.ingredient.associatedObject.ingredientIcon;
    }
}