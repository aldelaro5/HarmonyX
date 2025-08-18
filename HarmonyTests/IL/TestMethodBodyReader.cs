using HarmonyLib;
using HarmonyLibTests.Assets;
using NUnit.Framework;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace HarmonyLibTests.IL
{
	[TestFixture, NonParallelizable]
	public class TestMethodBodyReader : TestLogger
	{
		public static void WeirdMethodWithGoto()
		{
			LABEL:
			try
			{
				for (; ; )
				{
				}
			}
			catch (Exception)
			{
				goto LABEL;
			}
		}

		[Test]
		public void Test_Read_WeirdMethodWithGoto()
		{
			var method = SymbolExtensions.GetMethodInfo(() => WeirdMethodWithGoto());
			Assert.NotNull(method);
			var instructions = PatchProcessor.GetOriginalInstructions(method);
			Assert.NotNull(instructions);
			Assert.Greater(instructions.Count, 0);
		}
	}
}
