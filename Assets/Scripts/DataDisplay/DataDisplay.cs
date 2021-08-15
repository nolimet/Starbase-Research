using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DataDisplay : MonoBehaviour
{
    [Inject]
    private ResearchDataService researchDataService;

    [Inject]
    private ResearchItem.Factory researchItemFactory;

    [SerializeField]
    private Transform researchItemContainer;

    private readonly List<ResearchItem> researchItems = new List<ResearchItem>();

    private void Awake()
    {
        researchDataService.DataChanged += ResearchDataService_DataChanged;
    }

    private void ResearchDataService_DataChanged()
    {
        foreach (var researchItem in researchItems)
        {
            Destroy(researchItem.gameObject);
        }
        researchItems.Clear();

        foreach (var researchItem in researchDataService.ResearchObjects)
        {
            researchItems.Add(researchItemFactory.Create(researchItemContainer, researchItem));
        }
    }
}