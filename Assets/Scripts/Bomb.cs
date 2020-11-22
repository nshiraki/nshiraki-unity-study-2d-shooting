using UnityEngine;

public class Bomb : MonoBehaviour
{
	// 設定された方向に弾を移動させる
	float dx;
	float dy;
	public void Setting(float angle, float speed)
	{
		// 角度について
		// 敵の右側が0°
		// 反時計回りに角度は増える

		// 2 * Mathf.PI = 360°
		// Mathf.PI     = 180°
		// Mathf.PI / 2 = 90°
		// Mathf.PI / 4 = 45°

		// 度からラジアンへ変換
		// 度 × 円周率 ÷ 180
		float rad = angle * Mathf.PI / 180f;
		dx = Mathf.Cos(rad) * speed;
		dy = Mathf.Sin(rad) * speed;
	}

	// Update is called once per frame
	void Update()
	{
		Move();
	}

	private void Move()
	{
		transform.position += new Vector3(dx, dy, 0) * Time.deltaTime;

		// 表示範囲外の処理
		Vector3 pos = transform.position;
		if (pos.x < -9.0f || pos.x > 9.0f ||
			pos.y < -9.0f || pos.y > 9.0f)
		{
			Destroy(gameObject);
		}
	}
}
