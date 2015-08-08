﻿using NUnit.Framework;
using Rocks.Construction;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Rocks.Tests.Construction
{
	[TestFixture]
	public sealed class InMemoryCompilerTests
	{
		[Test]
		public void Compile()
		{
			var baseType = typeof(IBuilderTest);
			var handlers = new ReadOnlyDictionary<int, ReadOnlyCollection<HandlerInformation>>(
				new Dictionary<int, ReadOnlyCollection<HandlerInformation>>());
			var namespaces = new SortedSet<string> { baseType.Namespace };
			var options = new Options();

			var builder = new InMemoryBuilder(baseType, handlers, namespaces, options, false);
			builder.Build();

			var trees = new[] { builder.Tree };
			var compiler = new InMemoryCompiler(trees, options.Optimization, 
				new List<Assembly> { baseType.Assembly }.AsReadOnly(), builder.IsUnsafe);
			compiler.Compile();

			Assert.AreEqual(options.Optimization, compiler.Optimization, nameof(compiler.Optimization));
			Assert.AreSame(trees, compiler.Trees, nameof(compiler.Trees));
			Assert.IsNotNull(compiler.Result, nameof(compiler.Result));
			Assert.IsNotNull(
				(from type in compiler.Result.GetTypes()
				where baseType.IsAssignableFrom(type)
				select type).Single());
		}
	}

	public interface ICompilerTest { }
}
