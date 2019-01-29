#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: LVMergeSort.cs
// 
// Copyright (c) 2016 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
// All Rights Reserved.
// 
// -----------------------------------------------------------------------------------------------------------
// 
// GNU General Public License v3.0 (GPL-3.0)
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF
// MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  
// This file is subject to the terms and conditions defined in the file 
// 'LICENSE.md', which should be in the root directory of the source code package.
// 
// -----------------------------------------------------------------------------------------------------------

#endregion

#region Namespace

using System;
using System.Diagnostics;

using VisualPlus.Collections.CollectionsBase;
using VisualPlus.Enumerators;
using VisualPlus.Toolkit.Child;

#endregion

namespace VisualPlus.Structure
{
    public class LVMergeSort
    {
        #region Fields

        private bool _numericCompare;
        private int _sortColumn;
        private SortDirections _sortDirection;
        private bool _stopRequested;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="LVMergeSort" /> class.</summary>
        public LVMergeSort()
        {
            _sortDirection = SortDirections.Descending;
        }

        #endregion

        #region Public Properties

        /// <summary>Compare only numeric values in items.  Warning, this can end up slowing down routine quite a bit.</summary>
        public bool NumericCompare
        {
            get
            {
                return _numericCompare;
            }

            set
            {
                _numericCompare = value;
            }
        }

        /// <summary>Column within the items structure to sort.</summary>
        public int SortColumn
        {
            get
            {
                return _sortColumn;
            }

            set
            {
                _sortColumn = value;
            }
        }

        /// <summary>Direction this sorting routine will move items.</summary>
        public SortDirections SortDirection
        {
            get
            {
                return _sortDirection;
            }

            set
            {
                _sortDirection = value;
            }
        }

        /// <summary>Stop this sort before it finishes.</summary>
        public bool StopRequested
        {
            get
            {
                return _stopRequested;
            }

            set
            {
                _stopRequested = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The sort.</summary>
        /// <param name="items">The items.</param>
        /// <param name="low_0">The low.</param>
        /// <param name="high_0">The high.</param>
        public void Sort(VisualListViewItemCollection items, int low_0, int high_0)
        {
            int lo = low_0;
            int hi = high_0;
            if (lo >= hi)
            {
                return;
            }

            int mid = (lo + hi) / 2;

            Sort(items, lo, mid);
            Sort(items, mid + 1, hi);

            int end_lo = mid;
            int start_hi = mid + 1;
            while ((lo <= end_lo) && (start_hi <= hi))
            {
                if (StopRequested)
                {
                    return;
                }

                if (CompareItems(items[lo], items[start_hi], CompareDirection.LessThan))
                {
                    lo++;
                }
                else
                {
                    VisualListViewItem visualListViewItem = items[start_hi];
                    for (int k = start_hi - 1; k >= lo; k--)
                    {
                        items[k + 1] = items[k];
                    }

                    items[lo] = visualListViewItem;
                    lo++;
                    end_lo++;
                    start_hi++;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>Compare items using the compare direction.</summary>
        /// <param name="item1">The item 1.</param>
        /// <param name="item2">The item 2.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>The <see cref="bool" />.</returns>
        private bool CompareItems(VisualListViewItem item1, VisualListViewItem item2, CompareDirection direction)
        {
            bool dir = false;

            if (direction == CompareDirection.GreaterThan)
            {
                dir = true;
            }

            if (_sortDirection == SortDirections.Ascending)
            {
                dir = !dir; // flip it
            }

            if (!_numericCompare)
            {
                if (dir)
                {
                    return string.Compare(item1.SubItems[SortColumn].Text, item2.SubItems[SortColumn].Text, StringComparison.Ordinal) < 0;
                }
                else
                {
                    return string.Compare(item1.SubItems[SortColumn].Text, item2.SubItems[SortColumn].Text, StringComparison.Ordinal) > 0;
                }
            }
            else
            {
                try
                {
                    double n1 = double.Parse(item1.SubItems[SortColumn].Text);
                    double n2 = double.Parse(item2.SubItems[SortColumn].Text);

                    if (dir)
                    {
                        return n1 < n2;
                    }
                    else
                    {
                        return n1 > n2;
                    }
                }
                catch (Exception ex)
                {
                    // no numeric value (bad bad)
                    Debug.WriteLine(ex.ToString());
                    return false;
                }
            }
        }

        #endregion
    }
}