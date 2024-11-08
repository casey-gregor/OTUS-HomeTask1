using Game.Engine;
using UnityEngine;

namespace Game.Content
{
    public sealed class Character : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private MoveComponent _moveComponent;

        [SerializeField]
        private LookComponent _lookComponent;

        [Header("Harvesting")]
        [SerializeField]
        private HarvestComponent _harvestComponent;

        [SerializeField]
        private OverlapSphereComponent _overlapSphereComponent;

        [SerializeField]
        private TakeResourceComponent _takeResourceComponent;

        [Header("Storage")]
        [SerializeField]
        private ResourceStorageComponent _resourceStorage;

        private void OnEnable()
        {
            _moveComponent.OnMove += this.OnMove;
        }

        private void OnDisable()
        {
            _moveComponent.OnMove -= this.OnMove;
        }

        private void OnMove()
        {
            _lookComponent.Direction = _moveComponent.MoveDirection;
        }

        private void Start()
        {
            _harvestComponent.AddCondition(_resourceStorage.IsNotFull);
            _harvestComponent.SetProcessAction(this.RaycastResources);
        }

        private void RaycastResources()
        {
            _overlapSphereComponent.OverlapSphere(this.HarvestResource);
        }

        private bool HarvestResource(GameObject target)
        {
            return target.CompareTag(GameObjectTags.Tree) &&
                   target.activeSelf &&
                   _takeResourceComponent.TakeResources(target);
        }
    }
}