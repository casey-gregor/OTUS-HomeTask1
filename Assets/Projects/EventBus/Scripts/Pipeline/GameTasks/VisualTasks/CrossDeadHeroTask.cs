using UI;
using Unity.Mathematics;
using UnityEngine;

namespace EventBus
{
    public sealed class CrossDeadHeroTask : GameTask
    {
        private readonly UIService _uiService;
        private readonly HeroView _targetView;

        public CrossDeadHeroTask(UIService uiService, HeroView targetView)
        {
            _uiService = uiService;
            _targetView = targetView;
        }
        protected override void OnRun()
        {
            if (_uiService.TryGetCrossImagePrefab(out GameObject crossImagePrefab))
            {
                GameObject.Instantiate(
                    crossImagePrefab,
                    _targetView.gameObject.transform.position, 
                    quaternion.identity, 
                    _targetView.gameObject.transform);
            }
               
            
            Finish();
        }
    }
}