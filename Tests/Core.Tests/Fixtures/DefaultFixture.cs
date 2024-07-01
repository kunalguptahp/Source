using HP.ElementsCPS.Core.Security;
using HP.HPFx.Diagnostics.Testing;
using MbUnit.Framework;

namespace HP.ElementsCPS.Core.Tests.Fixtures
{
	[TestFixture]
	//[DependsOn(typeof(RemindersFixture))]
	[Importance(Importance.Default)]
	[Category(TestCategory.Kind.Unit)]
	public class DefaultFixture
	{

		#region Assembly Tests

		[Test]
		public void AssemblyTest_SmokeTest()
		{
			OldReflectionAssert.NotCreatable(typeof(SecurityManager));
		}

		#endregion

	}
}
