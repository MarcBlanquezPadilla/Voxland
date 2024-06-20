using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float sprintMultipler;

    // Varibale for save the default values
    private float defaultSpeed;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultipler;
    [SerializeField] private bool doubleJump;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded = false;
    private bool readyToJump = true;
    private bool readyDoubleJump = false;

    // Varibale for save the default values
    private float defaultJumpForce;

    [Header("References")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Rigidbody2D _rb2d;


    private void Awake() {

		if (GetComponent<Rigidbody>() != null) 
            _rb = GetComponent<Rigidbody>();

        if (GetComponent<Rigidbody2D>() != null)
            _rb2d = GetComponent<Rigidbody2D>();

        if (groundCheck == null)
            isGrounded = true;

        // Save default values 
        SetDefaultSpeed();
        SetDefaultJumpForce();
        ResetJump();
    }

	#region MOVEMENT

	#region 2D

	public void MoveRb2DForceMode(float x) {

		_rb2d.AddForce(new Vector2(x, _rb2d.velocity.y) * movementSpeed * 10f);
	}

	public void MoveRbSprinting2DForceMode(float x) {

		// Additional variable for the sprint
		_rb2d.AddForce(new Vector2(x, _rb2d.velocity.y) * movementSpeed * 10f * sprintMultipler);
	}

	public void MoveRbOnAir2DForceMode(float x) {

		_rb2d.AddForce(new Vector2(x, _rb2d.velocity.y) * movementSpeed * 10f * airMultipler);
	}

	public void SpeedControl2D() {

		Vector2 flatVel = new Vector2(_rb2d.velocity.x, 0f);

		if (flatVel.magnitude > movementSpeed) {

			Vector2 limitedVel = flatVel.normalized * movementSpeed;
			_rb2d.velocity = new Vector2(limitedVel.x, _rb2d.velocity.y);
		}
	}

	public void MoveRb2D(float x) {

        _rb2d.velocity = new Vector2(x * movementSpeed * 100f * Time.deltaTime, _rb2d.velocity.y);
    }

    public void MoveRbSprinting2D(float x) {

        // Additional variable for the sprint
        _rb2d.velocity = new Vector2(x * movementSpeed * 100f * sprintMultipler * Time.deltaTime, _rb2d.velocity.y);
    }

    public void MoveRbOnAir2D(float x) {

        _rb2d.velocity = new Vector2(x * movementSpeed * 100f * airMultipler * Time.deltaTime, _rb2d.velocity.y);
	}

    #endregion

    #region 3D

    public void MoveRb3DForceMode(Vector3 moveDirection, bool speedControl = true) {

		_rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);

        if (speedControl)
            SpeedControl3D();
	}

	public void MoveRbSprinting3DForceMode(Vector3 moveDirection) {

		// Additional variable for the sprint
		_rb.AddForce(moveDirection.normalized * movementSpeed * 10f * sprintMultipler, ForceMode.Force);
	}

	public void MoveRbOnAir3DForceMode(Vector3 moveDirection) {

		_rb.AddForce(moveDirection.normalized * movementSpeed * 10f * airMultipler, ForceMode.Force);
	}

	public void SpeedControl3D() {

		Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

		if (flatVel.magnitude > movementSpeed) {

			Vector3 limitedVel = flatVel.normalized * movementSpeed;
			_rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
		}
	}

	public void MoveRb3D(Vector3 moveDirection) {

        _rb.velocity = moveDirection * movementSpeed * 100f * Time.deltaTime;
    }

    public void MoveRbSprinting3D(Vector3 moveDirection) {

        _rb.velocity = moveDirection * movementSpeed * 100f * sprintMultipler * Time.deltaTime;
    }

    public void MoveRbOnAir3D(Vector3 moveDirection) {

        _rb.velocity = moveDirection * movementSpeed * 100f * airMultipler * Time.deltaTime;
    }

    public void StopRbVelocity3D()
    {
        _rb.velocity = Vector3.zero;
    }

    #endregion

    public void SetSpeed(float newSpeed) {

        movementSpeed = newSpeed;
    }

    public float ReturnSpeed() {

        return movementSpeed;
    }

    public void SetDefaultSpeed() {

        defaultSpeed = movementSpeed;
    }

    public float ReturnDefaultSpeed(){

        return defaultSpeed;
    }

    public void Stop() {

        movementSpeed = 0;
    }
    
	#endregion

	#region JUMP

	public void Jump3D() {

        if (readyToJump) {

            readyToJump = false;

            // Reset "Y" velocity
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    public void Jump2D() {

        if (readyToJump && isGrounded) {

            readyToJump = false;

            // Reset "Y" velocity
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f);
            _rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        else if (doubleJump && readyDoubleJump) {

            // Reset "Y" velocity
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f);
            _rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

            readyDoubleJump = false;
        }
    }

    public void IsGrounded2D() {

        CapsuleCollider2D cp;
        if (groundCheck.gameObject.GetComponent<CapsuleCollider2D>() != null) {

            cp = groundCheck.gameObject.GetComponent<CapsuleCollider2D>();
            isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(cp.size.x, cp.size.y), CapsuleDirection2D.Horizontal, 0, groundLayer);
        }

        if (isGrounded)
            readyDoubleJump = true;
    }


    private void ResetJump() {

        readyToJump = true;
    }

    public void SetDefaultJumpForce() {

        defaultJumpForce = jumpForce;
    }

    public bool CanJump() {

        return readyToJump;
    }

    #endregion
}