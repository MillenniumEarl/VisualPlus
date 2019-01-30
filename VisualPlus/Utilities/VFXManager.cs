#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VFXManager.cs
// 
// Copyright (c) 2019 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using VisualPlus.Enumerators;

#endregion

namespace VisualPlus.Utilities
{
    [Description("The VFX manager.")]
    public class VFXManager
    {
        #region Constants

        private const double MaxValue = 1.00;
        private const double MinValue = 0.00;

        #endregion

        #region Fields

        private readonly List<AnimationDirection> _animationDirections;
        private readonly Timer _animationTimer;
        private readonly List<object[]> _effectsData;
        private readonly List<double> _effectsProgression;
        private readonly List<Point> _effectsSources;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VFXManager" /> class.Constructor</summary>
        /// <param name="singular">
        ///     If true, only one animation is supported. The current animation will be replaced with the new
        ///     one. If false, a new animation is added to the list.
        /// </param>
        public VFXManager(bool singular = true)
        {
            _effectsProgression = new List<double>();
            _effectsSources = new List<Point>();
            _animationDirections = new List<AnimationDirection>();
            _effectsData = new List<object[]>();
            _animationTimer = new Timer { Interval = 5, Enabled = false };
            Increment = 0.03;
            SecondaryIncrement = 0.03;
            EffectType = EffectType.Linear;
            CancelAnimation = true;
            Singular = singular;

            if (Singular)
            {
                _effectsProgression.Add(0);
                _effectsSources.Add(new Point(0, 0));
                _animationDirections.Add(AnimationDirection.In);
            }

            _animationTimer.Tick += AnimationTimerOnTick;
        }

        #endregion

        #region Delegates

        public delegate void AnimationFinished(object sender);

        public delegate void AnimationProgress(object sender);

        #endregion

        #region Public Events

        public event AnimationFinished OnAnimationFinished;

        public event AnimationProgress OnAnimationProgress;

        #endregion

        #region Public Properties

        public bool CancelAnimation { get; set; }

        public EffectType EffectType { get; set; }

        public double Increment { get; set; }

        public double SecondaryIncrement { get; set; }

        public bool Singular { get; set; }

        #endregion

        #region Public Methods and Operators

        public int GetAnimationCount()
        {
            return _effectsProgression.Count;
        }

        public object[] GetData()
        {
            if (!Singular)
            {
                throw new Exception("Animation is not set to Singular.");
            }

            if (_effectsData.Count == 0)
            {
                throw new Exception("Invalid animation");
            }

            return _effectsData[0];
        }

        public object[] GetData(int index)
        {
            if (!(index < _effectsData.Count))
            {
                throw new IndexOutOfRangeException("Invalid animation index");
            }

            return _effectsData[index];
        }

        public AnimationDirection GetDirection()
        {
            if (!Singular)
            {
                throw new Exception("Animation is not set to Singular.");
            }

            if (_animationDirections.Count == 0)
            {
                throw new Exception("Invalid animation");
            }

            return _animationDirections[0];
        }

        public AnimationDirection GetDirection(int index)
        {
            if (!(index < _animationDirections.Count))
            {
                throw new IndexOutOfRangeException("Invalid animation index");
            }

            return _animationDirections[index];
        }

        public double GetProgress()
        {
            if (!Singular)
            {
                throw new Exception("Animation is not set to Singular.");
            }

            if (_effectsProgression.Count == 0)
            {
                throw new Exception("Invalid animation");
            }

            return GetProgress(0);
        }

        public double GetProgress(int index)
        {
            if (!(index < GetAnimationCount()))
            {
                throw new IndexOutOfRangeException("Invalid animation index");
            }

            switch (EffectType)
            {
                case EffectType.Linear:
                    return AnimationLinear.CalculateProgress(_effectsProgression[index]);
                case EffectType.EaseInOut:
                    return AnimationEaseInOut.CalculateProgress(_effectsProgression[index]);
                case EffectType.EaseOut:
                    return AnimationEaseOut.CalculateProgress(_effectsProgression[index]);
                case EffectType.CustomQuadratic:
                    return AnimationCustomQuadratic.CalculateProgress(_effectsProgression[index]);
                default:
                    throw new NotImplementedException("The given EffectType is not implemented");
            }
        }

        public Point GetSource(int index)
        {
            if (!(index < GetAnimationCount()))
            {
                throw new IndexOutOfRangeException("Invalid animation index");
            }

            return _effectsSources[index];
        }

        public Point GetSource()
        {
            if (!Singular)
            {
                throw new Exception("Animation is not set to Singular.");
            }

            if (_effectsSources.Count == 0)
            {
                throw new Exception("Invalid animation");
            }

            return _effectsSources[0];
        }

        public bool IsAnimating()
        {
            return _animationTimer.Enabled;
        }

        public void SetData(object[] data)
        {
            if (!Singular)
            {
                throw new Exception("Animation is not set to Singular.");
            }

            if (_effectsData.Count == 0)
            {
                throw new Exception("Invalid animation");
            }

            _effectsData[0] = data;
        }

        public void SetDirection(AnimationDirection direction)
        {
            if (!Singular)
            {
                throw new Exception("Animation is not set to Singular.");
            }

            if (_effectsProgression.Count == 0)
            {
                throw new Exception("Invalid animation");
            }

            _animationDirections[0] = direction;
        }

        public void SetProgress(double progress)
        {
            if (!Singular)
            {
                throw new Exception("Animation is not set to Singular.");
            }

            if (_effectsProgression.Count == 0)
            {
                throw new Exception("Invalid animation");
            }

            _effectsProgression[0] = progress;
        }

        public void StartNewAnimation(AnimationDirection animationDirection, object[] data = null)
        {
            StartNewAnimation(animationDirection, new Point(0, 0), data);
        }

        public void StartNewAnimation(AnimationDirection animationDirection, Point animationSource, object[] data = null)
        {
            if (!IsAnimating() || CancelAnimation)
            {
                if (Singular && (_animationDirections.Count > 0))
                {
                    _animationDirections[0] = animationDirection;
                }
                else
                {
                    _animationDirections.Add(animationDirection);
                }

                if (Singular && (_effectsSources.Count > 0))
                {
                    _effectsSources[0] = animationSource;
                }
                else
                {
                    _effectsSources.Add(animationSource);
                }

                if (!(Singular && (_effectsProgression.Count > 0)))
                {
                    switch (_animationDirections[_animationDirections.Count - 1])
                    {
                        case AnimationDirection.InOutRepeatingIn:
                        case AnimationDirection.InOutIn:
                        case AnimationDirection.In:
                            _effectsProgression.Add(MinValue);
                            break;
                        case AnimationDirection.InOutRepeatingOut:
                        case AnimationDirection.InOutOut:
                        case AnimationDirection.Out:
                            _effectsProgression.Add(MaxValue);
                            break;
                        default:
                            throw new Exception("Invalid AnimationDirection");
                    }
                }

                if (Singular && (_effectsData.Count > 0))
                {
                    _effectsData[0] = data ?? new object[] { };
                }
                else
                {
                    _effectsData.Add(data ?? new object[] { });
                }
            }

            _animationTimer.Start();
        }

        public void UpdateProgress(int index)
        {
            switch (_animationDirections[index])
            {
                case AnimationDirection.InOutRepeatingIn:
                case AnimationDirection.InOutIn:
                case AnimationDirection.In:
                    IncrementProgress(index);
                    break;
                case AnimationDirection.InOutRepeatingOut:
                case AnimationDirection.InOutOut:
                case AnimationDirection.Out:
                    DecrementProgress(index);
                    break;
                default:
                    throw new Exception("No AnimationDirection has been set");
            }
        }

        #endregion

        #region Methods

        private void AnimationTimerOnTick(object sender, EventArgs eventArgs)
        {
            for (var i = 0; i < _effectsProgression.Count; i++)
            {
                UpdateProgress(i);

                if (!Singular)
                {
                    if ((_animationDirections[i] == AnimationDirection.InOutIn) && (_effectsProgression[i] == MaxValue))
                    {
                        _animationDirections[i] = AnimationDirection.InOutOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingIn) && (_effectsProgression[i] == MinValue))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingOut) && (_effectsProgression[i] == MinValue))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                    }
                    else if (((_animationDirections[i] == AnimationDirection.In) && (_effectsProgression[i] == MaxValue))
                             || ((_animationDirections[i] == AnimationDirection.Out) && (_effectsProgression[i] == MinValue))
                             || ((_animationDirections[i] == AnimationDirection.InOutOut) && (_effectsProgression[i] == MinValue)))
                    {
                        _effectsProgression.RemoveAt(i);
                        _effectsSources.RemoveAt(i);
                        _animationDirections.RemoveAt(i);
                        _effectsData.RemoveAt(i);
                    }
                }
                else
                {
                    if ((_animationDirections[i] == AnimationDirection.InOutIn) && (_effectsProgression[i] == MaxValue))
                    {
                        _animationDirections[i] = AnimationDirection.InOutOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingIn) && (_effectsProgression[i] == MaxValue))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingOut) && (_effectsProgression[i] == MinValue))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                    }
                }
            }

            OnAnimationProgress?.Invoke(this);
        }

        private void DecrementProgress(int index)
        {
            _effectsProgression[index] -= (_animationDirections[index] == AnimationDirection.InOutOut)
                                          || (_animationDirections[index] == AnimationDirection.InOutRepeatingOut)
                                              ? SecondaryIncrement
                                              : Increment;
            if (_effectsProgression[index] < MinValue)
            {
                _effectsProgression[index] = MinValue;

                for (var i = 0; i < GetAnimationCount(); i++)
                {
                    if (_animationDirections[i] == AnimationDirection.InOutIn)
                    {
                        return;
                    }

                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn)
                    {
                        return;
                    }

                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut)
                    {
                        return;
                    }

                    if ((_animationDirections[i] == AnimationDirection.InOutOut) && (_effectsProgression[i] != MinValue))
                    {
                        return;
                    }

                    if ((_animationDirections[i] == AnimationDirection.Out) && (_effectsProgression[i] != MinValue))
                    {
                        return;
                    }
                }

                _animationTimer.Stop();
                OnAnimationFinished?.Invoke(this);
            }
        }

        private void IncrementProgress(int index)
        {
            _effectsProgression[index] += Increment;
            if (_effectsProgression[index] > MaxValue)
            {
                _effectsProgression[index] = MaxValue;

                for (var i = 0; i < GetAnimationCount(); i++)
                {
                    if (_animationDirections[i] == AnimationDirection.InOutIn)
                    {
                        return;
                    }

                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn)
                    {
                        return;
                    }

                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut)
                    {
                        return;
                    }

                    if ((_animationDirections[i] == AnimationDirection.InOutOut) && (_effectsProgression[i] != MaxValue))
                    {
                        return;
                    }

                    if ((_animationDirections[i] == AnimationDirection.In) && (_effectsProgression[i] != MaxValue))
                    {
                        return;
                    }
                }

                _animationTimer.Stop();
                OnAnimationFinished?.Invoke(this);
            }
        }

        #endregion
    }

    public class AnimationLinear
    {
        #region Public Methods and Operators

        public static double CalculateProgress(double progress)
        {
            return progress;
        }

        #endregion
    }

    public class AnimationEaseInOut
    {
        #region Static Fields

        public static double Pi = Math.PI;
        public static double PiHalf = Math.PI / 2;

        #endregion

        #region Public Methods and Operators

        public static double CalculateProgress(double progress)
        {
            return EaseInOut(progress);
        }

        #endregion

        #region Methods

        private static double EaseInOut(double s)
        {
            return s - Math.Sin((s * 2 * Pi) / (2 * Pi));
        }

        #endregion
    }

    public class AnimationEaseOut
    {
        #region Public Methods and Operators

        public static double CalculateProgress(double progress)
        {
            return -1 * progress * (progress - 2);
        }

        #endregion
    }

    public class AnimationCustomQuadratic
    {
        #region Public Methods and Operators

        public static double CalculateProgress(double progress)
        {
            const double Boost = 0.6;
            return 1 - Math.Cos(((Math.Max(progress, Boost) - Boost) * Math.PI) / (2 - (2 * Boost)));
        }

        #endregion
    }
}