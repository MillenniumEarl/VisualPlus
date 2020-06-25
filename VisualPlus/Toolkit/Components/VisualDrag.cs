namespace VisualPlus.Toolkit.Components
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using VisualPlus.Delegates;
    using VisualPlus.EventArgs;
    using VisualPlus.Localization;
    using VisualPlus.Localization.Descriptions;

    #endregion Namespace

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(VisualDrag), "VisualDrag.bmp")]
    [Description("The VisualDrag component enables controls to be draggable.")]
    public class VisualDrag : Component, ICloneable
    {
        private Control _control;
        private Cursor _cursorMove;
        private DragDirection _direction;
        private Point _lastPosition;

        /// <summary>Initializes a new instance of the <see cref="VisualDrag" /> class.</summary>
        /// <param name="container">The container.</param>
        public VisualDrag(IContainer container) : this()
        {
            container.Add(this);
        }

        /// <summary>Initializes a new instance of the <see cref="VisualDrag" /> class.</summary>
        /// <param name="control">The control to attach.</param>
        public VisualDrag(Control control) : this()
        {
            _control = control;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualDrag" /> class.</summary>
        /// <param name="control">The control to attach.</param>
        /// <param name="direction">Dragging enabled state.</param>
        public VisualDrag(Control control, DragDirection direction) : this()
        {
            _control = control;
            _direction = direction;

            if (Enabled)
            {
                AttachEvents();
            }
        }

        /// <summary>Initializes a new instance of the <see cref="VisualDrag" /> class.</summary>
        /// <param name="control">The control to attach.</param>
        /// <param name="direction">Dragging enabled state.</param>
        /// <param name="moveCursor">The move Cursor.</param>
        public VisualDrag(Control control, DragDirection direction, Cursor moveCursor)
        {
            _cursorMove = moveCursor;
            _control = control;
            _direction = direction;

            if (Enabled)
            {
                AttachEvents();
            }
        }

        /// <summary>Prevents a default instance of the <see cref="VisualDrag" /> class from being created.</summary>
        private VisualDrag()
        {
            _cursorMove = Cursors.SizeAll;
            _direction = DragDirection.Both;
        }

        [Category(Localization.Category.Events.DragDrop)]
        [Description(Event.ControlDragChanged)]
        public event ControlDragEventHandler ControlDrag;

        [Category(Localization.Category.Events.PropertyChanged)]
        [Description(Event.CursorChanged)]
        public event ControlDragCursorChangedEventHandler ControlDragCursorChanged;

        [Category(Localization.Category.Events.PropertyChanged)]
        [Description(Event.ControlDragToggleChanged)]
        public event ControlDragToggleEventHandler ControlDragToggle;

        public enum DragDirection
        {
            /// <summary>Both directions.</summary>
            Both,

            /// <summary>The horizontal direction.</summary>
            Horizontal,

            /// <summary>The vertical direction.</summary>
            Vertical,

            /// <summary>No directions.</summary>
            None
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category(PropertyCategory.Behavior)]
        [Description("The control to attach this component.")]
        public Control Control
        {
            get
            {
                return _control;
            }

            set
            {
                _control = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Cursor)]
        public Cursor CursorMove
        {
            get
            {
                return _cursorMove;
            }

            set
            {
                if ((_cursorMove == null) || (_cursorMove == value))
                {
                    return;
                }

                _cursorMove = value;
                OnControlDragCursorChanged(new CursorChangedEventArgs(_cursorMove));
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        public DragDirection Direction
        {
            get
            {
                return _direction;
            }

            set
            {
                _direction = value;

                if (_control == null)
                {
                    return;
                }

                if (_direction != DragDirection.None)
                {
                    AttachEvents();
                }
                else
                {
                    DetachEvents();
                }
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        public bool Enabled
        {
            get
            {
                return _direction != DragDirection.None;
            }
        }

        [Browsable(false)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.IsDragging)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsDragging { get; private set; }

        /// <summary>Hooks the drag events to the control.</summary>
        public void AttachEvents()
        {
            _control.MouseDown += ControlMouseDown;
            _control.MouseMove += ControlMouseMove;
            _control.MouseUp += ControlMouseUp;

            OnControlDragToggle(new ToggleEventArgs(Enabled));
        }

        /// <summary>Creates a copy of the current object.</summary>
        /// <returns>The <see cref="object" />.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Unhooks the drag events from the control.</summary>
        public void DetachEvents()
        {
            _control.MouseDown -= ControlMouseDown;
            _control.MouseMove -= ControlMouseMove;
            _control.MouseUp -= ControlMouseUp;

            OnControlDragToggle(new ToggleEventArgs(Enabled));
        }

        protected virtual void OnControlDrag(DragControlEventArgs e)
        {
            ControlDrag?.Invoke(e);
        }

        protected virtual void OnControlDragCursorChanged(CursorChangedEventArgs e)
        {
            ControlDragCursorChanged?.Invoke(e);
        }

        protected virtual void OnControlDragToggle(ToggleEventArgs e)
        {
            ControlDragToggle?.Invoke(e);
        }

        /// <summary>Control mouse down event.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ControlMouseDown(object sender, MouseEventArgs e)
        {
            if (!Enabled)
            {
                return;
            }

            _lastPosition = e.Location;
            _control.Cursor = CursorMove;
        }

        /// <summary>Control mouse move event.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ControlMouseMove(object sender, MouseEventArgs e)
        {
            if (!Enabled || (e.Button != MouseButtons.Left))
            {
                return;
            }

            switch (_direction)
            {
                case DragDirection.Both:
                    {
                        _control.Left += e.Location.X - _lastPosition.X;
                        _control.Top += e.Location.Y - _lastPosition.Y;
                        break;
                    }

                case DragDirection.Horizontal:
                    {
                        _control.Left += e.Location.X - _lastPosition.X;
                        break;
                    }

                case DragDirection.Vertical:
                    {
                        _control.Top += e.Location.Y - _lastPosition.Y;
                        break;
                    }

                case DragDirection.None:
                    {
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            IsDragging = true;
            OnControlDrag(new DragControlEventArgs(e.Location));
        }

        /// <summary>Control mouse up event.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ControlMouseUp(object sender, MouseEventArgs e)
        {
            if (Enabled)
            {
                _control.Cursor = Cursors.Default;
            }
        }
    }
}