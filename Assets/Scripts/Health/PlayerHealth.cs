using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Health
{
    public class PlayerHealth : BaseHealth
    {
        [Header("Player Canvas Settings")]
        [SerializeField] private Canvas playerWorldCanvasPrefab;
        [SerializeField] private Camera canvasCamera;
        
        [Header("Utility Settings")]
        [SerializeField] private bool isGodMode;
        
        private Image _healthBarFill;
        private bool _isShaking;
        
        private Coroutine _lerpHealthCoroutine;

        private void Start()
        {
            OnDamage += UpdateHealthBar;
            OnDamage += StartShakeAnimation;
            OnDie += OnPlayerDeath;
            
            CreateHealthBar();
        }

        private void Update()
        {
            var currentPosition = transform.position + Vector3.up * 3;
            _healthBarFill.transform.position = currentPosition;
            if (!_isShaking) return;
            _healthBarFill.transform.position = currentPosition + Random.insideUnitSphere * 0.2f;
        }

        private void CreateHealthBar()
        {
            var canvasInstance = Instantiate(playerWorldCanvasPrefab, Vector3.zero, Quaternion.identity);
            canvasInstance.renderMode = RenderMode.WorldSpace;
            
            canvasInstance.worldCamera = canvasCamera;
            
            var healthBarImages = canvasInstance.GetComponentsInChildren<Image>();
            
            _healthBarFill = healthBarImages.FirstOrDefault(image => image.name == "HealthBarFill");

            if (_healthBarFill != null) return;
            Debug.LogError("Health bar fill image not found");
        }

        public override float Damage(float damage)
        {
            if (isGodMode || damage <= 0) return maxHealth;
            return base.Damage(damage);
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

            _healthBarFill.transform.position =  transform.position + Vector3.up * 3;
        }

        protected override void SetToDeath()
        {
            base.SetToDeath();
            GetComponentInParent<Transform>().gameObject.SetActive(false);
        }

        private IEnumerator LerpHealth(float newFillAmount)
        {
            const float duration = 0.2f;

            var time = 0f;
            var startFill = _healthBarFill.fillAmount;

            while (time < duration)
            {
                _healthBarFill.fillAmount = Mathf.Lerp(startFill, newFillAmount, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            _healthBarFill.fillAmount = newFillAmount;
            StopCoroutine(_lerpHealthCoroutine);
            _lerpHealthCoroutine = null;
        }
    }
}