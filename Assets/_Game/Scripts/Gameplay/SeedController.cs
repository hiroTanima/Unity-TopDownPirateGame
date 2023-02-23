using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	
///<summary>
/// SeedController description
///</summary>
public class SeedController : MonoBehaviour
{

	string currentSeed;
	public string GetCurrentSeed()
	{
		return currentSeed;
	}

    private void Awake()
    {
		GenerateRandomSeed();
    }

	public void GenerateRandomSeed()
    {
		int tempSeed = (int)System.DateTime.Now.Ticks;
		currentSeed = tempSeed.ToString();

		Random.InitState(tempSeed);
    }

    public void SetRandomSeed(string seed = "")
    {
        currentSeed = seed;

        int tempSeed = 0;

        if (isNumeric(seed))
            tempSeed = System.Int32.Parse(seed);
        else
            tempSeed = seed.GetHashCode();

        Random.InitState(tempSeed);
    }
    public void SetRandomSeed(int seed)
    {
        currentSeed = seed.ToString();
        int tempSeed = seed;
        Random.InitState(tempSeed);
    }

    public void CopySeedToClipboard() => GUIUtility.systemCopyBuffer = currentSeed;

    public static bool isNumeric(string s)
	{
		return int.TryParse(s, out int n);
	}
}
