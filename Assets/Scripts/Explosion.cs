using UnityEngine;

public class Explosion : MonoBehaviour
{
	float dx, dy;
	float countTime;
	float animatorStateLength;

	// Start is called before the first frame update
	void Start()
	{
		countTime = 0;
		// 現在再生中のアニメーションの再生時間を取得する
		Animator animator = GetComponent<Animator>();
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		animatorStateLength = stateInfo.length;
	}

	void Update()
	{
		Move();
	}

	private void Move()
	{
		transform.position += new Vector3(dx, dy, 0) * Time.deltaTime;
		countTime += Time.deltaTime;
		// アニメーションの再生時間を超えたらオブジェクトを破棄
		if (countTime > animatorStateLength)
		{
			Destroy(gameObject);
		}
	}


	public void Init(float angle, float speed)
	{
		// ---------------------------
		// 角度について
		// ---------------------------
		// 敵の右側が0°
		// 反時計回りに角度は増える

		// 2 * Mathf.PI = 360°
		// Mathf.PI     = 180°
		// Mathf.PI / 2 = 90°
		// Mathf.PI / 4 = 45°
		// 度からラジアンへの変換
		// 度 * 円周率 / 180°
		// ---------------------------
		float rad = angle * Mathf.PI / 180f;
		dx = Mathf.Cos(rad) * speed;
		dy = Mathf.Sin(rad) * speed;
	}
}
