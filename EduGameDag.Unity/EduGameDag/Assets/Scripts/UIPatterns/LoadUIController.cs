﻿using System.Collections;
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
    private QuestLoader loader;

    [SerializeField]
    private TMP_Dropdown lessonDropdown;


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

    public void Sumbit() // я этот метод чисто через чтарый файл нашел ошалеть...
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
