using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.LDAP.Configuration
{
	public abstract class BaseConfigurationElementCollection<T> : ConfigurationElementCollection
		where T : ConfigurationElement, new()
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new T();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return GetElementKey((T)element);
		}

		protected object GetElementKey(T element)
		{
			return element;
		}

		public void Add(T element)
		{
			BaseAdd(element);
		}

		public T this[int index]
		{
			get
			{
				var i = 0;
				foreach (T x in this)
				{
					if (i == index)
						return x;
					i++;
				}
				throw new ArgumentOutOfRangeException();
			}
		}

	}
}
