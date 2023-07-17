using UnityEngine;

public class GamePanelAnimation : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _scaleAnimation;

    [SerializeField]
    private float _duration = 1f;

    private float _progress = 0f;
    private float _height = 1f;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _progress = 0;
        _rectTransform.localScale = Vector3.zero;
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _progress += Time.deltaTime;

        if (_progress > _duration)
            return;

        float progress = _progress / _duration;

        float animation = _scaleAnimation.Evaluate(progress) * _height;

        _rectTransform.localScale = new Vector3(animation, animation, animation);
    }

    private void Update()
    {
        PlayAnimation();
    }
}
