using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.AFPC.Scripts
{
    /// <summary>
    /// Example HUD class for health, shield and endurance values.
    /// </summary>
    public class HUD : MonoBehaviour
    {
        [Header("References")]
        public Hero hero;
        //public Slider slider_Shield;
        public Slider slider_Health;
        //public Slider slider_Endurance;
        public CanvasGroup canvasGroup_DamageFX;
        public CanvasGroup canvasGroup_HealFX;
        public TextMeshProUGUI timerDisplay;
        
        

        private void Awake () {
            if (hero) {
                //slider_Shield.maxValue = hero.lifecycle.referenceShield;
                slider_Health.maxValue = hero.lifecycle.referenceHealth;
                //slider_Endurance.maxValue = hero.movement.referenceEndurance;
            }
        }

        private void Update () {
            if (hero) {
                //slider_Shield.value = hero.lifecycle.GetShieldValue();
                slider_Health.value = hero.lifecycle.GetHealthValue();
                //slider_Endurance.value = hero.movement.GetEnduranceValue();
            }
            
            timerDisplay.text = hero.TimerValue.ToString();
            canvasGroup_DamageFX.alpha = Mathf.MoveTowards (canvasGroup_DamageFX.alpha, 0, Time.deltaTime * 2);
            if (canvasGroup_HealFX.alpha != 0) canvasGroup_HealFX.alpha = Mathf.MoveTowards (canvasGroup_HealFX.alpha, 0, Time.deltaTime * 2);
        }

        public void DamageFX () 
        {
            canvasGroup_DamageFX.alpha = 1;
        }

        public void HealFX()
        {
            canvasGroup_HealFX.alpha = 1;
        }
    }
}
