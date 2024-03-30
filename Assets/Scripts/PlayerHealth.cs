using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] private GameObject healthBarBackground;
    [SerializeField] private Image healthBarFill;

    private Vector3 _originalPosition;
    private bool _isShaking = false;

    private Coroutine _lerpHealthCoroutine;

    private void Start()
    {
        OnDamage += UpdateHealthBar;
        OnDamage += StartShakeAnimation;
        OnDie += OnPlayerDeath;
    }

    private void Update()
    {
        healthBarFill.transform.position = transform.position + Vector3.up * 3;
        if (!_isShaking) return;
        healthBarBackground.transform.position = _originalPosition + Random.insideUnitSphere * 0.2f;
    }

    public void SetHealthBar(GameObject healthBar)
    {
        healthBarBackground = healthBar;

        var healthBarImages = healthBarBackground.GetComponentsInChildren<Image>();

        healthBarFill = healthBarImages.FirstOrDefault(image => image.name == "HealthBarFill");
        _originalPosition = healthBarFill.transform.position;
    }

    private void UpdateHealthBar(float damage)
    {
        var newHealth = currentHealth - damage;
        var newFillAmount = newHealth / maxHealth;

        if (_lerpHealthCoroutine != null)
        {
            StopCoroutine(_lerpHealthCoroutine);
        }
        
        _lerpHealthCoroutine = StartCoroutine(LerpHealth(newFillAmount));
    }

    private void StartShakeAnimation(float _)
    {
        if (_isShaking) return;
        StartCoroutine(ShakeAnimation());
    }

    private void OnPlayerDeath(GameObject _)
    {
        if (_lerpHealthCoroutine == null) return;
        StopCoroutine(_lerpHealthCoroutine);
    }

    IEnumerator ShakeAnimation()
    {
        _isShaking = true;
        yield return new WaitForSeconds(0.5f);
        _isShaking = false;

        healthBarBackground.transform.position = _originalPosition;
    }

    protected override void SetToDeath()
    {
        base.SetToDeath();
        GetComponentInParent<Transform>().gameObject.SetActive(false);
    }
    
    private IEnumerator LerpHealth(float newFillAmount)
    {
        const float duration = 0.2f; // Duration in seconds over which the fill amount will lerp.

        var time = 0f;
        var startFill = healthBarFill.fillAmount;

        while (time < duration)
        {
            healthBarFill.fillAmount = Mathf.Lerp(startFill, newFillAmount, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        healthBarFill.fillAmount = newFillAmount;
        StopCoroutine(_lerpHealthCoroutine);
        _lerpHealthCoroutine = null;
    }
}