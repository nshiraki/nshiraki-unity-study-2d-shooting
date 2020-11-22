using System.Collections;
using UnityEngine;

public class EnemyBoss1 : BaseEnemy
{
	// Start is called before the first frame update
	override protected void Start()
	{
		base.Start();
		StartCoroutine(Move());
		gameController.ShowBossInfo();
	}

	IEnumerator Move()
	{
		// 移動パターン初期化
		int pattern = 0;

		// 移動し終えたら攻撃させる
		while (true)
		{
			switch (pattern)
			{
				// 初期化
				case 0:
					// 初期位置まで移動
					transform.position -= new Vector3(0, 1f, 0) * Time.deltaTime * 4f;
					// 遅延無し
					yield return null;
					if (transform.position.y <= 5f)
					{
						// 次のパターン
						pattern = 1;
					}
					break;
				case 1:
					// 攻撃させる
					// プレイヤーの位置を取得
					if (player != null)
					{
						targetPosition = player.transform.position;
					}
					yield return AimShot(8, 4);
					yield return new WaitForSeconds(1f);
					yield return SpiralLeftShot(0, 10, 4f);
					yield return new WaitForSeconds(1f);
					yield return SpiralRightShot(0, 10, 4f);
					yield return new WaitForSeconds(1f);
					yield return SpiralMultiLeftShot(0, 10, 4f);
					yield return new WaitForSeconds(1f);
					yield return SpiralMultiRightShot(0, 10, 4f);
					yield return new WaitForSeconds(1f);
					yield return SpiralRightShot(0, 10, 4f);
					yield return new WaitForSeconds(1f);
					yield return MultiWayShot(36, 4);
					yield return new WaitForSeconds(1f);
					break;
				case 2:
					break;
			}
		}
	}

	// 一定回数標的を狙って敵弾を撃つ
	protected IEnumerator AimShot(int repeatCount, int shotCount)
	{
		for (int i = 0; i < shotCount; i++)
		{
			AimOneShot(4f);
			yield return new WaitForSeconds(0.5f);
		}
	}

	// 多方向へ一定回数敵弾を撃つ
	// way 撃つ方向の数
	// speed 弾の速度
	// count 撃つ回数
	protected IEnumerator MultiWayShot(int way, int count)
	{
		float speed = 4f;
		for (int i = 0; i < count; i++)
		{
			MultiWayOneShot(way, speed);
			yield return new WaitForSeconds(0.5f);
		}
	}

	// 多方向へ敵弾を撃つ
	// way 撃つ方向の数
	// speed 弾の速度
	protected void MultiWayOneShot(int way, float speed)
	{
		for (int i = 0; i < way; i++)
		{
			float angle = i * (360 / way);
			ShotByAngle(angle, speed);
		}
	}

	// スパイラル状に弾を撃つ（左回り）
	// r1 開始角度
	// r2 角度増分
	// seconds 撃ち続ける秒数
	protected IEnumerator SpiralLeftShot(float r1, float r2, float seconds)
	{
		float startTime = Time.time;
		float angle = r1;
		while (Time.time - startTime < seconds)
		{
			ShotByAngle(angle, 4f);
			yield return new WaitForSeconds(0.02f);
			angle += r2;
		}
	}

	// スパイラル状に弾を撃つ（右回り）
	// r1 開始角度
	// r2 角度増分
	// seconds 撃ち続ける秒数
	protected IEnumerator SpiralRightShot(float r1, float r2, float seconds)
	{
		return SpiralLeftShot(r1, -r2, seconds);
	}

	// スパイラル状に多方向へ弾を撃つ（左回り）
	// r1 開始角度
	// r2 角度増分
	// seconds 撃ち続ける秒数
	protected IEnumerator SpiralMultiLeftShot(float r1, float r2, float seconds)
	{
		float startTime = Time.time;
		float angle = r1;
		while (Time.time - startTime < seconds)
		{
			ShotByAngle(angle, 4f);
			for (int i = 0; i < 4; i++)
			{
				ShotByAngle(angle + (i * 90), 4f);
			}
			yield return new WaitForSeconds(0.1f);
			angle += r2;
		}
	}

	// スパイラル状に多方向へ弾を撃つ（右回り）
	// r1 開始角度
	// r2 角度増分
	// seconds 撃ち続ける秒数
	protected IEnumerator SpiralMultiRightShot(float r1, float r2, float seconds)
	{
		return SpiralMultiLeftShot(r1, -r2, seconds);
	}

}
