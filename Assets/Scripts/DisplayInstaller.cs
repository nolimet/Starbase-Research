using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Display Installer", menuName = "Installers/Display Installer")]
public class DisplayInstaller : ScriptableObjectInstaller
{
    [SerializeField]
    private MaterialItem materialItemPrefab;

    [SerializeField]
    private ResearchItem researchItemPrefab;

    public override void InstallBindings()
    {
        Container.Bind<ResearchDataService>().ToSelf().AsCached().IfNotBound();

        Container.BindFactory<MaterialItem, MaterialItem.Factory>().FromComponentInNewPrefab(materialItemPrefab);
        Container.BindFactory<ResearchItem, ResearchItem.Factory>().FromComponentInNewPrefab(researchItemPrefab);
    }
}