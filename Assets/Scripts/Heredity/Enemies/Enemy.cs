using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Enemy : MonoBehaviour {

    public Sprite enemyIcone;

    protected enum Action {

        Still,
        Moving,
        Attacking,
        Fleeing,
        ChangingState,
    };

    protected Action action;

    private bool isVisible;
    private float cooldown;
    private Vector3 direction;

    private ExperienceBehaviour _eb;
    private MovementBehaviour _mb;
    private HealthBehaviour _hb;
    private Animator _anim;
    private AddExperienceLevels _ael;

	private void Awake() {

        _eb = GetComponent<ExperienceBehaviour>();
        _mb = GetComponent<MovementBehaviour>();
        _hb = GetComponent<HealthBehaviour>();
        _anim = GetComponentInChildren<Animator>();
        _ael = GetComponent<AddExperienceLevels>();

        _eb.SetLevel(Random.Range(1, 11));
        _hb.SetMaxHealth(100 + 25 * (_eb.ReturnLevel() - 1));
        _hb.Awake();
        _ael.ChangeExperienceToAdd(10 * _eb.ReturnLevel() - 1);
    }

    private void Update() {
        if (!ScriptingManager.scriptingMode)
        {
            switch (action) {
                case Action.Still:

                    _mb.Stop();
                    _anim.SetInteger("State", 0);

                    break;
                case Action.Moving:

                    _mb.SetSpeed(_mb.ReturnDefaultSpeed());
                    _anim.SetInteger("State", 1);

                    break;
                case Action.Attacking:

                    Vector3 attackingDirection = GameManager.Instance.player.transform.position - transform.position;
                    direction = attackingDirection;
                    _mb.SetSpeed(_mb.ReturnDefaultSpeed() * 1.5f);
                    _anim.SetInteger("State", 1); // Attack animation

                    break;
                case Action.Fleeing:

                    Vector3 fleeingDirection = transform.position - GameManager.Instance.player.transform.position;
                    direction = fleeingDirection;
                    _mb.SetSpeed(_mb.ReturnDefaultSpeed() * 1.5f);
                    _anim.SetInteger("State", 1); // Fleeing animation

                    break;
                case Action.ChangingState:

                    int rnd = Random.Range(0,2);

                    if (rnd == 0) {

                        action = Action.Still;
                    }
                    else if (rnd == 1) {

                        action = Action.Moving;
                        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                        direction.Normalize();
                    }

                    break;
            }

            if (cooldown <= 0) {

                cooldown = Random.Range(3, 6);
                action = Action.ChangingState;
            }

            cooldown -= Time.deltaTime;

            if (direction != Vector3.zero) {
           
                Quaternion target = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, 10 * Time.deltaTime);
            }
        }
        
    }

    private void FixedUpdate() {
        if (!ScriptingManager.scriptingMode)
            _mb.MoveRb3DForceMode(direction);
    }

    public float GetEnemyHealthPercent() {

        return _hb.ReturnHealthPercent();
    }

    public int GetEnemyLvl() {

        return _eb.ReturnLevel();
    }

    public void ChangeBaseColorSinceATime(bool damage) {

        StopAllCoroutines();

        EnemyManager.Instance.ShowEnemyStats(this);

        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < mesh.Length; i++) {

			if (damage)
                mesh[i].material.color = Color.red;
            else
                mesh[i].material.color = Color.gray;
        }

        StartCoroutine(C_ReturnToBaseColor(mesh));
    }

    IEnumerator C_ReturnToBaseColor(MeshRenderer[] mesh) {

        int correctMeshes = 0;

        for (int i = 0; i < mesh.Length; i++) {

            mesh[i].material.color = Vector4.MoveTowards(mesh[i].material.color, Color.white, 0.05f);

            if (mesh[i].material.color == Color.white)
                correctMeshes++;
        }

        yield return new WaitForSeconds(0);

        if (correctMeshes != mesh.Length)
            StartCoroutine(C_ReturnToBaseColor(mesh));
    }
}
