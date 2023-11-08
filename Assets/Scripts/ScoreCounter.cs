using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
	[SerializeField]
	private uint pointsPerKill;

	private uint _kills;
	private uint _rawPoints;

	public uint TotalPoints => (_kills * pointsPerKill) + _rawPoints;

	public void AddRawPoints(uint amount)
	{
		_rawPoints += amount;
	}

	public void CountEnemyKill()
	{
		_kills++;
		Debug.Log($"Enemy killed! Total Score: {TotalPoints}");
	}

	public void ResetPoints()
	{
		_rawPoints = 0;
		_kills = 0;
	}
}