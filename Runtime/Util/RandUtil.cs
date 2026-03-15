using UnityEngine;

namespace LordSheo
{
	public static class RandUtil
	{
		public static Vector3 Random(Vector3 min, Vector3 max)
		{
			var result = new Vector3();

			result.x = UnityEngine.Random.Range(min.x, max.x);
			result.y = UnityEngine.Random.Range(min.y, max.y);
			result.z = UnityEngine.Random.Range(min.z, max.z);
            
			return result;
		}
	}
}