using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Norne
{
    public class TextSlider : MonoBehaviour
    {
        [SerializeField]
        private string format = "{0:f1}s";

        private Slider _slider;
        private TextMeshProUGUI _text;
        private float _value;

        public float Value
        {
            get => _value;
            set => _slider.value = value;
        }

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            _text = GetComponentInChildren<TextMeshProUGUI>();

            _text.text = string.Format(format, _slider.value);

            _slider.onValueChanged.AddListener(value =>
            {
                _value = value;
                _text.text = string.Format(format, _value);
                OnValueChanged?.Invoke(_value);
            });
        }

        public event UnityAction<float> OnValueChanged;
    }
}