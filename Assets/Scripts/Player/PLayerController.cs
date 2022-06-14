using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{

    public float movementSpeed = 2;
    public float movementCrouchSpeed = 1;
    private Rigidbody rb;
    private PlayerStates _playerState;
    public PlayerStates PlayerState { get => _playerState; }
    private bool _isOnCrouchPosition;
    public bool IsOnCrouchPosition { get => _isOnCrouchPosition; }
    private int _indexCapsuleCollider = 0;
    private CapsuleCollider[] capsuleColliders;
    void Start() {
        _playerState = PlayerStates._none;
        rb = gameObject.GetComponent<Rigidbody>();
        capsuleColliders = this.GetComponents<CapsuleCollider>();
        capsuleColliders[0].enabled = true;
        if(capsuleColliders.Length > 1) {
            capsuleColliders[1].enabled = false;
        }
    }
    // Update is called once per frame
    void Update() {
        MovePlayer();
    }
    void MovePlayer() {
        _playerState = PlayerStates.still;

        /// shift button validation
        _isOnCrouchPosition = Input.GetKey(KeyCode.LeftShift);
        if(_isOnCrouchPosition && Input.GetKeyUp(KeyCode.LeftShift)) {
            _isOnCrouchPosition = false;
        }
        ///
        
        /// colliders management depending on _isOnCrouchPosition
        if(capsuleColliders.Length > 1) {
            _indexCapsuleCollider = _isOnCrouchPosition ? 1 : 0;
            capsuleColliders[_indexCapsuleCollider].enabled = true;
            capsuleColliders[( _indexCapsuleCollider + 1 ) % 2].enabled = false;
        }
        ///

        ///Movement
        var speed = _isOnCrouchPosition ? movementCrouchSpeed : movementSpeed;
        //Debug.Log($"IsOnCrouchPosition {_isOnCrouchPosition}");
        if(Input.GetButton("Vertical") || Input.GetButton("Horizontal")) {
            _playerState = PlayerStates.walking;
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            var vector2 = new Vector2(-horizontalInput, verticalInput);
            var angle = Vector2.SignedAngle(Vector2.up, vector2);
            //Debug.Log(angle + " " + vector2);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            var movement = ( transform.forward * verticalInput ) +
                           ( transform.right * -horizontalInput );
            transform.Translate(movement * Time.deltaTime * speed);
        }
        ///
    }
}
