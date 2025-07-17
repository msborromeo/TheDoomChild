using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace DChild.Gameplay.UI.Map
{
    public class MapZoomHandler : MonoBehaviour
    {
        [SerializeField] float _minimumScale = 0.5f;
        [SerializeField] float _initialScale = 1f;
        [SerializeField] float _maximumScale = 3f;

        [SerializeField] float _scaleIncrement = .5f;

        [HideInInspector] Vector3 _scale;

        RectTransform _thisTransform;

        private void Awake()
        {

            _thisTransform = transform as RectTransform;

            _scale.Set(_initialScale, _initialScale, 1f);
            _thisTransform.localScale = _scale;

        }

        public void OnScroll(PointerEventData eventData)
        {
            Vector2 relativeMousePosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_thisTransform, Input.mousePosition, null, out relativeMousePosition);

            float delta = eventData.scrollDelta.y;

            if (delta > 0 && _scale.x < _maximumScale)
            {   //zoom in

                _scale.Set(_scale.x + _scaleIncrement, _scale.y + _scaleIncrement, 1f);
                _thisTransform.localScale = _scale;
                _thisTransform.anchoredPosition -= (relativeMousePosition * _scaleIncrement);
            }

            else if (delta < 0 && _scale.x > _minimumScale)
            {   //zoom out
                float scalex = Mathf.Clamp(_scale.x - _scaleIncrement, _minimumScale, _maximumScale);
                float scaley = Mathf.Clamp(_scale.y - _scaleIncrement, _minimumScale, _maximumScale);
                _scale.Set(scalex, scaley, 1f);
                _thisTransform.localScale = _scale;
                _thisTransform.anchoredPosition += (relativeMousePosition * _scaleIncrement);
            }
        }
    }
}