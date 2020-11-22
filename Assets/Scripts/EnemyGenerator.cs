using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	// 敵のPrefabを格納
	public GameObject enemyGhostPrefab;
	// BossのPrefabを格納
	public GameObject bossEnemyPrefab;

	// 敵出現データ
	struct EnemySpawnData
	{
		// 出現までの遅延時間
		public float delaySeconds;
		// 敵の種別
		public BaseEnemy.Type type;
		// 出現位置
		public float x, y;

		public EnemySpawnData(float delaySeconds, BaseEnemy.Type type, float x, float y)
		{
			this.delaySeconds = delaySeconds;
			this.type = type;
			this.x = x;
			this.y = y;
		}
	}

	// 敵の出現位置
	private EnemySpawnData[] enemySpawnData = new EnemySpawnData[] {
		// 左   -7.0f 
		// 右    7.0f
		// 上    9.0f
		// 下   -9.0f
		// 中心  0.0f
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,-6f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,-6f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,-6f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,-6f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,6f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,6f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,6f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,6f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,-7f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,-6f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,-5f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,-4f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,7f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,6f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,5f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.GHOST ,4f, 9f),
	  new EnemySpawnData (1.0f, BaseEnemy.Type.BOSS ,0f, 9f),
	};

	// Start is called before the first frame update
	void Start()
	{
		StartEnemySpawn();
	}

	public void StartEnemySpawn()
	{
		print("StartEnemySpawn()");
		StopAllCoroutines();
		StartCoroutine(MakeEnemy());
	}

	IEnumerator MakeEnemy()
	{
		// 読み出し位置
		int position = 0;
		while (position < enemySpawnData.Length)
		{
			// 読み出す
			EnemySpawnData data = enemySpawnData[position];
			// 出現時間まで待機
			yield return new WaitForSeconds(data.delaySeconds);
			// 敵を出現させる
			SpawnEnemy(data);
			// 次の読み出し位置に進める
			position++;
		}
		yield break;
	}

	// 敵を生成する
	void SpawnEnemy(EnemySpawnData data)
	{
		Vector3 pos = new Vector3(data.x, data.y, transform.position.z);
		switch (data.type)
		{
			case BaseEnemy.Type.GHOST:
				// 敵を生成
				Instantiate(enemyGhostPrefab, pos, transform.rotation);
				break;
			case BaseEnemy.Type.BOSS:
				// Bossを生成
				Instantiate(bossEnemyPrefab, pos, transform.rotation);
				break;
			default:
				break;
		}
	}
}
