using UnityEngine;

public static class Util
{
	// startからtargetまでの角度を求める
	public static float GetAngle(Vector3 start, Vector3 target)
	{
		Vector3 diff = target - start;
		float rad = Mathf.Atan2(diff.y, diff.x);
		float degree = rad * Mathf.Rad2Deg;

		return degree;
	}

}
