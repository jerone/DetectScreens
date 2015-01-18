using System;
using System.Reflection;

namespace HelperFramework.Extension
{
	static class UtilitieExtension
	{
		public static object CreateANewInstance(this Type type)
		{
			Assembly a = Assembly.Load(type.Assembly.FullName);
			return a.CreateInstance(type.FullName);
		}
	}
}
