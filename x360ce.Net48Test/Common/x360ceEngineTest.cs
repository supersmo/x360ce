using JocysCom.ClassLibrary.Web.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using x360ce.App.Controls;
using x360ce.Engine.Data;
using x360ce.Engine.Maps;

namespace x360ce.Tests
{

	[TestClass]
	public class x360ceEngineTest
	{
		private static Type[] excludeTypes = new Type[] {
			typeof(JocysCom.WebSites.Engine.Security.Data.SecurityEntities),
			typeof(SoapHttpClientBase),
			typeof(x360ceModelContainer),
		};


		[TestMethod]
		public void Test_All()
		{
			MemoryLeakHelper.Test(typeof(Engine.EngineHelper).Assembly,
				// Include types. null = Test all.
				null,
				// Exclude types.
				excludeTypes);
		}

	}

	[TestClass]
	public class TapZoneTest
	{
		[TestMethod]
		public void TestTapTargetZero()
		{
			TapZone tapZone = new TapZone(1);
            Assert.AreEqual(0, tapZone.GetOutput(0));
            Assert.AreEqual(0, tapZone.GetOutput(0));
            Assert.AreEqual(0, tapZone.GetOutput(0));
            Assert.AreEqual(0, tapZone.GetOutput(0));
        }

        [TestMethod]
		public void TestTapTarget50Percent()
		{
            TapZone tapZone = new TapZone(1);
            Assert.AreEqual(1, tapZone.GetOutput(0.5f));
            Assert.AreEqual(0, tapZone.GetOutput(0.5f));
            Assert.AreEqual(1, tapZone.GetOutput(0.5f));
            Assert.AreEqual(0, tapZone.GetOutput(0.5f));
            Assert.AreEqual(1, tapZone.GetOutput(0.5f));
            Assert.AreEqual(0, tapZone.GetOutput(0.5f));
        }

		[TestMethod]
        public void TestTapTarget25Percent()
        {
            TapZone tapZone = new TapZone(1);
            Assert.AreEqual(1, tapZone.GetOutput(0.25f));//100%
            Assert.AreEqual(0, tapZone.GetOutput(0.25f));// 50%
            Assert.AreEqual(0, tapZone.GetOutput(0.25f));// 33%
            Assert.AreEqual(0, tapZone.GetOutput(0.25f));// 25%

            Assert.AreEqual(1, tapZone.GetOutput(0.25f));//100%
            Assert.AreEqual(0, tapZone.GetOutput(0.25f));// 50%
            Assert.AreEqual(0, tapZone.GetOutput(0.25f));// 33%
            Assert.AreEqual(0, tapZone.GetOutput(0.25f));// 25%

            Assert.AreEqual(1, tapZone.GetOutput(0.25f));//100%
        }

        [TestMethod]
        public void TestTapTarget40Percent()
        {
            TapZone tapZone = new TapZone(1);
            Assert.AreEqual(1, tapZone.GetOutput(0.4f));//100%
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 50%
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 33%
            Assert.AreEqual(1, tapZone.GetOutput(0.4f));// 50%
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 40%

            Assert.AreEqual(1, tapZone.GetOutput(0.4f));//100%
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 50%
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 33%
            Assert.AreEqual(1, tapZone.GetOutput(0.4f));// 50%
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 40%
        }

        [TestMethod]
        public void TestTapTarget75Percent()
        {
            TapZone tapZone = new TapZone(1);
            Assert.AreEqual(1, tapZone.GetOutput(0.75f));//100%
            Assert.AreEqual(0, tapZone.GetOutput(0.75f));// 50%
            Assert.AreEqual(1, tapZone.GetOutput(0.75f));// 67%
            Assert.AreEqual(1, tapZone.GetOutput(0.75f));// 75%

            Assert.AreEqual(0, tapZone.GetOutput(0.75f));// 60% release due to rounding
            Assert.AreEqual(1, tapZone.GetOutput(0.75f));// 66%
            Assert.AreEqual(1, tapZone.GetOutput(0.75f));// 71%
            Assert.AreEqual(1, tapZone.GetOutput(0.75f));// 75%

            Assert.AreEqual(1, tapZone.GetOutput(0.75f));// 78%
            Assert.AreEqual(0, tapZone.GetOutput(0.75f));// 70%
            Assert.AreEqual(1, tapZone.GetOutput(0.75f));// 73%
            Assert.AreEqual(1, tapZone.GetOutput(0.75f));// 75%
        }

        [TestMethod]
        public void TestTapTarget100Percent()
        {
            TapZone tapZone = new TapZone(1);
            Assert.AreEqual(1, tapZone.GetOutput(1));
            Assert.AreEqual(1, tapZone.GetOutput(1));
            Assert.AreEqual(1, tapZone.GetOutput(1));
            Assert.AreEqual(1, tapZone.GetOutput(1));
        }

        [TestMethod]
        public void TestTapTarget25PercentWithTapValue50Percent()
        {
            TapZone tapZone = new TapZone(0.5f);
            Assert.AreEqual(0.5, tapZone.GetOutput(0.25f));//50%
            Assert.AreEqual(0, tapZone.GetOutput(0.25f));  //25%
            Assert.AreEqual(0.5, tapZone.GetOutput(0.25f));//33%
            Assert.AreEqual(0, tapZone.GetOutput(0.25f));  //25%
            Assert.AreEqual(0.5, tapZone.GetOutput(0.25f));//30%
            Assert.AreEqual(0, tapZone.GetOutput(0.25f));  //25%
            Assert.AreEqual(0.5, tapZone.GetOutput(0.25f));//29%
            Assert.AreEqual(0, tapZone.GetOutput(0.25f));  //25%
            Assert.AreEqual(0.5, tapZone.GetOutput(0.25f));//28%
        }

        [TestMethod]
        public void TestTapTargetMinus25Percent()
        {
            TapZone tapZone = new TapZone(1);
            Assert.AreEqual(-1, tapZone.GetOutput(-0.5f));
            Assert.AreEqual(0, tapZone.GetOutput(-0.5f));
            Assert.AreEqual(-1, tapZone.GetOutput(-0.5f));
            Assert.AreEqual(0, tapZone.GetOutput(-0.5f));
            Assert.AreEqual(-1, tapZone.GetOutput(-0.5f));     
        }

        [TestMethod]
        public void TestTapTarget40PercentWithMinCycles2()
        {
            TapZone tapZone = new TapZone(1, 2);
            Assert.AreEqual(1, tapZone.GetOutput(0.4f));//100%
            Assert.AreEqual(1, tapZone.GetOutput(0.4f));//100% <-- Extra press cycle becaues of minCycles
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 66%
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 50%
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 40%

            Assert.AreEqual(1, tapZone.GetOutput(0.4f));//100%
            Assert.AreEqual(1, tapZone.GetOutput(0.4f));// 50% <-- Extra press cycle becaues of minCycles
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 33%
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 50%
            Assert.AreEqual(0, tapZone.GetOutput(0.4f));// 40%
        }

        [TestMethod]
        public void TestTapTarget60PercentWithMinCycles2()
        {
            TapZone tapZone = new TapZone(1, 2);
            Assert.AreEqual(1, tapZone.GetOutput(0.6f));//100%
            Assert.AreEqual(1, tapZone.GetOutput(0.6f));//100% <-- Extra press cycle Becaues of minCycles
            Assert.AreEqual(0, tapZone.GetOutput(0.6f));// 66%
            Assert.AreEqual(0, tapZone.GetOutput(0.6f));// 50%
            Assert.AreEqual(1, tapZone.GetOutput(0.6f));// 60%

            Assert.AreEqual(1, tapZone.GetOutput(0.6f));//67%
            Assert.AreEqual(0, tapZone.GetOutput(0.6f));// 57%
            Assert.AreEqual(0, tapZone.GetOutput(0.6f));// 50% <-- Extra release cycle becaues of minCycles
            Assert.AreEqual(1, tapZone.GetOutput(0.6f));// 56%
            Assert.AreEqual(1, tapZone.GetOutput(0.6f));// 60%
        }
    }
}
