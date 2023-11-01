using System.Collections;
using UnityEngine;

namespace RPSGame.Utilities
{
	/// <summary>
	/// Scales a <see cref="GameObject"/> to the desired scale over time.
	/// </summary>
	[RequireComponent(typeof(Transform))]
	public sealed class ScaleTo : MonoBehaviour
	{
		[SerializeField]
		private float durationInSeconds;

		[SerializeField]
		private Vector3 newScale;

		private Transform _transform;

		private void Awake()
		{
			_transform = transform;
		}

		private void Start()
		{
			StartCoroutine(FadeOutCoroutine());
			enabled = false;
		}

		private IEnumerator FadeOutCoroutine()
		{
			Vector3 originalScale = _transform.localScale;

			float timePassed = 0f;
			while (timePassed < durationInSeconds)
			{
				timePassed += Time.deltaTime;
				float percentDone = Mathf.Clamp01(timePassed / durationInSeconds);
				_transform.localScale = Vector3.Lerp(originalScale, newScale, percentDone);

				yield return null;
			}
		}
	}
}