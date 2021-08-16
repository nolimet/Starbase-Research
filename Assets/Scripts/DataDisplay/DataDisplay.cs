using System;
using System.Collections.Generic;
using System.Linq;
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

    public void OrderByTotalKV(bool reversed) => OrderBy(reversed, x => x.Searchdata.totalKV);

    public void OrderByCubeEfficiency(bool reversed) => OrderBy(reversed, x => x.Searchdata.cubeEfficiency);

    public void OrderByPowerEfficiency(bool reversed) => OrderBy(reversed, x => x.Searchdata.powerEfficiency);

    public void OrderByGearEfficiency(bool reversed) => OrderBy(reversed, x => x.Searchdata.gearEfficiency);

    public void OrderByShieldEfficiency(bool reversed) => OrderBy(reversed, x => x.Searchdata.shieldEfficiency);

    private void OrderBy<TKey>(bool reversed, Func<ResearchItem, TKey> keySelector)
    {
        List<ResearchItem> items;
        if (reversed)
        {
            items = researchItems.OrderBy(keySelector).Reverse().ToList();
        }
        else
        {
            items = researchItems.OrderBy(keySelector).ToList();
        }

        for (int i = 0; i < items.Count; i++)
        {
            ResearchItem item = items[i];
            item.transform.SetSiblingIndex(i);
            item.SetBackgroundEnabled(i % 2 == 0);
        }
    }

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
            var newItem = researchItemFactory.Create(researchItemContainer, researchItem);
            newItem.SetBackgroundEnabled(researchItems.Count % 2 == 0);
            researchItems.Add(newItem);
        }
    }
}