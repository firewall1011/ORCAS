using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ORCAS
{
    public class MovementController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float _baseMoveSpeed = 50f;
        [SerializeField] private float _minSpeed = 1f;
        [SerializeField] private float _maxSpeed = 500f;
        [SerializeField] private float _speedDelta = 5f;

        [Header("Rotation Settings")]
        [SerializeField] private float _rotationSpeed;

        private Vector3 _movementDirection = Vector3.zero;
        private Vector2 _lookDirection = Vector2.zero;
        private Vector2 _lookRotationAngle = Vector2.zero;
        private float _currentMoveSpeed;

        private void Awake()
        {
            _currentMoveSpeed = _baseMoveSpeed;

            EnableMovement();
        }

        public void EnableMovement()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void DisableMovement()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            Look();
            Move();
        }

        private void Look()
        {
            _lookRotationAngle += Time.deltaTime * _rotationSpeed * _lookDirection;
            transform.localRotation = Quaternion.Euler(_lookRotationAngle.x, _lookRotationAngle.y, 0f);
        }

        private void Move()
        {
            var translation = Time.deltaTime * _currentMoveSpeed * _movementDirection;
            transform.Translate(translation, Space.Self);
        }

        public void OnMove(InputValue movementDirection)
        {
            _movementDirection = movementDirection.Get<Vector3>().normalized;
        }

        public void OnControlSpeed(InputValue delta)
        {
            _currentMoveSpeed = Mathf.Clamp(_currentMoveSpeed + delta.Get<float>() * _speedDelta * Time.deltaTime, _minSpeed, _maxSpeed);
        }

        public void OnLook(InputValue lookDirection)
        {
            _lookDirection = lookDirection.Get<Vector2>().normalized;
            _lookDirection = new Vector2(-_lookDirection.y, _lookDirection.x);
        }
    }
}
