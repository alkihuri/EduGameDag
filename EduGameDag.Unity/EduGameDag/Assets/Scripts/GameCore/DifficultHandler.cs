using System.Collections.Generic;
using GameCore.Questions;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class DifficultHandler : MonoBehaviour
    {
        [SerializeField] private List<Button> difficultButtons = new List<Button>();

        //Важно!!! Количество кнопок должно соответствовать количеству уровней сложности
        [SerializeField] private List<DifficultStructure> difficultLevels;
        [SerializeField] private Color selectedColor;

        void Start()
        {
            SetDifficult(PlayerPrefs.GetInt("SelectedDifficult"));
        }

        private void OnEnable()
        {
            for (int i = 0; i < difficultButtons.Count; i++)
            {
                var i1 = i;
                difficultButtons[i].onClick.AddListener(() => SetDifficult(i1));
            }
        }

        private void SetDifficult(int index)
        {
            foreach (var btn in difficultButtons)
            {
                btn.gameObject.GetComponent<Image>().color = Color.white;
            }

            difficultButtons[index].GetComponent<Image>().color = selectedColor;
            QuestionGenerator.Instance.slowSpeed = difficultLevels[index].slowSpeed;
            QuestionGenerator.Instance.fastSpeed = difficultLevels[index].fastSpeed;
            PlayerPrefs.SetInt("SelectedDifficult", index);
        }

        private void OnDisable()
        {
            for (int i = 0; i < difficultButtons.Count; i++)
            {
                difficultButtons[i].onClick.RemoveAllListeners();
            }
        }
    }
}