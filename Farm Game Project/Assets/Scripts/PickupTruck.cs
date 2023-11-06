using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupTruck : MonoBehaviour
{
    [SerializeField] private float _setArrivalTime = 10f;
    private float _arrivalTime;

    [SerializeField] private List<SpriteRenderer> _spriteRenderers;
    private List<string> _loadedCrops;
    private int _loadIndex = 0;

    public enum State
    {
        Arrive, 
        Depart
    }
    private State _loadingState;

    private Animator _animator;
    private PlayerMechanics _player;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
        _loadedCrops = new List<string>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _arrivalTime = _setArrivalTime;

        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.sprite = null;
        }

        _loadingState = State.Arrive;
    }

    // Update is called once per frame
    void Update()
    {
        TruckState();
    }

    public void LoadTruck(string cropName, Sprite cropSprite)
    {
        if (_loadIndex < _spriteRenderers.Count)
        {
            _spriteRenderers[_loadIndex].sprite = cropSprite;
            _loadedCrops.Add(cropName);
            _loadIndex++;
        }
        else return;
    }

    private void TruckState()
    {
        switch (_loadingState)
        {
            case State.Arrive:
                if (_loadIndex == _spriteRenderers.Count)
                {
                    _animator.SetTrigger("Depart");
                    _loadingState = State.Depart;
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
                    _loadedCrops.Clear();
                    _loadIndex = 0;

                    _animator.SetTrigger("Arrive");
                    _arrivalTime = _setArrivalTime;
                    _loadingState = State.Arrive;
                }
                break;
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
