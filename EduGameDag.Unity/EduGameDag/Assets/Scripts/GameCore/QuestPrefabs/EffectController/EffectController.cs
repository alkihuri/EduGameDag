using System.Collections;
using System.Collections.Generic;
using GameCore.Questions;
using GameCore.QuestPrefabs;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] GameObject _particleSystem;

    private void OnDestroy()
    {
        QuestionGenerator.Instance.ObjectCleanup(Instantiate(Instantiate(_particleSystem, transform.position,
            Quaternion.identity)));
    }
}