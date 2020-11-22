using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 pos = transform.position;

		if (pos.y <-16.5f)
		{
			// 戻す
			pos.y = 16.5f;
		}

		pos.y -= speed * Time.deltaTime;

		transform.position = pos;
	}
}
