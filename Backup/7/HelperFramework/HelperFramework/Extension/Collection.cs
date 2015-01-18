using System.Collections.Generic;

namespace HelperFramework.Extension
{
	public static class CollectionExtension
	{
		public static T Next<T>(this List<T> list, ref int index)
		{
			index = ++index >= 0 && index < list.Count ? index : 0;
			return list[index];
		}

		public static T Previous<T>(this List<T> list, ref int index)
		{
			index = --index >= 0 && index < list.Count ? index : list.Count - 1;
			return list[index];
		}
	}
}