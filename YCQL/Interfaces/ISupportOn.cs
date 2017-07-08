/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace Ycql.Interfaces
{
	internal interface ISupportOn<T>
	{
		T On(BooleanExpression expression);
		T On(LogicalClause clause);
		T On(AllOperator expression);
		T On(AnyOperator expression);
		T On(ExistsOperator expression);
		T On(InOperator expression);
		T On(object clause);
	}
}
