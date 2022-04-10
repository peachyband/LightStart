using System;
using System.Collections;
using Features.UIBehaviour;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup screenFade;
        [SerializeField] private CanvasGroup goalReminder;
        public int LevelIndex;
        public float FinishResult;
        public bool CaptureRecord;
        public float GoalFadeDuration;

        private float goalFadeElapsedTime;
        private float elapsedTime;
        private float screenFadeDuration = 1f;
        public bool LevelHasGoal;
        public bool Menu;
    
        private void Awake()
        {
            if (!Menu) StartCoroutine(ScreenFadeOut());
            else Cursor.lockState = CursorLockMode.None;
            if (LevelHasGoal) StartCoroutine(GoalFadeIn());
        }

        private void Update()
        {
            if (!CaptureRecord && !Menu)
            {
                Debug.Log($"Saved result {FinishResult} to Level{LevelIndex}");
                PlayerPrefs.SetFloat($"Level{LevelIndex}", FinishResult);
                PlayerPrefs.Save();
            }
        }

        private IEnumerator ScreenFadeOut()
        {
            yield return new WaitForSeconds(screenFadeDuration);
            while (elapsedTime < screenFadeDuration)
            {
                elapsedTime += Time.deltaTime;
                screenFade.alpha = Mathf.Lerp(1, 0, elapsedTime / screenFadeDuration);
                yield return null;
            }

            elapsedTime = 0;
            yield return new WaitForSeconds(0.5f);
        }
    
        private IEnumerator GoalFadeIn()
        {
            while (goalFadeElapsedTime < GoalFadeDuration)
            {
                goalFadeElapsedTime += Time.deltaTime;
                goalReminder.alpha = Mathf.Lerp(1, 0, goalFadeElapsedTime / GoalFadeDuration);
                yield return null;
            }
        }

        public static void GoMenu()
        {
            SceneManager.LoadScene(0);
        }

        public static void NextLevel() 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public static void ExitGame() 
        {
            Application.Quit();
        }

        public void Restart() 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
