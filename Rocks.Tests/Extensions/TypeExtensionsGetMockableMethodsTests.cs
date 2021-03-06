﻿using NUnit.Framework;
using Rocks.Construction.InMemory;
using Rocks.Tests.Types;
using System;
using System.Linq;
using static Rocks.Extensions.TypeExtensions;

namespace Rocks.Tests.Extensions
{
	[TestFixture]
	public sealed class TypeExtensionsGetMockableMethodsTests
	{
		[Test]
		public void GetMockableMethodsWhenTypeHasProtectedInternalAbstractMethod()
		{
			var methods = typeof(HasProtectedInternalAbstractMethod).GetMockableMethods(new InMemoryNameGenerator());
			Assert.That(methods.Where(_ => _.Value.Name == "Target").Any(), Is.True);
		}

		[Test]
		public void GetMockableMethodsWhenTypeIsInterfaceAndHasObjectMethods()
		{
			var methods = typeof(IHaveObjectMethods).GetMockableMethods(new InMemoryNameGenerator());
			Assert.That(methods.Count, Is.EqualTo(1));
			Assert.That(methods[0].Value.Name == "Target", Is.True);
		}
	}

	public interface IHaveObjectMethods
	{
		Type GetType();
		string ToString();
		void Target();
	}
}
