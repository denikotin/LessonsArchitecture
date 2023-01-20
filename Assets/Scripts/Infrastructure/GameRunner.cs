using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper gameBootstrapper;
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if( bootstrapper == null)
                Instantiate(gameBootstrapper);
        }
    }

}
