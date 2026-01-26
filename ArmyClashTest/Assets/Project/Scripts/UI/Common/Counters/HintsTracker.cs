using TMPro;
using UnityEngine;

namespace Project.Scripts.UI.Common.Counters
{
    public class HintsTracker : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _valueTMP;


        private void Start()
        {
            //_gameplayEventProvider?.HintsChangedEvent.AddListener(UpdateInfo);
            UpdateInfo();
        }

        private void OnDestroy()
        {
            //_gameplayEventProvider?.HintsChangedEvent.RemoveListener(UpdateInfo);
        }

        private void UpdateInfo()
        {
            // UpdateInfo(_currencyDataService.HintsCount.Value);
        }

        private void UpdateInfo(int count) =>
            _valueTMP.text = $"{count}";
    }
}