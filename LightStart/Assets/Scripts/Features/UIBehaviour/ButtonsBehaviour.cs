using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform rectTransform;
    private float _maxScaleParam = 1.2f;
    private float _scaleParam;
    
    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        while (_scaleParam < 1.2f)
        {
            rectTransform.transform.localScale = new Vector3(_scaleParam, _scaleParam, 0);
            _scaleParam += 0.01f;
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        while (_scaleParam > 1)
        {
            rectTransform.transform.localScale = new Vector3(_scaleParam, _scaleParam, 0);
            _scaleParam -= 0.01f;
        }
        _scaleParam = 1;
    }
}