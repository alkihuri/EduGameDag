using DG.Tweening;
using GameCore;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GameVFX
{
    public class PostProcessingHandler : MonoBehaviour
    {
        [SerializeField] private Volume postProcessingVolume;
        [SerializeField] private Vignette vignette;

        void Start()
        {
            vignette = ScriptableObject.CreateInstance<Vignette>();

            vignette.center.overrideState = true;
            vignette.rounded.overrideState = true;
            vignette.color.overrideState = true;

            vignette.center.value = new Vector2(0.5f, 0.5f);
            vignette.color.value = Color.green;
            vignette.rounded.value = true;

            vignette.intensity.overrideState = true;
            vignette.intensity.value = 0f;
            postProcessingVolume.profile.components.Add(vignette);
            postProcessingVolume.profile.TryGet(out vignette);
            ScoreController.instance.OnScoreValueChangedWithParameter += InstanceOnOnScoreValueChange;
        }

        private void InstanceOnOnScoreValueChange(int i)
        {
            // vignette.intensity.value = 1f;
            vignette.color.value = i>0? Color.green : Color.red;
            DOTween.To(() => vignette.intensity.value,
                x => vignette.intensity.value = x, 0.5f, 0.7f).OnComplete(() =>
            {
                DOTween.To(() => vignette.intensity.value,
                    x => vignette.intensity.value = x, 0.0f, 0.3f);
            });
        }
    }
}