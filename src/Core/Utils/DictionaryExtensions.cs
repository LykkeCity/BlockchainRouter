using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Utils
{
	public static class DictionaryExtensions
	{
		public static T Get<T>(this Dictionary<string, string> parameters, string key)
		{
			if (!parameters.ContainsKey(key))
				throw new KeyNotFoundException("Key=" + key);
			var value = parameters[key];
			return (T)Convert.ChangeType(value, typeof(T));
		}

		public static T Get<T>(this Dictionary<string, string> parameters, Expression<Func<T>> key)
		{
			return parameters.Get<T>((key.Body as MemberExpression).Member.Name);
		}
	}
}
