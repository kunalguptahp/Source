using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ElementsCPS.Data.Tests.Utility
{
	public class CombinationUtility
	{
		private int[] _currentCombination = null;

		public int[] CurrentCombination
		{
			get { return _currentCombination; }
		}

		private int _totalSize;
		private int _combinationSize;
		
		private CombinationUtility()
		{}

		/// <summary>
		/// Create a new CombinationUtility.
		/// </summary>
		/// <param name="totalSize"></param>
		/// <param name="combinationSize"></param>
		public CombinationUtility(int totalSize, int combinationSize)
		{
			if (combinationSize> totalSize)
			{
				throw new ArgumentException(string.Format("combinationSize cann't be greater than totalSize! combinationSize: {0}, totalSize: {1}.", combinationSize, totalSize));
			}
			this._combinationSize = combinationSize;
			this._totalSize = totalSize;
		}

		public bool NextCombination()
		{
			if (this._currentCombination == null)
			{
				this._currentCombination = new int[this._combinationSize];
				for (int i = 0; i <= this._currentCombination.Length - 1; i++)
				{
					this._currentCombination[i] = i;
				}
				return true;
			}

			for(int i = this._currentCombination.Length - 1; i >= 0; i--)
			{
				int maxIndex = this._totalSize - this._currentCombination.Length + i;

				if (i == 0 && this._currentCombination[i] == maxIndex)
				{
					return false;
				}

				if (this._currentCombination[i] < maxIndex)
				{
					this._currentCombination[i] = this._currentCombination[i] + 1;

					for (int subIndex = i + 1; subIndex <= this._currentCombination.Length - 1; subIndex++) 
					{
						int subMaxIndex = this._totalSize - this._currentCombination.Length + subIndex;
						if (this._currentCombination[subIndex - 1] + 1 <= subMaxIndex)
						{
							this._currentCombination[subIndex] = this._currentCombination[subIndex - 1] + 1;
						}
					}

					return true;
				}
			}

			return false;
		}
	}
}
