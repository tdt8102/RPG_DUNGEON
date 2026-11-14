using UnityEngine;

public class CameraMotor : MonoBehaviour
{
	public Transform lookAt;

	public float boundX = 0.3f;

	public float boundY = 0.15f;

	private void Start()
	{
		lookAt = GameObject.Find("Player").transform;
	}

	private void LateUpdate()
	{
		Vector3 zero = Vector3.zero;
		float num = lookAt.position.x - base.transform.position.x;
		if (num > boundX || num < 0f - boundX)
		{
			if (base.transform.position.x < lookAt.position.x)
			{
				zero.x = num - boundX;
			}
			else
			{
				zero.x = num + boundX;
			}
		}
		float num2 = lookAt.position.y - base.transform.position.y;
		if (num2 > boundY || num2 < 0f - boundY)
		{
			if (base.transform.position.y < lookAt.position.y)
			{
				zero.y = num2 - boundY;
			}
			else
			{
				zero.y = num2 + boundY;
			}
		}
		base.transform.position += new Vector3(zero.x, zero.y, 0f);
	}
}
