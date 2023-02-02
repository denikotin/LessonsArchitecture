using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class HealthBar : MonoBehaviour
    {
        public Image imageCurrent;

        public void SetValue(float current, float max) => imageCurrent.fillAmount = current / max;
    }
}
