using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _scaleAnimation;

    [SerializeField]
    private float _duration = 1f;

    private float _progress;
    private float _height = 1f;

    private RectTransform _rectTransform;

    private bool _isRound = false;
    private bool _isActive = true;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _progress = _duration;
    }

    private void PlayAnimationUp()
    {
        if (_isRound == false)
            return;

        _progress += Time.deltaTime;

        if (_progress > _duration)
            _isRound = false;

        float progress = _progress / _duration;

        float animation = _scaleAnimation.Evaluate(progress) * _height;

        _rectTransform.localScale = new Vector3(animation, animation, animation);
    }

    private void PlayAnimationDown()
    {
        if (_isRound == true)
            return;

        _progress -= Time.deltaTime;

        if (_progress < (_duration - _duration * 0.2))
            _isRound = true;

        float progress = _progress / _duration;

        float animation = _scaleAnimation.Evaluate(progress) * _height;

        _rectTransform.localScale = new Vector3(animation, animation, animation);
    }

    private void Update()
    {
        if (_isActive == true)
        {
            PlayAnimationUp();
            PlayAnimationDown();
        }
    }
}
