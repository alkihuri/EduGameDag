using DG.Tweening;
using Entities.Ajdaha;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    public static UISlider instance;


    [SerializeField]
    private Image imageSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        FindObjectOfType<AjdahaDistanceController>().GetComponent<AjdahaDistanceController>().distanceValueChanged += UISlider.instance.ChangeSliderImageDistanceValue;
    }

    private float distanceFill;
    public void ChangeSliderImageDistanceValue(float distanceVal)
    {
        Debug.Log(distanceFill);
        Debug.Log("changed slider" + distanceVal);
        distanceFill += distanceVal;
        DOTween.To(() => imageSlider.fillAmount, x => imageSlider.fillAmount = x, distanceFill, 1);
    }
}
