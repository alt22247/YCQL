/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Interfaces
{
	internal interface ISupportHaving<T>
	{
		T Having(BooleanExpression expression);
		T Having(LogicalClause clause);
		T Having(AllOperator expression);
		T Having(AnyOperator expression);
		T Having(ExistsOperator expression);
		T Having(InOperator expression);
		T Having(object clause);
	}
}
