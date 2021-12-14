using System.Collections;
using System.Collections.Generic;
using GameCore;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Entities.Ajdaha;
using TMPro;

public class LoadUIController : MonoBehaviour
{
    [SerializeField]
    private QuestLoader _loader;

    [SerializeField]
    private TMP_Dropdown _lessonDropdown;

    [SerializeField] GameObject _firstPanel, _secondPanel;

    [SerializeField]
    AnimationCurve _panelChangeCurve;

    private int _currentSelectedValue;

    void Start()
    {
        _loader.OnJsonLoaded += () =>
        {
            for (int i = 0; i < _loader.TourQuests.tourQuests.Length; i++)
            {
                _lessonDropdown.options[i].text = _loader.TourQuests.tourQuests[i].tourName;
            }
            _lessonDropdown.onValueChanged.AddListener(
                delegate
                {
                    DebugOnDropDown(_lessonDropdown.value);
                });
        };
    }

    public void Sumbit() // я этот метод чисто через чтарый файл нашел ошалеть...
    {
        _loader.QuestionGenerator.CurrentQuestPack = _currentSelectedValue;
        //GetComponent<RectTransform>().DOAnchorPosX(-Screen.width, 1f); 
        StartCoroutine(PanelSwitcher());
        GameStateController.instance.StartGame();
    }

    IEnumerator PanelSwitcher()
    {
        for (float t = 0; t < 1;t+=Time.deltaTime/1.5f)
        {
            _firstPanel.transform.localScale = Vector3.one * _panelChangeCurve.Evaluate(t); 
            yield return new WaitForEndOfFrame();
        }
        _firstPanel.SetActive(false);
        _secondPanel.SetActive(true);
    }

    void DebugOnDropDown(int changeValue)
    {
        _currentSelectedValue = changeValue;
    }
}
