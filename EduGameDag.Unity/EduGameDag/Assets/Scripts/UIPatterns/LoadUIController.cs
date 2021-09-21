using System.Collections;
using System.Collections.Generic;
using GameCore;
using UnityEngine;
using UnityEngine.UI;

public class LoadUIController : MonoBehaviour
{
    [SerializeField]
    private QuestLoader loader;

    [SerializeField]
    private Dropdown lessonDropdown;

    void Start()
    {
        loader.OnJsonLoaded += () =>
        {
            for (int i = 0; i < loader.TourQuests.tourQuests.Length; i++)
            {
                lessonDropdown.options[i].text = loader.TourQuests.tourQuests[i].tourName;
            }
        };
    }
}
