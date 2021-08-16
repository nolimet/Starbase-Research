using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrderByButton : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<bool> OrderByFunction;

    [SerializeField]
    private Button SortSmallToLargeButton;

    [SerializeField]
    private Button SortLargeToSmallButton;

    public UnityAction<OrderByButton> buttonSelected;

    private sortOrderStates sortState = sortOrderStates.Delesected;

    private enum sortOrderStates
    {
        LargeToSmall,
        SmallToLarge,
        Delesected
    }

    public void SortSmallToLarge()
    {
        sortState = sortOrderStates.SmallToLarge;
        OrderByFunction.Invoke(true);
        SortSmallToLargeButton.interactable = false;
        SortLargeToSmallButton.interactable = true;
    }

    public void SortLargeToSmall()
    {
        sortState = sortOrderStates.LargeToSmall;
        OrderByFunction.Invoke(false);
        SortSmallToLargeButton.interactable = true;
        SortLargeToSmallButton.interactable = false;
    }

    public void Select()
    {
        if (sortState == sortOrderStates.Delesected || sortState == sortOrderStates.LargeToSmall)
        {
            SortSmallToLarge();
        }
        else if (sortState == sortOrderStates.SmallToLarge)
        {
            SortLargeToSmall();
        }

        buttonSelected?.Invoke(this);
    }

    public void Deleselect()
    {
        sortState = sortOrderStates.Delesected;
        SortSmallToLargeButton.interactable = false;
        SortLargeToSmallButton.interactable = false;
    }
}