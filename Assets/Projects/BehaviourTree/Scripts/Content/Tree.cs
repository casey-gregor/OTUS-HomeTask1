using Game.Engine;
using UnityEngine;

namespace Game.Content
{
    public sealed class Tree : MonoBehaviour
    {
        private static readonly int ChopAnimHash = Animator.StringToHash("Chop");

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private ResourceStorageComponent storage;

        private void OnEnable()
        {
            this.storage.OnStateChanged += this.OnStateChanged;
        }

        private void OnDisable()
        {
            this.storage.OnStateChanged -= this.OnStateChanged;
        }

        private void OnStateChanged()
        {
            if (this.storage.IsEmpty())
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                _animator.Play(ChopAnimHash, -1, 0);
            }
        }
    }
}