using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordParser : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> levelRecord;

    private void Awake()
    {
        /*PlayerPrefs.SetFloat($"Level{0}", 0);
        PlayerPrefs.SetFloat($"Level{1}", 0);
        PlayerPrefs.SetFloat($"Level{2}", 0);
        PlayerPrefs.SetFloat($"Level{3}", 0);*/
    }

    void Start()
    {
        var firstLevelResult = PlayerPrefs.GetFloat($"Level{0}") == 0 ? "empty" : PlayerPrefs.GetFloat($"Level{0}").ToString();
        var secondLevelResult = PlayerPrefs.GetFloat($"Level{1}") == 0 ? "empty" : PlayerPrefs.GetFloat($"Level{1}").ToString();
        var thirdLevelResult = PlayerPrefs.GetFloat($"Level{2}") == 0 ? "empty" : PlayerPrefs.GetFloat($"Level{2}").ToString();
        var forthLevelResult = PlayerPrefs.GetFloat($"Level{3}") == 0 ? "empty" : PlayerPrefs.GetFloat($"Level{3}").ToString();

        levelRecord[0].text = firstLevelResult;
        levelRecord[1].text = secondLevelResult;
        levelRecord[2].text = thirdLevelResult;
        levelRecord[3].text = forthLevelResult;
        
        /*for(var levelIndex = 0; levelIndex < levelRecord.Count; levelIndex++)
        {
            var result = PlayerPrefs.GetFloat($"Level{levelIndex}");
            Debug.Log($"Got result {result} from Level{levelIndex}");
            if (result == 0)
            {
                levelRecord[levelIndex].text = "empty";
                continue;
            }
            levelRecord[levelIndex].text = result.ToString();
            levelIndex += 1;
        }*/
    }
}
