using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    // [SerializeField] DamageHandler _damageHandler;
    [SerializeField] Color _flashColor = Color.white;
    [SerializeField][Range(0.25f, 1f)] private float _fadeOutTime = 0.5f;
    [SerializeField] float _minIntensity = -10f, _maxIntensity = 0f;

    [SerializeField] GameObject _explosionPrefab;

    [SerializeField] int _maxHealth = 5000;
    Renderer _renderer;
    Color _baseColor;
    static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private int _health;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _baseColor = _renderer.material.color;
        _health = _maxHealth;
    }

    public void Init(int shieldStrength)
    {
        // _damageHandler.Init(shieldStrength);
    }

    void OnEnable()
    {
        // _damageHandler.HealthChanged.AddListener(OnHealthChanged);
        // _damageHandler.ObjectDestroyed.AddListener(DestroyShields);
    }

    void OnDisable()
    {
        // _damageHandler.HealthChanged.RemoveListener(OnHealthChanged);
        // _damageHandler.ObjectDestroyed.RemoveListener(DestroyShields);
    }

    void OnHealthChanged()
    {
        StartCoroutine(FlashAndFadeShields());
    }

    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        _health -= damage;
        if (_health <= 0)
        {
            DestroyShields();
            return;
        }
        StartCoroutine(FlashAndFadeShields());
    }

    private void DestroyShields()
    {
        StopAllCoroutines();
        if (_explosionPrefab != null)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    IEnumerator FlashAndFadeShields()
    {
        Color shieldColor;
        Color emissionColor = _renderer.material.GetColor(EmissionColor);
        float currentTime = 0f;
        float intensity = _maxIntensity;
        while (currentTime < _fadeOutTime)
        {
            shieldColor = Color.Lerp(_flashColor, _baseColor, currentTime / _fadeOutTime);
            intensity = Mathf.Lerp(_maxIntensity, _minIntensity, currentTime / _fadeOutTime);
            ChangeShieldColor(shieldColor, emissionColor * Mathf.Pow(2, intensity));
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield break;
    }

    private void ChangeShieldColor(Color shieldColor, Color emissionColor)
    {
        _renderer.material.color = shieldColor;
        _renderer.material.SetColor(EmissionColor, emissionColor);
    }
}
