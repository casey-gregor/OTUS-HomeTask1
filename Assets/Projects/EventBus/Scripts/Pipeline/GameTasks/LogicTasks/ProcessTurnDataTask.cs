using System.Text;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace EventBus
{
    public sealed class ProcessTurnDataTask : GameTask
    {
        private readonly StoreAttackDataService _storeAttackDataService;
        private readonly StoreEffectsDataService _storeEffectsDataService;
        private readonly UIService _uiService;

        private GameObject _infoPanel;
        private Button _button;

        public ProcessTurnDataTask(
            StoreEffectsDataService storeEffectsDataService, 
            StoreAttackDataService storeAttackDataService, 
            UIService uiService)
        {
            _storeEffectsDataService = storeEffectsDataService;
            _storeAttackDataService = storeAttackDataService;
            _uiService = uiService;
        }
        
         protected override void OnRun()
         {
             if (_storeAttackDataService.Attacks.Count <= 0 ||
                 _storeEffectsDataService.Effects.Count <= 0)
             {
                 Finish();
                 return;
             }
             
             if (_uiService.TryGetInfoPanel(out _infoPanel))
             {
                 _button = _infoPanel.GetComponent<Button>();
                 
                 var textComponent = _infoPanel.GetComponentInChildren<TextMeshProUGUI>();
                 
                 string attackTurnLog = GetAttackLogData();
                 string effectsLog = GetSpecialEffectsLogData();
                 
                 textComponent.text = attackTurnLog + effectsLog;
                 
                 _infoPanel.SetActive(true);
                 _button.onClick.AddListener(HandleClick);
             }
             else
             {
                 Finish();
             }
         }
         
         private void HandleClick()
         {
             _button.onClick.RemoveListener(HandleClick);
             _infoPanel.SetActive(false);
             
             _storeAttackDataService.Attacks.Clear();
             _storeEffectsDataService.Effects.Clear();
             
             Finish();
         }


         private string GetAttackLogData()
         {
             StringBuilder attackTurnLog = new();
             foreach (AttackedUsedData entry in _storeAttackDataService.Attacks)
             {
                 if (entry.DamageTaken != 0)
                 {
                     attackTurnLog.Append($"{entry.Target.View.name} was hit with {entry.DamageTaken} hitpoints.\n");
                 }
             }

             return attackTurnLog.ToString();
         }

         private string GetSpecialEffectsLogData()
         {
             StringBuilder specialEffectsLog = new();
             foreach (IEffect effect in _storeEffectsDataService.Effects)
             {
                 specialEffectsLog.Append($"{effect.GetMessage()}\n");
             }

             return specialEffectsLog.ToString();
         }
         
    }
}