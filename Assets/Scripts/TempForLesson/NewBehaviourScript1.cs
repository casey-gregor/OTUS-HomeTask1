using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ILoadable
    {
        void Load();
    }

    public class GameManager : MonoBehaviour, ILoadable
    {
        public void Load()
        {
            Debug.Log("GameManager loaded");
        }
    }

    public class  Player : MonoBehaviour, ILoadable
    {
        public void Load()
        {
            Debug.Log("Player loaded");
        }
    }

    public class Inventory : MonoBehaviour, ILoadable
    {
        public void Load()
        {
            Debug.Log("Inventory loaded");
        }
    }

    [CreateAssetMenu(fileName = "LoadOrder", menuName = "ScriptableObjects/LoadOrder", order = 1)]
    public class LoadOrder : ScriptableObject
    {

        public GameObject[] loadables;
    }

    public class UniversalLoader : MonoBehaviour
    {
        [SerializeField] private LoadOrder loadOrder;

        private void Start()
        {
            LoadComponents();
        }

        private void LoadComponents()
        {
            foreach (GameObject loadable in loadOrder.loadables)
            {
                if (loadable.TryGetComponent<ILoadable>(out ILoadable component) != false)
                {
                    component.Load();
                }
            }
        }
    }
}