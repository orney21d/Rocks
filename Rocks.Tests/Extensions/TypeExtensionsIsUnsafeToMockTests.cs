﻿using NUnit.Framework;
using System;
using static Rocks.Extensions.TypeExtensions;

namespace Rocks.Tests.Extensions
{
	[TestFixture]
	public sealed class TypeExtensionsIsUnsafeToMockTests
	{
		[Test]
		public void IsUnsafeToMockWithSafeInterfaceWithSafeMembers()
		{
			Assert.That(typeof(ISafeMembers).IsUnsafeToMock(), Is.False);
		}

		[Test]
		public void IsUnsafeToMockWithUnsafeInterfaceWithUnsafeMethodWithUnsafeReturnValue()
		{
			Assert.That(typeof(IUnsafeMethodWithUnsafeReturnValue).IsUnsafeToMock(), Is.True);
		}

		[Test]
		public void IsUnsafeToMockWithUnsafeInterfaceWithUnsafeMethodWithUnsafeArguments()
		{
			Assert.That(typeof(IUnsafeMethodWithUnsafeArguments).IsUnsafeToMock(), Is.True);
		}

		[Test]
		public void IsUnsafeToMockWithUnsafeInterfaceWithUnsafePropertyType()
		{
			Assert.That(typeof(IUnsafePropertyWithUnsafePropertyType).IsUnsafeToMock(), Is.True);
		}

		[Test]
		public void IsUnsafeToMockWithUnsafeInterfaceWithUnsafeIndexer()
		{
			Assert.That(typeof(IUnsafePropertyWithUnsafeIndexer).IsUnsafeToMock(), Is.True);
		}

		[Test]
		public void IsUnsafeToMockWithSafeInterfaceWithUnsafeEventArgs()
		{
			Assert.That(typeof(ISafeEventWithUnsafeEventArgs).IsUnsafeToMock(), Is.False);
		}

		[Test]
		public void IsUnsafeToMockWithSafeClassWithSafeMembers()
		{
			Assert.That(typeof(SafeMembers).IsUnsafeToMock(), Is.False);
		}

		[Test]
		public void IsUnsafeToMockWithUnsafeClassWithUnsafeMethodWithUnsafeReturnValue()
		{
			Assert.That(typeof(UnsafeMethodWithUnsafeReturnValue).IsUnsafeToMock(), Is.True);
		}

		[Test]
		public void IsUnsafeToMockWithUnsafeClassWithUnsafeMethodWithUnsafeArguments()
		{
			Assert.That(typeof(UnsafeMethodWithUnsafeArguments).IsUnsafeToMock(), Is.True);
		}

		[Test]
		public void IsUnsafeToMockWithUnsafeClassWithUnsafePropertyType()
		{
			Assert.That(typeof(UnsafePropertyWithUnsafePropertyType).IsUnsafeToMock(), Is.True);
		}

		[Test]
		public void IsUnsafeToMockWithUnsafeClassWithUnsafeIndexer()
		{
			Assert.That(typeof(UnsafePropertyWithUnsafeIndexer).IsUnsafeToMock(), Is.True);
		}

		[Test]
		public void IsUnsafeToMockWithSafeClassWithUnsafeEventArgs()
		{
			Assert.That(typeof(SafeEventWithUnsafeEventArgs).IsUnsafeToMock(), Is.False);
		}
	}

	public interface ISafeMembers
	{
		void Target();
		int TargetReturn();
		int TargetProperty { get; set; }
		int this[int a] { get; set; }
		event EventHandler MyEvent;
	}

	public unsafe class UnsafeByteEventArgs : EventArgs
	{
		public byte* Value { get; set; }
	}

	public unsafe interface IUnsafeMethodWithUnsafeReturnValue
	{
		byte* Target();
	}

	public unsafe interface IUnsafeMethodWithUnsafeArguments
	{
		void Target(byte* a);
	}

	public unsafe interface IUnsafePropertyWithUnsafePropertyType
	{
		byte* Target { get; set; }
	}

	public unsafe interface IUnsafePropertyWithUnsafeIndexer
	{
		int this[byte* a] { get; set; }
	}

	public interface ISafeEventWithUnsafeEventArgs
	{
		event EventHandler<UnsafeByteEventArgs> Target;
	}

	public class SafeMembers
	{
		public virtual void Target() { }
		public virtual int TargetReturn() { return 0; }
		public virtual int TargetProperty { get; set; }
		public virtual int this[int a] { get { return 0; } set { } }
#pragma warning disable 67
		public virtual event EventHandler MyEvent;
#pragma warning restore 67
	}

	public unsafe class UnsafeMethodWithUnsafeReturnValue
	{
		public virtual byte* Target() { return default(byte*); }
	}

	public unsafe class UnsafeMethodWithUnsafeArguments
	{
		public virtual void Target(byte* a) { }
	}

	public unsafe class UnsafePropertyWithUnsafePropertyType
	{
		public virtual byte* Target { get; set; }
	}

	public unsafe class UnsafePropertyWithUnsafeIndexer
	{
		public virtual int this[byte* a] { get { return 0; } set { } }
	}

	public class SafeEventWithUnsafeEventArgs
	{
#pragma warning disable 67
		public virtual event EventHandler<UnsafeByteEventArgs> Target;
#pragma warning restore 67
	}
}
