using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace RPSGame.Utilities
{
	/// <summary>
	/// Performs an arbitrary action after <see cref="durationInSeconds"/> seconds have passed.
	/// </summary>
	public class Timer : MonoBehaviour
	{
		[SerializeField]
		private float durationInSeconds;

		[SerializeField]
		private UnityEvent<GameObject> expirationEvent;

		private void Start()
		{
			StartCoroutine(TickDownCoroutine());
			enabled = false;
		}

		private IEnumerator TickDownCoroutine()
		{
			yield return new WaitForSeconds(durationInSeconds);
			expirationEvent?.Invoke(gameObject);
		}

		// There isn't any built-in instance method that can be used to destroy a GameObject when the timer finishes.
		// Would probably be better in some Utils MonoBehavior, but it's here for now since this is the only place it's used.
		public void DestroySelf()
		{
			Destroy(gameObject);
		}
	}
}