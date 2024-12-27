using Projects.Inventory.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Inventory.Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private InventoryManager inventoryManager;
        public override void InstallBindings()
        {
            Container.Bind<IEntity>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container.Bind<InventoryEventNotifier>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<EquipmentEventNotifier>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<InventoryManager>()
                .FromInstance(inventoryManager)
                .AsSingle();
            
            Container.Bind<Inventory>()
                .AsSingle()
                .WithArguments(
                    inventoryManager.InventorySlotsNum, 
                    inventoryManager.inventorySlotCapacity)
                .NonLazy();
            
            Container.Bind<EquipmentManager>()
                .AsSingle()
                .WithArguments(
                    new EquipmentSlot(EquipmentSlotType.Head, inventoryManager.headSlotCapacity),
                    new EquipmentSlot(EquipmentSlotType.Body, inventoryManager.bodySlotCapacity),
                    new EquipmentSlot(EquipmentSlotType.RightHand, inventoryManager.rightHandSlotCapacity),
                    new EquipmentSlot(EquipmentSlotType.LeftHand, inventoryManager.leftHandSlotCapacity),
                    new EquipmentSlot(EquipmentSlotType.Feet, inventoryManager.feetSlotCapacity))
                .NonLazy();
            
            Container.Bind<InventoryLogger>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ComponentsObserver>().AsSingle().NonLazy();
            
            Container.Bind<UIManager>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<UISlotProvider>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<InventoryUIManager>()
                .AsSingle()
                .WithArguments(inventoryManager.itemPrefab)
                .NonLazy();
        }
    }
}