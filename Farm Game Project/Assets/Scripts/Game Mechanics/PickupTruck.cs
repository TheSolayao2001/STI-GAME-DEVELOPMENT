using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTruck : MonoBehaviour
{
    [SerializeField] private float _setArrivalTime = 10f;
    private float _arrivalTime;
    [SerializeField] private float _setDepartureTime = 30f;
    private float _departureTime;

    [SerializeField] private int _arrivalCount = 5;
    private bool _isGameOver = false;

    [SerializeField] private List<SpriteRenderer> _spriteRenderers;
    private int _loadIndex = 0;

    public delegate void Delegate_1(string cropName);
    public Delegate_1 loadCropDel;

    [Space(10)]
    [SerializeField] private AudioClip _sfxTruckDepart;
    private AudioSource _audioSFX;

    public enum State
    {
        Arrive, 
        Depart
    }
    private State _scheduleState;
    public State GetScheduleState { get { return _scheduleState; } }

    private Animator _animator;
    private PlayerMechanics _player;
    private GameManager _gameManager;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        _audioSFX = GameObject.Find("Audio Source - SFX").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _departureTime = _setDepartureTime;
        _arrivalTime = _setArrivalTime;

        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.sprite = null;
        }

        _scheduleState = State.Arrive;
    }

    // Update is called once per frame
    void Update()
    {
        TruckState();

        _gameManager.GetTextArrivalCount.text = _arrivalCount.ToString();
        if (_departureTime > 10f) _gameManager.GetTextDepartureTime.text = _departureTime.ToString("0");
        else _gameManager.GetTextDepartureTime.text = _departureTime.ToString("0.0");
    }

    public void LoadTruck(string cropName, Sprite cropSprite)
    {
        if (_loadIndex < _spriteRenderers.Count)
        {
            _spriteRenderers[_loadIndex].sprite = cropSprite;
            loadCropDel(cropName);
            _loadIndex++;
        }
        else return;
    }

    private void TruckState()
    {
        if (_arrivalCount > 0)
        {
            switch (_scheduleState)
            {
                case State.Arrive:
                    _departureTime -= Time.deltaTime;
                    if (_loadIndex == _spriteRenderers.Count || _departureTime <= 0f)
                    {
                        _gameManager.GetTextArrival.SetActive(false);
                        _animator.SetTrigger("Depart");

                        _audioSFX.clip = _sfxTruckDepart;
                        _audioSFX.Play();

                        _scheduleState = State.Depart;
                    }
                    break;

                case State.Depart:
                    if (_arrivalTime > 0f)
                    {
                        _arrivalTime -= Time.deltaTime;
                    }
                    else
                    {
                        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
                        {
                            spriteRenderer.sprite = null;
                        }
                        _loadIndex = 0;

                        _animator.SetTrigger("Arrive");
                        _departureTime = _setDepartureTime;
                        _arrivalTime = _setArrivalTime;
                        _arrivalCount--;
                        _gameManager.GetTextArrival.SetActive(true);
                        _scheduleState = State.Arrive;
                    }
                    break;
            }
        }
        else
        {
            if (!_isGameOver)
            {
                _gameManager.GameOver();
                _isGameOver = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(_player.tag))
        {
            _player.pickupTruck = this;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(_player.tag))
        {
            _player.pickupTruck = null;
        }
    }
}
