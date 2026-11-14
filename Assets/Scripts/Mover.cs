using UnityEngine;

public abstract class Mover : FIghter
{
	protected BoxCollider2D boxCollider;

	protected Vector3 moveDelta;

	protected RaycastHit2D hit;

	public float ySpeed = 0.75f;

	public float xSpeed = 1f;

	protected virtual void Start()
	{
		boxCollider = GetComponent<BoxCollider2D>();
	}

	protected virtual void UpdateMotor(Vector3 input)
	{
		moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0f);
		if (moveDelta.x > 0f)
		{
			base.transform.localScale = Vector3.one;
		}
		else if (moveDelta.x < 0f)
		{
			base.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		moveDelta += pushDirection;
		pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);
		hit = Physics2D.BoxCast(base.transform.position, boxCollider.size, 0f, new Vector2(0f, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
		if (hit.collider == null)
		{
			base.transform.Translate(0f, moveDelta.y * Time.deltaTime, 0f);
		}
		hit = Physics2D.BoxCast(base.transform.position, boxCollider.size, 0f, new Vector2(moveDelta.x, 0f), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
		if (hit.collider == null)
		{
			base.transform.Translate(moveDelta.x * Time.deltaTime, 0f, 0f);
		}
	}
}
