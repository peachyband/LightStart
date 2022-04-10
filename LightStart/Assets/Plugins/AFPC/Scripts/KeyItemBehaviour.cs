using System.Collections;
using Features.Manager;
using UnityEngine;

namespace Plugins.AFPC.Scripts
{
    public class KeyItemBehaviour : MonoBehaviour
    {
        [SerializeField] private CanvasGroup screenFade;
        [SerializeField] private LevelManager levelManager;

    
        private float elapsedTime;
        private float fadeDuration = 1f;

        private void OnCollisionEnter(Collision other)
        {
            levelManager.CaptureRecord = false;
            if (!other.transform.CompareTag("Player")) return;
            StartCoroutine(ScreenFadeIn());
        }

        private IEnumerator ScreenFadeIn()
        {
        
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                screenFade.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
                yield return null;
            }
        
            yield return new WaitForSeconds(0.5f);
            LevelManager.NextLevel();
        }
    }
}
