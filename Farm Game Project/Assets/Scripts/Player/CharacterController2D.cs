using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    public float SetMoveSpeed { private get; set; }

    [SerializeField] private Vector2 _moveDirection;
    private GameControls _controls;
    private Rigidbody2D _rb2D;

    void Awake()
    {
        _controls = new GameControls();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controls.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        _rb2D.velocity = _moveDirection * SetMoveSpeed;
    }

    private void ProcessInput()
    {
        _moveDirection = _controls.Player.Movement.ReadValue<Vector2>();
    }
}
