using UnityEngine;

public class EnemyGhost : BaseEnemy
{
	// 経過時間
	private float countTime = 0.0f;
	// 敵弾のdelay
	private float bombDelay = 0.0f;

	// Update is called once per frame
	void Update()
	{
		Move();
	}

	void Move()
	{
		// プレイヤーの位置を取得
		if (player != null)
		{
			targetPosition = player.transform.position;
		}
		if(countTime < 8.0f)
		{
			if (bombDelay > 3.0f)
			{

				AimOneShot(3.0f);
				bombDelay = 0;
			}
		}

		countTime += Time.deltaTime;
		bombDelay += Time.deltaTime;

		Vector3 pos = transform.position;
		pos.x += 0f * Time.deltaTime;
		pos.y += -2f * Time.deltaTime;
		transform.position = pos;

		// 表示範囲外の処理
		if (pos.y < -9.0f)
		{
			Destroy(gameObject);
		}
	}
}
