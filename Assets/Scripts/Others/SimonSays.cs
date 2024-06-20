using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimonSays : MonoBehaviour {
    
    [SerializeField] private List<GameObject> holes;
	[SerializeField] private List<int> order;
	[SerializeField] private List<int> playerOrder;
	[SerializeField] private int rounds;
	[SerializeField] private int round;
	[SerializeField] private int enableHoleColor;
	[SerializeField] private GameObject lastPlayerPositon;

	private bool sequenceCompleted;
	private bool complete;
	private bool waitingPlayer;
	private bool playerSelect;

	private GameObject cam;

	[SerializeField] private UnityEvent onWin;
	[SerializeField] private UnityEvent onLose;


	private void Awake() {

		cam = transform.Find("SimonSayCamera").gameObject;
		cam.SetActive(false);
	}

	private void Update() {

		if (waitingPlayer) {

			if (Input.GetKeyDown(KeyCode.Mouse0)) {

				RaycastHit hit;
				Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(r, out hit, 50)) {

					if (hit.collider.gameObject.TryGetComponent<HoleController>(out HoleController holeController)) {

						if (holeController.ReturnState()) {

							holeController.ChangeState(false);
							StartCoroutine(C_EnableHole(enableHoleColor));
							enableHoleColor++;
							playerSelect = true;

						}
						else if (!holeController.ReturnState()) {

							Lose();
							enableHoleColor = 0;
						}
					}
				}
			}	
		}
	}

	public void StartGame() {

		StopAllCoroutines();

		complete = false;
		waitingPlayer = false;
		playerSelect = false;


		for (int i = 0; i < rounds; i++) {

			order.Add(Random.Range(0, holes.Count));
		}

		StartCoroutine(C_Game());
	}

	IEnumerator C_Game() {

		yield return new WaitForSeconds(1);

		StartCoroutine(C_StartSequence());
		sequenceCompleted = false;

		while (!sequenceCompleted) {

			yield return null;
		}

		for (round = 0; round < rounds; round++) {

			StartCoroutine(C_Round(round + 1));
			sequenceCompleted = false;

			while (!sequenceCompleted) {

				yield return null;
			}

			StartCoroutine(C_WaitToPlayer());
			sequenceCompleted = false;

			while (!sequenceCompleted) {

				yield return null;
			}

			for (int i = 0; i < holes.Count; i++) {

				holes[i].GetComponent<BoxCollider>().enabled = false;
			}

			yield return new WaitForSeconds(0.5f);

			StartCoroutine(C_WarningHolesColor(2, Color.green));
			sequenceCompleted = false;
			enableHoleColor = 0;

			while (!sequenceCompleted) {

				yield return null;
			}
		}
		waitingPlayer = false;

		AudioManager.Instance.playAudio("WinSimonSays");
		onWin.Invoke();
	}

	IEnumerator C_StartSequence() {

        yield return new WaitForSeconds(0.2f);

		StartCoroutine(C_WarningHoles(2));
	}

	IEnumerator C_Round(int numOfHoles) {

		for (int i = 0; i < numOfHoles; i++) {

			StartCoroutine(C_EnableHole(i));
			complete = false;

			while (!complete) {

				yield return null;
			}
		}

		yield return new WaitForSeconds(0.2f);

		sequenceCompleted = true;
	}

	IEnumerator C_EnableHole(int value) {

		yield return new WaitForSeconds(0.2f);

		int holeNumber = order[value];

		Color color = holes[holeNumber].GetComponent<HoleController>().ReturnColor();
		holes[holeNumber].transform.Find("HoleBaseMesh").GetComponent<MeshRenderer>().material.SetColor("_Color",color);

		yield return new WaitForSeconds(0.2f);

		holes[holeNumber].transform.Find("HoleBaseMesh").GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);

		complete = true;
	}

	IEnumerator C_WarningHolesColor(int numOfWarnings, Color color) {

		for (int i = 0; i < numOfWarnings; i++) {

			for (int j = 0; j < holes.Count; j++) {

				MeshRenderer mesh;
				mesh = holes[j].transform.Find("HoleBaseMesh").GetComponent<MeshRenderer>();
				mesh.material.SetColor("_Color", color * 0.75f);
			}

			yield return new WaitForSeconds(0.2f);

			for (int j = 0; j < holes.Count; j++) {

				MeshRenderer mesh;
				mesh = holes[j].transform.Find("HoleBaseMesh").GetComponent<MeshRenderer>();
				mesh.material.SetColor("_Color", Color.black * 0.75f);
			}

			yield return new WaitForSeconds(0.2f);
		}

		sequenceCompleted = true;
	}

	IEnumerator C_WarningHoles(int numOfWarnings) {

		for (int i = 0; i < numOfWarnings; i++) {

			for (int j = 0; j < holes.Count; j++) {

				MeshRenderer mesh;
				Color color;
				mesh = holes[j].transform.Find("HoleBaseMesh").GetComponent<MeshRenderer>();
				color = holes[j].GetComponent<HoleController>().ReturnColor();
				mesh.material.SetColor("_Color", color * 0.75f);
			}

			yield return new WaitForSeconds(0.2f);

			for (int j = 0; j < holes.Count; j++) {

				MeshRenderer mesh;
				mesh = holes[j].transform.Find("HoleBaseMesh").GetComponent<MeshRenderer>();
				mesh.material.SetColor("_Color", Color.black * 0.75f);
			}

			yield return new WaitForSeconds(0.2f);
		}

		sequenceCompleted = true;
	}

	IEnumerator C_WaitToPlayer() {

        for (int i = 0; i < holes.Count; i++) {

			holes[i].GetComponent<BoxCollider>().enabled = true;
        }

		waitingPlayer = true;

		for (int i = 0; i < round + 1; i++) {

			playerSelect = false;
			holes[order[i]].GetComponent<HoleController>().ChangeState(true);

			while (!playerSelect) {

				yield return null;
			}

			AudioManager.Instance.playAudio("SelectHole");
		}

		while (!complete) {

			yield return null;
		}

		sequenceCompleted = true;
	}


	private void ChangeHolesColors(Color color) {

		for (int i = 0; i < holes.Count; i++) {

			holes[i].transform.Find("HoleBaseMesh").GetComponent<MeshRenderer>().material.SetColor("_Color", color * 0.75f);
		}
	}

	private void Lose() {

		order.Clear();
		StopAllCoroutines();
		StartCoroutine(C_WarningHolesColor(4, Color.red));

		for (int i = 0; i < holes.Count; i++) {

			holes[i].GetComponent<BoxCollider>().enabled = false;
		}

		AudioManager.Instance.playAudio("LoseSimonSays");
		onLose.Invoke();
	}

	public void LastPosition() {

		lastPlayerPositon.transform.position = GameManager.Instance.player.transform.position;
	}
}
