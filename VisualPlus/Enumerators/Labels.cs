#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: Labels.cs
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

#endregion License

#region Namespace

using System.Runtime.InteropServices;

#endregion Namespace

namespace VisualPlus.Enumerators
{
    /// <summary>The <see cref="Labels" /> enum.</summary>
    [ComVisible(true)]
    public enum Labels
    {
        /// <summary>The broadcast message.</summary>
        Announcement,

        /// <summary>Good issue to start with as a new contributor.</summary>
        Beginner,

        /// <summary>A bounty to earn the rewards offered for completion of the task.</summary>
        Bounty,

        /// <summary>A breaking change.</summary>
        BreakingChange,

        /// <summary>Software code error exception.</summary>
        Bug,

        /// <summary>This issue is closed because there are no plans to fix this.</summary>
        ClosedWontFix,

        /// <summary>This issue is closed because the code is working as a designed.</summary>
        ClosedByDesign,

        /// <summary>This issue is closed because it is a duplicate of another issue.</summary>
        ClosedDuplicate,

        /// <summary>This issue is closed because the work done to resolve it is complete.</summary>
        ClosedResolved,

        /// <summary>The process of finding and resolving defects or problems.</summary>
        Debug,

        /// <summary>Might need further analyzing to diagnose.</summary>
        Diagnosing,

        /// <summary>A need of a discussion.</summary>
        Discussion,

        /// <summary>Requests for issues with the documentation.</summary>
        Documentation,

        /// <summary>Improvements and other optimizations and quality of life automation..</summary>
        Enhancement,

        /// <summary>A distinctive service or benefit unique or superior.</summary>
        Feature,

        /// <summary>The GitHub service configurations.</summary>
        GitHub,

        /// <summary>Requesting help to contribute or resolve the issue. Submit a properly written PR to fix this.</summary>
        HelpWanted,

        /// <summary>Working on it.</summary>
        InProgress,

        /// <summary>Invalid intentions.</summary>
        Invalid,

        /// <summary>A known issue.</summary>
        KnownIssue,

        /// <summary>Please provide more information.</summary>
        NeedMoreInfo,

        /// <summary>The version is outdated.</summary>
        Outdated,

        /// <summary>The road map to future goals and plans.</summary>
        Planned,

        /// <summary>Research and development.</summary>
        Questions,

        /// <summary>Requires a possible code refactoring.</summary>
        Refactoring,

        /// <summary>Unable to reproduce the cause of the issue.</summary>
        Unreproducible,

        /// <summary>The user experience.</summary>
        UX,

        /// <summary>Contains changes to the wiki.</summary>
        Wiki
    }
}