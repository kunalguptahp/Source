using HP.HPFx.Diagnostics.Testing;
using MbUnit.Framework;

namespace HP.ElementsCPS.Core.Tests.Fixtures
{
	[TestFixture]
	[Importance(Importance.Critical)]
	[Category(TestCategory.Kind.Unit)]
	[Category(TestCategory.Speed.Fast)]
	public class RemindersFixture
	{

		/// <summary>
		/// Raises any warnings not related to any existing test, such as warnings about missing tests.
		/// </summary>
		[Test]
		public void LogWarnings()
		{
		}

	}
}
