using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ParticleSystemUI : MonoBehaviour
{
    [SerializeField]
    private Sprite _sprite;

    [SerializeField]
    private Color _color = new Color(1f, 1f, 1f, 0.1f);

    [SerializeField]
    private Vector2 _randomMoveSpeed = new Vector2(0.1f, 0.5f);

    [SerializeField]
    private Vector2 _randomScale = new Vector2(1f, 3f);

    [SerializeField]
    private float _spawnDuration = 1f;

    private Transform _transform;

    private PoolObjects<Image> _particlePool;

    private Vector3[] _randomPosition;
    private float[] _moveSpeeds;
    private Vector3[] _localScales;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        GenerateRandomPosition();
        GenerateMoveSpeeds();
        GenerateLocalScales();
        CreateParticle();

        StartCoroutine(ActiveParticle());
    }

    private void Update()
    {
        MoveParticle();
    }

    private void GenerateLocalScales()
    {
        _localScales = new Vector3[40];

        for (int i = 0; i < _randomPosition.Length; i++)
        {
            float scale = Random.Range(_randomScale.x, _randomScale.y);

            _localScales[i] = new Vector3(scale, scale, 1.0f);
        }
    }

    private void GenerateMoveSpeeds()
    {
        _moveSpeeds = new float[40];

        for (int i = 0; i < _moveSpeeds.Length; i++)
        {
            _moveSpeeds[i] = Random.Range(_randomMoveSpeed.x, _randomMoveSpeed.y);
        }
    }

    private void GenerateRandomPosition()
    {
        _randomPosition = new Vector3[40];

        for (int i = 0; i < _randomPosition.Length; i++)
        {
            _randomPosition[i] = new Vector3(
                x: Random.Range(-5, 5),
                y: Random.Range(-10, 10),
                z: 0);
        }
    }

    private void MoveParticle()
    {
        if (_particlePool == null)
        {
            return;
        }

        int count = 0;

        foreach (var item in _particlePool.Pool)
        {
            item.transform.position = Vector3.MoveTowards(item.transform.position, _randomPosition[count], _moveSpeeds[count] * Time.deltaTime);
            item.transform.localScale = _localScales[count];

            if (item.transform.position == _randomPosition[count])
            {
                item.transform.position = new Vector3(
                    x: Random.Range(-5, 5),
                    y: Random.Range(-5, 5),
                    z: 0);

                item.gameObject.SetActive(false);
            }

            count++;

            if (count > _randomPosition.Length - 1)
                count = 0;
        }
    }

    private void CreateParticle()
    {
        GameObject sprite = new GameObject();

        sprite.name = "Particle";
        Image image = sprite.AddComponent<Image>();
        image.sprite = _sprite;
        image.color = _color;
        image.raycastTarget = false;

        _particlePool = new PoolObjects<Image>(image, 40, _transform);
        _particlePool.AutoExpand = true;
    }

    private IEnumerator ActiveParticle()
    {
        while (true)
        {
            Image image = _particlePool.GetFreeElement();
            image.transform.position = new Vector3(
                x: Random.Range(-5, 5),
                y: Random.Range(-5, 5),
                z: 0);

            yield return new WaitForSeconds(_spawnDuration);
        }
    }
}
