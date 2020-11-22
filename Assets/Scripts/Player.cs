using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// プレイヤーの弾を発射する位置
	public Transform firePoint;
	// プレイヤーの弾のプレハブ
	public GameObject bulletPrefab;
	// GameControllerにアクセスする変数
	GameController gameController;

	float shotDelay;
	float shotCount;
	bool shotFlag = false;

	// Start is called before the first frame update
	void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

	// Update is called once per frame
	void Update()
	{
		Move();

	}

	private void Move()
	{
		shotDelay += Time.deltaTime;

		if (Input.GetKey(KeyCode.Space))
		{
			if (shotDelay >= 0.06f)
			{
				Instantiate(bulletPrefab, firePoint.position, transform.rotation);
				gameController.PlaySE(0);
				shotDelay = 0.0f;
				shotCount++;
			}
		}

		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");
		float speed = 10.0f;
		Vector3 pos = transform.position + new Vector3(
			x * Time.deltaTime * speed,
			y * Time.deltaTime * speed,
			0);

		// 移動範囲を補正
		pos = new Vector3(
			Mathf.Clamp(pos.x, -7f, 7f),
			Mathf.Clamp(pos.y, -7f, 7f),
			pos.z);

		transform.position = pos;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("EnemyBullet"))
		{
			// Playerの爆破エフェクト表示
			gameController.ShowExplosionEffect(transform);
			// 自身のオブジェクトを破棄する
			Destroy(gameObject);
			// 衝突したオブジェクトを破棄する
			Destroy(collision.gameObject);
			// ゲームオーバーを表示する
			gameController.GameOver();
		}
	}
}
