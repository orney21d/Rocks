﻿using NUnit.Framework;

namespace Rocks.Tests
{
	[TestFixture]
	public sealed class HandleFunc11ArgumentTests
	{
		[Test]
		public void Make()
		{
			var rock = Rock.Create<IHandleFunc11ArgumentTests>();
			rock.HandleFunc(_ => _.ReferenceTarget(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11));
			rock.HandleFunc(_ => _.ValueTarget(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110));

			var chunk = rock.Make();
			chunk.ReferenceTarget(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
			chunk.ValueTarget(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110);

			rock.Verify();
		}

		[Test]
		public void MakeWithHandler()
		{
			var argumentA = 0;
			var argumentB = 0;
			var argumentC = 0;
			var argumentD = 0;
			var argumentE = 0;
			var argumentF = 0;
			var argumentG = 0;
			var argumentH = 0;
			var argumentI = 0;
			var argumentJ = 0;
			var argumentK = 0;
			var stringReturnValue = "a";
			var intReturnValue = 1;

			var rock = Rock.Create<IHandleFunc11ArgumentTests>();
			rock.HandleFunc<int, int, int, int, int, int, int, int, int, int, int, string>(_ => _.ReferenceTarget(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11),
				(a, b, c, d, e, f, g, h, i, j, k) => { argumentA = a; argumentB = b; argumentC = c; argumentD = d; argumentE = e; argumentF = f; argumentG = g; argumentH = h; argumentI = i; argumentJ = j; argumentK = k; return stringReturnValue; });
			rock.HandleFunc<int, int, int, int, int, int, int, int, int, int, int, int>(_ => _.ValueTarget(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110),
				(a, b, c, d, e, f, g, h, i, j, k) => { argumentA = a; argumentB = b; argumentC = c; argumentD = d; argumentE = e; argumentF = f; argumentG = g; argumentH = h; argumentI = i; argumentJ = j; argumentK = k; return intReturnValue; });
			
			var chunk = rock.Make();
			Assert.AreEqual(stringReturnValue, chunk.ReferenceTarget(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11), nameof(chunk.ReferenceTarget));
			Assert.AreEqual(1, argumentA, nameof(argumentA));
			Assert.AreEqual(2, argumentB, nameof(argumentB));
			Assert.AreEqual(3, argumentC, nameof(argumentC));
			Assert.AreEqual(4, argumentD, nameof(argumentD));
			Assert.AreEqual(5, argumentE, nameof(argumentE));
			Assert.AreEqual(6, argumentF, nameof(argumentF));
			Assert.AreEqual(7, argumentG, nameof(argumentG));
			Assert.AreEqual(8, argumentH, nameof(argumentH));
			Assert.AreEqual(9, argumentI, nameof(argumentI));
			Assert.AreEqual(10, argumentJ, nameof(argumentJ));
			Assert.AreEqual(11, argumentK, nameof(argumentK));
			argumentA = 0;
			argumentB = 0;
			argumentC = 0;
			argumentD = 0;
			argumentE = 0;
			argumentF = 0;
			argumentG = 0;
			argumentH = 0;
			argumentI = 0;
			argumentJ = 0;
			argumentK = 0;
			Assert.AreEqual(intReturnValue, chunk.ValueTarget(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110), nameof(chunk.ValueTarget));
			Assert.AreEqual(10, argumentA, nameof(argumentA));
			Assert.AreEqual(20, argumentB, nameof(argumentB));
			Assert.AreEqual(30, argumentC, nameof(argumentC));
			Assert.AreEqual(40, argumentD, nameof(argumentD));
			Assert.AreEqual(50, argumentE, nameof(argumentE));
			Assert.AreEqual(60, argumentF, nameof(argumentF));
			Assert.AreEqual(70, argumentG, nameof(argumentG));
			Assert.AreEqual(80, argumentH, nameof(argumentH));
			Assert.AreEqual(90, argumentI, nameof(argumentI));
			Assert.AreEqual(100, argumentJ, nameof(argumentJ));
			Assert.AreEqual(110, argumentK, nameof(argumentK));

			rock.Verify();
		}

		[Test]
		public void MakeWithExpectedCallCount()
		{
			var rock = Rock.Create<IHandleFunc11ArgumentTests>();
			rock.HandleFunc(_ => _.ReferenceTarget(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11), 2);
			rock.HandleFunc(_ => _.ValueTarget(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110), 2);

			var chunk = rock.Make();
			chunk.ReferenceTarget(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
			chunk.ReferenceTarget(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
			chunk.ValueTarget(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110);
			chunk.ValueTarget(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110);

			rock.Verify();
		}

		[Test]
		public void MakeWithHandlerAndExpectedCallCount()
		{
			var argumentA = 0;
			var argumentB = 0;
			var argumentC = 0;
			var argumentD = 0;
			var argumentE = 0;
			var argumentF = 0;
			var argumentG = 0;
			var argumentH = 0;
			var argumentI = 0;
			var argumentJ = 0;
			var argumentK = 0;
			var stringReturnValue = "a";
			var intReturnValue = 1;

			var rock = Rock.Create<IHandleFunc11ArgumentTests>();
			rock.HandleFunc<int, int, int, int, int, int, int, int, int, int, int, string>(_ => _.ReferenceTarget(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11),
				(a, b, c, d, e, f, g, h, i, j, k) => { argumentA = a; argumentB = b; argumentC = c; argumentD = d; argumentE = e; argumentF = f; argumentG = g; argumentH = h; argumentI = i; argumentJ = j; argumentK = k; return stringReturnValue; }, 2);
			rock.HandleFunc<int, int, int, int, int, int, int, int, int, int, int, int>(_ => _.ValueTarget(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110),
				(a, b, c, d, e, f, g, h, i, j, k) => { argumentA = a; argumentB = b; argumentC = c; argumentD = d; argumentE = e; argumentF = f; argumentG = g; argumentH = h; argumentI = i; argumentJ = j; argumentK = k; return intReturnValue; }, 2);

			var chunk = rock.Make();
			Assert.AreEqual(stringReturnValue, chunk.ReferenceTarget(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11), nameof(chunk.ReferenceTarget));
			Assert.AreEqual(1, argumentA, nameof(argumentA));
			Assert.AreEqual(2, argumentB, nameof(argumentB));
			Assert.AreEqual(3, argumentC, nameof(argumentC));
			Assert.AreEqual(4, argumentD, nameof(argumentD));
			Assert.AreEqual(5, argumentE, nameof(argumentE));
			Assert.AreEqual(6, argumentF, nameof(argumentF));
			Assert.AreEqual(7, argumentG, nameof(argumentG));
			Assert.AreEqual(8, argumentH, nameof(argumentH));
			Assert.AreEqual(9, argumentI, nameof(argumentI));
			Assert.AreEqual(10, argumentJ, nameof(argumentJ));
			Assert.AreEqual(11, argumentK, nameof(argumentK));
			argumentA = 0;
			argumentB = 0;
			argumentC = 0;
			argumentD = 0;
			argumentE = 0;
			argumentF = 0;
			argumentG = 0;
			argumentH = 0;
			argumentI = 0;
			argumentJ = 0;
			argumentK = 0;
			Assert.AreEqual(stringReturnValue, chunk.ReferenceTarget(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11), nameof(chunk.ReferenceTarget));
			Assert.AreEqual(1, argumentA, nameof(argumentA));
			Assert.AreEqual(2, argumentB, nameof(argumentB));
			Assert.AreEqual(3, argumentC, nameof(argumentC));
			Assert.AreEqual(4, argumentD, nameof(argumentD));
			Assert.AreEqual(5, argumentE, nameof(argumentE));
			Assert.AreEqual(6, argumentF, nameof(argumentF));
			Assert.AreEqual(7, argumentG, nameof(argumentG));
			Assert.AreEqual(8, argumentH, nameof(argumentH));
			Assert.AreEqual(9, argumentI, nameof(argumentI));
			Assert.AreEqual(10, argumentJ, nameof(argumentJ));
			Assert.AreEqual(11, argumentK, nameof(argumentK));
			argumentA = 0;
			argumentB = 0;
			argumentC = 0;
			argumentD = 0;
			argumentE = 0;
			argumentF = 0;
			argumentG = 0;
			argumentH = 0;
			argumentI = 0;
			argumentJ = 0;
			argumentK = 0;
			Assert.AreEqual(intReturnValue, chunk.ValueTarget(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110), nameof(chunk.ValueTarget));
			Assert.AreEqual(10, argumentA, nameof(argumentA));
			Assert.AreEqual(20, argumentB, nameof(argumentB));
			Assert.AreEqual(30, argumentC, nameof(argumentC));
			Assert.AreEqual(40, argumentD, nameof(argumentD));
			Assert.AreEqual(50, argumentE, nameof(argumentE));
			Assert.AreEqual(60, argumentF, nameof(argumentF));
			Assert.AreEqual(70, argumentG, nameof(argumentG));
			Assert.AreEqual(80, argumentH, nameof(argumentH));
			Assert.AreEqual(90, argumentI, nameof(argumentI));
			Assert.AreEqual(100, argumentJ, nameof(argumentJ));
			Assert.AreEqual(110, argumentK, nameof(argumentK));
			argumentA = 0;
			argumentB = 0;
			argumentC = 0;
			argumentD = 0;
			argumentE = 0;
			argumentF = 0;
			argumentG = 0;
			argumentH = 0;
			argumentI = 0;
			argumentJ = 0;
			argumentK = 0;
			Assert.AreEqual(intReturnValue, chunk.ValueTarget(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110), nameof(chunk.ValueTarget));
			Assert.AreEqual(10, argumentA, nameof(argumentA));
			Assert.AreEqual(20, argumentB, nameof(argumentB));
			Assert.AreEqual(30, argumentC, nameof(argumentC));
			Assert.AreEqual(40, argumentD, nameof(argumentD));
			Assert.AreEqual(50, argumentE, nameof(argumentE));
			Assert.AreEqual(60, argumentF, nameof(argumentF));
			Assert.AreEqual(70, argumentG, nameof(argumentG));
			Assert.AreEqual(80, argumentH, nameof(argumentH));
			Assert.AreEqual(90, argumentI, nameof(argumentI));
			Assert.AreEqual(100, argumentJ, nameof(argumentJ));
			Assert.AreEqual(110, argumentK, nameof(argumentK));

			rock.Verify();
		}
	}

	public interface IHandleFunc11ArgumentTests
	{
		string ReferenceTarget(int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k);
		int ValueTarget(int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k);
	}
}