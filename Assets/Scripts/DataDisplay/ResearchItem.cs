using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResearchItem : MonoBehaviour
{
    [SerializeField]
    private new TextMeshProUGUI name = null, cube = null, power = null, shield = null, gear = null;

    [SerializeField]
    private TextMeshProUGUI totalKV = null, cubeEfficiency = null, powerEfficiency = null, shieldEfficiency = null, gearEfficiency = null;

    [SerializeField]
    private Transform MaterialContainer = null;

    [SerializeField]
    private Graphic background = null;

    [Inject]
    private ResearchDataService researchDataService;

    private readonly List<MaterialItem> materialObjects = new List<MaterialItem>();

    public ResearchObject ResearchObject { get; private set; }
    public SearchdataObject Searchdata { get; private set; }

    public void SetBackgroundEnabled(bool isEnabled)
    {
        background.enabled = isEnabled;
    }

    public void Remove()
    {
        researchDataService.RemoveResearchObject(ResearchObject);
    }

    public readonly struct SearchdataObject
    {
        public readonly double totalKV;
        public readonly double cubeEfficiency;
        public readonly double powerEfficiency;
        public readonly double shieldEfficiency;
        public readonly double gearEfficiency;

        public SearchdataObject(double totalKV, double cubeEfficiency, double powerEfficiency, double shieldEfficiency, double gearEfficiency)
        {
            this.totalKV = totalKV;
            this.cubeEfficiency = cubeEfficiency;
            this.powerEfficiency = powerEfficiency;
            this.shieldEfficiency = shieldEfficiency;
            this.gearEfficiency = gearEfficiency;
        }
    }

    public class Factory : PlaceholderFactory<ResearchItem>
    {
        [Inject]
        private MaterialItem.Factory materialItemFactory;

        public ResearchItem Create(Transform parent, ResearchObject researchObject)
        {
            var n = base.Create();
            n.transform.SetParent(parent, false);
            n.ResearchObject = researchObject;

            n.name.text = researchObject.Name;
            n.cube.text = researchObject.Cube.ToString();
            n.power.text = researchObject.Power.ToString();
            n.shield.text = researchObject.Shield.ToString();
            n.gear.text = researchObject.Gear.ToString();

            double totalKV = researchObject.Materials.Sum(x => x.Amount);

            n.Searchdata = new SearchdataObject
                (
                    totalKV: totalKV,
                    cubeEfficiency: researchObject.Cube / totalKV,
                    powerEfficiency: researchObject.Power / totalKV,
                    shieldEfficiency: researchObject.Shield / totalKV,
                    gearEfficiency: researchObject.Gear / totalKV
                );

            n.totalKV.text = string.Format("{0:#.####}kv", totalKV);
            n.cubeEfficiency.text = (researchObject.Cube / totalKV).ToString("0.####");
            n.powerEfficiency.text = (researchObject.Power / totalKV).ToString("0.####");
            n.shieldEfficiency.text = (researchObject.Shield / totalKV).ToString("0.####");
            n.gearEfficiency.text = (researchObject.Gear / totalKV).ToString("0.####");

            foreach (var material in researchObject.Materials)
            {
                n.materialObjects.Add(materialItemFactory.Create(n.MaterialContainer, material));
            }

            return n;
        }
    }
}