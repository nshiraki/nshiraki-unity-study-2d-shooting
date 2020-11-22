using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy基底クラス
// 
public class BaseEnemy : MonoBehaviour
{
	// 敵弾のプレハブ
	public Bomb bombPrefab;
	// GameControllerにアクセスする変数
	protected GameController gameController;
	// Playerにアクセスする変数
	protected GameObject player;
	// プレイヤーの位置を保持する変数
	protected Vector3 targetPosition;

	// 敵の情報
	public Type type;
	public int score;
	public int life;
	protected bool isDead = false;

	// 敵の種別
	public enum Type
	{
		GHOST,
		BOSS
	}

	// Start is called before the first frame update
	virtual protected void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		player = GameObject.Find("Player");

		if (type == Type.BOSS)
		{
			gameController.ShowBossInfo();
			gameController.SetBossInfo(life);
		}
	}

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			// Playerの爆破エフェクト表示
			gameController.ShowExplosionEffect(collision.transform);
			// 衝突したオブジェクトを破棄する
			Destroy(collision.gameObject);
			// ゲームオーバーを表示
			gameController.GameOver();
		}
		else if (collision.CompareTag("PlayerBullet"))
		{
			var power = collision.gameObject.GetComponent<Bullet>().power;

			life -= power;
			StartCoroutine(DamegeFlash());
			if (life <= 0)
			{
				isDead = true;
				// スコアを加算する
				gameController.AddScore(score);
				// 敵の爆破エフェクトを表示する
				gameController.ShowExplosionEffect(transform);
				// 自身のオブジェクトを破棄する
				Destroy(gameObject);
				// Bossの場合は敵の出現処理をリセットする
				if (type == Type.BOSS)
				{
					gameController.ResetEnemySpawn();
					gameController.HideBossInfo();
				}
			}
			if (type == Type.BOSS)
			{
				gameController.SetBossInfo(life);
			}
			// 衝突したオブジェクトを破棄する
			Destroy(collision.gameObject);
		}
	}

	IEnumerator DamegeFlash()
	{
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.color = Color.yellow;
		yield return new WaitForSeconds(0.08f);
		renderer.color = Color.white;
	}

	// プレイヤーを狙って敵弾を撃つ
	protected void AimOneShot(float speed)
	{
		// 起点（自分）からプレイヤーまでの角度を求める
		float angle = Util.GetAngle(transform.position, targetPosition);

		ShotByAngle(angle, speed);
	}

	// 敵弾を生成する
	protected void ShotByAngle(float angle, float speed)
	{
		Bomb bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
		bomb.Setting(angle, speed);
	}
}
