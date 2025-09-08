using MainFolder.Scripts.Managers;
using MainFolder.Scripts.Bird;
using MainFolder.Scripts.Environment;
using MainFolder.Scripts.Zenject.Signals;
using UnityEngine;
using Zenject;

namespace MainFolder.Scripts.Zenject
{
    public class GameInstaller : MonoInstaller
    {
        [Header("CAMERA")]
        [SerializeField] private Camera mainCam;

        [Header("GAMEPLAY PREFABS")]
        [SerializeField] private ColumnsGroup columnsGroupPrefab;

        [SerializeField] private GameObject birdPrefab;

        [SerializeField] private SoundManager soundManager;
    
        public override void InstallBindings()
        {
            //SIGNALS INSTALLER
            SignalBusInstaller.Install(Container);
        
            //BIRD SIGNALS
            Container.DeclareSignal<BirdDestroy>();
            Container.DeclareSignal<ColumnSuccess>();
            Container.DeclareSignal<CreateNewColumn>();
            Container.DeclareSignal<BirdReset>();
            
            //UI SIGNALS
            Container.DeclareSignal<GameOver>();
            Container.DeclareSignal<RestartGame>();
        
            //BINDS
            Container.Bind<GameInstaller>().AsSingle();
            Container.Bind<Camera>().FromInstance(mainCam).AsSingle();
            Container.Bind<SoundManager>().FromComponentInNewPrefab(soundManager).AsSingle();
        
            //Manager Binds
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MapManager>().FromComponentInHierarchy().AsSingle();
            
            //Player Binds
            Container.Bind<BirdController>().FromComponentInNewPrefab(birdPrefab).AsSingle();
            Container.Bind<BirdInputs>().AsSingle();
            
            //MEMORY POOL
            Container.BindMemoryPool<ColumnsGroup, ColumnsGroup.Pool>()
                .WithInitialSize(3)
                .FromComponentInNewPrefab(columnsGroupPrefab)
                .UnderTransformGroup("Map");
        
        }
    }
}
