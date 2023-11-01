using UnityEngine;

namespace RPSGame.Utilities
{
	/// <summary>
	/// Determines the type and amount of damage a <see cref="Collider2D"/> does.
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public sealed class DamageData : MonoBehaviour
	{
		public enum DamageType
		{
			Rock,
			Paper,
			Scissors
			// TODO: Universal?
		}

		[SerializeField]
		private int amount;

		[SerializeField]
		private DamageType type;

		/// <summary>
		/// If <see langword="true"/>, this damage harms the player. If <see langword="false"/>, this damage harms enemies.
		/// </summary>
		// TODO: Harm both?
		[SerializeField]
		private bool hostile;
	}
}