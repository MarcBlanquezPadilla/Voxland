using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyDeactivateBehavoiur : MonoBehaviour {

    [Header("Fx | Audio")]
    [SerializeField] private GameObject fx;
    [SerializeField] private string audioName;

    [Header("Events")]
    [SerializeField] private UnityEvent OnDeactivateDestroy;

    public void DestroyGameObject() {

        PlayFX();
        OnDeactivateDestroy.Invoke();

        Destroy(gameObject);
    }

    public void DeactivateGameObject() {

        PlayFX();
        OnDeactivateDestroy.Invoke();

        gameObject.SetActive(false);
    }

    private void PlayFX() {

        if (fx != null) 
            Instantiate(fx, transform.position, Quaternion.identity);
    
        if(audioName != "") 
            AudioManager.Instance.playAudio(audioName);
    }
}
