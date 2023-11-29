using RPSGame.Utilities;
using UnityEngine;

namespace RPSGame
{
	public sealed class Health : MonoBehaviour
	{
		private int _currentHealth;
		private int _immuneFrames;

		[SerializeField]
		private DamageType type;

		[SerializeField]
		private bool hostile;

		[SerializeField]
		private int maxHealth;

		[SerializeField]
		private int immuneFramesOnHit;

		[SerializeField]
		private Collider2D hurtCollider;

		public bool CurrentlyImmune => _immuneFrames > 0;

		public void Start()
		{
			_currentHealth = maxHealth;
			_immuneFrames = 0;
		}

		public void Update()
		{
			if (_immuneFrames > 0)
			{
				_immuneFrames--;
			}
		}

		public bool TryDamage(int amount, DamageType type, bool hostile)
		{
			if (CurrentlyImmune || amount <= 0 || this.hostile == hostile || !type.Beats(this.type))
			{
				return false;
			}

			_immuneFrames = immuneFramesOnHit;
			_currentHealth -= amount;
			Debug.Log($"{gameObject} took {amount} damage and is now at {_currentHealth} / {maxHealth} health");
			if (_currentHealth <= 0)
			{
				Destroy(gameObject);
			}

			return true;
		}

		private void OnTriggerStay2D(Collider2D collision)
		{
			if (hurtCollider.IsTouching(collision))
			{
				CollideWith(collision.gameObject);
			}
		}

		private void CollideWith(GameObject collisionObject)
		{
			if (!gameObject.Equals(collisionObject) && collisionObject != null && collisionObject.GetComponent<DamageData>() is DamageData data)
			{
				TryDamage(data.amount, data.type, data.hostile);
			}
		}
	}
}