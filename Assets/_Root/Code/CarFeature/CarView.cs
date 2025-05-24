using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.Code.CarFeature
{
    public class  CarView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        
        [SerializeField] private Image _healthBar;
        [SerializeField] private Sprite[] _carSprites;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public CarPresenter CarPresenter { get; private set; }
        
        public event Action OnSoundPlay = delegate { };

        public void SetSprite(int value)
        {
            if (_spriteRenderer.sprite == _carSprites[value])
            {
                return;
            }
            _spriteRenderer.sprite = _carSprites[value];
        }

        public void Initialize(CarPresenter carPresenter)
        {
            CarPresenter = carPresenter;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            CarPresenter.GetDamage();
        }

        public void ChangeHealthBar(float maxHealth, float currentHealth)
        {
            var amount = currentHealth / maxHealth;
            _healthBar.rectTransform.localScale = new Vector3(amount, 1, 1);
        }

        public void PlaySound()
        {
            OnSoundPlay();
        }
    }
}