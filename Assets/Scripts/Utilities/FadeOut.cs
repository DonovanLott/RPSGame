using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace RPSGame.Utilities
{
	/// <summary>
	/// Fades out over a certain time period.
	/// </summary>
	[RequireComponent(typeof(SpriteRenderer))]
	public sealed class FadeOut : MonoBehaviour
	{
		[SerializeField]
		private float durationInSeconds;

		private SpriteRenderer _renderer;

		private void Awake()
		{
			_renderer = GetComponent<SpriteRenderer>();
		}

		private void Start()
		{
			StartCoroutine(FadeOutCoroutine());
			enabled = false;
		}

		private IEnumerator FadeOutCoroutine()
		{
			Material material = _renderer.material;
			Color originalColor = material.color;

			float timePassed = 0f;
			while (timePassed < durationInSeconds)
			{
				timePassed += Time.deltaTime;
				float percentDone = Mathf.Clamp01(timePassed / durationInSeconds);
				material.color = originalColor.WithAlpha(Mathf.Lerp(originalColor.a, 0f, percentDone));

				yield return null;
			}
		}
	}
}