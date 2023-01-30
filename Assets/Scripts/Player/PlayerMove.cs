using Assets.Scripts.Data;
using Assets.Scripts.Data.PlayerProgressFolder;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.InputServices;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player
{
    public class PlayerMove : MonoBehaviour, ISavedProgressWriter
    {
        public CharacterController characterController;
        public float movementSpeed;
        private IInputService _inputeService;

        private void Awake()
        {
            _inputeService = ServiceLocator.Container.Single<IInputService>();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputeService.Axis.sqrMagnitude > Mathf.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputeService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            characterController.Move(movementVector * Time.deltaTime * movementSpeed);
        }


        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(GetCurrentLevelName(), transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (GetCurrentLevelName() == progress.WorldData.PositionOnLevel.Level)
            {
                NewVector3 savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                {
                    Warp(savedPosition);
                }
            }
        }

        private void Warp(NewVector3 savedPosition)
        {
            characterController.enabled = false;
            transform.position = savedPosition.AsUnityVector().AddY(characterController.height);
            characterController.enabled = true;
        }

        private static string GetCurrentLevelName() => SceneManager.GetActiveScene().name;
    }
}


