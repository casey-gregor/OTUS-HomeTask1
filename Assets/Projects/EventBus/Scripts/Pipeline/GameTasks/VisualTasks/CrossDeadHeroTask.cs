using UI;
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
            // Debug.Log("cross dead hero task run?????");
            GameObject crossImagePrefab = _uiService.GetCrossImagePrefab();
            GameObject.Instantiate(crossImagePrefab, _targetView.gameObject.transform);
            
            Finish();
        }
    }
}