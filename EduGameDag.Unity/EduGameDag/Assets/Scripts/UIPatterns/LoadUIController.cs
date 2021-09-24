using System.Collections;
using System.Collections.Generic;
using GameCore;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Entities.Ajdaha;
using GameCore.Qustions;

public class LoadUIController : MonoBehaviour
{
    [SerializeField]
    private QuestLoader loader;

    [SerializeField]
    private Dropdown lessonDropdown;


    private int currentSelectedValue;

    void Start()
    {
        loader.OnJsonLoaded += () =>
        {
            for (int i = 0; i < loader.TourQuests.tourQuests.Length; i++)
            {
                lessonDropdown.options[i].text = loader.TourQuests.tourQuests[i].tourName;
            }
            lessonDropdown.onValueChanged.AddListener(
                delegate
                {
                    DebugOnDropDown(lessonDropdown.value);
                });
        };
    }

    public void Sumbit()
    {
        loader.QuestionGenerator.CurrentQuestPack = currentSelectedValue;
        GetComponent<RectTransform>().DOAnchorPosX(-1080, 1f);
        GameStateController.instance.StartGame();
    }

    void DebugOnDropDown(int changeValue)
    {
        currentSelectedValue = changeValue;
    }
}
