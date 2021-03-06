﻿using Rocks.Exceptions;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Rocks.Extensions
{
	internal static class ExpressionExtensions
	{
		internal static ArgumentExpectation Create(this Expression @this)
		{
			return @this.Create(@this.Type, null);
		}

      internal static ArgumentExpectation Create(this Expression @this, Type expectationType, ParameterInfo parameter)
		{
			var argumentExpectationType = typeof(ArgumentExpectation<>).MakeGenericType(expectationType);

			switch (@this.NodeType)
			{
				case ExpressionType.Constant:
					var value = (@this as ConstantExpression).Value;
					return argumentExpectationType.GetConstructor(ReflectionValues.PublicNonPublicInstance,
						null, new[] { @this.Type }, null).Invoke(new[] { value }) as ArgumentExpectation;
				case ExpressionType.Call:
					var argumentMethodCall = (@this as MethodCallExpression);
					var argumentMethod = argumentMethodCall.Method;
					var isMethod = typeof(Arg).GetMethod(nameof(Arg.Is));
					var isAnyMethod = typeof(Arg).GetMethod(nameof(Arg.IsAny));
					var isDefaultMethod = typeof(Arg).GetMethod(nameof(Arg.IsDefault));

					if (argumentMethod.Name == isAnyMethod.Name && argumentMethod.DeclaringType == isAnyMethod.DeclaringType)
					{
						return argumentExpectationType.GetConstructor(ReflectionValues.PublicNonPublicInstance,
							null, Type.EmptyTypes, null).Invoke(null) as ArgumentExpectation;
					}
					else if (argumentMethod.Name == isMethod.Name && argumentMethod.DeclaringType == isMethod.DeclaringType)
					{
						var evaluation = argumentMethodCall.Arguments[0];
						var genericMethodType = typeof(Func<,>).MakeGenericType(@this.Type, typeof(bool));
						return argumentExpectationType.GetConstructor(ReflectionValues.PublicNonPublicInstance,
							null, new[] { genericMethodType }, null).Invoke(new[] { (evaluation as LambdaExpression).Compile() }) as ArgumentExpectation;
					}
					else if(argumentMethod.Name == isDefaultMethod.Name)
					{
						if(!parameter.IsOptional)
						{
							throw new ExpectationException(ErrorMessages.GetCannotUseIsDefaultWithNonOptionalArguments
								(expectationType.Name, argumentMethod.Name, parameter.Name));
						}
						else
						{
							return argumentExpectationType.GetConstructor(ReflectionValues.PublicNonPublicInstance,
								null, new[] { @this.Type }, null).Invoke(new[] { parameter.DefaultValue }) as ArgumentExpectation;
						}
					}
					else
					{
						return argumentExpectationType.GetConstructor(ReflectionValues.PublicNonPublicInstance,
							null, new[] { typeof(Expression) }, null).Invoke(new[] { @this }) as ArgumentExpectation;
					}
				default:
					return argumentExpectationType.GetConstructor(ReflectionValues.PublicNonPublicInstance,
						null, new[] { typeof(Expression) }, null).Invoke(new[] { @this }) as ArgumentExpectation;
			}
		}
	}
}
