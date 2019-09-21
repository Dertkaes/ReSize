using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace ReSize
{
    public class ContentSizeFitter : MonoBehaviour
    {
        private RectTransform m_backgroundSize;
        private TextMeshProUGUI m_text;
        private float m_deltaSize;
        private float m_linesize;
        private UnityEvent m_reSizeEvent;

        void Start()
        {
            if (m_reSizeEvent == null)
            {
                m_reSizeEvent = new UnityEvent();
            }
            GameObject obj = this.transform.parent.gameObject;
            m_backgroundSize = obj.GetComponent<RectTransform>();
            m_text = gameObject.GetComponent<TextMeshProUGUI>();
            m_linesize = m_text.preferredHeight;
            m_deltaSize = m_backgroundSize.sizeDelta.y - m_linesize;
            m_reSizeEvent.AddListener(ReSize);
        }
        private void OnGUI()
        {
            if (m_reSizeEvent != null)
            {
                m_reSizeEvent.Invoke();
            }
        }

        private void OnDestroy()
        {
            m_reSizeEvent.RemoveAllListeners();
        }

        private void ReSize()
        {
            float sizeBackground = m_linesize + m_deltaSize;
            if (m_text.preferredHeight != 0)
            {
                sizeBackground = m_text.preferredHeight + m_deltaSize;
            }
            m_backgroundSize.sizeDelta = new Vector2(300, sizeBackground);
        }
    }
}