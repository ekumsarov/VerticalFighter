using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using SimpleJSON;

namespace VecrticalFighter.UI
{
    public class CharacteristicPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _charName;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private List<Image> _points;

        [SerializeField] private Image _coinIcon;
        [SerializeField] private Image _plusIcon;

        public void SetupPanel(int currentPoint, int maxPoint, int coinCost)
        {
            if (currentPoint >= maxPoint)
            {
                _coinIcon.gameObject.SetActive(false);
                _plusIcon.gameObject.SetActive(false);
                _costText.gameObject.SetActive(false);
            }
            else
            {
                _coinIcon.gameObject.SetActive(true);
                _plusIcon.gameObject.SetActive(true);
                _costText.gameObject.SetActive(true);
            }

            for (int i = 0; i < 5; i++)
            {
                if (i < currentPoint)
                {
                    _points[i].color = new Color(1f, 1f, 1f);
                    _points[i].gameObject.SetActive(true);
                }
                else if (i >= currentPoint && i < maxPoint)
                {
                    _points[i].color = new Color(1f, 0f, 0f);
                    _points[i].gameObject.SetActive(true);
                }
                else
                    _points[i].gameObject.SetActive(false);
            }

            _costText.text = coinCost.ToString();
        }

        public void SetupCloseShipPanel(int currentPoint, int maxPoint)
        {
            _coinIcon.gameObject.SetActive(false);
            _plusIcon.gameObject.SetActive(false);
            _costText.gameObject.SetActive(false);

            for (int i = 0; i < 5; i++)
            {
                if (i < currentPoint)
                {
                    _points[i].color = new Color(1f, 1f, 1f);
                    _points[i].gameObject.SetActive(true);
                }
                else if (i >= currentPoint && i < maxPoint)
                {
                    _points[i].color = new Color(1f, 0f, 0f);
                    _points[i].gameObject.SetActive(true);
                }
                else
                    _points[i].gameObject.SetActive(false);
            }
        }
    }
}
