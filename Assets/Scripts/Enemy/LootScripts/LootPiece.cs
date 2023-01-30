using Assets.Scripts.Data.LootData;
using Assets.Scripts.Data.PlayerProgressFolder;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Enemy.LootScripts
{
    public class LootPiece : MonoBehaviour
    {
        public GameObject skull;
        public GameObject pickUpFxPrefab;
        public TextMeshProUGUI lootText;
        public GameObject PickUpPopUp;

        private Loot _loot;
        private bool _picked = false;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other)
        {
            PickUp();
        }

        private void PickUp()
        {
            if (_picked)
                return;

            _picked = true;
            _worldData.LootData.Collect(_loot);
            skull.SetActive(false);
            PlayPickUpFX();
            ShowText();
            StartCoroutine(StartDestroyTimer());
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }

        private void PlayPickUpFX()
        {
            Instantiate(pickUpFxPrefab, transform.position, Quaternion.identity);
        }

        private void ShowText()
        {
            lootText.text = $"{_loot.value}";
            PickUpPopUp.SetActive(true);
        }
    }
}