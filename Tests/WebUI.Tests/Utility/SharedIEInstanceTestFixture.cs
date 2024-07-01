using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.Tests.Utility;
using HP.HPFx.Extensions.Watin;
using MbUnit.Framework;
using WatiN.Core;

namespace HP.ElementsCPS.Apps.WebUI.Tests.Utility
{
	/// <summary>
	/// This abstract test fixture base class enables the easy creation of faster test fixtures and methods 
	/// by allowing WatiN tests within the same fixture to easily "share" and reuse a single WatiN <see cref="IE"/> instance.
	/// </summary>
	/// <remarks>
	/// This class enables inheriting fixtures to transparently create and dispose a single IE instance (once for the entire test fixture), 
	/// rather than relying on each test method to create and dispose a separate IE instance for itself.
	/// </remarks>
	public abstract class SharedIEInstanceTestFixture
	{

		static SharedIEInstanceTestFixture()
		{
			ElementsCPSWatinUtility.DoNothing();
		}

		public EnhancedIE SharedIEInstance { get; set; }

		[FixtureSetUp]
		public virtual void TestFixtureSetUp()
		{
			this.TestFixtureSharedIEInstanceSetUp();
			this.TestFixtureDataSetUp();
			//NOTE: If TestFixtureDataSetUp does nothing, then TestFixtureUserRoleSetUp will fail and cause MbUnit framework runtime problems
			try
			{
				//HACK: Review Needed: Review implementation for correctness: Should this really catch and ignore all exceptions?
#warning HACK: Review Needed: Review implementation for correctness: Should this really catch and ignore all exceptions?
				this.TestFixtureUserRoleSetUp();
			}
			catch
			{
				//do nothing
			}
		}

		/// <summary>
		/// Inheriting classes may override this method (instead of the <see cref="TestFixtureSetUp"/> method) 
		/// to modify the default behavior for instantiating <see cref="SharedIEInstance"/>.
		/// </summary>
		protected virtual void TestFixtureSharedIEInstanceSetUp()
		{
			//IE.Settings.MakeNewIeInstanceVisible = false;
			this.SharedIEInstance = WatinUtility.NewIE();
			//this.SharedIEInstance.ShowWindow(NativeMethods.WindowShowStyle.Hide);
		}

		/// <summary>
		/// Inheriting classes may override this method (instead of the <see cref="TestFixtureSetUp"/> method) 
		/// to execute data setup code during the TestFixtureSetup phase.
		/// </summary>
		protected virtual void TestFixtureDataSetUp()
		{
		}

		/// <summary>
		/// Inheriting classes may override this method (instead of the <see cref="TestFixtureSetUp"/> method) 
		/// to modify the default user role setup behavior during the TestFixtureSetup phase.
		/// </summary>
		protected virtual void TestFixtureUserRoleSetUp()
		{
			DataUtility.SetTestUserRoles(UserRoleId.Administrator);
		}

		[FixtureTearDown]
		public virtual void TestFixtureTearDown()
		{
			this.TestFixtureSharedIEInstanceTearDown();
			this.TestFixtureDataTearDown();
			//NOTE: If TestFixtureDataTearDown removes all data, then TestFixtureUserRoleTearDown will fail and could cause MbUnit framework runtime problems
			try
			{
				//HACK: Review Needed: Review implementation for correctness: Should this really catch and ignore all exceptions?
#warning HACK: Review Needed: Review implementation for correctness: Should this really catch and ignore all exceptions?
				this.TestFixtureUserRoleTearDown();
			}
			catch
			{
				//do nothing
			}
		}

		/// <summary>
		/// Inheriting classes may override this method (instead of the <see cref="TestFixtureTearDown"/> method) 
		/// to modify the default behavior for instantiating <see cref="SharedIEInstance"/>.
		/// </summary>
		protected virtual void TestFixtureSharedIEInstanceTearDown()
		{
			try
			{
				if (this.SharedIEInstance != null)
				{
					this.SharedIEInstance.Dispose();
				}
			}
			finally
			{
				this.SharedIEInstance = null;
			}
		}

		/// <summary>
		/// Inheriting classes may override this method (instead of the <see cref="TestFixtureTearDown"/> method) 
		/// to execute data TearDown code during the TestFixtureTearDown phase.
		/// </summary>
		protected virtual void TestFixtureDataTearDown()
		{
		}

		/// <summary>
		/// Inheriting classes may override this method (instead of the <see cref="TestFixtureTearDown"/> method) 
		/// to modify the default user role TearDown behavior during the TestFixtureTearDown phase.
		/// </summary>
		protected virtual void TestFixtureUserRoleTearDown()
		{
			DataUtility.SetTestUserRoles(UserRoleId.Administrator);
		}

	}
}