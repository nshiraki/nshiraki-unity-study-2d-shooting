using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	// スコアを表示するText
	public Text scoreText;
	// ゲームオーバー時に表示するText
	public GameObject gameOverText;
	// Bossの情報を表示するText
	public GameObject bossInfoText;
	// スコアを格納する変数
	private int score;
	// EnemyGeneratorにアクセスする変数
	public EnemyGenerator enemyGenerator;
	// Soundにアクセスする変数
	public Sound sound;
	// 爆破エフェクト
	public GameObject explosionPrefab;
	// 背景
	public GameObject backgroundPrefab;

	// Start is called before the first frame update
	void Start()
	{
		score = 0;
		gameOverText.SetActive(false);
		scoreText.text = "SCORE:" + score;
		HideBossInfo();

		// 背景初期化
		for (int i = 0; i < 2; i++)
		{
			Instantiate(backgroundPrefab, new Vector3(0,i * 16.5f,0), transform.rotation);
		}
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape)) Quit();

		if (gameOverText.activeSelf)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene("MainScene");
			}
		}
	}

	// targetの周辺に爆破エフェクトを表示する
	public void ShowExplosionEffect(Transform target)
	{
		int max = 24;
		for (int i = 0; i < max; i++)
		{
			int angle = Random.Range(0, max) * 360 / max;
			int speed = Random.Range(0, 8);
			//  Prefabから爆発エフェクトを生成
			Explosion explosion = Instantiate(explosionPrefab,
				target.position, target.rotation).GetComponent<Explosion>();
			explosion.Init(angle, speed);
		}
		PlaySE(1);
	}

	public void AddScore(int score)
	{
		this.score += score;
		scoreText.text = "SCORE:" + this.score;
	}
	public void SetBossInfo(int life)
	{
		bossInfoText.GetComponent<Text>().text = "BOSS\n" + "HP: " + life;
	}

	public void ShowBossInfo()
	{
		bossInfoText.SetActive(true);
	}

	public void HideBossInfo()
	{
		bossInfoText.SetActive(false);

	}
	public void GameOver()
	{
		HideBossInfo();
		gameOverText.SetActive(true);
	}
	// 敵の出現処理をリセット
	public void ResetEnemySpawn()
	{
		enemyGenerator.StartEnemySpawn();
	}

	public void PlaySE(int num)
	{
		sound.Play(num);
	}

	void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
			  UnityEngine.Application.Quit();
#endif
	}

}
