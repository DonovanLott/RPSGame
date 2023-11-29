using UnityEngine;

namespace RPSGame.Utilities
{
	/// <summary>
	/// Determines the type and amount of damage a <see cref="Collider2D"/> does.
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public sealed class DamageData : MonoBehaviour
	{
		[SerializeField]
		public int amount;

		[SerializeField]
		public DamageType type;

		/// <summary>
		/// If <see langword="true"/>, this damage harms the player. If <see langword="false"/>, this damage harms enemies.
		/// </summary>
		[SerializeField]
		public bool hostile;
	}
}