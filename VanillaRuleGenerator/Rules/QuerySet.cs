using System.Collections.Generic;

namespace VanillaRuleGenerator.Rules
{
	public class QuerySet
	{
		public void Add(QueryableProperty prop)
		{
			this.QueryableProperties.Add(prop);
			prop.QuerySet = this;
		}

		public static QuerySet GetSerialNumberQueries()
		{
			return new QuerySet
			{
				QueryableProperties = new List<QueryableProperty>
				{
					QueryableProperty.DoesSerialNumberStartWithLetter,
					QueryableProperty.IsSerialNumberOdd
				}
			};
		}

		public static QuerySet GetWireQueries()
		{
			return new QuerySet
			{
				QueryableProperties = new List<QueryableProperty>
				{
					QueryableWireProperty.IsExactlyOneColorWire,
					QueryableWireProperty.IsExactlyZeroColorWire,
					QueryableWireProperty.LastWireIsColor,
					QueryableWireProperty.MoreThanOneColorWire
				}
			};
		}

		public static QuerySet GetMemoryQueries()
		{
			return new QuerySet
			{
				QueryableProperties = new List<QueryableProperty>
				{
					QueryableMemoryProperty.IsDisplayDigit
				}
			};
		}

		public List<QueryableProperty> QueryableProperties = new List<QueryableProperty>();
	}
}
