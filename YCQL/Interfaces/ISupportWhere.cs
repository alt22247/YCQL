/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Interfaces
{
	internal interface ISupportWhere<T>
	{
		T Where(BooleanExpression expression);
		T Where(LogicalClause clause);
		T Where(AllOperator expression);
		T Where(AnyOperator expression);
		T Where(ExistsOperator expression);
		T Where(InOperator expression);
		T Where(object clause);
	}
}
