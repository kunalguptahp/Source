using System;
using System.Collections.Generic;
using HP.HPFx.Data.Query;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	public abstract class BaseQuerySpecificationEditDataUserControl : BaseEditDataUserControl
	{
		#region Events

		public event EventHandler QueryChanged;

		protected void RaiseQueryChanged(EventArgs e)
		{
			if (this.QueryChanged != null)
			{
				this.QueryChanged(this, e);
			}
		}

		public event EventHandler ImmutableQueryConditionsChanged;

		protected void RaiseImmutableQueryConditionsChanged(EventArgs e)
		{
			if (this.ImmutableQueryConditionsChanged != null)
			{
				this.ImmutableQueryConditionsChanged(this, e);
			}
		}

		#endregion

		#region Constants

		private const string ViewStateKey_QuerySpecification = "QuerySpecification";
		private const string ViewStateKey_ImmutableQueryConditions = "ImmutableQueryConditions";

		#endregion

		#region Properties

		private IQuerySpecification _CachedQuerySpecification = null;

		/// <remarks>
		/// NOTE: Setting the value of this property automatically invokes the <see cref="SaveQuerySpecificationChanges"/> method.
		/// </remarks>
		public IQuerySpecification QuerySpecification
		{
			get
			{
				this.RefreshQuerySpecificationCache();
				return this._CachedQuerySpecification;
			}
			set
			{
				this.RefreshQuerySpecificationCache(); //this is needed in case the set method is called before the get method.
				this._CachedQuerySpecification = value; //update the backing field (i.e. the "cached" copy) immediately
				this.SaveQuerySpecificationChanges(); //this will raise the QueryChanged event (if a change occurred)
			}
		}

		/// <summary>
		/// Returns an <see cref="IQuerySpecification"/> that represents the result of combining the 
		/// <see cref="QuerySpecification"/> and <see cref="ImmutableQueryConditions"/> values.
		/// </summary>
		public IQuerySpecification CombinedQuerySpecification
		{
			get
			{
				IQuerySpecification combinedQuery = this.QuerySpecification.InnerData.Copy();
				combinedQuery.Conditions.Add(this.ImmutableQueryConditions.Conditions);
				return combinedQuery;
			}
		}

		private void RefreshQuerySpecificationCache()
		{
			if (this._CachedQuerySpecification == null)
			{
				this._CachedQuerySpecification = this.QuerySpecificationViewState;
				if (this._CachedQuerySpecification == null)
				{
					//NOTE: use ImmutableQueryConditions as the default value so that any filter-initialization code will properly reflect the ImmutableQueryConditions
					this._CachedQuerySpecification = new QuerySpecificationWrapper(this.ImmutableQueryConditions.InnerData.Copy());
				}
			}
		}

		/// <summary>
		/// Indicates whether any changes have been made to the <see cref="QuerySpecification"/> since 
		/// <see cref="SaveQuerySpecificationChanges"/> was last invoked.
		/// </summary>
		/// <returns></returns>
		public bool HasQuerySpecificationChanged()
		{
			return (this._CachedQuerySpecification != this.QuerySpecificationViewState);
		}

		/// <remarks>
		/// <para>
		/// Note that, unlike the <see cref="QuerySpecification"/> property, this property may return a <c>null</c> value.
		/// </para>
		/// <para>
		/// Note that, unlike the <see cref="QuerySpecification"/> property, any direct modifications of the value returned by this property WILL NOT be persisted.
		/// </para>
		/// </remarks>
		private IQuerySpecification QuerySpecificationViewState
		{
			get
			{
				//TODO: Review Needed: Review serialization implementation for correctness
#warning Review Needed: Review serialization implementation for correctness
				string xml = this.ViewState[ViewStateKey_QuerySpecification] as string;
				return (string.IsNullOrEmpty(xml)) ? null : QuerySpecificationWrapper.FromXml(xml);
				//return this.ViewState[ViewStateKey_QuerySpecification] as IQuerySpecification;
			}
			set
			{
				IQuerySpecification currentQuerySpecification = this.QuerySpecificationViewState;
				if (value == currentQuerySpecification)
				{
					return; //do nothing if the new value is the same as the current value
				}

				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_QuerySpecification);
				}
				else
				{
					this.ViewState[ViewStateKey_QuerySpecification] = value.ToString();
				}

				this.OnQueryChange(new EventArgs());
			}
		}

		/// <summary>
		/// Stores any query conditions that are fixed or immutable except via this property (e.g. shouldn't be modifiable by filter controls, etc.).
		/// </summary>
		/// <remarks>
		/// <para>
		/// Note that, unlike the <see cref="QuerySpecification"/> property, any direct modifications of the value returned by this property WILL NOT be persisted.
		/// </para>
		/// </remarks>
		public IQuerySpecification ImmutableQueryConditions
		{
			get
			{
				string xml = this.ViewState[ViewStateKey_ImmutableQueryConditions] as string;
				return (string.IsNullOrEmpty(xml)) ? new QuerySpecificationWrapper() : QuerySpecificationWrapper.FromXml(xml);
			}
			set
			{
				IQuerySpecification newValue = value;

				newValue = CleanUpImmutableQueryConditions(newValue);

				IQuerySpecification currentImmutableQueryConditions = this.ImmutableQueryConditions;
				if (newValue == currentImmutableQueryConditions)
				{
					return; //do nothing if the new value is the same as the current value
				}

				if (newValue == null)
				{
					this.ViewState.Remove(ViewStateKey_ImmutableQueryConditions);
				}
				else
				{
					this.ViewState[ViewStateKey_ImmutableQueryConditions] = newValue.ToString();
				}

				this.OnImmutableQueryConditionsChange(new EventArgs());
			}
		}

		/// <summary>
		/// Perform "clean up" conversion/standardization of an <see cref="ImmutableQueryConditions"/> value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static IQuerySpecification CleanUpImmutableQueryConditions(IQuerySpecification value)
		{
			if (value != null)
			{
				if (value.Conditions.Count < 1)
				{
					//there are no conditions, so the value is equivelent to null
					value = null;
				}
				else
				{
					value = value.InnerData.Copy(); //copy the value before modifying it
					//ignore/clear any existing paging and sorting info in the value (since those would be ignored anyway)
					value.Paging.Clear();
					value.SortBy.Clear();
				}
			}
			return value;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves any changes that have been made (e.g. directly to the objects properties or sub-objects) 
		/// and conditionally raises the QueryChanged event (only if a change actually occurred).
		/// </summary>
		public void SaveQuerySpecificationChanges()
		{
			if (!this.HasQuerySpecificationChanged())
			{
				return; //do nothing so that the QueryChanged event DOES NOT get raised
			}

			IQuerySpecification oldValue = this.QuerySpecificationViewState;
			IQuerySpecification newValue = this._CachedQuerySpecification;

			//if either the conditions or the sort order have changed, then auto-reset the page index
			//because it doesn't make sense to go directly to a specific page (except the first) 
			//when the sort order and/or filtering conditions have changed
			if ((oldValue != null) && (!HPFx.Data.Query.QuerySpecification.AreEqualExceptForPaging(oldValue, newValue)))
			{
				newValue.Paging.ResetPageIndex();
			}

			//Save the new instance to the ViewState (i.e. the persistent copy)
			this.QuerySpecificationViewState = newValue;
			this._CachedQuerySpecification = newValue; //just to be safe

			//indicate to any listeners that changes have occurred
			this.OnQueryChange(new EventArgs());
		}

		protected void OnImmutableQueryConditionsChange(EventArgs e)
		{
			this.RaiseImmutableQueryConditionsChanged(e);
		}

		protected void OnQueryChange(EventArgs e)
		{
			this.RaiseQueryChanged(e);
		}

		#endregion

	}
}