﻿using NUnit.Framework;

namespace Rocks.Tests
{
	[TestFixture]
	public sealed class RefAndOutTests
	{
		private void MyActionOutTarget(out int a)
		{
			a = 2;
		}

		private void MyActionRefTarget(ref int a)
		{
			a = 2;
		}

		private int MyFuncRefTarget(ref int a)
		{
			a = 2;
			return 4;
		}

		private int MyFuncOutTarget(out int a)
		{
			a = 2;
			return 4;
		}

		[Test]
		public void MakeRefActionWithDelegate()
		{
			var a = 1;
			var rock = Rock.Create<IHaveRefAndOut>();
			rock.HandleAction(_ => _.RefTarget(ref a), new RefTarget(this.MyActionRefTarget));

			var chunk = rock.Make();
			chunk.RefTarget(ref a);

			Assert.AreEqual(2, a, nameof(a));
			rock.Verify();
		}

		[Test]
		public void MakeRefActionWithDelegateAndExpectedCallCount()
		{
			var a = 1;
			var rock = Rock.Create<IHaveRefAndOut>();
			rock.HandleAction(_ => _.RefTarget(ref a), new RefTarget(this.MyActionRefTarget), 2);

			var chunk = rock.Make();
			chunk.RefTarget(ref a);
			chunk.RefTarget(ref a);

			Assert.AreEqual(2, a, nameof(a));
			rock.Verify();
		}

		[Test]
		public void MakeOutActionWithDelegate()
		{
			var a = 1;
			var rock = Rock.Create<IHaveRefAndOut>();
			rock.HandleAction(_ => _.OutTarget(out a), new OutTarget(this.MyActionOutTarget));

			var chunk = rock.Make();
			chunk.OutTarget(out a);

			Assert.AreEqual(2, a, nameof(a));
			rock.Verify();
		}

		[Test]
		public void MakeOutActionWithDelegateAndExpectedCallCount()
		{
			var a = 1;
			var rock = Rock.Create<IHaveRefAndOut>();
			rock.HandleAction(_ => _.OutTarget(out a), new OutTarget(this.MyActionOutTarget), 2);

			var chunk = rock.Make();
			chunk.OutTarget(out a);
			chunk.OutTarget(out a);

			Assert.AreEqual(2, a, nameof(a));
			rock.Verify();
		}

		[Test]
		public void MakeRefFuncWithDelegate()
		{
			var a = 1;
			var rock = Rock.Create<IHaveRefAndOut>();
			rock.HandleAction(_ => _.RefTargetWithReturn(ref a), new RefTargetWithReturn(this.MyFuncRefTarget));

			var chunk = rock.Make();
			chunk.RefTargetWithReturn(ref a);

			Assert.AreEqual(2, a, nameof(a));
			rock.Verify();
		}

		[Test]
		public void MakeRefFuncWithDelegateAndExpectedCallCount()
		{
			var a = 1;
			var rock = Rock.Create<IHaveRefAndOut>();
			rock.HandleAction(_ => _.RefTargetWithReturn(ref a), new RefTargetWithReturn(this.MyFuncRefTarget), 2);

			var chunk = rock.Make();
			chunk.RefTargetWithReturn(ref a);
			chunk.RefTargetWithReturn(ref a);

			Assert.AreEqual(2, a, nameof(a));
			rock.Verify();
		}

		[Test]
		public void MakeOutFuncWithDelegate()
		{
			var a = 1;
			var rock = Rock.Create<IHaveRefAndOut>();
			rock.HandleAction(_ => _.OutTargetWithReturn(out a), new OutTargetWithReturn(this.MyFuncOutTarget));

			var chunk = rock.Make();
			chunk.OutTargetWithReturn(out a);

			Assert.AreEqual(2, a, nameof(a));
			rock.Verify();
		}

		[Test]
		public void MakeOutFuncWithDelegateWithExpectedCallCount()
		{
			var a = 1;
			var rock = Rock.Create<IHaveRefAndOut>();
			rock.HandleAction(_ => _.OutTargetWithReturn(out a), new OutTargetWithReturn(this.MyFuncOutTarget), 2);

			var chunk = rock.Make();
			chunk.OutTargetWithReturn(out a);
			chunk.OutTargetWithReturn(out a);

			Assert.AreEqual(2, a, nameof(a));
			rock.Verify();
		}
	}

	public interface IHaveRefAndOut
	{
		void OutTarget(out int a);
		int OutTargetWithReturn(out int a);
		void RefTarget(ref int a);
		int RefTargetWithReturn(ref int a);
	}

	public delegate void OutTarget(out int a);
	public delegate int OutTargetWithReturn(out int a);
	public delegate void RefTarget(ref int a);
	public delegate int RefTargetWithReturn(ref int a);
}
