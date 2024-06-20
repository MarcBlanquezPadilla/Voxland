using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

	[Header("KeyBinds")]
	public KeyCode moveTo = KeyCode.Mouse1;
    public KeyCode useEquip = KeyCode.Mouse0;
    public KeyCode jump = KeyCode.Space;

    [Header("Interactualbe Layers")]
    public LayerMask canMove;
    public LayerMask interactable;

    [Header("Info")]
    public Vector3 hitPosition;
    private Vector3 direction;
    private bool usingEquip = false;

    [Header("References")]
    public GameObject droppingPoint;
    public GameObject attractPoint;
    public MovementIndicatorController _mi;
    private MovementBehaviour _mb;
    public Animator _anim;

    [Header("Events")]
    public UnityEvent onChangeEquipment;
    public UnityEvent onClick;

    public bool blockControls = false;

    private Ray canMoveRay;

    private void Awake() {

        _mb = GetComponent<MovementBehaviour>();
        _anim = GetComponentInChildren<Animator>();
            
        hitPosition = transform.position;
        canMoveRay = new Ray(transform.position + Vector3.up, Vector3.forward + Vector3.down * 2);
    }
    
    private void Update() {

        if (!ScriptingManager.scriptingMode && !blockControls)
            MyInput();
    }
   
    private void FixedUpdate() {

        canMoveRay = new Ray(transform.position + Vector3.up, (hitPosition - transform.position).normalized + Vector3.down);
        if (!ScriptingManager.scriptingMode && !blockControls && Physics.Raycast(canMoveRay, 10, canMove))
            MovePlayer();
        else
            Stop();
    }

	private void MyInput() {

        Vector2 mousePosition;

        //Ray canMoveRay

        if (!usingEquip) {

            if (Input.GetKey(moveTo)) {

                onClick.Invoke();

                RaycastHit hit;
                mousePosition = Input.mousePosition;
                Ray r = Camera.main.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(r, out hit, 50, canMove)) {
                    hitPosition = hit.point;
                }
            }

            // SI SE PUEDE CAMBIAR!
            if (Input.GetKeyDown(moveTo) && _mb.ReturnSpeed() != 0)
                _mi.DisableIndicator();

            if (Input.GetKeyUp(moveTo) && _mb.ReturnSpeed() != 0)
                _mi.EnableIndicator();

            if (Input.GetAxis("Mouse ScrollWheel") > 0f) {

                EquipmentManager.Instance.NextEquip();
                onChangeEquipment.Invoke();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {

                EquipmentManager.Instance.PreviousEquip();
                onChangeEquipment.Invoke();
            }

            canMoveRay = new Ray(transform.position + Vector3.up, (hitPosition - transform.position).normalized + Vector3.down);
        }

        if (Input.GetKey(useEquip)) {

            EquipmentManager.Instance.UseEquip();
            hitPosition = transform.position;
        }
    }


    private void MovePlayer() {

        float distance = Vector3.Distance(transform.position, hitPosition);

        // Move the player while the position is different from the point pressed.
        if (distance > 0.3f) {

            // Direction
            direction = hitPosition - transform.position;
            direction.y = 0;
            direction.Normalize();

            // Rotation
            var lookDir = direction;
            lookDir.y = 0;
            Quaternion target = Quaternion.LookRotation(lookDir);
            StartCoroutine(Rotation(target));


            // Movement
            _mb.SetSpeed(_mb.ReturnDefaultSpeed());
            _mb.MoveRb3DForceMode(direction);
            ChangeAnimation(1);
        }
        else {

            Stop();
        }
    }

    private void Stop()
    {
        _mb.Stop();
        _mb.StopRbVelocity3D();
        ChangeAnimation(0);
        _mi.DisableIndicator();
    }


    public void ChangeAnimation(int i) {

        _anim.SetInteger("State", i);
    }

    // CAMBIAR!!
    IEnumerator Rotation(Quaternion target) {

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10);

        yield return new WaitForSeconds(0);
    }

    public float ReturnPlayerTransitionTime() {

        return GameManager.Instance.player.GetComponentInChildren<Animator>().GetAnimatorTransitionInfo(0).duration;
    }

    public void DisableControlls() {

        _mb.Stop();
        _mb.StopRbVelocity3D();
        hitPosition = transform.position;
        blockControls = true;
	}

    public void EnableControlls() {

        blockControls = false;
    }

    public bool ReturnUsingEquip() {

        return usingEquip;
    }

    public void UsingEquip(bool value) {

        usingEquip = value;
    }
}
