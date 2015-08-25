﻿#pragma warning disable CS0618
using NUnit.Framework;
using Rocks.Construction;
using Rocks.Exceptions;
using Rocks.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Rocks.Tests.Construction
{
	[TestFixture]
	public sealed class InMemoryBuilderTests
	{
		[Test]
		public void Build()
		{
			var baseType = typeof(IBuilderTest);
			var handlers = new ReadOnlyDictionary<int, ReadOnlyCollection<HandlerInformation>>(
				new Dictionary<int, ReadOnlyCollection<HandlerInformation>>());
			var namespaces = new SortedSet<string> { baseType.Namespace };
			var options = new RockOptions();

			var builder = new InMemoryBuilder(baseType, handlers, namespaces, options, false);
			builder.Build();

			Assert.AreSame(baseType, builder.BaseType, nameof(builder.BaseType));
			Assert.AreSame(handlers, builder.Handlers, nameof(builder.Handlers));
			Assert.AreSame(namespaces, builder.Namespaces, nameof(builder.Namespaces));
			Assert.AreEqual(5, namespaces.Count, nameof(namespaces.Count));
			Assert.IsTrue(namespaces.Contains(typeof(Rock).Namespace), nameof(namespaces));
			Assert.IsTrue(namespaces.Contains(typeof(CompilationException).Namespace), nameof(namespaces));
			Assert.IsTrue(namespaces.Contains(typeof(object).Namespace), nameof(namespaces));
			Assert.IsTrue(namespaces.Contains(typeof(ReadOnlyDictionary<,>).Namespace), nameof(namespaces));
			Assert.IsTrue(namespaces.Contains(typeof(BindingFlags).Namespace), nameof(namespaces));

			Assert.AreSame(options, builder.Options, nameof(builder.Options));
			Assert.IsNotNull(builder.Tree, nameof(builder.Tree));
			Assert.IsTrue(!string.IsNullOrWhiteSpace(builder.TypeName), nameof(builder.TypeName));

			var tree = builder.Tree.ToString();

			Assert.IsFalse(tree.StartsWith("#pragma warning disable CS0618"));
			Assert.IsFalse(tree.Contains("#pragma warning disable CS0672"));
			Assert.IsFalse(tree.Contains("#pragma warning restore CS0672"));
			Assert.IsFalse(tree.EndsWith("#pragma warning restore CS0618"));
		}

		[Test]
		public void BuildWhenTypeIsObsolete()
		{
			var baseType = typeof(IAmObsolete);
			var handlers = new ReadOnlyDictionary<int, ReadOnlyCollection<HandlerInformation>>(
				new Dictionary<int, ReadOnlyCollection<HandlerInformation>>());
			var namespaces = new SortedSet<string> { baseType.Namespace };
			var options = new RockOptions();

			var builder = new InMemoryBuilder(baseType, handlers, namespaces, options, false);
			builder.Build();

			var tree = builder.Tree.ToString();

			Assert.IsTrue(tree.StartsWith("#pragma warning disable CS0618"));
			Assert.IsTrue(tree.Contains("#pragma warning disable CS0672"));
			Assert.IsTrue(tree.Contains("#pragma warning restore CS0672"));
			Assert.IsTrue(tree.EndsWith("#pragma warning restore CS0618"));
		}

		[Test]
		public void BuildWhenMethodIsObsolete()
		{
			var baseType = typeof(IHaveAnObsoleteMethod);
			var handlers = new ReadOnlyDictionary<int, ReadOnlyCollection<HandlerInformation>>(
				new Dictionary<int, ReadOnlyCollection<HandlerInformation>>());
			var namespaces = new SortedSet<string> { baseType.Namespace };
			var options = new RockOptions();

			var builder = new InMemoryBuilder(baseType, handlers, namespaces, options, false);
			builder.Build();

			var tree = builder.Tree.ToString();

			Assert.IsTrue(tree.StartsWith("#pragma warning disable CS0618"));
			Assert.IsTrue(tree.Contains("#pragma warning disable CS0672"));
			Assert.IsTrue(tree.Contains("#pragma warning restore CS0672"));
			Assert.IsTrue(tree.EndsWith("#pragma warning restore CS0618"));
		}

		[Test]
		public void BuildWhenPropertyIsObsolete()
		{
			var baseType = typeof(IHaveAnObsoleteProperty);
			var handlers = new ReadOnlyDictionary<int, ReadOnlyCollection<HandlerInformation>>(
				new Dictionary<int, ReadOnlyCollection<HandlerInformation>>());
			var namespaces = new SortedSet<string> { baseType.Namespace };
			var options = new RockOptions();

			var builder = new InMemoryBuilder(baseType, handlers, namespaces, options, false);
			builder.Build();

			var tree = builder.Tree.ToString();

			Assert.IsTrue(tree.StartsWith("#pragma warning disable CS0618"));
			Assert.IsTrue(tree.Contains("#pragma warning disable CS0672"));
			Assert.IsTrue(tree.Contains("#pragma warning restore CS0672"));
			Assert.IsTrue(tree.EndsWith("#pragma warning restore CS0618"));
		}

		[Test]
		public void BuildWhenEventIsObsolete()
		{
			var baseType = typeof(IHaveAnObsoleteEvent);
			var handlers = new ReadOnlyDictionary<int, ReadOnlyCollection<HandlerInformation>>(
				new Dictionary<int, ReadOnlyCollection<HandlerInformation>>());
			var namespaces = new SortedSet<string> { baseType.Namespace };
			var options = new RockOptions();

			var builder = new InMemoryBuilder(baseType, handlers, namespaces, options, false);
			builder.Build();

			var tree = builder.Tree.ToString();

			Assert.IsTrue(tree.StartsWith("#pragma warning disable CS0618"));
			Assert.IsTrue(tree.Contains("#pragma warning disable CS0672"));
			Assert.IsTrue(tree.Contains("#pragma warning restore CS0672"));
			Assert.IsTrue(tree.EndsWith("#pragma warning restore CS0618"));
		}
	}

	public interface IBuilderTest { }

	[Obsolete("", false)]
	public interface IAmObsolete { }

	public interface IHaveAnObsoleteMethod
	{
		[Obsolete]
		void TargetMethod();
		int TargetProperty { get; set; }
		event EventHandler TargetEvent;
	}

	public interface IHaveAnObsoleteProperty
	{
		void TargetMethod();
		[Obsolete]
		int TargetProperty { get; set; }
		event EventHandler TargetEvent;
	}

	public interface IHaveAnObsoleteEvent
	{
		void TargetMethod();
		int TargetProperty { get; set; }
		[Obsolete]
		event EventHandler TargetEvent;
	}
}
#pragma warning restore CS0618