using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AddressablesProject
{
    public class Test
    {
        private Transform parent;
        private Item prefab;

        private List<AsyncOperationHandle<Sprite>> loadedSprites = new();
        private async void GetSprites()
        {
            const int count = 10;
            for (int i = 0; i < count; i++)
            {
                var spriteIndex = i%count;

                loadedSprites.Add(Addressables.LoadAssetAsync<Sprite>($"Sprites/sprite_{spriteIndex}.png"));
            }

            var sprites = await Task.WhenAll(loadedSprites.Select(obj => obj.Task));
            
            for (int i = 0; i < sprites.Length; i++)
            {
                var item = GameObject.Instantiate(prefab,  parent);
                item.Render(sprites[i]);
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < loadedSprites.Count; i++)
            {
                Addressables.ReleaseInstance(loadedSprites[i]);
            }
        }
    }

    public class Item : MonoBehaviour
    {
        public void Render(object sprite)
        {
            
        }
    }
}