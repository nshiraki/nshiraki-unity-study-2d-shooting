using UnityEngine;

public class Bullet : MonoBehaviour
{
	// 威力
	public int power;
	// Update is called once per frame
	public float speed;

	void Update()
	{
		Vector3 pos = transform.position;
		pos.y += speed * Time.deltaTime;
		transform.position = pos;
		if (transform.position.y > 9.0f)
		{
			Destroy(gameObject);
		}
	}
}
