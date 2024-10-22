using TMPro;
using UI;
using UnityEngine;
using Button = UnityEngine.UIElements.Button;

namespace EventBus
{
    public sealed class ProcessTurnDataTask : GameTask
    {
        private readonly StoreAttackDataService _storeAttackDataService;
        private readonly StoreEffectsUseService storeEffectsUseService;
        private readonly PipelineContext _pipelineContext;
        private readonly UIService _uiService;

        private GameObject _infoPanel;
        public ProcessTurnDataTask(StoreEffectsUseService storeEffectsUseService, UIService uiService, PipelineContext pipelineContext, StoreAttackDataService storeAttackDataService)
        {
            this.storeEffectsUseService = storeEffectsUseService;
            _uiService = uiService;
            _pipelineContext = pipelineContext;
            this._storeAttackDataService = storeAttackDataService;
        }
        
         protected override void OnRun()
         {
             string specialEffectsLog = "";
             foreach (EffectsUsedData entry in storeEffectsUseService.Effects)
             {
                 Debug.Log("found stored effect : " + entry.Effect.EffectName);
                 specialEffectsLog += $"{entry.Effect.GetMessage()}\n";
                 
             }
             
             string attackTurnLog = "";
             foreach (AttackedUsedData entry in _storeAttackDataService.Attacks)
             {
                 Debug.Log("target : " + entry.Target.View.name);
                 Debug.Log("entry.Target.HealthComponent.HealthAfterTurn : " + entry.Target.HealthComponent.HealthAfterTurn);
                 Debug.Log("entry.Target.HealthComponent.CurrentHealth : " + entry.Target.HealthComponent.CurrentHealth);
                 if (entry.DamageTaken != 0)
                 {
                    attackTurnLog += $"{entry.Target.View.name} was hit with {entry.DamageTaken} hitpoints.\n";
                 }
                 
                 entry.Target.HealthComponent.SetHealthAfterTurn(entry.Target.HealthComponent.CurrentHealth);
             }
            
            _infoPanel = _uiService.GetInfoPanel();
            _infoPanel.SetActive(true);
            _infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = attackTurnLog + specialEffectsLog;
            _infoPanel.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            _infoPanel.GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(HandleClick);
            _uiService.GetInfoPanel().SetActive(false);
            
            _storeAttackDataService.Attacks.Clear();
            storeEffectsUseService.Effects.Clear();
            
            Debug.Log("_storeAttackDataService.Attacks.Count : " + _storeAttackDataService.Attacks.Count);
            Debug.Log(" _captureEffectsUseService.Effects.Count : " +  storeEffectsUseService.Effects.Count);
            
            Finish();
        }
    }
}