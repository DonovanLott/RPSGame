namespace RPSGame.Utilities
{
	public enum DamageType
	{
		Rock,
		Paper,
		Scissors
		// TODO: Universal?
	}

	public static class DamageTypeExtensions
	{
		public static bool Beats(this DamageType attacker, DamageType defender)
		{
			return (attacker == DamageType.Rock && defender == DamageType.Scissors)
				|| (attacker == DamageType.Paper && defender == DamageType.Rock)
				|| (attacker == DamageType.Scissors && defender == DamageType.Paper);
		}
	}
}