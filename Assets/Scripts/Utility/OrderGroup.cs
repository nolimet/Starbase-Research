using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGroup : MonoBehaviour
{
    [SerializeField]
    private OrderByButton[] orderByButtons = new OrderByButton[0];

    // Start is called before the first frame update
    private void Start()
    {
        foreach (var orderByButton in orderByButtons)
        {
            orderByButton.buttonSelected += OnButtonSelected;
            orderByButton.Deleselect();
        }
    }

    private void OnButtonSelected(OrderByButton arg0)
    {
        foreach (var orderByButton in orderByButtons)
        {
            if (orderByButton != arg0)
            {
                orderByButton.Deleselect();
            }
        }
    }
}