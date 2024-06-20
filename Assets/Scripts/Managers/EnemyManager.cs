using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyManager : MonoBehaviour {

    #region Instance

    private static EnemyManager _instance;
    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnemyManager>();
            }
            return _instance;
        }
    }

    #endregion

    private GameObject enemyStats;
    private FillBarController fillBarController;
    private Image enemyImage;
    private TextMeshProUGUI levelText;

    private void Awake() {

        enemyStats = GameObject.Find("EnemyStats");
        fillBarController = enemyStats.GetComponentInChildren<FillBarController>();
        enemyImage = enemyStats.GetComponentInChildren<Image>();
        levelText = enemyStats.GetComponentInChildren<TextMeshProUGUI>();

        enemyStats.SetActive(false);
    }

    public void ShowEnemyStats(Enemy enemy) {

        StopAllCoroutines();

        if (!enemyStats.activeInHierarchy)
            enemyStats.SetActive(true);

        enemyImage.sprite = enemy.enemyIcone;
        fillBarController.UpdateBar(enemy.GetEnemyHealthPercent());
        levelText.text = enemy.GetEnemyLvl().ToString();

        StartCoroutine(WaitForFade());
    }

    IEnumerator WaitForFade() {

        yield return new WaitForSeconds(3.5f);

        enemyStats.GetComponent<CanvasFade>().CloseFading();
    }
}
