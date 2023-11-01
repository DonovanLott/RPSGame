using System.Collections;
using UnityEngine;

namespace RPSGame
{
	/// <summary>
	/// The movement system for Ron.
	/// </summary>
	[RequireComponent(typeof(Transform))]
	public sealed class RonMovement : MonoBehaviour
	{
		[SerializeField]
		private float maxMovementDistance = 10f;

		[SerializeField]
		private float jumpTimeInSeconds = 0.75f;

		[SerializeField]
		private float minJumpHeight = 1.25f;

		[SerializeField]
		private float maxJumpHeight = 5.25f;

		[SerializeField]
		private float shadowScaleWhenAirborne = 0.75f;

		[SerializeField]
		private float pauseTimeInSeconds = 0.25f;

		[SerializeField]
		private float slamTimeInSeconds = 0.05f;

		[SerializeField]
		private float stunTimeInSeconds = 1f;

		[SerializeField]
		private GameObject ronSprite;

		[SerializeField]
		private GameObject ronShadowSprite;

		[SerializeField]
		private GameObject ronShockwavePrefab;

		private Vector3 _preJumpPosition = Vector2.zero;
		private Vector3 _targetPosition = Vector3.zero;
		private bool _targetIsNew = false;
		private bool _moving = false;
		private float _jumpDistance = 0f;
		private bool _stunned = false;

		private Camera _camera;
		private Transform _transform;
		private Transform _ronSpriteTransform;
		private Transform _ronShadowSpriteTransform;

		private void Awake()
		{
			// Cache any reference vars that need to be grabbed from Unity.
			_camera = Camera.main;
			_transform = transform;
			_ronSpriteTransform = ronSprite.transform;
			_ronShadowSpriteTransform = ronShadowSprite.transform;
		}

		// Update is called once per frame
		private void Update()
		{
			UpdateTarget();
			if (!_moving && _targetIsNew && !_stunned)
			{
				BeginMovingTowardsNewTarget();
			}
		}

		private void UpdateTarget()
		{
			// Don't try and set a new target if the player isn't clicking or if they're moving.
			// Players can still target when stunned, which should prevent any issues where the player clicks when moving and doesn't retarget, making them think it's a missed input.
			if (!Input.GetMouseButtonDown(0) || _moving)
			{
				return;
			}

			// TODO: Tell the player about the range that Ron can move.
			// Maybe a targeting reticle that follows the cursor and stops at the edge of the radius?
			// Maybe a shadowed area as large as the radius? (May be hard to see Ron's shadow through this.)
			Vector3 originalPosition = _moving ? _targetPosition : _transform.position;
			Vector3 clickedPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
			clickedPosition.z = originalPosition.z;
			Vector3 movement = Vector3.ClampMagnitude(clickedPosition - originalPosition, maxMovementDistance);

			_targetPosition = originalPosition + movement;
			_jumpDistance = movement.magnitude;
			_targetIsNew = true;
		}

		private void BeginMovingTowardsNewTarget()
		{
			_targetIsNew = false;
			_moving = true;
			_preJumpPosition = _transform.position;

			StartCoroutine(PerformJumpCoroutine());
		}

		private IEnumerator PerformJumpCoroutine()
		{
			// TODO: Add some rotation, screenshake, etc.
			// Just make the jump feel more natural and impactful.
			// The shockwave will help with this a fair bit.

			Vector3 originalSpriteLocalPosition = _ronSpriteTransform.localPosition;
			Vector3 originalShadowLocalScale = _ronShadowSpriteTransform.localScale;
			// Cache these so that a new target can be selected when jumping.
			Vector3 targetPosition = _targetPosition;
			float jumpHeight = Mathf.Lerp(minJumpHeight, maxJumpHeight, 1f - (_jumpDistance / maxMovementDistance));
			float finalShadowScale = Mathf.Lerp(shadowScaleWhenAirborne, 0.5f * shadowScaleWhenAirborne, 1f - (_jumpDistance / maxMovementDistance));

			// Jump towards target position
			float currentTime = 0f;
			while (currentTime < jumpTimeInSeconds)
			{
				currentTime += Time.deltaTime;
				float percentDone = Mathf.Clamp01(currentTime / jumpTimeInSeconds);

				_transform.position = Vector3.Lerp(_preJumpPosition, targetPosition, percentDone);
				_ronSpriteTransform.localPosition = originalSpriteLocalPosition + new Vector3(0f, Mathf.Sqrt(percentDone) * jumpHeight, 0f);
				_ronShadowSpriteTransform.localScale = Vector3.Lerp(originalShadowLocalScale, originalShadowLocalScale * finalShadowScale, percentDone * percentDone);

				yield return null;
			}

			// Pause
			yield return new WaitForSeconds(pauseTimeInSeconds);

			// Slam
			currentTime = 0f;
			while (currentTime < slamTimeInSeconds)
			{
				currentTime += Time.deltaTime;
				float invPercentDone = 1f - Mathf.Clamp01(currentTime / slamTimeInSeconds);

				_ronSpriteTransform.localPosition = originalSpriteLocalPosition + new Vector3(0f, invPercentDone * jumpHeight, 0f);
				_ronShadowSpriteTransform.localScale = Vector3.Lerp(originalShadowLocalScale, originalShadowLocalScale * finalShadowScale, invPercentDone * invPercentDone);

				yield return null;
			}

			_ronSpriteTransform.localPosition = originalSpriteLocalPosition;
			_ronShadowSpriteTransform.localScale = originalShadowLocalScale;

			// Create Shockwave
			// Do this at shadow position to visually sync better
			Instantiate(ronShockwavePrefab, _ronShadowSpriteTransform.position, Quaternion.identity);

			// Stun
			_moving = false;
			StartCoroutine(StunCoroutine());
		}

		private IEnumerator StunCoroutine()
		{
			_stunned = true;
			Material material = ronSprite.GetComponent<SpriteRenderer>().material;
			Color originalColor = material.color;

			float currentTime = 0f;
			while (currentTime < stunTimeInSeconds)
			{
				currentTime += Time.deltaTime;
				float percentDone = Mathf.Clamp01(currentTime / stunTimeInSeconds);
				material.color = Color.Lerp(Color.gray, originalColor, (percentDone - 0.5f) / 0.5f);

				yield return null;
			}

			_stunned = false;
		}
	}
}