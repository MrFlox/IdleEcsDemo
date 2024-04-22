    using System;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private Input _input;
        private GameObject _model;
        

        private void Start()
        {
            /*_model = transform.GetChild(0).gameObject;
            _input = new Input();
            _input.Default.Enable();*/
        }

        private void Update()
        {
            /*
            var moveInput = _input.Default.Move.ReadValue<Vector2>();
            var direction = new Vector3(moveInput.x, 0, moveInput.y);
            transform.Translate(direction * 3 * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                _animator.Play("run");
                _model.transform.rotation = Quaternion.RotateTowards(_model.transform.rotation, toRotation, Time.deltaTime * 300.0f);
            }
            else
            {
                _animator.Play("idle");
            }
            */
            
        }
    }