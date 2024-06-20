using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MapUIController : MonoBehaviour {

	public GameObject mapCamera;
	public float speed;

	private Vector3 mousePositon;
	private Vector3 direction;

	private Vector3 previousPosition;

	[SerializeField] private float maxMapPositionX;
	[SerializeField] private float minMapPositionX;
	[SerializeField] private float maxMapPositionZ;
	[SerializeField] private float minMapPositionZ;

	bool move;

    private void Awake()
    {
		move = false;
    }

    private void Update()
    {
		if (move && !ScriptingManager.scriptingMode)
        {
			previousPosition = mapCamera.transform.position;
			mousePositon = Input.mousePosition;
			direction = -(mousePositon - new Vector3(Screen.width / 2, Screen.height / 2, 0));
			direction.Normalize();
			mapCamera.transform.position = new Vector3(mapCamera.transform.position.x - direction.x * Time.deltaTime * speed, mapCamera.transform.position.y, mapCamera.transform.position.z - direction.y * Time.deltaTime * speed);

			CheckOutLimits();
		}
	}

	private void CheckOutLimits()
    {
		if (mapCamera.transform.position.x > maxMapPositionX || mapCamera.transform.position.x < minMapPositionX)
			mapCamera.transform.position = new Vector3(previousPosition.x, mapCamera.transform.position.y, mapCamera.transform.position.z);
		if (mapCamera.transform.position.z > maxMapPositionZ || mapCamera.transform.position.z < minMapPositionZ)
			mapCamera.transform.position = new Vector3(mapCamera.transform.position.x, mapCamera.transform.position.y, previousPosition.z);
	}

	public void PointerEnter()
	{
		move = true;
	}

	public void PointerExit()
	{
		move = false;
	}
}

