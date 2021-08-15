using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Editor Installer", menuName = "Installers/Editor Installer")]
public class EditorInstaller : ScriptableObjectInstaller
{
    [SerializeField]
    private MaterialDataEditor materialDataEditorPrefab;

    [SerializeField]
    private MaterialIconsConfiguration materialIconsConfiguration;

    public override void InstallBindings()
    {
        Container.Bind<ResearchDataService>().ToSelf().AsCached().IfNotBound();
        Container.BindInterfacesAndSelfTo<TabActionService>().AsSingle();

        Container.BindInstance(materialIconsConfiguration).AsSingle();

        Container.BindFactory<MaterialDataEditor, MaterialDataEditor.Factory>().FromComponentInNewPrefab(materialDataEditorPrefab);
    }
}