﻿using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            Quaternion rotation = _mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
    }
}