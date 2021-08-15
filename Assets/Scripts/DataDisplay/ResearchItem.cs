using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

public class ResearchItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI name, cube, power, shield, gear;

    [SerializeField]
    private TextMeshProUGUI totalKV, cubeEfficiency, powerEfficiency, shieldEfficiency, gearEfficiency;

    [SerializeField]
    private Transform MaterialContainer;

    [Inject]
    private ResearchDataService researchDataService;

    private readonly List<MaterialItem> materialObjects = new List<MaterialItem>();

    private ResearchObject researchObject;

    public void Remove()
    {
        researchDataService.RemoveResearchObject(researchObject);
    }

    public class Factory : PlaceholderFactory<ResearchItem>
    {
        [Inject]
        private MaterialItem.Factory materialItemFactory;

        public ResearchItem Create(Transform parent, ResearchObject researchObject)
        {
            var n = base.Create();
            n.transform.SetParent(parent, false);
            n.researchObject = researchObject;

            n.name.text = researchObject.Name;
            n.cube.text = researchObject.Cube.ToString();
            n.power.text = researchObject.Power.ToString();
            n.shield.text = researchObject.Shield.ToString();
            n.gear.text = researchObject.Gear.ToString();

            double totalKV = researchObject.Materials.Sum(x => x.Amount);

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