using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ArkRemoteAdmin
{
    internal abstract class ThemeContainer154 : ContainerControl
    {
        private bool hasShown;

        private void DoAnimation(bool i)
        {
            OnAnimation();
            if (i)
                Invalidate();
        }

        protected override sealed void OnPaint(PaintEventArgs e)
        {
            if (Width == 0 || Height == 0)
                return;

            if (transparent && controlMode)
            {
                PaintHook();
                e.Graphics.DrawImage(B, 0, 0);
            }
            else
            {
                G = e.Graphics;
                PaintHook();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ThemeShare.RemoveAnimationCallback(DoAnimation);
            base.OnHandleDestroyed(e);
        }

        private void FormShown(object sender, EventArgs e)
        {
            if (controlMode || hasShown)
                return;

            if (startPosition == FormStartPosition.CenterParent || startPosition == FormStartPosition.CenterScreen)
            {
                Rectangle sb = Screen.PrimaryScreen.Bounds;
                Rectangle cb = ParentForm.Bounds;
                ParentForm.Location = new Point(sb.Width / 2 - cb.Width / 2, sb.Height / 2 - cb.Width / 2);
            }

            hasShown = true;
        }

        #region " Initialization "

        protected Graphics G;

        protected Bitmap B;

        protected ThemeContainer154()
        {
            SetStyle((ControlStyles)139270, true);

            ImageSize = Size.Empty;
            Font = new Font("Verdana", 8);

            measureBitmap = new Bitmap(1, 1);
            measureGraphics = Graphics.FromImage(measureBitmap);

            drawRadialPath = new GraphicsPath();

            InvalidateCustimization();
        }

        protected override sealed void OnHandleCreated(EventArgs e)
        {
            if (doneCreation)
                InitializeMessages();

            InvalidateCustimization();
            ColorHook();

            if (lockWidth != 0)
                Width = lockWidth;
            if (lockHeight != 0)
                Height = lockHeight;
            if (!controlMode)
                base.Dock = DockStyle.Fill;

            Transparent = transparent;
            if (transparent && backColor)
                BackColor = Color.Transparent;

            base.OnHandleCreated(e);
        }

        private bool doneCreation;

        protected override sealed void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (Parent == null)
                return;
            IsParentForm = Parent is Form;

            if (!controlMode)
            {
                InitializeMessages();

                if (IsParentForm)
                {
                    ParentForm.FormBorderStyle = borderStyle;
                    ParentForm.TransparencyKey = transparencyKey;

                    if (!DesignMode)
                    {
                        ParentForm.Shown += FormShown;
                    }
                }

                Parent.BackColor = BackColor;
            }

            OnCreation();
            doneCreation = true;
            InvalidateTimer();
        }

        #endregion

        #region " Size Handling "

        private Rectangle frame;

        protected override sealed void OnSizeChanged(EventArgs e)
        {
            if (Movable && !controlMode)
            {
                frame = new Rectangle(7, 7, Width - 14, header - 7);
            }

            InvalidateBitmap();
            Invalidate();

            base.OnSizeChanged(e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (lockWidth != 0)
                width = lockWidth;
            if (lockHeight != 0)
                height = lockHeight;
            base.SetBoundsCore(x, y, width, height, specified);
        }

        #endregion

        #region " State Handling "

        protected MouseState State;

        private void SetState(MouseState current)
        {
            State = current;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!(IsParentForm && ParentForm.WindowState == FormWindowState.Maximized))
            {
                if (Sizable && !controlMode)
                    InvalidateMouse();
            }

            base.OnMouseMove(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            SetState(Enabled ? MouseState.None : MouseState.Block);
            base.OnEnabledChanged(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            SetState(MouseState.Over);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            SetState(MouseState.Over);
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            SetState(MouseState.None);

            if (GetChildAtPoint(PointToClient(MousePosition)) != null)
            {
                if (Sizable && !controlMode)
                {
                    Cursor = Cursors.Default;
                    previous = 0;
                }
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SetState(MouseState.Down);

            if (!(IsParentForm && ParentForm.WindowState == FormWindowState.Maximized || controlMode))
            {
                if (Movable && frame.Contains(e.Location))
                {
                    Capture = false;
                    wmLmbuttondown = true;
                    DefWndProc(ref messages[0]);
                }
                else if (Sizable && previous != 0)
                {
                    Capture = false;
                    wmLmbuttondown = true;
                    DefWndProc(ref messages[previous]);
                }
            }

            base.OnMouseDown(e);
        }

        private bool wmLmbuttondown;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (wmLmbuttondown && m.Msg == 513)
            {
                wmLmbuttondown = false;

                SetState(MouseState.Over);
                if (!SmartBounds)
                    return;

                CorrectBounds(IsParentMdi ? new Rectangle(Point.Empty, Parent.Parent.Size) : Screen.FromControl(Parent).WorkingArea);
            }
        }

        private Point getIndexPoint;
        private bool b1;
        private bool b2;
        private bool b3;
        private bool b4;

        private int GetIndex()
        {
            getIndexPoint = PointToClient(MousePosition);
            b1 = getIndexPoint.X < 7;
            b2 = getIndexPoint.X > Width - 7;
            b3 = getIndexPoint.Y < 7;
            b4 = getIndexPoint.Y > Height - 7;

            if (b1 && b3)
                return 4;
            if (b1 && b4)
                return 7;
            if (b2 && b3)
                return 5;
            if (b2 && b4)
                return 8;
            if (b1)
                return 1;
            if (b2)
                return 2;
            if (b3)
                return 3;
            if (b4)
                return 6;
            return 0;
        }

        private int current;
        private int previous;

        private void InvalidateMouse()
        {
            current = GetIndex();
            if (current == previous)
                return;

            previous = current;
            switch (previous)
            {
                case 0:
                    Cursor = Cursors.Default;
                    break;
                case 1:
                case 2:
                    Cursor = Cursors.SizeWE;
                    break;
                case 3:
                case 6:
                    Cursor = Cursors.SizeNS;
                    break;
                case 4:
                case 8:
                    Cursor = Cursors.SizeNWSE;
                    break;
                case 5:
                case 7:
                    Cursor = Cursors.SizeNESW;
                    break;
            }
        }

        private readonly Message[] messages = new Message[9];

        private void InitializeMessages()
        {
            messages[0] = Message.Create(Parent.Handle, 161, new IntPtr(2), IntPtr.Zero);
            for (int I = 1; I <= 8; I++)
            {
                messages[I] = Message.Create(Parent.Handle, 161, new IntPtr(I + 9), IntPtr.Zero);
            }
        }

        private void CorrectBounds(Rectangle bounds)
        {
            if (Parent.Width > bounds.Width)
                Parent.Width = bounds.Width;
            if (Parent.Height > bounds.Height)
                Parent.Height = bounds.Height;

            int x = Parent.Location.X;
            int y = Parent.Location.Y;

            if (x < bounds.X)
                x = bounds.X;
            if (y < bounds.Y)
                y = bounds.Y;

            int width = bounds.X + bounds.Width;
            int height = bounds.Y + bounds.Height;

            if (x + Parent.Width > width)
                x = width - Parent.Width;
            if (y + Parent.Height > height)
                y = height - Parent.Height;

            Parent.Location = new Point(x, y);
        }

        #endregion

        #region " Base Properties "

        public override DockStyle Dock
        {
            get { return base.Dock; }
            set
            {
                if (!controlMode)
                    return;
                base.Dock = value;
            }
        }

        private bool backColor;

        [Category("Misc")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                if (value == base.BackColor)
                    return;

                if (!IsHandleCreated && controlMode && value == Color.Transparent)
                {
                    backColor = true;
                    return;
                }

                base.BackColor = value;
                if (Parent != null)
                {
                    if (!controlMode)
                        Parent.BackColor = value;
                    ColorHook();
                }
            }
        }

        public override Size MinimumSize
        {
            get { return base.MinimumSize; }
            set
            {
                base.MinimumSize = value;
                if (Parent != null)
                    Parent.MinimumSize = value;
            }
        }

        public override Size MaximumSize
        {
            get { return base.MaximumSize; }
            set
            {
                base.MaximumSize = value;
                if (Parent != null)
                    Parent.MaximumSize = value;
            }
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        public override sealed Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                Invalidate();
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get { return Color.Empty; }
            set { }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage
        {
            get { return null; }
            set { }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ImageLayout BackgroundImageLayout
        {
            get { return ImageLayout.None; }
            set { }
        }

        #endregion

        #region " Public Properties "

        public bool SmartBounds { get; set; } = true;

        public bool Movable { get; set; } = true;

        public bool Sizable { get; set; } = true;

        private Color transparencyKey;

        public Color TransparencyKey
        {
            get
            {
                if (IsParentForm && !controlMode)
                    return ParentForm.TransparencyKey;
                return transparencyKey;
            }
            set
            {
                if (value == transparencyKey)
                    return;
                transparencyKey = value;

                if (IsParentForm && !controlMode)
                {
                    ParentForm.TransparencyKey = value;
                    ColorHook();
                }
            }
        }

        private FormBorderStyle borderStyle;

        public FormBorderStyle BorderStyle
        {
            get
            {
                if (IsParentForm && !controlMode)
                    return ParentForm.FormBorderStyle;
                return borderStyle;
            }
            set
            {
                borderStyle = value;

                if (IsParentForm && !controlMode)
                {
                    ParentForm.FormBorderStyle = value;

                    if (!(value == FormBorderStyle.None))
                    {
                        Movable = false;
                        Sizable = false;
                    }
                }
            }
        }

        private FormStartPosition startPosition;

        public FormStartPosition StartPosition
        {
            get
            {
                if (IsParentForm && !controlMode)
                    return ParentForm.StartPosition;
                return startPosition;
            }
            set
            {
                startPosition = value;

                if (IsParentForm && !controlMode)
                {
                    ParentForm.StartPosition = value;
                }
            }
        }

        private bool noRounding;

        public bool NoRounding
        {
            get { return noRounding; }
            set
            {
                noRounding = value;
                Invalidate();
            }
        }

        private Image image;

        public Image Image
        {
            get { return image; }
            set
            {
                if (value == null)
                    ImageSize = Size.Empty;
                else
                    ImageSize = value.Size;

                image = value;
                Invalidate();
            }
        }

        private readonly Dictionary<string, Color> items = new Dictionary<string, Color>();

        public Bloom[] Colors
        {
            get
            {
                List<Bloom> T = new List<Bloom>();
                Dictionary<string, Color>.Enumerator e = items.GetEnumerator();

                while (e.MoveNext())
                {
                    T.Add(new Bloom(e.Current.Key, e.Current.Value));
                }

                return T.ToArray();
            }
            set
            {
                foreach (Bloom b in value)
                {
                    if (items.ContainsKey(b.Name))
                        items[b.Name] = b.Value;
                }

                InvalidateCustimization();
                ColorHook();
                Invalidate();
            }
        }

        private string customization;

        public string Customization
        {
            get { return customization; }
            set
            {
                if (value == customization)
                    return;

                byte[] data = null;
                Bloom[] items = Colors;

                try
                {
                    data = Convert.FromBase64String(value);
                    for (int I = 0; I <= items.Length - 1; I++)
                    {
                        items[I].Value = Color.FromArgb(BitConverter.ToInt32(data, I * 4));
                    }
                }
                catch
                {
                    return;
                }

                customization = value;

                Colors = items;
                ColorHook();
                Invalidate();
            }
        }

        private bool transparent;

        public bool Transparent
        {
            get { return transparent; }
            set
            {
                transparent = value;
                if (!(IsHandleCreated || controlMode))
                    return;

                if (!value && !(BackColor.A == 255))
                {
                    throw new Exception("Unable to change value to false while a transparent BackColor is in use.");
                }

                SetStyle(ControlStyles.Opaque, !value);
                SetStyle(ControlStyles.SupportsTransparentBackColor, value);

                InvalidateBitmap();
                Invalidate();
            }
        }

        #endregion

        #region " Private Properties "

        protected Size ImageSize { get; private set; }

        protected bool IsParentForm { get; private set; }

        protected bool IsParentMdi
        {
            get
            {
                if (Parent == null)
                    return false;
                return Parent.Parent != null;
            }
        }

        private int lockWidth;

        protected int LockWidth
        {
            get { return lockWidth; }
            set
            {
                lockWidth = value;
                if (!(LockWidth == 0) && IsHandleCreated)
                    Width = LockWidth;
            }
        }

        private int lockHeight;

        protected int LockHeight
        {
            get { return lockHeight; }
            set
            {
                lockHeight = value;
                if (!(LockHeight == 0) && IsHandleCreated)
                    Height = LockHeight;
            }
        }

        private int header = 24;

        protected int Header
        {
            get { return header; }
            set
            {
                header = value;

                if (!controlMode)
                {
                    frame = new Rectangle(7, 7, Width - 14, value - 7);
                    Invalidate();
                }
            }
        }

        private bool controlMode;

        protected bool ControlMode
        {
            get { return controlMode; }
            set
            {
                controlMode = value;

                Transparent = transparent;
                if (transparent && backColor)
                    BackColor = Color.Transparent;

                InvalidateBitmap();
                Invalidate();
            }
        }

        private bool isAnimated;

        protected bool IsAnimated
        {
            get { return isAnimated; }
            set
            {
                isAnimated = value;
                InvalidateTimer();
            }
        }

        #endregion

        #region " Property Helpers "

        protected Pen GetPen(string name)
        {
            return new Pen(items[name]);
        }

        protected Pen GetPen(string name, float width)
        {
            return new Pen(items[name], width);
        }

        protected SolidBrush GetBrush(string name)
        {
            return new SolidBrush(items[name]);
        }

        protected Color GetColor(string name)
        {
            return items[name];
        }

        protected void SetColor(string name, Color value)
        {
            if (items.ContainsKey(name))
                items[name] = value;
            else
                items.Add(name, value);
        }

        protected void SetColor(string name, byte r, byte g, byte b)
        {
            SetColor(name, Color.FromArgb(r, g, b));
        }

        protected void SetColor(string name, byte a, byte r, byte g, byte b)
        {
            SetColor(name, Color.FromArgb(a, r, g, b));
        }

        protected void SetColor(string name, byte a, Color value)
        {
            SetColor(name, Color.FromArgb(a, value));
        }

        private void InvalidateBitmap()
        {
            if (transparent && controlMode)
            {
                if (Width == 0 || Height == 0)
                    return;
                B = new Bitmap(Width, Height, PixelFormat.Format32bppPArgb);
                G = Graphics.FromImage(B);
            }
            else
            {
                G = null;
                B = null;
            }
        }

        private void InvalidateCustimization()
        {
            MemoryStream m = new MemoryStream(items.Count * 4);

            foreach (Bloom b in Colors)
            {
                m.Write(BitConverter.GetBytes(b.Value.ToArgb()), 0, 4);
            }

            m.Close();
            customization = Convert.ToBase64String(m.ToArray());
        }

        private void InvalidateTimer()
        {
            if (DesignMode || !doneCreation)
                return;

            if (isAnimated)
            {
                ThemeShare.AddAnimationCallback(DoAnimation);
            }
            else
            {
                ThemeShare.RemoveAnimationCallback(DoAnimation);
            }
        }

        #endregion

        #region " User Hooks "

        protected abstract void ColorHook();
        protected abstract void PaintHook();

        protected virtual void OnCreation()
        {
        }

        protected virtual void OnAnimation()
        {
        }

        #endregion

        #region " Offset "

        private Rectangle offsetReturnRectangle;

        protected Rectangle Offset(Rectangle r, int amount)
        {
            offsetReturnRectangle = new Rectangle(r.X + amount, r.Y + amount, r.Width - (amount * 2), r.Height - (amount * 2));
            return offsetReturnRectangle;
        }

        private Size offsetReturnSize;

        protected Size Offset(Size s, int amount)
        {
            offsetReturnSize = new Size(s.Width + amount, s.Height + amount);
            return offsetReturnSize;
        }

        private Point offsetReturnPoint;

        protected Point Offset(Point p, int amount)
        {
            offsetReturnPoint = new Point(p.X + amount, p.Y + amount);
            return offsetReturnPoint;
        }

        #endregion

        #region " Center "

        private Point centerReturn;

        protected Point Center(Rectangle p, Rectangle c)
        {
            centerReturn = new Point((p.Width / 2 - c.Width / 2) + p.X + c.X, (p.Height / 2 - c.Height / 2) + p.Y + c.Y);
            return centerReturn;
        }

        protected Point Center(Rectangle p, Size c)
        {
            centerReturn = new Point((p.Width / 2 - c.Width / 2) + p.X, (p.Height / 2 - c.Height / 2) + p.Y);
            return centerReturn;
        }

        protected Point Center(Rectangle child)
        {
            return Center(Width, Height, child.Width, child.Height);
        }

        protected Point Center(Size child)
        {
            return Center(Width, Height, child.Width, child.Height);
        }

        protected Point Center(int childWidth, int childHeight)
        {
            return Center(Width, Height, childWidth, childHeight);
        }

        protected Point Center(Size p, Size c)
        {
            return Center(p.Width, p.Height, c.Width, c.Height);
        }

        protected Point Center(int pWidth, int pHeight, int cWidth, int cHeight)
        {
            centerReturn = new Point(pWidth / 2 - cWidth / 2, pHeight / 2 - cHeight / 2);
            return centerReturn;
        }

        #endregion

        #region " Measure "

        private readonly Bitmap measureBitmap;

        private readonly Graphics measureGraphics;

        protected Size Measure()
        {
            lock (measureGraphics)
            {
                return measureGraphics.MeasureString(Text, Font, Width).ToSize();
            }
        }

        protected Size Measure(string text)
        {
            lock (measureGraphics)
            {
                return measureGraphics.MeasureString(text, Font, Width).ToSize();
            }
        }

        #endregion

        #region " DrawPixel "

        private SolidBrush drawPixelBrush;

        protected void DrawPixel(Color c1, int x, int y)
        {
            if (transparent)
            {
                B.SetPixel(x, y, c1);
            }
            else
            {
                drawPixelBrush = new SolidBrush(c1);
                G.FillRectangle(drawPixelBrush, x, y, 1, 1);
            }
        }

        #endregion

        #region " DrawCorners "

        private SolidBrush drawCornersBrush;

        protected void DrawCorners(Color c1, int offset)
        {
            DrawCorners(c1, 0, 0, Width, Height, offset);
        }

        protected void DrawCorners(Color c1, Rectangle r1, int offset)
        {
            DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset);
        }

        protected void DrawCorners(Color c1, int x, int y, int width, int height, int offset)
        {
            DrawCorners(c1, x + offset, y + offset, width - (offset * 2), height - (offset * 2));
        }

        protected void DrawCorners(Color c1)
        {
            DrawCorners(c1, 0, 0, Width, Height);
        }

        protected void DrawCorners(Color c1, Rectangle r1)
        {
            DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height);
        }

        protected void DrawCorners(Color c1, int x, int y, int width, int height)
        {
            if (noRounding)
                return;

            if (transparent)
            {
                B.SetPixel(x, y, c1);
                B.SetPixel(x + (width - 1), y, c1);
                B.SetPixel(x, y + (height - 1), c1);
                B.SetPixel(x + (width - 1), y + (height - 1), c1);
            }
            else
            {
                drawCornersBrush = new SolidBrush(c1);
                G.FillRectangle(drawCornersBrush, x, y, 1, 1);
                G.FillRectangle(drawCornersBrush, x + (width - 1), y, 1, 1);
                G.FillRectangle(drawCornersBrush, x, y + (height - 1), 1, 1);
                G.FillRectangle(drawCornersBrush, x + (width - 1), y + (height - 1), 1, 1);
            }
        }

        #endregion

        #region " DrawBorders "

        protected void DrawBorders(Pen p1, int offset)
        {
            DrawBorders(p1, 0, 0, Width, Height, offset);
        }

        protected void DrawBorders(Pen p1, Rectangle r, int offset)
        {
            DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset);
        }

        protected void DrawBorders(Pen p1, int x, int y, int width, int height, int offset)
        {
            DrawBorders(p1, x + offset, y + offset, width - (offset * 2), height - (offset * 2));
        }

        protected void DrawBorders(Pen p1)
        {
            DrawBorders(p1, 0, 0, Width, Height);
        }

        protected void DrawBorders(Pen p1, Rectangle r)
        {
            DrawBorders(p1, r.X, r.Y, r.Width, r.Height);
        }

        protected void DrawBorders(Pen p1, int x, int y, int width, int height)
        {
            G.DrawRectangle(p1, x, y, width - 1, height - 1);
        }

        #endregion

        #region " DrawText "

        private Point drawTextPoint;

        private Size drawTextSize;

        protected void DrawText(Brush b1, HorizontalAlignment a, int x, int y)
        {
            DrawText(b1, Text, a, x, y);
        }

        protected void DrawText(Brush b1, string text, HorizontalAlignment a, int x, int y)
        {
            if (text.Length == 0)
                return;

            drawTextSize = Measure(text);
            drawTextPoint = new Point(Width / 2 - drawTextSize.Width / 2, Header / 2 - drawTextSize.Height / 2);

            switch (a)
            {
                case HorizontalAlignment.Left:
                    G.DrawString(text, Font, b1, x, drawTextPoint.Y + y);
                    break;
                case HorizontalAlignment.Center:
                    G.DrawString(text, Font, b1, drawTextPoint.X + x, drawTextPoint.Y + y);
                    break;
                case HorizontalAlignment.Right:
                    G.DrawString(text, Font, b1, Width - drawTextSize.Width - x, drawTextPoint.Y + y);
                    break;
            }
        }

        protected void DrawText(Brush b1, Point p1)
        {
            if (Text.Length == 0)
                return;
            G.DrawString(Text, Font, b1, p1);
        }

        protected void DrawText(Brush b1, int x, int y)
        {
            if (Text.Length == 0)
                return;
            G.DrawString(Text, Font, b1, x, y);
        }

        #endregion

        #region " DrawImage "

        private Point drawImagePoint;

        protected void DrawImage(HorizontalAlignment a, int x, int y)
        {
            DrawImage(image, a, x, y);
        }

        protected void DrawImage(Image image, HorizontalAlignment a, int x, int y)
        {
            if (image == null)
                return;
            drawImagePoint = new Point(Width / 2 - image.Width / 2, Header / 2 - image.Height / 2);

            switch (a)
            {
                case HorizontalAlignment.Left:
                    G.DrawImage(image, x, drawImagePoint.Y + y, image.Width, image.Height);
                    break;
                case HorizontalAlignment.Center:
                    G.DrawImage(image, drawImagePoint.X + x, drawImagePoint.Y + y, image.Width, image.Height);
                    break;
                case HorizontalAlignment.Right:
                    G.DrawImage(image, Width - image.Width - x, drawImagePoint.Y + y, image.Width, image.Height);
                    break;
            }
        }

        protected void DrawImage(Point p1)
        {
            DrawImage(image, p1.X, p1.Y);
        }

        protected void DrawImage(int x, int y)
        {
            DrawImage(image, x, y);
        }

        protected void DrawImage(Image image, Point p1)
        {
            DrawImage(image, p1.X, p1.Y);
        }

        protected void DrawImage(Image image, int x, int y)
        {
            if (image == null)
                return;
            G.DrawImage(image, x, y, image.Width, image.Height);
        }

        #endregion

        #region " DrawGradient "

        private LinearGradientBrush drawGradientBrush;

        private Rectangle drawGradientRectangle;

        protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height)
        {
            drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(blend, drawGradientRectangle);
        }

        protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height, float angle)
        {
            drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(blend, drawGradientRectangle, angle);
        }

        protected void DrawGradient(ColorBlend blend, Rectangle r)
        {
            drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, 90f);
            drawGradientBrush.InterpolationColors = blend;
            G.FillRectangle(drawGradientBrush, r);
        }

        protected void DrawGradient(ColorBlend blend, Rectangle r, float angle)
        {
            drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, angle);
            drawGradientBrush.InterpolationColors = blend;
            G.FillRectangle(drawGradientBrush, r);
        }


        protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height)
        {
            drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(c1, c2, drawGradientRectangle);
        }

        protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height, float angle)
        {
            drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(c1, c2, drawGradientRectangle, angle);
        }

        protected void DrawGradient(Color c1, Color c2, Rectangle r)
        {
            drawGradientBrush = new LinearGradientBrush(r, c1, c2, 90f);
            G.FillRectangle(drawGradientBrush, r);
        }

        protected void DrawGradient(Color c1, Color c2, Rectangle r, float angle)
        {
            drawGradientBrush = new LinearGradientBrush(r, c1, c2, angle);
            G.FillRectangle(drawGradientBrush, r);
        }

        #endregion

        #region " DrawRadial "

        private readonly GraphicsPath drawRadialPath;
        private PathGradientBrush drawRadialBrush1;
        private LinearGradientBrush drawRadialBrush2;

        private Rectangle drawRadialRectangle;

        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height)
        {
            drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, drawRadialRectangle, width / 2, height / 2);
        }

        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, Point center)
        {
            drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, drawRadialRectangle, center.X, center.Y);
        }

        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, int cx, int cy)
        {
            drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, drawRadialRectangle, cx, cy);
        }

        public void DrawRadial(ColorBlend blend, Rectangle r)
        {
            DrawRadial(blend, r, r.Width / 2, r.Height / 2);
        }

        public void DrawRadial(ColorBlend blend, Rectangle r, Point center)
        {
            DrawRadial(blend, r, center.X, center.Y);
        }

        public void DrawRadial(ColorBlend blend, Rectangle r, int cx, int cy)
        {
            drawRadialPath.Reset();
            drawRadialPath.AddEllipse(r.X, r.Y, r.Width - 1, r.Height - 1);

            drawRadialBrush1 = new PathGradientBrush(drawRadialPath);
            drawRadialBrush1.CenterPoint = new Point(r.X + cx, r.Y + cy);
            drawRadialBrush1.InterpolationColors = blend;

            if (G.SmoothingMode == SmoothingMode.AntiAlias)
            {
                G.FillEllipse(drawRadialBrush1, r.X + 1, r.Y + 1, r.Width - 3, r.Height - 3);
            }
            else
            {
                G.FillEllipse(drawRadialBrush1, r);
            }
        }


        protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height)
        {
            drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(c1, c2, drawGradientRectangle);
        }

        protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height, float angle)
        {
            drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(c1, c2, drawGradientRectangle, angle);
        }

        protected void DrawRadial(Color c1, Color c2, Rectangle r)
        {
            drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, 90f);
            G.FillRectangle(drawGradientBrush, r);
        }

        protected void DrawRadial(Color c1, Color c2, Rectangle r, float angle)
        {
            drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, angle);
            G.FillEllipse(drawGradientBrush, r);
        }

        #endregion

        #region " CreateRound "

        private GraphicsPath createRoundPath;

        private Rectangle createRoundRectangle;

        public GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
        {
            createRoundRectangle = new Rectangle(x, y, width, height);
            return CreateRound(createRoundRectangle, slope);
        }

        public GraphicsPath CreateRound(Rectangle r, int slope)
        {
            createRoundPath = new GraphicsPath(FillMode.Winding);
            createRoundPath.AddArc(r.X, r.Y, slope, slope, 180f, 90f);
            createRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270f, 90f);
            createRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0f, 90f);
            createRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90f, 90f);
            createRoundPath.CloseFigure();
            return createRoundPath;
        }

        #endregion
    }

    internal abstract class ThemeControl154 : Control
    {
        private void DoAnimation(bool i)
        {
            OnAnimation();
            if (i)
                Invalidate();
        }

        protected override sealed void OnPaint(PaintEventArgs e)
        {
            if (Width == 0 || Height == 0)
                return;

            if (transparent)
            {
                PaintHook();
                e.Graphics.DrawImage(B, 0, 0);
            }
            else
            {
                G = e.Graphics;
                PaintHook();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ThemeShare.RemoveAnimationCallback(DoAnimation);
            base.OnHandleDestroyed(e);
        }

        #region " Initialization "

        protected Graphics G;

        protected Bitmap B;

        public ThemeControl154()
        {
            SetStyle((ControlStyles)139270, true);

            ImageSize = Size.Empty;
            Font = new Font("Verdana", 8);

            measureBitmap = new Bitmap(1, 1);
            measureGraphics = Graphics.FromImage(measureBitmap);

            drawRadialPath = new GraphicsPath();

            InvalidateCustimization();
            //Remove?
        }

        protected override sealed void OnHandleCreated(EventArgs e)
        {
            InvalidateCustimization();
            ColorHook();

            if (!(lockWidth == 0))
                Width = lockWidth;
            if (!(lockHeight == 0))
                Height = lockHeight;

            Transparent = transparent;
            if (transparent && backColor)
                BackColor = Color.Transparent;

            base.OnHandleCreated(e);
        }

        private bool doneCreation;

        protected override sealed void OnParentChanged(EventArgs e)
        {
            if (Parent != null)
            {
                OnCreation();
                doneCreation = true;
                InvalidateTimer();
            }

            base.OnParentChanged(e);
        }

        #endregion

        #region " Size Handling "

        protected override sealed void OnSizeChanged(EventArgs e)
        {
            if (transparent)
            {
                InvalidateBitmap();
            }

            Invalidate();
            base.OnSizeChanged(e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (!(lockWidth == 0))
                width = lockWidth;
            if (!(lockHeight == 0))
                height = lockHeight;
            base.SetBoundsCore(x, y, width, height, specified);
        }

        #endregion

        #region " State Handling "

        private bool inPosition;

        protected override void OnMouseEnter(EventArgs e)
        {
            inPosition = true;
            SetState(MouseState.Over);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (inPosition)
                SetState(MouseState.Over);
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SetState(MouseState.Down);
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            inPosition = false;
            SetState(MouseState.None);
            base.OnMouseLeave(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
                SetState(MouseState.None);
            else
                SetState(MouseState.Block);
            base.OnEnabledChanged(e);
        }

        protected MouseState State;

        private void SetState(MouseState current)
        {
            State = current;
            Invalidate();
        }

        #endregion

        #region " Base Properties "

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get { return Color.Empty; }
            set { }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage
        {
            get { return null; }
            set { }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ImageLayout BackgroundImageLayout
        {
            get { return ImageLayout.None; }
            set { }
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                Invalidate();
            }
        }

        private bool backColor;

        [Category("Misc")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                if (!IsHandleCreated && value == Color.Transparent)
                {
                    backColor = true;
                    return;
                }

                base.BackColor = value;
                if (Parent != null)
                    ColorHook();
            }
        }

        #endregion

        #region " Public Properties "

        private bool noRounding;

        public bool NoRounding
        {
            get { return noRounding; }
            set
            {
                noRounding = value;
                Invalidate();
            }
        }

        private Image image;

        public Image Image
        {
            get { return image; }
            set
            {
                if (value == null)
                {
                    ImageSize = Size.Empty;
                }
                else
                {
                    ImageSize = value.Size;
                }

                image = value;
                Invalidate();
            }
        }

        private bool transparent;

        public bool Transparent
        {
            get { return transparent; }
            set
            {
                transparent = value;
                if (!IsHandleCreated)
                    return;

                if (!value && !(BackColor.A == 255))
                {
                    throw new Exception("Unable to change value to false while a transparent BackColor is in use.");
                }

                SetStyle(ControlStyles.Opaque, !value);
                SetStyle(ControlStyles.SupportsTransparentBackColor, value);

                if (value)
                    InvalidateBitmap();
                else
                    B = null;
                Invalidate();
            }
        }

        private readonly Dictionary<string, Color> items = new Dictionary<string, Color>();

        public Bloom[] Colors
        {
            get
            {
                List<Bloom> T = new List<Bloom>();
                Dictionary<string, Color>.Enumerator e = items.GetEnumerator();

                while (e.MoveNext())
                {
                    T.Add(new Bloom(e.Current.Key, e.Current.Value));
                }

                return T.ToArray();
            }
            set
            {
                foreach (Bloom b in value)
                {
                    if (items.ContainsKey(b.Name))
                        items[b.Name] = b.Value;
                }

                InvalidateCustimization();
                ColorHook();
                Invalidate();
            }
        }

        private string customization;

        public string Customization
        {
            get { return customization; }
            set
            {
                if (value == customization)
                    return;

                byte[] data = null;
                Bloom[] items = Colors;

                try
                {
                    data = Convert.FromBase64String(value);
                    for (int I = 0; I <= items.Length - 1; I++)
                    {
                        items[I].Value = Color.FromArgb(BitConverter.ToInt32(data, I * 4));
                    }
                }
                catch
                {
                    return;
                }

                customization = value;

                Colors = items;
                ColorHook();
                Invalidate();
            }
        }

        #endregion

        #region " Private Properties "

        protected Size ImageSize { get; private set; }

        private int lockWidth;

        protected int LockWidth
        {
            get { return lockWidth; }
            set
            {
                lockWidth = value;
                if (!(LockWidth == 0) && IsHandleCreated)
                    Width = LockWidth;
            }
        }

        private int lockHeight;

        protected int LockHeight
        {
            get { return lockHeight; }
            set
            {
                lockHeight = value;
                if (!(LockHeight == 0) && IsHandleCreated)
                    Height = LockHeight;
            }
        }

        private bool isAnimated;

        protected bool IsAnimated
        {
            get { return isAnimated; }
            set
            {
                isAnimated = value;
                InvalidateTimer();
            }
        }

        #endregion

        #region " Property Helpers "

        protected Pen GetPen(string name)
        {
            return new Pen(items[name]);
        }

        protected Pen GetPen(string name, float width)
        {
            return new Pen(items[name], width);
        }

        protected SolidBrush GetBrush(string name)
        {
            return new SolidBrush(items[name]);
        }

        protected Color GetColor(string name)
        {
            return items[name];
        }

        protected void SetColor(string name, Color value)
        {
            if (items.ContainsKey(name))
                items[name] = value;
            else
                items.Add(name, value);
        }

        protected void SetColor(string name, byte r, byte g, byte b)
        {
            SetColor(name, Color.FromArgb(r, g, b));
        }

        protected void SetColor(string name, byte a, byte r, byte g, byte b)
        {
            SetColor(name, Color.FromArgb(a, r, g, b));
        }

        protected void SetColor(string name, byte a, Color value)
        {
            SetColor(name, Color.FromArgb(a, value));
        }

        private void InvalidateBitmap()
        {
            if (Width == 0 || Height == 0)
                return;
            B = new Bitmap(Width, Height, PixelFormat.Format32bppPArgb);
            G = Graphics.FromImage(B);
        }

        private void InvalidateCustimization()
        {
            MemoryStream m = new MemoryStream(items.Count * 4);

            foreach (Bloom b in Colors)
            {
                m.Write(BitConverter.GetBytes(b.Value.ToArgb()), 0, 4);
            }

            m.Close();
            customization = Convert.ToBase64String(m.ToArray());
        }

        private void InvalidateTimer()
        {
            if (DesignMode || !doneCreation)
                return;

            if (isAnimated)
            {
                ThemeShare.AddAnimationCallback(DoAnimation);
            }
            else
            {
                ThemeShare.RemoveAnimationCallback(DoAnimation);
            }
        }

        #endregion

        #region " User Hooks "

        protected abstract void ColorHook();
        protected abstract void PaintHook();

        protected virtual void OnCreation()
        {
        }

        protected virtual void OnAnimation()
        {
        }

        #endregion

        #region " Offset "

        private Rectangle offsetReturnRectangle;

        protected Rectangle Offset(Rectangle r, int amount)
        {
            offsetReturnRectangle = new Rectangle(r.X + amount, r.Y + amount, r.Width - (amount * 2), r.Height - (amount * 2));
            return offsetReturnRectangle;
        }

        private Size offsetReturnSize;

        protected Size Offset(Size s, int amount)
        {
            offsetReturnSize = new Size(s.Width + amount, s.Height + amount);
            return offsetReturnSize;
        }

        private Point offsetReturnPoint;

        protected Point Offset(Point p, int amount)
        {
            offsetReturnPoint = new Point(p.X + amount, p.Y + amount);
            return offsetReturnPoint;
        }

        #endregion

        #region " Center "

        private Point centerReturn;

        protected Point Center(Rectangle p, Rectangle c)
        {
            centerReturn = new Point((p.Width / 2 - c.Width / 2) + p.X + c.X, (p.Height / 2 - c.Height / 2) + p.Y + c.Y);
            return centerReturn;
        }

        protected Point Center(Rectangle p, Size c)
        {
            centerReturn = new Point((p.Width / 2 - c.Width / 2) + p.X, (p.Height / 2 - c.Height / 2) + p.Y);
            return centerReturn;
        }

        protected Point Center(Rectangle child)
        {
            return Center(Width, Height, child.Width, child.Height);
        }

        protected Point Center(Size child)
        {
            return Center(Width, Height, child.Width, child.Height);
        }

        protected Point Center(int childWidth, int childHeight)
        {
            return Center(Width, Height, childWidth, childHeight);
        }

        protected Point Center(Size p, Size c)
        {
            return Center(p.Width, p.Height, c.Width, c.Height);
        }

        protected Point Center(int pWidth, int pHeight, int cWidth, int cHeight)
        {
            centerReturn = new Point(pWidth / 2 - cWidth / 2, pHeight / 2 - cHeight / 2);
            return centerReturn;
        }

        #endregion

        #region " Measure "

        private readonly Bitmap measureBitmap;
        //TODO: Potential issues during multi-threading.
        private readonly Graphics measureGraphics;

        protected Size Measure()
        {
            return measureGraphics.MeasureString(Text, Font, Width).ToSize();
        }

        protected Size Measure(string text)
        {
            return measureGraphics.MeasureString(text, Font, Width).ToSize();
        }

        #endregion

        #region " DrawPixel "

        private SolidBrush drawPixelBrush;

        protected void DrawPixel(Color c1, int x, int y)
        {
            if (transparent)
            {
                B.SetPixel(x, y, c1);
            }
            else
            {
                drawPixelBrush = new SolidBrush(c1);
                G.FillRectangle(drawPixelBrush, x, y, 1, 1);
            }
        }

        #endregion

        #region " DrawCorners "

        private SolidBrush drawCornersBrush;

        protected void DrawCorners(Color c1, int offset)
        {
            DrawCorners(c1, 0, 0, Width, Height, offset);
        }

        protected void DrawCorners(Color c1, Rectangle r1, int offset)
        {
            DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset);
        }

        protected void DrawCorners(Color c1, int x, int y, int width, int height, int offset)
        {
            DrawCorners(c1, x + offset, y + offset, width - (offset * 2), height - (offset * 2));
        }

        protected void DrawCorners(Color c1)
        {
            DrawCorners(c1, 0, 0, Width, Height);
        }

        protected void DrawCorners(Color c1, Rectangle r1)
        {
            DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height);
        }

        protected void DrawCorners(Color c1, int x, int y, int width, int height)
        {
            if (noRounding)
                return;

            if (transparent)
            {
                B.SetPixel(x, y, c1);
                B.SetPixel(x + (width - 1), y, c1);
                B.SetPixel(x, y + (height - 1), c1);
                B.SetPixel(x + (width - 1), y + (height - 1), c1);
            }
            else
            {
                drawCornersBrush = new SolidBrush(c1);
                G.FillRectangle(drawCornersBrush, x, y, 1, 1);
                G.FillRectangle(drawCornersBrush, x + (width - 1), y, 1, 1);
                G.FillRectangle(drawCornersBrush, x, y + (height - 1), 1, 1);
                G.FillRectangle(drawCornersBrush, x + (width - 1), y + (height - 1), 1, 1);
            }
        }

        #endregion

        #region " DrawBorders "

        protected void DrawBorders(Pen p1, int offset)
        {
            DrawBorders(p1, 0, 0, Width, Height, offset);
        }

        protected void DrawBorders(Pen p1, Rectangle r, int offset)
        {
            DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset);
        }

        protected void DrawBorders(Pen p1, int x, int y, int width, int height, int offset)
        {
            DrawBorders(p1, x + offset, y + offset, width - (offset * 2), height - (offset * 2));
        }

        protected void DrawBorders(Pen p1)
        {
            DrawBorders(p1, 0, 0, Width, Height);
        }

        protected void DrawBorders(Pen p1, Rectangle r)
        {
            DrawBorders(p1, r.X, r.Y, r.Width, r.Height);
        }

        protected void DrawBorders(Pen p1, int x, int y, int width, int height)
        {
            G.DrawRectangle(p1, x, y, width - 1, height - 1);
        }

        #endregion

        #region " DrawText "

        private Point drawTextPoint;

        private Size drawTextSize;

        protected void DrawText(Brush b1, HorizontalAlignment a, int x, int y)
        {
            DrawText(b1, Text, a, x, y);
        }

        protected void DrawText(Brush b1, string text, HorizontalAlignment a, int x, int y)
        {
            if (text.Length == 0)
                return;

            drawTextSize = Measure(text);
            drawTextPoint = Center(drawTextSize);

            switch (a)
            {
                case HorizontalAlignment.Left:
                    G.DrawString(text, Font, b1, x, drawTextPoint.Y + y);
                    break;
                case HorizontalAlignment.Center:
                    G.DrawString(text, Font, b1, drawTextPoint.X + x, drawTextPoint.Y + y);
                    break;
                case HorizontalAlignment.Right:
                    G.DrawString(text, Font, b1, Width - drawTextSize.Width - x, drawTextPoint.Y + y);
                    break;
            }
        }

        protected void DrawText(Brush b1, Point p1)
        {
            if (Text.Length == 0)
                return;
            G.DrawString(Text, Font, b1, p1);
        }

        protected void DrawText(Brush b1, int x, int y)
        {
            if (Text.Length == 0)
                return;
            G.DrawString(Text, Font, b1, x, y);
        }

        #endregion

        #region " DrawImage "

        private Point drawImagePoint;

        protected void DrawImage(HorizontalAlignment a, int x, int y)
        {
            DrawImage(image, a, x, y);
        }

        protected void DrawImage(Image image, HorizontalAlignment a, int x, int y)
        {
            if (image == null)
                return;
            drawImagePoint = Center(image.Size);

            switch (a)
            {
                case HorizontalAlignment.Left:
                    G.DrawImage(image, x, drawImagePoint.Y + y, image.Width, image.Height);
                    break;
                case HorizontalAlignment.Center:
                    G.DrawImage(image, drawImagePoint.X + x, drawImagePoint.Y + y, image.Width, image.Height);
                    break;
                case HorizontalAlignment.Right:
                    G.DrawImage(image, Width - image.Width - x, drawImagePoint.Y + y, image.Width, image.Height);
                    break;
            }
        }

        protected void DrawImage(Point p1)
        {
            DrawImage(image, p1.X, p1.Y);
        }

        protected void DrawImage(int x, int y)
        {
            DrawImage(image, x, y);
        }

        protected void DrawImage(Image image, Point p1)
        {
            DrawImage(image, p1.X, p1.Y);
        }

        protected void DrawImage(Image image, int x, int y)
        {
            if (image == null)
                return;
            G.DrawImage(image, x, y, image.Width, image.Height);
        }

        #endregion

        #region " DrawGradient "

        private LinearGradientBrush drawGradientBrush;

        private Rectangle drawGradientRectangle;

        protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height)
        {
            drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(blend, drawGradientRectangle);
        }

        protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height, float angle)
        {
            drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(blend, drawGradientRectangle, angle);
        }

        protected void DrawGradient(ColorBlend blend, Rectangle r)
        {
            drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, 90f);
            drawGradientBrush.InterpolationColors = blend;
            G.FillRectangle(drawGradientBrush, r);
        }

        protected void DrawGradient(ColorBlend blend, Rectangle r, float angle)
        {
            drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, angle);
            drawGradientBrush.InterpolationColors = blend;
            G.FillRectangle(drawGradientBrush, r);
        }


        protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height)
        {
            drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(c1, c2, drawGradientRectangle);
        }

        protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height, float angle)
        {
            drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(c1, c2, drawGradientRectangle, angle);
        }

        protected void DrawGradient(Color c1, Color c2, Rectangle r)
        {
            drawGradientBrush = new LinearGradientBrush(r, c1, c2, 90f);
            G.FillRectangle(drawGradientBrush, r);
        }

        protected void DrawGradient(Color c1, Color c2, Rectangle r, float angle)
        {
            drawGradientBrush = new LinearGradientBrush(r, c1, c2, angle);
            G.FillRectangle(drawGradientBrush, r);
        }

        #endregion

        #region " DrawRadial "

        private readonly GraphicsPath drawRadialPath;
        private PathGradientBrush drawRadialBrush1;
        private LinearGradientBrush drawRadialBrush2;

        private Rectangle drawRadialRectangle;

        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height)
        {
            drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, drawRadialRectangle, width / 2, height / 2);
        }

        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, Point center)
        {
            drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, drawRadialRectangle, center.X, center.Y);
        }

        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, int cx, int cy)
        {
            drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, drawRadialRectangle, cx, cy);
        }

        public void DrawRadial(ColorBlend blend, Rectangle r)
        {
            DrawRadial(blend, r, r.Width / 2, r.Height / 2);
        }

        public void DrawRadial(ColorBlend blend, Rectangle r, Point center)
        {
            DrawRadial(blend, r, center.X, center.Y);
        }

        public void DrawRadial(ColorBlend blend, Rectangle r, int cx, int cy)
        {
            drawRadialPath.Reset();
            drawRadialPath.AddEllipse(r.X, r.Y, r.Width - 1, r.Height - 1);

            drawRadialBrush1 = new PathGradientBrush(drawRadialPath);
            drawRadialBrush1.CenterPoint = new Point(r.X + cx, r.Y + cy);
            drawRadialBrush1.InterpolationColors = blend;

            if (G.SmoothingMode == SmoothingMode.AntiAlias)
            {
                G.FillEllipse(drawRadialBrush1, r.X + 1, r.Y + 1, r.Width - 3, r.Height - 3);
            }
            else
            {
                G.FillEllipse(drawRadialBrush1, r);
            }
        }


        protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height)
        {
            drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(c1, c2, drawRadialRectangle);
        }

        protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height, float angle)
        {
            drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(c1, c2, drawRadialRectangle, angle);
        }

        protected void DrawRadial(Color c1, Color c2, Rectangle r)
        {
            drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, 90f);
            G.FillEllipse(drawRadialBrush2, r);
        }

        protected void DrawRadial(Color c1, Color c2, Rectangle r, float angle)
        {
            drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, angle);
            G.FillEllipse(drawRadialBrush2, r);
        }

        #endregion

        #region " CreateRound "

        private GraphicsPath createRoundPath;

        private Rectangle createRoundRectangle;

        public GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
        {
            createRoundRectangle = new Rectangle(x, y, width, height);
            return CreateRound(createRoundRectangle, slope);
        }

        public GraphicsPath CreateRound(Rectangle r, int slope)
        {
            createRoundPath = new GraphicsPath(FillMode.Winding);
            createRoundPath.AddArc(r.X, r.Y, slope, slope, 180f, 90f);
            createRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270f, 90f);
            createRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0f, 90f);
            createRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90f, 90f);
            createRoundPath.CloseFigure();
            return createRoundPath;
        }

        #endregion
    }

    internal static class ThemeShare
    {
        #region " Animation "

        private static int frames;
        private static bool invalidate;

        public static PrecisionTimer ThemeTimer = new PrecisionTimer();
        //1000 / 50 = 20 FPS
        private const int Fps = 50;

        private const int Rate = 10;

        public delegate void AnimationDelegate(bool invalidate);


        private static readonly List<AnimationDelegate> Callbacks = new List<AnimationDelegate>();

        private static void HandleCallbacks(IntPtr state, bool reserve)
        {
            invalidate = (frames >= Fps);
            if (invalidate)
                frames = 0;

            lock (Callbacks)
            {
                for (int I = 0; I <= Callbacks.Count - 1; I++)
                {
                    Callbacks[I].Invoke(invalidate);
                }
            }

            frames += Rate;
        }

        private static void InvalidateThemeTimer()
        {
            if (Callbacks.Count == 0)
            {
                ThemeTimer.Delete();
            }
            else
            {
                ThemeTimer.Create(0, Rate, HandleCallbacks);
            }
        }

        public static void AddAnimationCallback(AnimationDelegate callback)
        {
            lock (Callbacks)
            {
                if (Callbacks.Contains(callback))
                    return;

                Callbacks.Add(callback);
                InvalidateThemeTimer();
            }
        }

        public static void RemoveAnimationCallback(AnimationDelegate callback)
        {
            lock (Callbacks)
            {
                if (!Callbacks.Contains(callback))
                    return;

                Callbacks.Remove(callback);
                InvalidateThemeTimer();
            }
        }

        #endregion
    }

    internal enum MouseState : byte
    {
        None = 0,
        Over = 1,
        Down = 2,
        Block = 3
    }

    internal struct Bloom
    {
        public string _Name;

        public Bloom(string name, Color value)
        {
            _Name = name;
            Value = value;
        }

        public string Name
        {
            get { return _Name; }
        }

        public Color Value { get; set; }

        public string ValueHex
        {
            get { return string.Concat("#", Value.R.ToString("X2", null), Value.G.ToString("X2", null), Value.B.ToString("X2", null)); }
            set
            {
                try
                {
                    Value = ColorTranslator.FromHtml(value);
                }
                catch
                {
                }
            }
        }
    }

    internal class PrecisionTimer : IDisposable
    {
        public delegate void TimerDelegate(IntPtr r1, bool r2);

        private IntPtr handle;
        private TimerDelegate timerCallback;
        public bool Enabled { get; private set; }

        public void Dispose()
        {
            Delete();
        }

        [DllImport("kernel32.dll", EntryPoint = "CreateTimerQueueTimer")]
        private static extern bool CreateTimerQueueTimer(ref IntPtr handle, IntPtr queue, TimerDelegate callback, IntPtr state, uint dueTime, uint period, uint flags);

        [DllImport("kernel32.dll", EntryPoint = "DeleteTimerQueueTimer")]
        private static extern bool DeleteTimerQueueTimer(IntPtr queue, IntPtr handle, IntPtr callback);

        public void Create(uint dueTime, uint period, TimerDelegate callback)
        {
            if (Enabled)
                return;

            timerCallback = callback;
            bool success = CreateTimerQueueTimer(ref handle, IntPtr.Zero, timerCallback, IntPtr.Zero, dueTime, period, 0);

            if (!success)
                ThrowNewException("CreateTimerQueueTimer");
            Enabled = success;
        }

        public void Delete()
        {
            if (!Enabled)
                return;
            bool success = DeleteTimerQueueTimer(IntPtr.Zero, handle, IntPtr.Zero);

            if (!success && !(Marshal.GetLastWin32Error() == 997))
            {
                ThrowNewException("DeleteTimerQueueTimer");
            }

            Enabled = !success;
        }

        private void ThrowNewException(string name)
        {
            throw new Exception(string.Format("{0} failed. Win32Error: {1}", name, Marshal.GetLastWin32Error()));
        }
    }

    public class ThemeModule
    {
        internal static Graphics G;
        private static Bitmap textBitmap;
        private static Graphics textGraphics;
        private static GraphicsPath createRoundPath;
        private static Rectangle createRoundRectangle;

        public ThemeModule()
        {
            textBitmap = new Bitmap(1, 1);
            textGraphics = Graphics.FromImage(textBitmap);
        }

        internal static SizeF MeasureString(string text, Font font)
        {
            return textGraphics.MeasureString(text, font);
        }

        internal static SizeF MeasureString(string text, Font font, int width)
        {
            return textGraphics.MeasureString(text, font, width, StringFormat.GenericTypographic);
        }

        internal static GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
        {
            createRoundRectangle = new Rectangle(x, y, width, height);
            return CreateRound(createRoundRectangle, slope);
        }

        internal static GraphicsPath CreateRound(Rectangle r, int slope)
        {
            createRoundPath = new GraphicsPath(FillMode.Winding);
            createRoundPath.AddArc(r.X, r.Y, slope, slope, 180f, 90f);
            createRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270f, 90f);
            createRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0f, 90f);
            createRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90f, 90f);
            createRoundPath.CloseFigure();
            return createRoundPath;
        }
    }

    internal class NsTheme : ThemeContainer154
    {
        private readonly SolidBrush B1;
        private readonly Pen p1;
        private readonly Pen p2;
        private int accentOffset = 42;
        private int pad;
        private Rectangle r1;

        public NsTheme()
        {
            Header = 30;
            BackColor = Color.FromArgb(50, 50, 50);

            p1 = new Pen(Color.FromArgb(35, 35, 35));
            p2 = new Pen(Color.FromArgb(60, 60, 60));

            B1 = new SolidBrush(Color.FromArgb(50, 50, 50));
        }

        public int AccentOffset
        {
            get { return accentOffset; }
            set
            {
                accentOffset = value;
                Invalidate();
            }
        }

        protected override void ColorHook()
        {
        }

        protected override void PaintHook()
        {
            G.Clear(BackColor);
            DrawBorders(p2, 1);

            G.DrawLine(p1, 0, 26, Width, 26);
            G.DrawLine(p2, 0, 25, Width, 25);

            pad = Math.Max(Measure().Width + 20, 80);
            r1 = new Rectangle(pad, 17, Width - (pad * 2) + accentOffset, 8);

            G.DrawRectangle(p2, r1);
            G.DrawRectangle(p1, r1.X + 1, r1.Y + 1, r1.Width - 2, r1.Height);

            G.DrawLine(p1, 0, 29, Width, 29);
            G.DrawLine(p2, 0, 30, Width, 30);

            DrawText(Brushes.Black, HorizontalAlignment.Left, 8, 1);
            DrawText(Brushes.White, HorizontalAlignment.Left, 7, 0);

            G.FillRectangle(B1, 0, 27, Width, 2);
            DrawBorders(Pens.Black);
        }
    }

    internal class NsButton : Control
    {
        private readonly Pen p1;
        private readonly Pen p2;
        private LinearGradientBrush gb1;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private bool isMouseDown;
        private PathGradientBrush pb1;
        private PointF pt1;
        private SizeF sz1;

        public NsButton()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            p1 = new Pen(Color.FromArgb(35, 35, 35));
            p2 = new Pen(Color.FromArgb(65, 65, 65));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            gp1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
            gp2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

            if (isMouseDown)
            {
                pb1 = new PathGradientBrush(gp1);
                pb1.CenterColor = Color.FromArgb(60, 60, 60);
                pb1.SurroundColors = new[] { Color.FromArgb(55, 55, 55) };
                pb1.FocusScales = new PointF(0.8f, 0.5f);

                g.FillPath(pb1, gp1);
            }
            else
            {
                gb1 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
                g.FillPath(gb1, gp1);
            }

            g.DrawPath(p1, gp1);
            g.DrawPath(p2, gp2);

            sz1 = g.MeasureString(Text, Font);
            pt1 = new PointF(5, Height / 2 - sz1.Height / 2);

            if (isMouseDown)
            {
                pt1.X += 1f;
                pt1.Y += 1f;
            }

            g.DrawString(Text, Font, Brushes.Black, pt1.X + 1, pt1.Y + 1);
            g.DrawString(Text, Font, Brushes.White, pt1);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            isMouseDown = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isMouseDown = false;
            Invalidate();
        }
    }

    internal class NsProgressBar : Control
    {
        private readonly SolidBrush b1;
        private readonly Pen p1;
        private readonly Pen p2;
        private int maximum = 100;
        private int minimum;
        private int value;
        private LinearGradientBrush gb1;
        private LinearGradientBrush gb2;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private GraphicsPath gp3;
        private int i1;
        private Rectangle r1;
        private Rectangle r2;

        public NsProgressBar()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            p1 = new Pen(Color.FromArgb(35, 35, 35));
            p2 = new Pen(Color.FromArgb(55, 55, 55));
            b1 = new SolidBrush(Color.FromArgb(160, 200, 0));
        }

        public int Minimum
        {
            get { return minimum; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

                minimum = value;
                if (value > this.value)
                    this.value = value;
                if (value > maximum)
                    maximum = value;
                Invalidate();
            }
        }

        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

                maximum = value;
                if (value < this.value)
                    this.value = value;
                if (value < minimum)
                    minimum = value;
                Invalidate();
            }
        }

        public int Value
        {
            get { return value; }
            set
            {
                if (value > maximum || value < minimum)
                {
                    throw new Exception("Property value is not valid.");
                }

                this.value = value;
                Invalidate();
            }
        }

        private void Increment(int amount)
        {
            Value += amount;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            gp1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
            gp2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

            r1 = new Rectangle(0, 2, Width - 1, Height - 1);
            gb1 = new LinearGradientBrush(r1, Color.FromArgb(45, 45, 45), Color.FromArgb(50, 50, 50), 90f);

            g.SetClip(gp1);
            g.FillRectangle(gb1, r1);

            i1 = Convert.ToInt32((value - minimum) / (maximum - minimum) * (Width - 3));

            if (i1 > 1)
            {
                gp3 = ThemeModule.CreateRound(1, 1, i1, Height - 3, 7);

                r2 = new Rectangle(1, 1, i1, Height - 3);
                gb2 = new LinearGradientBrush(r2, Color.FromArgb(150, 205, 0), Color.FromArgb(110, 150, 0), 90f);

                g.FillPath(gb2, gp3);
                g.DrawPath(p1, gp3);

                g.SetClip(gp3);
                g.SmoothingMode = SmoothingMode.None;

                g.FillRectangle(b1, r2.X, r2.Y + 1, r2.Width, r2.Height / 2);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.ResetClip();
            }

            g.DrawPath(p2, gp1);
            g.DrawPath(p1, gp2);
        }
    }

    internal class NsLabel : Control
    {
        private readonly SolidBrush b1;
        private string value1 = "NET";
        private string value2 = "SEAL";
        private PointF pt1;
        private PointF pt2;
        private SizeF sz1;
        private SizeF sz2;

        public NsLabel()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            Font = new Font("Segoe UI", 11.25f, FontStyle.Bold);

            b1 = new SolidBrush(Color.FromArgb(150, 205, 0));
        }

        public string Value1
        {
            get { return value1; }
            set
            {
                value1 = value;
                Invalidate();
            }
        }

        public string Value2
        {
            get { return value2; }
            set
            {
                value2 = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(BackColor);

            sz1 = g.MeasureString(Value1, Font, Width, StringFormat.GenericTypographic);
            sz2 = g.MeasureString(Value2, Font, Width, StringFormat.GenericTypographic);

            pt1 = new PointF(0, Height / 2 - sz1.Height / 2);
            pt2 = new PointF(sz1.Width + 1, Height / 2 - sz1.Height / 2);

            g.DrawString(Value1, Font, Brushes.Black, pt1.X + 1, pt1.Y + 1);
            g.DrawString(Value1, Font, Brushes.White, pt1);

            g.DrawString(Value2, Font, Brushes.Black, pt2.X + 1, pt2.Y + 1);
            g.DrawString(Value2, Font, b1, pt2);
        }
    }

    [DefaultEvent("TextChanged")]
    internal class NsTextBox : Control
    {
        private readonly TextBox Base;
        private readonly Pen p1;
        private readonly Pen p2;
        private int maxLength = 32767;
        private bool multiline;
        private bool readOnly;
        private HorizontalAlignment textAlign = HorizontalAlignment.Left;
        private bool useSystemPasswordChar;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private PathGradientBrush pb1;

        public NsTextBox()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, true);

            Cursor = Cursors.IBeam;

            Base = new TextBox();
            Base.Font = Font;
            Base.Text = Text;
            Base.MaxLength = maxLength;
            Base.Multiline = multiline;
            Base.ReadOnly = readOnly;
            Base.UseSystemPasswordChar = useSystemPasswordChar;

            Base.ForeColor = Color.White;
            Base.BackColor = Color.FromArgb(50, 50, 50);

            Base.BorderStyle = BorderStyle.None;

            Base.Location = new Point(5, 5);
            Base.Width = Width - 14;

            if (multiline)
            {
                Base.Height = Height - 11;
            }
            else
            {
                Height = Base.Height + 11;
            }

            Base.TextChanged += OnBaseTextChanged;
            Base.KeyDown += OnBaseKeyDown;

            p1 = new Pen(Color.FromArgb(35, 35, 35));
            p2 = new Pen(Color.FromArgb(55, 55, 55));
        }

        public HorizontalAlignment TextAlign
        {
            get { return textAlign; }
            set
            {
                textAlign = value;
                if (Base != null)
                {
                    Base.TextAlign = value;
                }
            }
        }

        public int MaxLength
        {
            get { return maxLength; }
            set
            {
                maxLength = value;
                if (Base != null)
                {
                    Base.MaxLength = value;
                }
            }
        }

        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                if (Base != null)
                {
                    Base.ReadOnly = value;
                }
            }
        }

        public bool UseSystemPasswordChar
        {
            get { return useSystemPasswordChar; }
            set
            {
                useSystemPasswordChar = value;
                if (Base != null)
                {
                    Base.UseSystemPasswordChar = value;
                }
            }
        }

        public bool Multiline
        {
            get { return multiline; }
            set
            {
                multiline = value;
                if (Base != null)
                {
                    Base.Multiline = value;

                    if (value)
                    {
                        Base.Height = Height - 11;
                    }
                    else
                    {
                        Height = Base.Height + 11;
                    }
                }
            }
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                if (Base != null)
                {
                    Base.Text = value;
                }
            }
        }

        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                if (Base != null)
                {
                    Base.Font = value;
                    Base.Location = new Point(5, 5);
                    Base.Width = Width - 8;

                    if (!multiline)
                    {
                        Height = Base.Height + 11;
                    }
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!Controls.Contains(Base))
            {
                Controls.Add(Base);
            }

            base.OnHandleCreated(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            gp1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
            gp2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

            pb1 = new PathGradientBrush(gp1);
            pb1.CenterColor = Color.FromArgb(50, 50, 50);
            pb1.SurroundColors = new[] { Color.FromArgb(45, 45, 45) };
            pb1.FocusScales = new PointF(0.9f, 0.5f);

            g.FillPath(pb1, gp1);

            g.DrawPath(p2, gp1);
            g.DrawPath(p1, gp2);
        }

        private void OnBaseTextChanged(object s, EventArgs e)
        {
            Text = Base.Text;
        }

        private void OnBaseKeyDown(object s, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                Base.SelectAll();
                e.SuppressKeyPress = true;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            Base.Location = new Point(5, 5);

            Base.Width = Width - 10;
            Base.Height = Height - 11;

            base.OnResize(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Base.Focus();
            base.OnMouseDown(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            Base.Focus();
            Invalidate();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            Invalidate();
            base.OnLeave(e);
        }
    }

    [DefaultEvent("CheckedChanged")]
    internal class NsCheckBox : Control
    {
        public delegate void CheckedChangedEventHandler(object sender);

        private readonly Pen p11;
        private readonly Pen p22;
        private readonly Pen p3;
        private readonly Pen p4;
        private bool _Checked;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private PathGradientBrush pb1;
        private PointF pt1;
        private SizeF sz1;

        public NsCheckBox()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            p11 = new Pen(Color.FromArgb(55, 55, 55));
            p22 = new Pen(Color.FromArgb(35, 35, 35));
            p3 = new Pen(Color.Black, 2f);
            p4 = new Pen(Color.White, 2f);
        }

        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                if (CheckedChanged != null)
                {
                    CheckedChanged(this);
                }

                Invalidate();
            }
        }

        public event CheckedChangedEventHandler CheckedChanged;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            gp1 = ThemeModule.CreateRound(0, 2, Height - 5, Height - 5, 5);
            gp2 = ThemeModule.CreateRound(1, 3, Height - 7, Height - 7, 5);

            pb1 = new PathGradientBrush(gp1);
            pb1.CenterColor = Color.FromArgb(50, 50, 50);
            pb1.SurroundColors = new[] { Color.FromArgb(45, 45, 45) };
            pb1.FocusScales = new PointF(0.3f, 0.3f);

            g.FillPath(pb1, gp1);
            g.DrawPath(p11, gp1);
            g.DrawPath(p22, gp2);

            if (_Checked)
            {
                g.DrawLine(p3, 5, Height - 9, 8, Height - 7);
                g.DrawLine(p3, 7, Height - 7, Height - 8, 7);

                g.DrawLine(p4, 4, Height - 10, 7, Height - 8);
                g.DrawLine(p4, 6, Height - 8, Height - 9, 6);
            }

            sz1 = g.MeasureString(Text, Font);
            pt1 = new PointF(Height - 3, Height / 2 - sz1.Height / 2);

            g.DrawString(Text, Font, Brushes.Black, pt1.X + 1, pt1.Y + 1);
            g.DrawString(Text, Font, Brushes.White, pt1);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Checked = !Checked;
        }
    }

    [DefaultEvent("CheckedChanged")]
    internal class NsRadioButton : Control
    {
        public delegate void CheckedChangedEventHandler(object sender);

        private readonly Pen p1;
        private readonly Pen p2;
        private bool _Checked;
        private GraphicsPath gp1;
        private PathGradientBrush pb1;
        private PointF pt1;
        private SizeF sz1;

        public NsRadioButton()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            p1 = new Pen(Color.FromArgb(55, 55, 55));
            p2 = new Pen(Color.FromArgb(35, 35, 35));
        }

        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;

                if (_Checked)
                {
                    InvalidateParent();
                }

                if (CheckedChanged != null)
                {
                    CheckedChanged(this);
                }
                Invalidate();
            }
        }

        public event CheckedChangedEventHandler CheckedChanged;

        private void InvalidateParent()
        {
            if (Parent == null)
                return;

            foreach (Control c in Parent.Controls)
            {
                if ((!ReferenceEquals(c, this)) && (c is NsRadioButton))
                {
                    ((NsRadioButton)c).Checked = false;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            gp1 = new GraphicsPath();
            gp1.AddEllipse(0, 2, Height - 5, Height - 5);

            pb1 = new PathGradientBrush(gp1);
            pb1.CenterColor = Color.FromArgb(50, 50, 50);
            pb1.SurroundColors = new[] { Color.FromArgb(45, 45, 45) };
            pb1.FocusScales = new PointF(0.3f, 0.3f);

            g.FillPath(pb1, gp1);

            g.DrawEllipse(p1, 0, 2, Height - 5, Height - 5);
            g.DrawEllipse(p2, 1, 3, Height - 7, Height - 7);

            if (_Checked)
            {
                g.FillEllipse(Brushes.Black, 6, 8, Height - 15, Height - 15);
                g.FillEllipse(Brushes.White, 5, 7, Height - 15, Height - 15);
            }

            sz1 = g.MeasureString(Text, Font);
            pt1 = new PointF(Height - 3, Height / 2 - sz1.Height / 2);

            g.DrawString(Text, Font, Brushes.Black, pt1.X + 1, pt1.Y + 1);
            g.DrawString(Text, Font, Brushes.White, pt1);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Checked = true;
            base.OnMouseDown(e);
        }
    }

    internal class NsComboBox : ComboBox
    {
        private readonly SolidBrush b1;
        private readonly SolidBrush b2;
        private readonly Pen p1;
        private readonly Pen p2;
        private readonly Pen p3;
        private readonly Pen p4;
        private LinearGradientBrush gb1;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private PointF pt1;
        private SizeF sz1;

        public NsComboBox()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;

            BackColor = Color.FromArgb(50, 50, 50);
            ForeColor = Color.White;

            p1 = new Pen(Color.FromArgb(35, 35, 35));
            p2 = new Pen(Color.White, 2f);
            p3 = new Pen(Brushes.Black, 2f);
            p4 = new Pen(Color.FromArgb(65, 65, 65));

            b1 = new SolidBrush(Color.FromArgb(65, 65, 65));
            b2 = new SolidBrush(Color.FromArgb(55, 55, 55));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            gp1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
            gp2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

            gb1 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
            g.SetClip(gp1);
            g.FillRectangle(gb1, ClientRectangle);
            g.ResetClip();

            g.DrawPath(p1, gp1);
            g.DrawPath(p4, gp2);

            sz1 = g.MeasureString(Text, Font);
            pt1 = new PointF(5, Height / 2 - sz1.Height / 2);

            g.DrawString(Text, Font, Brushes.Black, pt1.X + 1, pt1.Y + 1);
            g.DrawString(Text, Font, Brushes.White, pt1);

            g.DrawLine(p3, Width - 15, 10, Width - 11, 13);
            g.DrawLine(p3, Width - 7, 10, Width - 11, 13);
            g.DrawLine(Pens.Black, Width - 11, 13, Width - 11, 14);

            g.DrawLine(p2, Width - 16, 9, Width - 12, 12);
            g.DrawLine(p2, Width - 8, 9, Width - 12, 12);
            g.DrawLine(Pens.White, Width - 12, 12, Width - 12, 13);

            g.DrawLine(p1, Width - 22, 0, Width - 22, Height);
            g.DrawLine(p4, Width - 23, 1, Width - 23, Height - 2);
            g.DrawLine(p4, Width - 21, 1, Width - 21, Height - 2);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(b1, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(b2, e.Bounds);
            }

            if (e.Index != -1)
            {
                ObjectCollection items = Items;
                e.Graphics.DrawString(GetItemText(items[e.Index]), e.Font, Brushes.White, e.Bounds);
            }
        }
    }

    internal class NsTabControl : TabControl
    {
        private readonly SolidBrush b1;
        private readonly SolidBrush b2;
        private readonly SolidBrush b3;
        private readonly SolidBrush b4;
        private readonly Pen p1;
        private readonly Pen p2;
        private readonly Pen p3;
        private readonly StringFormat sf1;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private GraphicsPath gp3;
        private GraphicsPath gp4;
        private int itemHeight;
        private int offset;
        private PathGradientBrush pb1;
        private Rectangle r1;
        private Rectangle r2;
        private TabPage tp1;

        public NsTabControl()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            SizeMode = TabSizeMode.Fixed;
            Alignment = TabAlignment.Left;
            ItemSize = new Size(28, 115);

            DrawMode = TabDrawMode.OwnerDrawFixed;

            p1 = new Pen(Color.FromArgb(55, 55, 55));
            p2 = new Pen(Color.FromArgb(35, 35, 35));
            p3 = new Pen(Color.FromArgb(45, 45, 45), 2);

            b1 = new SolidBrush(Color.FromArgb(50, 50, 50));
            b2 = new SolidBrush(Color.FromArgb(35, 35, 35));
            b3 = new SolidBrush(Color.FromArgb(150, 205, 0));
            b4 = new SolidBrush(Color.FromArgb(65, 65, 65));

            sf1 = new StringFormat();
            sf1.LineAlignment = StringAlignment.Center;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is TabPage)
            {
                e.Control.BackColor = Color.FromArgb(50, 50, 50);
            }

            base.OnControlAdded(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(Color.FromArgb(50, 50, 50));
            g.SmoothingMode = SmoothingMode.AntiAlias;

            itemHeight = ItemSize.Height + 2;

            gp1 = ThemeModule.CreateRound(0, 0, itemHeight + 3, Height - 1, 7);
            gp2 = ThemeModule.CreateRound(1, 1, itemHeight + 3, Height - 3, 7);

            pb1 = new PathGradientBrush(gp1);
            pb1.CenterColor = Color.FromArgb(50, 50, 50);
            pb1.SurroundColors = new[] { Color.FromArgb(45, 45, 45) };
            pb1.FocusScales = new PointF(0.8f, 0.95f);

            g.FillPath(pb1, gp1);

            g.DrawPath(p1, gp1);
            g.DrawPath(p2, gp2);

            for (int I = 0; I <= TabCount - 1; I++)
            {
                r1 = GetTabRect(I);
                r1.Y += 2;
                r1.Height -= 3;
                r1.Width += 1;
                r1.X -= 1;

                tp1 = TabPages[I];
                offset = 0;

                if (SelectedIndex == I)
                {
                    g.FillRectangle(b1, r1);

                    for (int j = 0; j <= 1; j++)
                    {
                        g.FillRectangle(b2, r1.X + 5 + (j * 5), r1.Y + 6, 2, r1.Height - 9);

                        g.SmoothingMode = SmoothingMode.None;
                        g.FillRectangle(b3, r1.X + 5 + (j * 5), r1.Y + 5, 2, r1.Height - 9);
                        g.SmoothingMode = SmoothingMode.AntiAlias;

                        offset += 5;
                    }

                    g.DrawRectangle(p3, r1.X + 1, r1.Y - 1, r1.Width, r1.Height + 2);
                    g.DrawRectangle(p1, r1.X + 1, r1.Y + 1, r1.Width - 2, r1.Height - 2);
                    g.DrawRectangle(p2, r1);
                }
                else
                {
                    for (int j = 0; j <= 1; j++)
                    {
                        g.FillRectangle(b2, r1.X + 5 + (j * 5), r1.Y + 6, 2, r1.Height - 9);

                        g.SmoothingMode = SmoothingMode.None;
                        g.FillRectangle(b4, r1.X + 5 + (j * 5), r1.Y + 5, 2, r1.Height - 9);
                        g.SmoothingMode = SmoothingMode.AntiAlias;

                        offset += 5;
                    }
                }

                r1.X += 5 + offset;

                r2 = r1;
                r2.Y += 1;
                r2.X += 1;

                g.DrawString(tp1.Text, Font, Brushes.Black, r2, sf1);
                g.DrawString(tp1.Text, Font, Brushes.White, r1, sf1);
            }

            gp3 = ThemeModule.CreateRound(itemHeight, 0, Width - itemHeight - 1, Height - 1, 7);
            gp4 = ThemeModule.CreateRound(itemHeight + 1, 1, Width - itemHeight - 3, Height - 3, 7);

            g.DrawPath(p2, gp3);
            g.DrawPath(p1, gp4);
        }
    }

    [DefaultEvent("CheckedChanged")]
    internal class NsOnOffBox : Control
    {
        public delegate void CheckedChangedEventHandler(object sender);

        private readonly SolidBrush b1;
        private readonly SolidBrush b2;
        private readonly SolidBrush b3;
        private readonly SolidBrush b4;
        private readonly SolidBrush b5;
        private readonly Pen p1;
        private readonly Pen p2;
        private readonly Pen p3;
        private readonly StringFormat sf1;
        private readonly StringFormat sf2;
        private bool _Checked;
        private LinearGradientBrush gb1;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private GraphicsPath gp3;
        private GraphicsPath gp4;
        private int offset;
        private PathGradientBrush pb1;
        private Rectangle r1;
        private Rectangle r2;
        private Rectangle r3;

        public NsOnOffBox()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            p1 = new Pen(Color.FromArgb(55, 55, 55));
            p2 = new Pen(Color.FromArgb(35, 35, 35));
            p3 = new Pen(Color.FromArgb(65, 65, 65));

            b1 = new SolidBrush(Color.FromArgb(35, 35, 35));
            b2 = new SolidBrush(Color.FromArgb(85, 85, 85));
            b3 = new SolidBrush(Color.FromArgb(65, 65, 65));
            b4 = new SolidBrush(Color.FromArgb(150, 205, 0));
            b5 = new SolidBrush(Color.FromArgb(40, 40, 40));

            sf1 = new StringFormat();
            sf1.LineAlignment = StringAlignment.Center;
            sf1.Alignment = StringAlignment.Near;

            sf2 = new StringFormat();
            sf2.LineAlignment = StringAlignment.Center;
            sf2.Alignment = StringAlignment.Far;

            Size = new Size(56, 24);
            MinimumSize = Size;
            MaximumSize = Size;
        }

        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                if (CheckedChanged != null)
                {
                    CheckedChanged(this);
                }

                Invalidate();
            }
        }

        public event CheckedChangedEventHandler CheckedChanged;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            gp1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
            gp2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

            pb1 = new PathGradientBrush(gp1);
            pb1.CenterColor = Color.FromArgb(50, 50, 50);
            pb1.SurroundColors = new[] { Color.FromArgb(45, 45, 45) };
            pb1.FocusScales = new PointF(0.3f, 0.3f);

            g.FillPath(pb1, gp1);
            g.DrawPath(p1, gp1);
            g.DrawPath(p2, gp2);

            r1 = new Rectangle(5, 0, Width - 10, Height + 2);
            r2 = new Rectangle(6, 1, Width - 10, Height + 2);

            r3 = new Rectangle(1, 1, (Width / 2) - 1, Height - 3);

            if (_Checked)
            {
                g.DrawString("On", Font, Brushes.Black, r2, sf1);
                g.DrawString("On", Font, Brushes.White, r1, sf1);

                r3.X += (Width / 2) - 1;
            }
            else
            {
                g.DrawString("Off", Font, b1, r2, sf2);
                g.DrawString("Off", Font, b2, r1, sf2);
            }

            gp3 = ThemeModule.CreateRound(r3, 7);
            gp4 = ThemeModule.CreateRound(r3.X + 1, r3.Y + 1, r3.Width - 2, r3.Height - 2, 7);

            gb1 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);

            g.FillPath(gb1, gp3);
            g.DrawPath(p2, gp3);
            g.DrawPath(p3, gp4);

            offset = r3.X + (r3.Width / 2) - 3;

            for (int I = 0; I <= 1; I++)
            {
                if (_Checked)
                {
                    g.FillRectangle(b1, offset + (I * 5), 7, 2, Height - 14);
                }
                else
                {
                    g.FillRectangle(b3, offset + (I * 5), 7, 2, Height - 14);
                }

                g.SmoothingMode = SmoothingMode.None;

                if (_Checked)
                {
                    g.FillRectangle(b4, offset + (I * 5), 7, 2, Height - 14);
                }
                else
                {
                    g.FillRectangle(b5, offset + (I * 5), 7, 2, Height - 14);
                }

                g.SmoothingMode = SmoothingMode.AntiAlias;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Checked = !Checked;
            base.OnMouseDown(e);
        }
    }

    internal class NsControlButton : Control
    {
        public enum Button : byte
        {
            None = 0,
            Minimize = 1,
            MaximizeRestore = 2,
            Close = 3
        }

        private Button controlButton = Button.Close;
        private GraphicsPath closePath;
        private Graphics g;

        public NsControlButton()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            Anchor = AnchorStyles.Top | AnchorStyles.Right;

            Width = 18;
            Height = 20;

            MinimumSize = Size;
            MaximumSize = Size;

            Margin = new Padding(0);
        }

        public Button ControlButton
        {
            get { return controlButton; }
            set
            {
                controlButton = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;
            g.Clear(BackColor);

            switch (controlButton)
            {
                case Button.Minimize:
                    DrawMinimize(3, 10);
                    break;
                case Button.MaximizeRestore:
                    if (FindForm().WindowState == FormWindowState.Normal)
                    {
                        DrawMaximize(3, 5);
                    }
                    else
                    {
                        DrawRestore(3, 4);
                    }
                    break;
                case Button.Close:
                    DrawClose(4, 5);
                    break;
            }
        }

        private void DrawMinimize(int x, int y)
        {
            g.FillRectangle(Brushes.White, x, y, 12, 5);
            g.DrawRectangle(Pens.Black, x, y, 11, 4);
        }

        private void DrawMaximize(int x, int y)
        {
            g.DrawRectangle(new Pen(Color.White, 2), x + 2, y + 2, 8, 6);
            g.DrawRectangle(Pens.Black, x, y, 11, 9);
            g.DrawRectangle(Pens.Black, x + 3, y + 3, 5, 3);
        }

        private void DrawRestore(int x, int y)
        {
            g.FillRectangle(Brushes.White, x + 3, y + 1, 8, 4);
            g.FillRectangle(Brushes.White, x + 7, y + 5, 4, 4);
            g.DrawRectangle(Pens.Black, x + 2, y + 0, 9, 9);

            g.FillRectangle(Brushes.White, x + 1, y + 3, 2, 6);
            g.FillRectangle(Brushes.White, x + 1, y + 9, 8, 2);
            g.DrawRectangle(Pens.Black, x, y + 2, 9, 9);
            g.DrawRectangle(Pens.Black, x + 3, y + 5, 3, 3);
        }

        private void DrawClose(int x, int y)
        {
            if (closePath == null)
            {
                closePath = new GraphicsPath();
                closePath.AddLine(x + 1, y, x + 3, y);
                closePath.AddLine(x + 5, y + 2, x + 7, y);
                closePath.AddLine(x + 9, y, x + 10, y + 1);
                closePath.AddLine(x + 7, y + 4, x + 7, y + 5);
                closePath.AddLine(x + 10, y + 8, x + 9, y + 9);
                closePath.AddLine(x + 7, y + 9, x + 5, y + 7);
                closePath.AddLine(x + 3, y + 9, x + 1, y + 9);
                closePath.AddLine(x + 0, y + 8, x + 3, y + 5);
                closePath.AddLine(x + 3, y + 4, x + 0, y + 1);
            }

            g.FillPath(Brushes.White, closePath);
            g.DrawPath(Pens.Black, closePath);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Form f = FindForm();

                switch (controlButton)
                {
                    case Button.Minimize:
                        f.WindowState = FormWindowState.Minimized;
                        break;
                    case Button.MaximizeRestore:
                        if (f.WindowState == FormWindowState.Normal)
                        {
                            f.WindowState = FormWindowState.Maximized;
                        }
                        else
                        {
                            f.WindowState = FormWindowState.Normal;
                        }
                        break;
                    case Button.Close:
                        f.Close();
                        break;
                }
            }

            Invalidate();
            base.OnMouseClick(e);
        }
    }

    internal class NsGroupBox : ContainerControl
    {
        private readonly Font subTitleFont;
        private readonly Font titleFont;
        private readonly SolidBrush b1;
        private readonly Pen p1;
        private readonly Pen p2;
        private bool drawSeperator;
        private string subTitle = "Details";
        private string title = "GroupBox";
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private PointF pt1;
        private SizeF sz1;
        private SizeF sz2;

        public NsGroupBox()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            titleFont = new Font("Verdana", 10f);
            subTitleFont = new Font("Verdana", 6.5f);

            p1 = new Pen(Color.FromArgb(35, 35, 35));
            p2 = new Pen(Color.FromArgb(55, 55, 55));

            b1 = new SolidBrush(Color.FromArgb(150, 205, 0));
        }

        public bool DrawSeperator
        {
            get { return drawSeperator; }
            set
            {
                drawSeperator = value;
                Invalidate();
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                Invalidate();
            }
        }

        public string SubTitle
        {
            get { return subTitle; }
            set
            {
                subTitle = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            gp1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
            gp2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

            g.DrawPath(p1, gp1);
            g.DrawPath(p2, gp2);

            sz1 = g.MeasureString(title, titleFont, Width, StringFormat.GenericTypographic);
            sz2 = g.MeasureString(subTitle, subTitleFont, Width, StringFormat.GenericTypographic);

            g.DrawString(title, titleFont, Brushes.Black, 6, 6);
            g.DrawString(title, titleFont, b1, 5, 5);

            pt1 = new PointF(6f, sz1.Height + 4f);

            g.DrawString(subTitle, subTitleFont, Brushes.Black, pt1.X + 1, pt1.Y + 1);
            g.DrawString(subTitle, subTitleFont, Brushes.White, pt1.X, pt1.Y);

            if (drawSeperator)
            {
                int y = Convert.ToInt32(pt1.Y + sz2.Height) + 8;

                g.DrawLine(p1, 4, y, Width - 5, y);
                g.DrawLine(p2, 4, y + 1, Width - 5, y + 1);
            }
        }
    }

    internal class NsSeperator : Control
    {
        private readonly Pen p1;
        private readonly Pen p2;

        public NsSeperator()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            Height = 10;

            p1 = new Pen(Color.FromArgb(35, 35, 35));
            p2 = new Pen(Color.FromArgb(55, 55, 55));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackColor);

            g.DrawLine(p1, 0, 5, Width, 5);
            g.DrawLine(p2, 0, 6, Width, 6);
        }
    }

    [DefaultEvent("Scroll")]
    internal class NsTrackBar : Control
    {
        public delegate void ScrollEventHandler(object sender);

        private readonly Pen p1;
        private readonly Pen p2;
        private readonly Pen p3;
        private readonly Pen p4;
        private int maximum = 10;
        private int minimum;
        private int value;
        private LinearGradientBrush gb1;
        private LinearGradientBrush gb2;
        private LinearGradientBrush gb3;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private GraphicsPath gp3;
        private GraphicsPath gp4;
        private int i1;
        private Rectangle r1;
        private Rectangle r2;
        private Rectangle r3;
        private bool trackDown;

        public NsTrackBar()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            Height = 17;

            p1 = new Pen(Color.FromArgb(110, 150, 0), 2);
            p2 = new Pen(Color.FromArgb(55, 55, 55));
            p3 = new Pen(Color.FromArgb(35, 35, 35));
            p4 = new Pen(Color.FromArgb(65, 65, 65));
        }

        public int Minimum
        {
            get { return minimum; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

                minimum = value;
                if (value > this.value)
                    this.value = value;
                if (value > maximum)
                    maximum = value;
                Invalidate();
            }
        }

        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

                maximum = value;
                if (value < this.value)
                    this.value = value;
                if (value < minimum)
                    minimum = value;
                Invalidate();
            }
        }

        public int Value
        {
            get { return value; }
            set
            {
                if (value == this.value)
                    return;

                if (value > maximum || value < minimum)
                {
                    throw new Exception("Property value is not valid.");
                }

                this.value = value;
                Invalidate();

                if (Scroll != null)
                {
                    Scroll(this);
                }
            }
        }

        public event ScrollEventHandler Scroll;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            gp1 = ThemeModule.CreateRound(0, 5, Width - 1, 10, 5);
            gp2 = ThemeModule.CreateRound(1, 6, Width - 3, 8, 5);

            r1 = new Rectangle(0, 7, Width - 1, 5);
            gb1 = new LinearGradientBrush(r1, Color.FromArgb(45, 45, 45), Color.FromArgb(50, 50, 50), 90f);

            i1 = Convert.ToInt32((value - minimum) / (maximum - minimum) * (Width - 11));
            r2 = new Rectangle(i1, 0, 10, 20);

            g.SetClip(gp2);
            g.FillRectangle(gb1, r1);

            r3 = new Rectangle(1, 7, r2.X + r2.Width - 2, 8);
            gb2 = new LinearGradientBrush(r3, Color.FromArgb(150, 205, 0), Color.FromArgb(110, 150, 0), 90f);

            g.SmoothingMode = SmoothingMode.None;
            g.FillRectangle(gb2, r3);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            for (int I = 0; I <= r3.Width - 15; I += 5)
            {
                g.DrawLine(p1, I, 0, I + 15, Height);
            }

            g.ResetClip();

            g.DrawPath(p2, gp1);
            g.DrawPath(p3, gp2);

            gp3 = ThemeModule.CreateRound(r2, 5);
            gp4 = ThemeModule.CreateRound(r2.X + 1, r2.Y + 1, r2.Width - 2, r2.Height - 2, 5);
            gb3 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);

            g.FillPath(gb3, gp3);
            g.DrawPath(p3, gp3);
            g.DrawPath(p4, gp4);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                i1 = Convert.ToInt32((value - minimum) / (maximum - minimum) * (Width - 11));
                r2 = new Rectangle(i1, 0, 10, 20);

                trackDown = r2.Contains(e.Location);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (trackDown && e.X > -1 && e.X < (Width + 1))
            {
                Value = minimum + Convert.ToInt32((maximum - minimum) * (e.X / Width));
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            trackDown = false;
            base.OnMouseUp(e);
        }
    }

    [DefaultEvent("ValueChanged")]
    internal class NsRandomPool : Control
    {
        public delegate void ValueChangedEventHandler(object sender);

        private readonly StringBuilder value = new StringBuilder();
        private readonly SolidBrush b1;
        private readonly int drawSize = 8;
        private readonly int itemSize = 9;
        private readonly Pen p1;
        private readonly Pen p2;
        private readonly Random rng = new Random();
        private string fullValue;
        private SolidBrush b2;
        private int columnSize;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private int index1 = -1;
        private int index2;
        private bool invertColors;
        private PathGradientBrush pb1;
        private int rowSize;
        private byte[] table;
        private Rectangle wa;

        public NsRandomPool()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            p1 = new Pen(Color.FromArgb(55, 55, 55));
            p2 = new Pen(Color.FromArgb(35, 35, 35));

            b1 = new SolidBrush(Color.FromArgb(30, 30, 30));
        }

        public string Value
        {
            get { return value.ToString(); }
        }

        public string FullValue
        {
            get { return BitConverter.ToString(table).Replace("-", ""); }
        }

        public event ValueChangedEventHandler ValueChanged;

        protected override void OnHandleCreated(EventArgs e)
        {
            UpdateTable();
            base.OnHandleCreated(e);
        }

        private void UpdateTable()
        {
            wa = new Rectangle(5, 5, Width - 10, Height - 10);

            rowSize = wa.Width / itemSize;
            columnSize = wa.Height / itemSize;

            wa.Width = rowSize * itemSize;
            wa.Height = columnSize * itemSize;

            wa.X = (Width / 2) - (wa.Width / 2);
            wa.Y = (Height / 2) - (wa.Height / 2);

            table = new byte[(rowSize * columnSize)];

            for (int I = 0; I <= table.Length - 1; I++)
            {
                table[I] = Convert.ToByte(rng.Next(100));
            }

            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateTable();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            HandleDraw(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            HandleDraw(e);
            base.OnMouseDown(e);
        }

        private void HandleDraw(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                if (!wa.Contains(e.Location))
                    return;

                invertColors = (e.Button == MouseButtons.Right);

                index1 = GetIndex(e.X, e.Y);
                if (index1 == index2)
                    return;

                bool l = !(index1 % rowSize == 0);
                bool r = !(index1 % rowSize == (rowSize - 1));

                Randomize(index1 - rowSize);

                if (l)
                    Randomize(index1 - 1);
                Randomize(index1);
                if (r)
                    Randomize(index1 + 1);
                Randomize(index1 + rowSize);

                value.Append(table[index1].ToString("X"));
                if (value.Length > 32)
                    value.Remove(0, 2);

                if (ValueChanged != null)
                {
                    ValueChanged(this);
                }

                index2 = index1;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            gp1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
            gp2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

            pb1 = new PathGradientBrush(gp1);
            pb1.CenterColor = Color.FromArgb(50, 50, 50);
            pb1.SurroundColors = new[] { Color.FromArgb(45, 45, 45) };
            pb1.FocusScales = new PointF(0.9f, 0.5f);

            g.FillPath(pb1, gp1);

            g.DrawPath(p1, gp1);
            g.DrawPath(p2, gp2);

            g.SmoothingMode = SmoothingMode.None;

            for (int I = 0; I <= table.Length - 1; I++)
            {
                int c = Math.Max((int)table[I], 75);

                int x = ((I % rowSize) * itemSize) + wa.X;
                int y = ((I / rowSize) * itemSize) + wa.Y;

                b2 = new SolidBrush(Color.FromArgb(c, c, c));

                g.FillRectangle(b1, x + 1, y + 1, drawSize, drawSize);
                g.FillRectangle(b2, x, y, drawSize, drawSize);

                b2.Dispose();
            }
        }

        private int GetIndex(int x, int y)
        {
            return (((y - wa.Y) / itemSize) * rowSize) + ((x - wa.X) / itemSize);
        }

        private void Randomize(int index)
        {
            if (index > -1 && index < table.Length)
            {
                if (invertColors)
                {
                    table[index] = Convert.ToByte(rng.Next(100));
                }
                else
                {
                    table[index] = Convert.ToByte(rng.Next(100, 256));
                }
            }
        }
    }

    internal class NsKeyboard : Control
    {
        private const string LowerKeys = "1234567890-=qwertyuiop[]asdfghjkl\\;'zxcvbnm,./`";
        private const string UpperKeys = "!@#$%^&*()_+QWERTYUIOP{}ASDFGHJKL|:\"ZXCVBNM<>?~";
        private readonly SolidBrush b1;
        private readonly char[] lower;

        private readonly string[] other =
        {
            "Shift",
            "Space",
            "Back"
        };

        private readonly Pen p1;
        private readonly Pen p2;
        private readonly Pen p3;
        private readonly Bitmap textBitmap;
        private readonly Graphics textGraphics;
        private readonly char[] upper;
        private Rectangle[] buttons;
        private Graphics g;
        private LinearGradientBrush gb1;
        private GraphicsPath gp1;
        private PointF[] lowerCache;
        private PathGradientBrush pb1;
        private int pressed = -1;
        private PointF pt1;
        private bool shift;
        private SizeF sz1;
        private PointF[] upperCache;

        public NsKeyboard()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            Font = new Font("Verdana", 8.25f);

            textBitmap = new Bitmap(1, 1);
            textGraphics = Graphics.FromImage(textBitmap);

            MinimumSize = new Size(386, 162);
            MaximumSize = new Size(386, 162);

            lower = LowerKeys.ToCharArray();
            upper = UpperKeys.ToCharArray();

            PrepareCache();

            p1 = new Pen(Color.FromArgb(45, 45, 45));
            p2 = new Pen(Color.FromArgb(65, 65, 65));
            p3 = new Pen(Color.FromArgb(35, 35, 35));

            b1 = new SolidBrush(Color.FromArgb(100, 100, 100));
        }

        public Control Target { get; set; }

        private void PrepareCache()
        {
            buttons = new Rectangle[51];
            upperCache = new PointF[upper.Length];
            lowerCache = new PointF[lower.Length];

            int I = 0;

            SizeF s = default(SizeF);
            Rectangle r = default(Rectangle);

            for (int y = 0; y <= 3; y++)
            {
                for (int x = 0; x <= 11; x++)
                {
                    I = (y * 12) + x;
                    r = new Rectangle(x * 32, y * 32, 32, 32);

                    buttons[I] = r;

                    if (!(I == 47) && !char.IsLetter(upper[I]))
                    {
                        s = textGraphics.MeasureString(upper[I].ToString(), Font);
                        upperCache[I] = new PointF(r.X + (r.Width / 2 - s.Width / 2), r.Y + r.Height - s.Height - 2);

                        s = textGraphics.MeasureString(lower[I].ToString(), Font);
                        lowerCache[I] = new PointF(r.X + (r.Width / 2 - s.Width / 2), r.Y + r.Height - s.Height - 2);
                    }
                }
            }

            buttons[48] = new Rectangle(0, 4 * 32, 2 * 32, 32);
            buttons[49] = new Rectangle(buttons[48].Right, 4 * 32, 8 * 32, 32);
            buttons[50] = new Rectangle(buttons[49].Right, 4 * 32, 2 * 32, 32);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(BackColor);

            Rectangle r = default(Rectangle);

            int offset = 0;
            g.DrawRectangle(p1, 0, 0, (12 * 32) + 1, (5 * 32) + 1);

            for (int I = 0; I <= buttons.Length - 1; I++)
            {
                r = buttons[I];

                offset = 0;
                if (I == pressed)
                {
                    offset = 1;

                    gp1 = new GraphicsPath();
                    gp1.AddRectangle(r);

                    pb1 = new PathGradientBrush(gp1);
                    pb1.CenterColor = Color.FromArgb(60, 60, 60);
                    pb1.SurroundColors = new[] { Color.FromArgb(55, 55, 55) };
                    pb1.FocusScales = new PointF(0.8f, 0.5f);

                    g.FillPath(pb1, gp1);
                }
                else
                {
                    gb1 = new LinearGradientBrush(r, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
                    g.FillRectangle(gb1, r);
                }

                switch (I)
                {
                    case 48:
                    case 49:
                    case 50:
                        sz1 = g.MeasureString(other[I - 48], Font);
                        g.DrawString(other[I - 48], Font, Brushes.Black, r.X + (r.Width / 2 - sz1.Width / 2) + offset + 1, r.Y + (r.Height / 2 - sz1.Height / 2) + offset + 1);
                        g.DrawString(other[I - 48], Font, Brushes.White, r.X + (r.Width / 2 - sz1.Width / 2) + offset, r.Y + (r.Height / 2 - sz1.Height / 2) + offset);
                        break;
                    case 47:
                        DrawArrow(Color.Black, r.X + offset + 1, r.Y + offset + 1);
                        DrawArrow(Color.White, r.X + offset, r.Y + offset);
                        break;
                    default:
                        if (shift)
                        {
                            g.DrawString(upper[I].ToString(), Font, Brushes.Black, r.X + 3 + offset + 1, r.Y + 2 + offset + 1);
                            g.DrawString(upper[I].ToString(), Font, Brushes.White, r.X + 3 + offset, r.Y + 2 + offset);

                            if (!char.IsLetter(lower[I]))
                            {
                                pt1 = lowerCache[I];
                                g.DrawString(lower[I].ToString(), Font, b1, pt1.X + offset, pt1.Y + offset);
                            }
                        }
                        else
                        {
                            g.DrawString(lower[I].ToString(), Font, Brushes.Black, r.X + 3 + offset + 1, r.Y + 2 + offset + 1);
                            g.DrawString(lower[I].ToString(), Font, Brushes.White, r.X + 3 + offset, r.Y + 2 + offset);

                            if (!char.IsLetter(upper[I]))
                            {
                                pt1 = upperCache[I];
                                g.DrawString(upper[I].ToString(), Font, b1, pt1.X + offset, pt1.Y + offset);
                            }
                        }
                        break;
                }

                g.DrawRectangle(p2, r.X + 1 + offset, r.Y + 1 + offset, r.Width - 2, r.Height - 2);
                g.DrawRectangle(p3, r.X + offset, r.Y + offset, r.Width, r.Height);

                if (I == pressed)
                {
                    g.DrawLine(p1, r.X, r.Y, r.Right, r.Y);
                    g.DrawLine(p1, r.X, r.Y, r.X, r.Bottom);
                }
            }
        }

        private void DrawArrow(Color color, int rx, int ry)
        {
            Rectangle r = new Rectangle(rx + 8, ry + 8, 16, 16);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen p = new Pen(color, 1);
            AdjustableArrowCap c = new AdjustableArrowCap(3, 2);
            p.CustomEndCap = c;

            g.DrawArc(p, r, 0f, 290f);

            p.Dispose();
            c.Dispose();
            g.SmoothingMode = SmoothingMode.None;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            int index = ((e.Y / 32) * 12) + (e.X / 32);

            if (index > 47)
            {
                for (int I = 48; I <= buttons.Length - 1; I++)
                {
                    if (buttons[I].Contains(e.X, e.Y))
                    {
                        pressed = I;
                        break;
                    }
                }
            }
            else
            {
                pressed = index;
            }

            HandleKey();
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            pressed = -1;
            Invalidate();
        }

        private void HandleKey()
        {
            if (Target == null)
                return;
            if (pressed == -1)
                return;

            switch (pressed)
            {
                case 47:
                    Target.Text = string.Empty;
                    break;
                case 48:
                    shift = !shift;
                    break;
                case 49:
                    Target.Text += " ";
                    break;
                case 50:
                    if (!(Target.Text.Length == 0))
                    {
                        Target.Text = Target.Text.Remove(Target.Text.Length - 1);
                    }
                    break;
                default:
                    if (shift)
                    {
                        Target.Text += upper[pressed];
                    }
                    else
                    {
                        Target.Text += lower[pressed];
                    }
                    break;
            }
        }
    }

    [DefaultEvent("SelectedIndexChanged")]
    internal class NsPaginator : Control
    {
        public delegate void SelectedIndexChangedEventHandler(object sender, EventArgs e);

        private readonly SolidBrush b1;
        private readonly SolidBrush b2;
        private readonly Pen p1;
        private readonly Pen p2;
        private readonly Pen p3;
        private readonly Bitmap textBitmap;
        private readonly Graphics textGraphics;
        private int numberOfPages;
        private int selectedIndex;
        private Graphics g;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private int itemWidth;
        private Point pt1;
        private Rectangle r1;
        private Size sz1;

        public NsPaginator()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            Size = new Size(202, 26);

            textBitmap = new Bitmap(1, 1);
            textGraphics = Graphics.FromImage(textBitmap);

            InvalidateItems();

            b1 = new SolidBrush(Color.FromArgb(50, 50, 50));
            b2 = new SolidBrush(Color.FromArgb(55, 55, 55));

            p1 = new Pen(Color.FromArgb(35, 35, 35));
            p2 = new Pen(Color.FromArgb(55, 55, 55));
            p3 = new Pen(Color.FromArgb(65, 65, 65));
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = Math.Max(Math.Min(value, MaximumIndex), 0);
                Invalidate();
            }
        }

        public int NumberOfPages
        {
            get { return numberOfPages; }
            set
            {
                numberOfPages = value;
                selectedIndex = Math.Max(Math.Min(selectedIndex, MaximumIndex), 0);
                Invalidate();
            }
        }

        public int MaximumIndex
        {
            get { return NumberOfPages - 1; }
        }

        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;

                InvalidateItems();
                Invalidate();
            }
        }

        public event SelectedIndexChangedEventHandler SelectedIndexChanged;

        private void InvalidateItems()
        {
            Size s = textGraphics.MeasureString("000 ..", Font).ToSize();
            itemWidth = s.Width + 10;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            bool leftEllipse = false;
            bool rightEllipse = false;

            if (selectedIndex < 4)
            {
                for (int I = 0; I <= Math.Min(MaximumIndex, 4); I++)
                {
                    rightEllipse = (I == 4) && (MaximumIndex > 4);
                    DrawBox(I * itemWidth, I, false, rightEllipse);
                }
            }
            else if (selectedIndex > 3 && selectedIndex < (MaximumIndex - 3))
            {
                for (int I = 0; I <= 4; I++)
                {
                    leftEllipse = (I == 0);
                    rightEllipse = (I == 4);
                    DrawBox(I * itemWidth, selectedIndex + I - 2, leftEllipse, rightEllipse);
                }
            }
            else
            {
                for (int I = 0; I <= 4; I++)
                {
                    leftEllipse = (I == 0) && (MaximumIndex > 4);
                    DrawBox(I * itemWidth, MaximumIndex - (4 - I), leftEllipse, false);
                }
            }
        }

        private void DrawBox(int x, int index, bool leftEllipse, bool rightEllipse)
        {
            r1 = new Rectangle(x, 0, itemWidth - 4, Height - 1);

            gp1 = ThemeModule.CreateRound(r1, 7);
            gp2 = ThemeModule.CreateRound(r1.X + 1, r1.Y + 1, r1.Width - 2, r1.Height - 2, 7);

            string T = Convert.ToString(index + 1);

            if (leftEllipse)
                T = ".. " + T;
            if (rightEllipse)
                T = T + " ..";

            sz1 = g.MeasureString(T, Font).ToSize();
            pt1 = new Point(r1.X + (r1.Width / 2 - sz1.Width / 2), r1.Y + (r1.Height / 2 - sz1.Height / 2));

            if (index == selectedIndex)
            {
                g.FillPath(b1, gp1);

                Font f = new Font(Font, FontStyle.Underline);
                g.DrawString(T, f, Brushes.Black, pt1.X + 1, pt1.Y + 1);
                g.DrawString(T, f, Brushes.White, pt1);
                f.Dispose();

                g.DrawPath(p1, gp2);
                g.DrawPath(p2, gp1);
            }
            else
            {
                g.FillPath(b2, gp1);

                g.DrawString(T, Font, Brushes.Black, pt1.X + 1, pt1.Y + 1);
                g.DrawString(T, Font, Brushes.White, pt1);

                g.DrawPath(p3, gp2);
                g.DrawPath(p1, gp1);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int newIndex = 0;
                int oldIndex = selectedIndex;

                if (selectedIndex < 4)
                {
                    newIndex = (e.X / itemWidth);
                }
                else if (selectedIndex > 3 && selectedIndex < (MaximumIndex - 3))
                {
                    newIndex = (e.X / itemWidth);

                    switch (newIndex)
                    {
                        case 2:
                            newIndex = oldIndex;
                            break;
                        default:
                            if (newIndex < 2)
                                newIndex = oldIndex - (2 - newIndex);
                            if (newIndex > 2)
                                newIndex = oldIndex + (newIndex - 2);
                            break;
                    }
                }
                else
                {
                    newIndex = MaximumIndex - (4 - (e.X / itemWidth));
                }

                if ((newIndex < numberOfPages) && (!(newIndex == oldIndex)))
                {
                    SelectedIndex = newIndex;
                    if (SelectedIndexChanged != null)
                    {
                        SelectedIndexChanged(this, null);
                    }
                }
            }

            base.OnMouseDown(e);
        }
    }

    [DefaultEvent("Scroll")]
    internal class NsvScrollBar : Control
    {
        public delegate void ScrollEventHandler(object sender);

        private readonly SolidBrush b1;
        private readonly SolidBrush b2;
        private readonly int buttonSize = 16;
        private readonly Pen p1;
        private readonly Pen p2;
        private readonly Pen p3;
        private readonly Pen p4;
        // 14 minimum
        private readonly int thumbSize = 24;
        private int largeChange = 10;
        private int maximum = 100;
        private int minimum;
        private int smallChange = 1;
        private int value;
        private Rectangle bsa;
        private Graphics g;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private GraphicsPath gp3;
        private GraphicsPath gp4;
        private int i1;
        private Rectangle shaft;
        private bool showThumb;
        private Rectangle thumb;
        private bool thumbDown;
        private Rectangle tsa;

        public NsvScrollBar()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            Width = 18;

            b1 = new SolidBrush(Color.FromArgb(55, 55, 55));
            b2 = new SolidBrush(Color.FromArgb(35, 35, 35));

            p1 = new Pen(Color.FromArgb(35, 35, 35));
            p2 = new Pen(Color.FromArgb(65, 65, 65));
            p3 = new Pen(Color.FromArgb(55, 55, 55));
            p4 = new Pen(Color.FromArgb(40, 40, 40));
        }

        public int Minimum
        {
            get { return minimum; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

                minimum = value;
                if (value > this.value)
                    this.value = value;
                if (value > maximum)
                    maximum = value;

                InvalidateLayout();
            }
        }

        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

                maximum = value;
                if (value < this.value)
                    this.value = value;
                if (value < minimum)
                    minimum = value;

                InvalidateLayout();
            }
        }

        public int Value
        {
            get
            {
                if (!showThumb)
                    return minimum;
                return value;
            }
            set
            {
                if (value == this.value)
                    return;

                if (value > maximum || value < minimum)
                {
                    throw new Exception("Property value is not valid.");
                }

                this.value = value;
                InvalidatePosition();

                if (Scroll != null)
                {
                    Scroll(this);
                }
            }
        }

        public int SmallChange
        {
            get { return smallChange; }
            set
            {
                if (value < 1)
                {
                    throw new Exception("Property value is not valid.");
                }

                smallChange = value;
            }
        }

        public int LargeChange
        {
            get { return largeChange; }
            set
            {
                if (value < 1)
                {
                    throw new Exception("Property value is not valid.");
                }

                largeChange = value;
            }
        }

        public event ScrollEventHandler Scroll;

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;
            g.Clear(BackColor);

            gp1 = DrawArrow(4, 6, false);
            gp2 = DrawArrow(5, 7, false);

            g.FillPath(b1, gp2);
            g.FillPath(b2, gp1);

            gp3 = DrawArrow(4, Height - 11, true);
            gp4 = DrawArrow(5, Height - 10, true);

            g.FillPath(b1, gp4);
            g.FillPath(b2, gp3);

            if (showThumb)
            {
                g.FillRectangle(b1, thumb);
                g.DrawRectangle(p1, thumb);
                g.DrawRectangle(p2, thumb.X + 1, thumb.Y + 1, thumb.Width - 2, thumb.Height - 2);

                int y = 0;
                int ly = thumb.Y + (thumb.Height / 2) - 3;

                for (int I = 0; I <= 2; I++)
                {
                    y = ly + (I * 3);

                    g.DrawLine(p1, thumb.X + 5, y, thumb.Right - 5, y);
                    g.DrawLine(p2, thumb.X + 5, y + 1, thumb.Right - 5, y + 1);
                }
            }

            g.DrawRectangle(p3, 0, 0, Width - 1, Height - 1);
            g.DrawRectangle(p4, 1, 1, Width - 3, Height - 3);
        }

        private GraphicsPath DrawArrow(int x, int y, bool flip)
        {
            GraphicsPath gp = new GraphicsPath();

            int w = 9;
            int h = 5;

            if (flip)
            {
                gp.AddLine(x + 1, y, x + w + 1, y);
                gp.AddLine(x + w, y, x + h, y + h - 1);
            }
            else
            {
                gp.AddLine(x, y + h, x + w, y + h);
                gp.AddLine(x + w, y + h, x + h, y);
            }

            gp.CloseFigure();
            return gp;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            InvalidateLayout();
        }

        private void InvalidateLayout()
        {
            tsa = new Rectangle(0, 0, Width, buttonSize);
            bsa = new Rectangle(0, Height - buttonSize, Width, buttonSize);
            shaft = new Rectangle(0, tsa.Bottom + 1, Width, Height - (buttonSize * 2) - 1);

            showThumb = ((maximum - minimum) > shaft.Height);

            if (showThumb)
            {
                //ThumbSize = Math.Max(0, 14) 'TODO: Implement this.
                thumb = new Rectangle(1, 0, Width - 3, thumbSize);
            }

            if (Scroll != null)
            {
                Scroll(this);
            }
            InvalidatePosition();
        }

        private void InvalidatePosition()
        {
            thumb.Y = Convert.ToInt32(GetProgress() * (shaft.Height - thumbSize)) + tsa.Height;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && showThumb)
            {
                if (tsa.Contains(e.Location))
                {
                    i1 = value - smallChange;
                }
                else if (bsa.Contains(e.Location))
                {
                    i1 = value + smallChange;
                }
                else
                {
                    if (thumb.Contains(e.Location))
                    {
                        thumbDown = true;
                        return;
                    }
                    if (e.Y < thumb.Y)
                    {
                        i1 = value - largeChange;
                    }
                    else
                    {
                        i1 = value + largeChange;
                    }
                }

                Value = Math.Min(Math.Max(i1, minimum), maximum);
                InvalidatePosition();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (thumbDown && showThumb)
            {
                int thumbPosition = e.Y - tsa.Height - (thumbSize / 2);
                int thumbBounds = shaft.Height - thumbSize;

                i1 = Convert.ToInt32((thumbPosition / thumbBounds) * (maximum - minimum)) + minimum;

                Value = Math.Min(Math.Max(i1, minimum), maximum);
                InvalidatePosition();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            thumbDown = false;
        }

        private double GetProgress()
        {
            return (value - minimum) / (maximum - minimum);
        }
    }

    [DefaultEvent("Scroll")]
    internal class NshScrollBar : Control
    {
        public delegate void ScrollEventHandler(object sender);

        private readonly SolidBrush b1;
        private readonly SolidBrush b2;
        private readonly int buttonSize = 16;
        private readonly Pen p1;
        private readonly Pen p2;
        private readonly Pen p3;
        private readonly Pen p4;
        // 14 minimum
        private readonly int thumbSize = 24;
        private int largeChange = 10;
        private int maximum = 100;
        private int minimum;
        private int smallChange = 1;
        private int value;
        private Graphics g;
        private GraphicsPath gp1;
        private GraphicsPath gp2;
        private GraphicsPath gp3;
        private GraphicsPath gp4;
        private int i1;
        private Rectangle lsa;
        private Rectangle rsa;
        private Rectangle shaft;
        private bool showThumb;
        private Rectangle thumb;
        private bool thumbDown;

        public NshScrollBar()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            Height = 18;

            b1 = new SolidBrush(Color.FromArgb(55, 55, 55));
            b2 = new SolidBrush(Color.FromArgb(35, 35, 35));

            p1 = new Pen(Color.FromArgb(35, 35, 35));
            p2 = new Pen(Color.FromArgb(65, 65, 65));
            p3 = new Pen(Color.FromArgb(55, 55, 55));
            p4 = new Pen(Color.FromArgb(40, 40, 40));
        }

        public int Minimum
        {
            get { return minimum; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

                minimum = value;
                if (value > this.value)
                    this.value = value;
                if (value > maximum)
                    maximum = value;

                InvalidateLayout();
            }
        }

        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

                maximum = value;
                if (value < this.value)
                    this.value = value;
                if (value < minimum)
                    minimum = value;

                InvalidateLayout();
            }
        }

        public int Value
        {
            get
            {
                if (!showThumb)
                    return minimum;
                return value;
            }
            set
            {
                if (value == this.value)
                    return;

                if (value > maximum || value < minimum)
                {
                    throw new Exception("Property value is not valid.");
                }

                this.value = value;
                InvalidatePosition();

                if (Scroll != null)
                {
                    Scroll(this);
                }
            }
        }

        public int SmallChange
        {
            get { return smallChange; }
            set
            {
                if (value < 1)
                {
                    throw new Exception("Property value is not valid.");
                }

                smallChange = value;
            }
        }

        public int LargeChange
        {
            get { return largeChange; }
            set
            {
                if (value < 1)
                {
                    throw new Exception("Property value is not valid.");
                }

                largeChange = value;
            }
        }

        public event ScrollEventHandler Scroll;

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;
            g.Clear(BackColor);

            gp1 = DrawArrow(6, 4, false);
            gp2 = DrawArrow(7, 5, false);

            g.FillPath(b1, gp2);
            g.FillPath(b2, gp1);

            gp3 = DrawArrow(Width - 11, 4, true);
            gp4 = DrawArrow(Width - 10, 5, true);

            g.FillPath(b1, gp4);
            g.FillPath(b2, gp3);

            if (showThumb)
            {
                g.FillRectangle(b1, thumb);
                g.DrawRectangle(p1, thumb);
                g.DrawRectangle(p2, thumb.X + 1, thumb.Y + 1, thumb.Width - 2, thumb.Height - 2);

                int x = 0;
                int lx = thumb.X + (thumb.Width / 2) - 3;

                for (int I = 0; I <= 2; I++)
                {
                    x = lx + (I * 3);

                    g.DrawLine(p1, x, thumb.Y + 5, x, thumb.Bottom - 5);
                    g.DrawLine(p2, x + 1, thumb.Y + 5, x + 1, thumb.Bottom - 5);
                }
            }

            g.DrawRectangle(p3, 0, 0, Width - 1, Height - 1);
            g.DrawRectangle(p4, 1, 1, Width - 3, Height - 3);
        }

        private GraphicsPath DrawArrow(int x, int y, bool flip)
        {
            GraphicsPath gp = new GraphicsPath();

            int w = 5;
            int h = 9;

            if (flip)
            {
                gp.AddLine(x, y + 1, x, y + h + 1);
                gp.AddLine(x, y + h, x + w - 1, y + w);
            }
            else
            {
                gp.AddLine(x + w, y, x + w, y + h);
                gp.AddLine(x + w, y + h, x + 1, y + w);
            }

            gp.CloseFigure();
            return gp;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            InvalidateLayout();
        }

        private void InvalidateLayout()
        {
            lsa = new Rectangle(0, 0, buttonSize, Height);
            rsa = new Rectangle(Width - buttonSize, 0, buttonSize, Height);
            shaft = new Rectangle(lsa.Right + 1, 0, Width - (buttonSize * 2) - 1, Height);

            showThumb = ((maximum - minimum) > shaft.Width);

            if (showThumb)
            {
                //ThumbSize = Math.Max(0, 14) 'TODO: Implement this.
                thumb = new Rectangle(0, 1, thumbSize, Height - 3);
            }

            if (Scroll != null)
            {
                Scroll(this);
            }
            InvalidatePosition();
        }

        private void InvalidatePosition()
        {
            thumb.X = Convert.ToInt32(GetProgress() * (shaft.Width - thumbSize)) + lsa.Width;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && showThumb)
            {
                if (lsa.Contains(e.Location))
                {
                    i1 = value - smallChange;
                }
                else if (rsa.Contains(e.Location))
                {
                    i1 = value + smallChange;
                }
                else
                {
                    if (thumb.Contains(e.Location))
                    {
                        thumbDown = true;
                        return;
                    }
                    if (e.X < thumb.X)
                    {
                        i1 = value - largeChange;
                    }
                    else
                    {
                        i1 = value + largeChange;
                    }
                }

                Value = Math.Min(Math.Max(i1, minimum), maximum);
                InvalidatePosition();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (thumbDown && showThumb)
            {
                int thumbPosition = e.X - lsa.Width - (thumbSize / 2);
                int thumbBounds = shaft.Width - thumbSize;

                i1 = Convert.ToInt32((thumbPosition / thumbBounds) * (maximum - minimum)) + minimum;

                Value = Math.Min(Math.Max(i1, minimum), maximum);
                InvalidatePosition();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            thumbDown = false;
        }

        private double GetProgress()
        {
            return (value - minimum) / (maximum - minimum);
        }
    }

    internal class Test : Control
    {
        public  ICollection<ColumnHeader> Columns { get; } = new List<ColumnHeader>();
        public IEnumerable<object> Items { get; } = new List<object>();

        private Pen innerBorderPen;
        private Pen outerBorderPen;

        public Test()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);
            Font = new Font("Verdana", 8.25f);

            outerBorderPen = new Pen(Color.FromArgb(35, 35, 35));
            innerBorderPen = new Pen(Color.FromArgb(55, 55, 55));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.HighQuality;

            // Headers
            int x = 1, y = 1;
            foreach (var column in Columns)
            {
                LinearGradientBrush bgBrush = new LinearGradientBrush(new Rectangle(x, y, column.Width, 25), Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
                g.FillRectangle(bgBrush, new Rectangle(x, y, column.Width, 25));
                //g.DrawLine(outerBorderPen, x + column.Width - 1, y, x + column.Width - 1, y + 25);

                g.DrawString(column.Text, new Font(Font, FontStyle.Bold), Brushes.White, new Point(x + 5, 5));

                x += column.Width;
            }


            // Borders
            GraphicsPath outerBorderPath = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
            GraphicsPath innerBorderPath = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

            g.DrawPath(outerBorderPen, outerBorderPath);
            g.DrawPath(innerBorderPen, innerBorderPath);

            // Horizontal Header Line
            g.DrawLine(outerBorderPen, 0, y + 26, Width - 1, y + 26);

            // Vertical Header Lines
            x = 1;
            foreach (var column in Columns)
            {
                g.DrawLine(outerBorderPen, x + column.Width - 1, y, x + column.Width - 1, y + 25);
                x += column.Width;
            }
        }
    }

    internal class NsContextMenu : ContextMenuStrip
    {
        public NsContextMenu()
        {
            Renderer = new ToolStripProfessionalRenderer(new NsColorTable());
            ForeColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
        }
    }

    internal class NsColorTable : ProfessionalColorTable
    {
        private readonly Color backColor = Color.FromArgb(55, 55, 55);

        public override Color ButtonSelectedBorder
        {
            get { return backColor; }
        }

        public override Color CheckBackground
        {
            get { return backColor; }
        }

        public override Color CheckPressedBackground
        {
            get { return backColor; }
        }

        public override Color CheckSelectedBackground
        {
            get { return backColor; }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return backColor; }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return backColor; }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return backColor; }
        }

        public override Color MenuBorder
        {
            get { return Color.FromArgb(25, 25, 25); }
        }

        public override Color MenuItemBorder
        {
            get { return backColor; }
        }

        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(65, 65, 65); }
        }

        public override Color SeparatorDark
        {
            get { return Color.FromArgb(35, 35, 35); }
        }

        public override Color ToolStripDropDownBackground
        {
            get { return backColor; }
        }

        private abstract class ThemeContainer154 : ContainerControl
        {
            private bool hasShown;

            private void DoAnimation(bool i)
            {
                OnAnimation();
                if (i)
                    Invalidate();
            }

            protected override sealed void OnPaint(PaintEventArgs e)
            {
                if (Width == 0 || Height == 0)
                    return;

                if (transparent && controlMode)
                {
                    PaintHook();
                    e.Graphics.DrawImage(B, 0, 0);
                }
                else
                {
                    G = e.Graphics;
                    PaintHook();
                }
            }

            protected override void OnHandleDestroyed(EventArgs e)
            {
                ThemeShare.RemoveAnimationCallback(DoAnimation);
                base.OnHandleDestroyed(e);
            }

            private void FormShown(object sender, EventArgs e)
            {
                if (controlMode || hasShown)
                    return;

                if (startPosition == FormStartPosition.CenterParent || startPosition == FormStartPosition.CenterScreen)
                {
                    Rectangle sb = Screen.PrimaryScreen.Bounds;
                    Rectangle cb = ParentForm.Bounds;
                    ParentForm.Location = new Point(sb.Width / 2 - cb.Width / 2, sb.Height / 2 - cb.Width / 2);
                }

                hasShown = true;
            }

            #region " Initialization "

            protected Graphics G;

            protected Bitmap B;

            public ThemeContainer154()
            {
                SetStyle((ControlStyles)139270, true);

                ImageSize = Size.Empty;
                Font = new Font("Verdana", 8);

                measureBitmap = new Bitmap(1, 1);
                measureGraphics = Graphics.FromImage(measureBitmap);

                drawRadialPath = new GraphicsPath();

                InvalidateCustimization();
            }

            protected override sealed void OnHandleCreated(EventArgs e)
            {
                if (doneCreation)
                    InitializeMessages();

                InvalidateCustimization();
                ColorHook();

                if (!(lockWidth == 0))
                    Width = lockWidth;
                if (!(lockHeight == 0))
                    Height = lockHeight;
                if (!controlMode)
                    base.Dock = DockStyle.Fill;

                Transparent = transparent;
                if (transparent && backColor)
                    BackColor = Color.Transparent;

                base.OnHandleCreated(e);
            }

            private bool doneCreation;

            protected override sealed void OnParentChanged(EventArgs e)
            {
                base.OnParentChanged(e);

                if (Parent == null)
                    return;
                IsParentForm = Parent is Form;

                if (!controlMode)
                {
                    InitializeMessages();

                    if (IsParentForm)
                    {
                        ParentForm.FormBorderStyle = borderStyle;
                        ParentForm.TransparencyKey = transparencyKey;

                        if (!DesignMode)
                        {
                            ParentForm.Shown += FormShown;
                        }
                    }

                    Parent.BackColor = BackColor;
                }

                OnCreation();
                doneCreation = true;
                InvalidateTimer();
            }

            #endregion

            #region " Size Handling "

            private Rectangle frame;

            protected override sealed void OnSizeChanged(EventArgs e)
            {
                if (Movable && !controlMode)
                {
                    frame = new Rectangle(7, 7, Width - 14, header - 7);
                }

                InvalidateBitmap();
                Invalidate();

                base.OnSizeChanged(e);
            }

            protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
            {
                if (lockWidth != 0)
                    width = lockWidth;
                if (lockHeight != 0)
                    height = lockHeight;
                base.SetBoundsCore(x, y, width, height, specified);
            }

            #endregion

            #region " State Handling "

            protected MouseState State;

            private void SetState(MouseState current)
            {
                State = current;
                Invalidate();
            }

            protected override void OnMouseMove(MouseEventArgs e)
            {
                if (!(IsParentForm && ParentForm.WindowState == FormWindowState.Maximized))
                {
                    if (Sizable && !controlMode)
                        InvalidateMouse();
                }

                base.OnMouseMove(e);
            }

            protected override void OnEnabledChanged(EventArgs e)
            {
                if (Enabled)
                    SetState(MouseState.None);
                else
                    SetState(MouseState.Block);
                base.OnEnabledChanged(e);
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                SetState(MouseState.Over);
                base.OnMouseEnter(e);
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                SetState(MouseState.Over);
                base.OnMouseUp(e);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                SetState(MouseState.None);

                if (GetChildAtPoint(PointToClient(MousePosition)) != null)
                {
                    if (Sizable && !controlMode)
                    {
                        Cursor = Cursors.Default;
                        previous = 0;
                    }
                }

                base.OnMouseLeave(e);
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                    SetState(MouseState.Down);

                if (!(IsParentForm && ParentForm.WindowState == FormWindowState.Maximized || controlMode))
                {
                    if (Movable && frame.Contains(e.Location))
                    {
                        Capture = false;
                        wmLmbuttondown = true;
                        DefWndProc(ref messages[0]);
                    }
                    else if (Sizable && previous != 0)
                    {
                        Capture = false;
                        wmLmbuttondown = true;
                        DefWndProc(ref messages[previous]);
                    }
                }

                base.OnMouseDown(e);
            }

            private bool wmLmbuttondown;

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (wmLmbuttondown && m.Msg == 513)
                {
                    wmLmbuttondown = false;

                    SetState(MouseState.Over);
                    if (!SmartBounds)
                        return;

                    if (IsParentMdi)
                    {
                        CorrectBounds(new Rectangle(Point.Empty, Parent.Parent.Size));
                    }
                    else
                    {
                        CorrectBounds(Screen.FromControl(Parent).WorkingArea);
                    }
                }
            }

            private Point getIndexPoint;
            private bool b1;
            private bool b2;
            private bool b3;
            private bool b4;

            private int GetIndex()
            {
                getIndexPoint = PointToClient(MousePosition);
                b1 = getIndexPoint.X < 7;
                b2 = getIndexPoint.X > Width - 7;
                b3 = getIndexPoint.Y < 7;
                b4 = getIndexPoint.Y > Height - 7;

                if (b1 && b3)
                    return 4;
                if (b1 && b4)
                    return 7;
                if (b2 && b3)
                    return 5;
                if (b2 && b4)
                    return 8;
                if (b1)
                    return 1;
                if (b2)
                    return 2;
                if (b3)
                    return 3;
                if (b4)
                    return 6;
                return 0;
            }

            private int current;
            private int previous;

            private void InvalidateMouse()
            {
                current = GetIndex();
                if (current == previous)
                    return;

                previous = current;
                switch (previous)
                {
                    case 0:
                        Cursor = Cursors.Default;
                        break;
                    case 1:
                    case 2:
                        Cursor = Cursors.SizeWE;
                        break;
                    case 3:
                    case 6:
                        Cursor = Cursors.SizeNS;
                        break;
                    case 4:
                    case 8:
                        Cursor = Cursors.SizeNWSE;
                        break;
                    case 5:
                    case 7:
                        Cursor = Cursors.SizeNESW;
                        break;
                }
            }

            private readonly Message[] messages = new Message[9];

            private void InitializeMessages()
            {
                messages[0] = Message.Create(Parent.Handle, 161, new IntPtr(2), IntPtr.Zero);
                for (int I = 1; I <= 8; I++)
                {
                    messages[I] = Message.Create(Parent.Handle, 161, new IntPtr(I + 9), IntPtr.Zero);
                }
            }

            private void CorrectBounds(Rectangle bounds)
            {
                if (Parent.Width > bounds.Width)
                    Parent.Width = bounds.Width;
                if (Parent.Height > bounds.Height)
                    Parent.Height = bounds.Height;

                int x = Parent.Location.X;
                int y = Parent.Location.Y;

                if (x < bounds.X)
                    x = bounds.X;
                if (y < bounds.Y)
                    y = bounds.Y;

                int width = bounds.X + bounds.Width;
                int height = bounds.Y + bounds.Height;

                if (x + Parent.Width > width)
                    x = width - Parent.Width;
                if (y + Parent.Height > height)
                    y = height - Parent.Height;

                Parent.Location = new Point(x, y);
            }

            #endregion

            #region " Base Properties "

            public override DockStyle Dock
            {
                get { return base.Dock; }
                set
                {
                    if (!controlMode)
                        return;
                    base.Dock = value;
                }
            }

            private bool backColor;

            [Category("Misc")]
            public override Color BackColor
            {
                get { return base.BackColor; }
                set
                {
                    if (value == base.BackColor)
                        return;

                    if (!IsHandleCreated && controlMode && value == Color.Transparent)
                    {
                        backColor = true;
                        return;
                    }

                    base.BackColor = value;
                    if (Parent != null)
                    {
                        if (!controlMode)
                            Parent.BackColor = value;
                        ColorHook();
                    }
                }
            }

            public override Size MinimumSize
            {
                get { return base.MinimumSize; }
                set
                {
                    base.MinimumSize = value;
                    if (Parent != null)
                        Parent.MinimumSize = value;
                }
            }

            public override Size MaximumSize
            {
                get { return base.MaximumSize; }
                set
                {
                    base.MaximumSize = value;
                    if (Parent != null)
                        Parent.MaximumSize = value;
                }
            }

            public override string Text
            {
                get { return base.Text; }
                set
                {
                    base.Text = value;
                    Invalidate();
                }
            }

            public override Font Font
            {
                get { return base.Font; }
                set
                {
                    base.Font = value;
                    Invalidate();
                }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override Color ForeColor
            {
                get { return Color.Empty; }
                set { }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override Image BackgroundImage
            {
                get { return null; }
                set { }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override ImageLayout BackgroundImageLayout
            {
                get { return ImageLayout.None; }
                set { }
            }

            #endregion

            #region " Public Properties "

            public bool SmartBounds { get; } = true;

            public bool Movable { get; set; } = true;

            public bool Sizable { get; set; } = true;

            private Color transparencyKey;

            public Color TransparencyKey
            {
                get
                {
                    if (IsParentForm && !controlMode)
                        return ParentForm.TransparencyKey;
                    return transparencyKey;
                }
                set
                {
                    if (value == transparencyKey)
                        return;
                    transparencyKey = value;

                    if (IsParentForm && !controlMode)
                    {
                        ParentForm.TransparencyKey = value;
                        ColorHook();
                    }
                }
            }

            private FormBorderStyle borderStyle;

            public FormBorderStyle BorderStyle
            {
                get
                {
                    if (IsParentForm && !controlMode)
                        return ParentForm.FormBorderStyle;
                    return borderStyle;
                }
                set
                {
                    borderStyle = value;

                    if (IsParentForm && !controlMode)
                    {
                        ParentForm.FormBorderStyle = value;

                        if (value != FormBorderStyle.None)
                        {
                            Movable = false;
                            Sizable = false;
                        }
                    }
                }
            }

            private FormStartPosition startPosition;

            public FormStartPosition StartPosition
            {
                get
                {
                    if (IsParentForm && !controlMode)
                        return ParentForm.StartPosition;
                    return startPosition;
                }
                set
                {
                    startPosition = value;

                    if (IsParentForm && !controlMode)
                    {
                        ParentForm.StartPosition = value;
                    }
                }
            }

            private bool noRounding;

            public bool NoRounding
            {
                get { return noRounding; }
                set
                {
                    noRounding = value;
                    Invalidate();
                }
            }

            private Image image;

            public Image Image
            {
                get { return image; }
                set
                {
                    if (value == null)
                        ImageSize = Size.Empty;
                    else
                        ImageSize = value.Size;

                    image = value;
                    Invalidate();
                }
            }

            private readonly Dictionary<string, Color> items = new Dictionary<string, Color>();

            public Bloom[] Colors
            {
                get
                {
                    List<Bloom> T = new List<Bloom>();
                    Dictionary<string, Color>.Enumerator e = items.GetEnumerator();

                    while (e.MoveNext())
                    {
                        T.Add(new Bloom(e.Current.Key, e.Current.Value));
                    }

                    return T.ToArray();
                }
                set
                {
                    foreach (Bloom b in value)
                    {
                        if (items.ContainsKey(b.Name))
                            items[b.Name] = b.Value;
                    }

                    InvalidateCustimization();
                    ColorHook();
                    Invalidate();
                }
            }

            private string customization;

            public string Customization
            {
                get { return customization; }
                set
                {
                    if (value == customization)
                        return;

                    byte[] data = null;
                    Bloom[] items = Colors;

                    try
                    {
                        data = Convert.FromBase64String(value);
                        for (int I = 0; I <= items.Length - 1; I++)
                        {
                            items[I].Value = Color.FromArgb(BitConverter.ToInt32(data, I * 4));
                        }
                    }
                    catch
                    {
                        return;
                    }

                    customization = value;

                    Colors = items;
                    ColorHook();
                    Invalidate();
                }
            }

            private bool transparent;

            public bool Transparent
            {
                get { return transparent; }
                set
                {
                    transparent = value;
                    if (!(IsHandleCreated || controlMode))
                        return;

                    if (!value && BackColor.A != 255)
                    {
                        throw new Exception("Unable to change value to false while a transparent BackColor is in use.");
                    }

                    SetStyle(ControlStyles.Opaque, !value);
                    SetStyle(ControlStyles.SupportsTransparentBackColor, value);

                    InvalidateBitmap();
                    Invalidate();
                }
            }

            #endregion

            #region " Private Properties "

            protected Size ImageSize { get; private set; }

            protected bool IsParentForm { get; private set; }

            protected bool IsParentMdi => Parent?.Parent != null;

            private int lockWidth;

            protected int LockWidth
            {
                get { return lockWidth; }
                set
                {
                    lockWidth = value;
                    if (LockWidth != 0 && IsHandleCreated)
                        Width = LockWidth;
                }
            }

            private int lockHeight;

            protected int LockHeight
            {
                get { return lockHeight; }
                set
                {
                    lockHeight = value;
                    if (LockHeight != 0 && IsHandleCreated)
                        Height = LockHeight;
                }
            }

            private int header = 24;

            protected int Header
            {
                get { return header; }
                set
                {
                    header = value;

                    if (!controlMode)
                    {
                        frame = new Rectangle(7, 7, Width - 14, value - 7);
                        Invalidate();
                    }
                }
            }

            private bool controlMode;

            protected bool ControlMode
            {
                get { return controlMode; }
                set
                {
                    controlMode = value;

                    Transparent = transparent;
                    if (transparent && backColor)
                        BackColor = Color.Transparent;

                    InvalidateBitmap();
                    Invalidate();
                }
            }

            private bool isAnimated;

            protected bool IsAnimated
            {
                get { return isAnimated; }
                set
                {
                    isAnimated = value;
                    InvalidateTimer();
                }
            }

            #endregion

            #region " Property Helpers "

            protected Pen GetPen(string name)
            {
                return new Pen(items[name]);
            }

            protected Pen GetPen(string name, float width)
            {
                return new Pen(items[name], width);
            }

            protected SolidBrush GetBrush(string name)
            {
                return new SolidBrush(items[name]);
            }

            protected Color GetColor(string name)
            {
                return items[name];
            }

            protected void SetColor(string name, Color value)
            {
                if (items.ContainsKey(name))
                    items[name] = value;
                else
                    items.Add(name, value);
            }

            protected void SetColor(string name, byte r, byte g, byte b)
            {
                SetColor(name, Color.FromArgb(r, g, b));
            }

            protected void SetColor(string name, byte a, byte r, byte g, byte b)
            {
                SetColor(name, Color.FromArgb(a, r, g, b));
            }

            protected void SetColor(string name, byte a, Color value)
            {
                SetColor(name, Color.FromArgb(a, value));
            }

            private void InvalidateBitmap()
            {
                if (transparent && controlMode)
                {
                    if (Width == 0 || Height == 0)
                        return;
                    B = new Bitmap(Width, Height, PixelFormat.Format32bppPArgb);
                    G = Graphics.FromImage(B);
                }
                else
                {
                    G = null;
                    B = null;
                }
            }

            private void InvalidateCustimization()
            {
                MemoryStream m = new MemoryStream(items.Count * 4);

                foreach (Bloom b in Colors)
                {
                    m.Write(BitConverter.GetBytes(b.Value.ToArgb()), 0, 4);
                }

                m.Close();
                customization = Convert.ToBase64String(m.ToArray());
            }

            private void InvalidateTimer()
            {
                if (DesignMode || !doneCreation)
                    return;

                if (isAnimated)
                {
                    ThemeShare.AddAnimationCallback(DoAnimation);
                }
                else
                {
                    ThemeShare.RemoveAnimationCallback(DoAnimation);
                }
            }

            #endregion

            #region " User Hooks "

            protected abstract void ColorHook();
            protected abstract void PaintHook();

            protected virtual void OnCreation()
            {
            }

            protected virtual void OnAnimation()
            {
            }

            #endregion

            #region " Offset "

            private Rectangle offsetReturnRectangle;

            protected Rectangle Offset(Rectangle r, int amount)
            {
                offsetReturnRectangle = new Rectangle(r.X + amount, r.Y + amount, r.Width - (amount * 2), r.Height - (amount * 2));
                return offsetReturnRectangle;
            }

            private Size offsetReturnSize;

            protected Size Offset(Size s, int amount)
            {
                offsetReturnSize = new Size(s.Width + amount, s.Height + amount);
                return offsetReturnSize;
            }

            private Point offsetReturnPoint;

            protected Point Offset(Point p, int amount)
            {
                offsetReturnPoint = new Point(p.X + amount, p.Y + amount);
                return offsetReturnPoint;
            }

            #endregion

            #region " Center "

            private Point centerReturn;

            protected Point Center(Rectangle p, Rectangle c)
            {
                centerReturn = new Point((p.Width / 2 - c.Width / 2) + p.X + c.X, (p.Height / 2 - c.Height / 2) + p.Y + c.Y);
                return centerReturn;
            }

            protected Point Center(Rectangle p, Size c)
            {
                centerReturn = new Point((p.Width / 2 - c.Width / 2) + p.X, (p.Height / 2 - c.Height / 2) + p.Y);
                return centerReturn;
            }

            protected Point Center(Rectangle child)
            {
                return Center(Width, Height, child.Width, child.Height);
            }

            protected Point Center(Size child)
            {
                return Center(Width, Height, child.Width, child.Height);
            }

            protected Point Center(int childWidth, int childHeight)
            {
                return Center(Width, Height, childWidth, childHeight);
            }

            protected Point Center(Size p, Size c)
            {
                return Center(p.Width, p.Height, c.Width, c.Height);
            }

            protected Point Center(int pWidth, int pHeight, int cWidth, int cHeight)
            {
                centerReturn = new Point(pWidth / 2 - cWidth / 2, pHeight / 2 - cHeight / 2);
                return centerReturn;
            }

            #endregion

            #region " Measure "

            private readonly Bitmap measureBitmap;

            private readonly Graphics measureGraphics;

            protected Size Measure()
            {
                lock (measureGraphics)
                {
                    return measureGraphics.MeasureString(Text, Font, Width).ToSize();
                }
            }

            protected Size Measure(string text)
            {
                lock (measureGraphics)
                {
                    return measureGraphics.MeasureString(text, Font, Width).ToSize();
                }
            }

            #endregion

            #region " DrawPixel "

            private SolidBrush drawPixelBrush;

            protected void DrawPixel(Color c1, int x, int y)
            {
                if (transparent)
                {
                    B.SetPixel(x, y, c1);
                }
                else
                {
                    drawPixelBrush = new SolidBrush(c1);
                    G.FillRectangle(drawPixelBrush, x, y, 1, 1);
                }
            }

            #endregion

            #region " DrawCorners "

            private SolidBrush drawCornersBrush;

            protected void DrawCorners(Color c1, int offset)
            {
                DrawCorners(c1, 0, 0, Width, Height, offset);
            }

            protected void DrawCorners(Color c1, Rectangle r1, int offset)
            {
                DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset);
            }

            protected void DrawCorners(Color c1, int x, int y, int width, int height, int offset)
            {
                DrawCorners(c1, x + offset, y + offset, width - (offset * 2), height - (offset * 2));
            }

            protected void DrawCorners(Color c1)
            {
                DrawCorners(c1, 0, 0, Width, Height);
            }

            protected void DrawCorners(Color c1, Rectangle r1)
            {
                DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height);
            }

            protected void DrawCorners(Color c1, int x, int y, int width, int height)
            {
                if (noRounding)
                    return;

                if (transparent)
                {
                    B.SetPixel(x, y, c1);
                    B.SetPixel(x + (width - 1), y, c1);
                    B.SetPixel(x, y + (height - 1), c1);
                    B.SetPixel(x + (width - 1), y + (height - 1), c1);
                }
                else
                {
                    drawCornersBrush = new SolidBrush(c1);
                    G.FillRectangle(drawCornersBrush, x, y, 1, 1);
                    G.FillRectangle(drawCornersBrush, x + (width - 1), y, 1, 1);
                    G.FillRectangle(drawCornersBrush, x, y + (height - 1), 1, 1);
                    G.FillRectangle(drawCornersBrush, x + (width - 1), y + (height - 1), 1, 1);
                }
            }

            #endregion

            #region " DrawBorders "

            protected void DrawBorders(Pen p1, int offset)
            {
                DrawBorders(p1, 0, 0, Width, Height, offset);
            }

            protected void DrawBorders(Pen p1, Rectangle r, int offset)
            {
                DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset);
            }

            protected void DrawBorders(Pen p1, int x, int y, int width, int height, int offset)
            {
                DrawBorders(p1, x + offset, y + offset, width - (offset * 2), height - (offset * 2));
            }

            protected void DrawBorders(Pen p1)
            {
                DrawBorders(p1, 0, 0, Width, Height);
            }

            protected void DrawBorders(Pen p1, Rectangle r)
            {
                DrawBorders(p1, r.X, r.Y, r.Width, r.Height);
            }

            protected void DrawBorders(Pen p1, int x, int y, int width, int height)
            {
                G.DrawRectangle(p1, x, y, width - 1, height - 1);
            }

            #endregion

            #region " DrawText "

            private Point drawTextPoint;

            private Size drawTextSize;

            protected void DrawText(Brush b1, HorizontalAlignment a, int x, int y)
            {
                DrawText(b1, Text, a, x, y);
            }

            protected void DrawText(Brush b1, string text, HorizontalAlignment a, int x, int y)
            {
                if (text.Length == 0)
                    return;

                drawTextSize = Measure(text);
                drawTextPoint = new Point(Width / 2 - drawTextSize.Width / 2, Header / 2 - drawTextSize.Height / 2);

                switch (a)
                {
                    case HorizontalAlignment.Left:
                        G.DrawString(text, Font, b1, x, drawTextPoint.Y + y);
                        break;
                    case HorizontalAlignment.Center:
                        G.DrawString(text, Font, b1, drawTextPoint.X + x, drawTextPoint.Y + y);
                        break;
                    case HorizontalAlignment.Right:
                        G.DrawString(text, Font, b1, Width - drawTextSize.Width - x, drawTextPoint.Y + y);
                        break;
                }
            }

            protected void DrawText(Brush b1, Point p1)
            {
                if (Text.Length == 0)
                    return;
                G.DrawString(Text, Font, b1, p1);
            }

            protected void DrawText(Brush b1, int x, int y)
            {
                if (Text.Length == 0)
                    return;
                G.DrawString(Text, Font, b1, x, y);
            }

            #endregion

            #region " DrawImage "

            private Point drawImagePoint;

            protected void DrawImage(HorizontalAlignment a, int x, int y)
            {
                DrawImage(image, a, x, y);
            }

            protected void DrawImage(Image image, HorizontalAlignment a, int x, int y)
            {
                if (image == null)
                    return;
                drawImagePoint = new Point(Width / 2 - image.Width / 2, Header / 2 - image.Height / 2);

                switch (a)
                {
                    case HorizontalAlignment.Left:
                        G.DrawImage(image, x, drawImagePoint.Y + y, image.Width, image.Height);
                        break;
                    case HorizontalAlignment.Center:
                        G.DrawImage(image, drawImagePoint.X + x, drawImagePoint.Y + y, image.Width, image.Height);
                        break;
                    case HorizontalAlignment.Right:
                        G.DrawImage(image, Width - image.Width - x, drawImagePoint.Y + y, image.Width, image.Height);
                        break;
                }
            }

            protected void DrawImage(Point p1)
            {
                DrawImage(image, p1.X, p1.Y);
            }

            protected void DrawImage(int x, int y)
            {
                DrawImage(image, x, y);
            }

            protected void DrawImage(Image image, Point p1)
            {
                DrawImage(image, p1.X, p1.Y);
            }

            protected void DrawImage(Image image, int x, int y)
            {
                if (image == null)
                    return;
                G.DrawImage(image, x, y, image.Width, image.Height);
            }

            #endregion

            #region " DrawGradient "

            private LinearGradientBrush drawGradientBrush;

            private Rectangle drawGradientRectangle;

            protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height)
            {
                drawGradientRectangle = new Rectangle(x, y, width, height);
                DrawGradient(blend, drawGradientRectangle);
            }

            protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height, float angle)
            {
                drawGradientRectangle = new Rectangle(x, y, width, height);
                DrawGradient(blend, drawGradientRectangle, angle);
            }

            protected void DrawGradient(ColorBlend blend, Rectangle r)
            {
                drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, 90f);
                drawGradientBrush.InterpolationColors = blend;
                G.FillRectangle(drawGradientBrush, r);
            }

            protected void DrawGradient(ColorBlend blend, Rectangle r, float angle)
            {
                drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, angle);
                drawGradientBrush.InterpolationColors = blend;
                G.FillRectangle(drawGradientBrush, r);
            }


            protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height)
            {
                drawGradientRectangle = new Rectangle(x, y, width, height);
                DrawGradient(c1, c2, drawGradientRectangle);
            }

            protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height, float angle)
            {
                drawGradientRectangle = new Rectangle(x, y, width, height);
                DrawGradient(c1, c2, drawGradientRectangle, angle);
            }

            protected void DrawGradient(Color c1, Color c2, Rectangle r)
            {
                drawGradientBrush = new LinearGradientBrush(r, c1, c2, 90f);
                G.FillRectangle(drawGradientBrush, r);
            }

            protected void DrawGradient(Color c1, Color c2, Rectangle r, float angle)
            {
                drawGradientBrush = new LinearGradientBrush(r, c1, c2, angle);
                G.FillRectangle(drawGradientBrush, r);
            }

            #endregion

            #region " DrawRadial "

            private readonly GraphicsPath drawRadialPath;
            private PathGradientBrush drawRadialBrush1;
            private LinearGradientBrush drawRadialBrush2;

            private Rectangle drawRadialRectangle;

            public void DrawRadial(ColorBlend blend, int x, int y, int width, int height)
            {
                drawRadialRectangle = new Rectangle(x, y, width, height);
                DrawRadial(blend, drawRadialRectangle, width / 2, height / 2);
            }

            public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, Point center)
            {
                drawRadialRectangle = new Rectangle(x, y, width, height);
                DrawRadial(blend, drawRadialRectangle, center.X, center.Y);
            }

            public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, int cx, int cy)
            {
                drawRadialRectangle = new Rectangle(x, y, width, height);
                DrawRadial(blend, drawRadialRectangle, cx, cy);
            }

            public void DrawRadial(ColorBlend blend, Rectangle r)
            {
                DrawRadial(blend, r, r.Width / 2, r.Height / 2);
            }

            public void DrawRadial(ColorBlend blend, Rectangle r, Point center)
            {
                DrawRadial(blend, r, center.X, center.Y);
            }

            public void DrawRadial(ColorBlend blend, Rectangle r, int cx, int cy)
            {
                drawRadialPath.Reset();
                drawRadialPath.AddEllipse(r.X, r.Y, r.Width - 1, r.Height - 1);

                drawRadialBrush1 = new PathGradientBrush(drawRadialPath);
                drawRadialBrush1.CenterPoint = new Point(r.X + cx, r.Y + cy);
                drawRadialBrush1.InterpolationColors = blend;

                if (G.SmoothingMode == SmoothingMode.AntiAlias)
                {
                    G.FillEllipse(drawRadialBrush1, r.X + 1, r.Y + 1, r.Width - 3, r.Height - 3);
                }
                else
                {
                    G.FillEllipse(drawRadialBrush1, r);
                }
            }


            protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height)
            {
                drawRadialRectangle = new Rectangle(x, y, width, height);
                DrawRadial(c1, c2, drawGradientRectangle);
            }

            protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height, float angle)
            {
                drawRadialRectangle = new Rectangle(x, y, width, height);
                DrawRadial(c1, c2, drawGradientRectangle, angle);
            }

            protected void DrawRadial(Color c1, Color c2, Rectangle r)
            {
                drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, 90f);
                G.FillRectangle(drawGradientBrush, r);
            }

            protected void DrawRadial(Color c1, Color c2, Rectangle r, float angle)
            {
                drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, angle);
                G.FillEllipse(drawGradientBrush, r);
            }

            #endregion

            #region " CreateRound "

            private GraphicsPath createRoundPath;

            private Rectangle createRoundRectangle;

            public GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
            {
                createRoundRectangle = new Rectangle(x, y, width, height);
                return CreateRound(createRoundRectangle, slope);
            }

            public GraphicsPath CreateRound(Rectangle r, int slope)
            {
                createRoundPath = new GraphicsPath(FillMode.Winding);
                createRoundPath.AddArc(r.X, r.Y, slope, slope, 180f, 90f);
                createRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270f, 90f);
                createRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0f, 90f);
                createRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90f, 90f);
                createRoundPath.CloseFigure();
                return createRoundPath;
            }

            #endregion
        }

        private abstract class ThemeControl154 : Control
        {
            private void DoAnimation(bool i)
            {
                OnAnimation();
                if (i)
                    Invalidate();
            }

            protected override sealed void OnPaint(PaintEventArgs e)
            {
                if (Width == 0 || Height == 0)
                    return;

                if (transparent)
                {
                    PaintHook();
                    e.Graphics.DrawImage(B, 0, 0);
                }
                else
                {
                    G = e.Graphics;
                    PaintHook();
                }
            }

            protected override void OnHandleDestroyed(EventArgs e)
            {
                ThemeShare.RemoveAnimationCallback(DoAnimation);
                base.OnHandleDestroyed(e);
            }

            #region " Initialization "

            protected Graphics G;

            protected Bitmap B;

            public ThemeControl154()
            {
                SetStyle((ControlStyles)139270, true);

                ImageSize = Size.Empty;
                Font = new Font("Verdana", 8);

                Bitmap measureBitmap = new Bitmap(1, 1);
                measureGraphics = Graphics.FromImage(measureBitmap);

                drawRadialPath = new GraphicsPath();

                InvalidateCustimization();
                //Remove?
            }

            protected override sealed void OnHandleCreated(EventArgs e)
            {
                InvalidateCustimization();
                ColorHook();

                if (lockWidth != 0)
                    Width = lockWidth;
                if (lockHeight != 0)
                    Height = lockHeight;

                Transparent = transparent;
                if (transparent && backColor)
                    BackColor = Color.Transparent;

                base.OnHandleCreated(e);
            }

            private bool doneCreation;

            protected override sealed void OnParentChanged(EventArgs e)
            {
                if (Parent != null)
                {
                    OnCreation();
                    doneCreation = true;
                    InvalidateTimer();
                }

                base.OnParentChanged(e);
            }

            #endregion

            #region " Size Handling "

            protected override sealed void OnSizeChanged(EventArgs e)
            {
                if (transparent)
                {
                    InvalidateBitmap();
                }

                Invalidate();
                base.OnSizeChanged(e);
            }

            protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
            {
                if (lockWidth != 0)
                    width = lockWidth;
                if (lockHeight != 0)
                    height = lockHeight;
                base.SetBoundsCore(x, y, width, height, specified);
            }

            #endregion

            #region " State Handling "

            private bool inPosition;

            protected override void OnMouseEnter(EventArgs e)
            {
                inPosition = true;
                SetState(MouseState.Over);
                base.OnMouseEnter(e);
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                if (inPosition)
                    SetState(MouseState.Over);
                base.OnMouseUp(e);
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                    SetState(MouseState.Down);
                base.OnMouseDown(e);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                inPosition = false;
                SetState(MouseState.None);
                base.OnMouseLeave(e);
            }

            protected override void OnEnabledChanged(EventArgs e)
            {
                if (Enabled)
                    SetState(MouseState.None);
                else
                    SetState(MouseState.Block);
                base.OnEnabledChanged(e);
            }

            protected MouseState State;

            private void SetState(MouseState current)
            {
                State = current;
                Invalidate();
            }

            #endregion

            #region " Base Properties "

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override Color ForeColor
            {
                get { return Color.Empty; }
                set { }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override Image BackgroundImage
            {
                get { return null; }
                set { }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override ImageLayout BackgroundImageLayout
            {
                get { return ImageLayout.None; }
                set { }
            }

            public override string Text
            {
                get { return base.Text; }
                set
                {
                    base.Text = value;
                    Invalidate();
                }
            }

            public override Font Font
            {
                get { return base.Font; }
                set
                {
                    base.Font = value;
                    Invalidate();
                }
            }

            private bool backColor;

            [Category("Misc")]
            public override Color BackColor
            {
                get { return base.BackColor; }
                set
                {
                    if (!IsHandleCreated && value == Color.Transparent)
                    {
                        backColor = true;
                        return;
                    }

                    base.BackColor = value;
                    if (Parent != null)
                        ColorHook();
                }
            }

            #endregion

            #region " Public Properties "

            private bool noRounding;

            public bool NoRounding
            {
                get { return noRounding; }
                set
                {
                    noRounding = value;
                    Invalidate();
                }
            }

            private Image image;

            public Image Image
            {
                get { return image; }
                set
                {
                    if (value == null)
                    {
                        ImageSize = Size.Empty;
                    }
                    else
                    {
                        ImageSize = value.Size;
                    }

                    image = value;
                    Invalidate();
                }
            }

            private bool transparent;

            public bool Transparent
            {
                get { return transparent; }
                set
                {
                    transparent = value;
                    if (!IsHandleCreated)
                        return;

                    if (!value && BackColor.A != 255)
                    {
                        throw new Exception("Unable to change value to false while a transparent BackColor is in use.");
                    }

                    SetStyle(ControlStyles.Opaque, !value);
                    SetStyle(ControlStyles.SupportsTransparentBackColor, value);

                    if (value)
                        InvalidateBitmap();
                    else
                        B = null;
                    Invalidate();
                }
            }

            private readonly Dictionary<string, Color> items = new Dictionary<string, Color>();

            public Bloom[] Colors
            {
                get
                {
                    List<Bloom> T = new List<Bloom>();
                    Dictionary<string, Color>.Enumerator e = items.GetEnumerator();

                    while (e.MoveNext())
                    {
                        T.Add(new Bloom(e.Current.Key, e.Current.Value));
                    }

                    return T.ToArray();
                }
                set
                {
                    foreach (Bloom b in value)
                    {
                        if (items.ContainsKey(b.Name))
                            items[b.Name] = b.Value;
                    }

                    InvalidateCustimization();
                    ColorHook();
                    Invalidate();
                }
            }

            private string customization;

            public string Customization
            {
                get { return customization; }
                set
                {
                    if (value == customization)
                        return;

                    byte[] data = null;
                    Bloom[] items = Colors;

                    try
                    {
                        data = Convert.FromBase64String(value);
                        for (int I = 0; I <= items.Length - 1; I++)
                        {
                            items[I].Value = Color.FromArgb(BitConverter.ToInt32(data, I * 4));
                        }
                    }
                    catch
                    {
                        return;
                    }

                    customization = value;

                    Colors = items;
                    ColorHook();
                    Invalidate();
                }
            }

            #endregion

            #region " Private Properties "

            protected Size ImageSize { get; private set; }

            private int lockWidth;

            protected int LockWidth
            {
                get { return lockWidth; }
                set
                {
                    lockWidth = value;
                    if (LockWidth != 0 && IsHandleCreated)
                        Width = LockWidth;
                }
            }

            private int lockHeight;

            protected int LockHeight
            {
                get { return lockHeight; }
                set
                {
                    lockHeight = value;
                    if (LockHeight != 0 && IsHandleCreated)
                        Height = LockHeight;
                }
            }

            private bool isAnimated;

            protected bool IsAnimated
            {
                get { return isAnimated; }
                set
                {
                    isAnimated = value;
                    InvalidateTimer();
                }
            }

            #endregion

            #region " Property Helpers "

            protected Pen GetPen(string name)
            {
                return new Pen(items[name]);
            }

            protected Pen GetPen(string name, float width)
            {
                return new Pen(items[name], width);
            }

            protected SolidBrush GetBrush(string name)
            {
                return new SolidBrush(items[name]);
            }

            protected Color GetColor(string name)
            {
                return items[name];
            }

            protected void SetColor(string name, Color value)
            {
                if (items.ContainsKey(name))
                    items[name] = value;
                else
                    items.Add(name, value);
            }

            protected void SetColor(string name, byte r, byte g, byte b)
            {
                SetColor(name, Color.FromArgb(r, g, b));
            }

            protected void SetColor(string name, byte a, byte r, byte g, byte b)
            {
                SetColor(name, Color.FromArgb(a, r, g, b));
            }

            protected void SetColor(string name, byte a, Color value)
            {
                SetColor(name, Color.FromArgb(a, value));
            }

            private void InvalidateBitmap()
            {
                if (Width == 0 || Height == 0)
                    return;
                B = new Bitmap(Width, Height, PixelFormat.Format32bppPArgb);
                G = Graphics.FromImage(B);
            }

            private void InvalidateCustimization()
            {
                MemoryStream m = new MemoryStream(items.Count * 4);

                foreach (Bloom b in Colors)
                {
                    m.Write(BitConverter.GetBytes(b.Value.ToArgb()), 0, 4);
                }

                m.Close();
                customization = Convert.ToBase64String(m.ToArray());
            }

            private void InvalidateTimer()
            {
                if (DesignMode || !doneCreation)
                    return;

                if (isAnimated)
                {
                    ThemeShare.AddAnimationCallback(DoAnimation);
                }
                else
                {
                    ThemeShare.RemoveAnimationCallback(DoAnimation);
                }
            }

            #endregion

            #region " User Hooks "

            protected abstract void ColorHook();
            protected abstract void PaintHook();

            protected virtual void OnCreation()
            {
            }

            protected virtual void OnAnimation()
            {
            }

            #endregion

            #region " Offset "

            private Rectangle offsetReturnRectangle;

            protected Rectangle Offset(Rectangle r, int amount)
            {
                offsetReturnRectangle = new Rectangle(r.X + amount, r.Y + amount, r.Width - (amount * 2), r.Height - (amount * 2));
                return offsetReturnRectangle;
            }

            private Size offsetReturnSize;

            protected Size Offset(Size s, int amount)
            {
                offsetReturnSize = new Size(s.Width + amount, s.Height + amount);
                return offsetReturnSize;
            }

            private Point offsetReturnPoint;

            protected Point Offset(Point p, int amount)
            {
                offsetReturnPoint = new Point(p.X + amount, p.Y + amount);
                return offsetReturnPoint;
            }

            #endregion

            #region " Center "

            private Point centerReturn;

            protected Point Center(Rectangle p, Rectangle c)
            {
                centerReturn = new Point((p.Width / 2 - c.Width / 2) + p.X + c.X, (p.Height / 2 - c.Height / 2) + p.Y + c.Y);
                return centerReturn;
            }

            protected Point Center(Rectangle p, Size c)
            {
                centerReturn = new Point((p.Width / 2 - c.Width / 2) + p.X, (p.Height / 2 - c.Height / 2) + p.Y);
                return centerReturn;
            }

            protected Point Center(Rectangle child)
            {
                return Center(Width, Height, child.Width, child.Height);
            }

            protected Point Center(Size child)
            {
                return Center(Width, Height, child.Width, child.Height);
            }

            protected Point Center(int childWidth, int childHeight)
            {
                return Center(Width, Height, childWidth, childHeight);
            }

            protected Point Center(Size p, Size c)
            {
                return Center(p.Width, p.Height, c.Width, c.Height);
            }

            protected Point Center(int pWidth, int pHeight, int cWidth, int cHeight)
            {
                centerReturn = new Point(pWidth / 2 - cWidth / 2, pHeight / 2 - cHeight / 2);
                return centerReturn;
            }

            #endregion

            #region " Measure "

            //TODO: Potential issues during multi-threading.
            private readonly Graphics measureGraphics;

            protected Size Measure()
            {
                return measureGraphics.MeasureString(Text, Font, Width).ToSize();
            }

            protected Size Measure(string text)
            {
                return measureGraphics.MeasureString(text, Font, Width).ToSize();
            }

            #endregion

            #region " DrawPixel "

            private SolidBrush drawPixelBrush;

            protected void DrawPixel(Color c1, int x, int y)
            {
                if (transparent)
                {
                    B.SetPixel(x, y, c1);
                }
                else
                {
                    drawPixelBrush = new SolidBrush(c1);
                    G.FillRectangle(drawPixelBrush, x, y, 1, 1);
                }
            }

            #endregion

            #region " DrawCorners "

            private SolidBrush drawCornersBrush;

            protected void DrawCorners(Color c1, int offset)
            {
                DrawCorners(c1, 0, 0, Width, Height, offset);
            }

            protected void DrawCorners(Color c1, Rectangle r1, int offset)
            {
                DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset);
            }

            protected void DrawCorners(Color c1, int x, int y, int width, int height, int offset)
            {
                DrawCorners(c1, x + offset, y + offset, width - (offset * 2), height - (offset * 2));
            }

            protected void DrawCorners(Color c1)
            {
                DrawCorners(c1, 0, 0, Width, Height);
            }

            protected void DrawCorners(Color c1, Rectangle r1)
            {
                DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height);
            }

            protected void DrawCorners(Color c1, int x, int y, int width, int height)
            {
                if (noRounding)
                    return;

                if (transparent)
                {
                    B.SetPixel(x, y, c1);
                    B.SetPixel(x + (width - 1), y, c1);
                    B.SetPixel(x, y + (height - 1), c1);
                    B.SetPixel(x + (width - 1), y + (height - 1), c1);
                }
                else
                {
                    drawCornersBrush = new SolidBrush(c1);
                    G.FillRectangle(drawCornersBrush, x, y, 1, 1);
                    G.FillRectangle(drawCornersBrush, x + (width - 1), y, 1, 1);
                    G.FillRectangle(drawCornersBrush, x, y + (height - 1), 1, 1);
                    G.FillRectangle(drawCornersBrush, x + (width - 1), y + (height - 1), 1, 1);
                }
            }

            #endregion

            #region " DrawBorders "

            protected void DrawBorders(Pen p1, int offset)
            {
                DrawBorders(p1, 0, 0, Width, Height, offset);
            }

            protected void DrawBorders(Pen p1, Rectangle r, int offset)
            {
                DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset);
            }

            protected void DrawBorders(Pen p1, int x, int y, int width, int height, int offset)
            {
                DrawBorders(p1, x + offset, y + offset, width - (offset * 2), height - (offset * 2));
            }

            protected void DrawBorders(Pen p1)
            {
                DrawBorders(p1, 0, 0, Width, Height);
            }

            protected void DrawBorders(Pen p1, Rectangle r)
            {
                DrawBorders(p1, r.X, r.Y, r.Width, r.Height);
            }

            protected void DrawBorders(Pen p1, int x, int y, int width, int height)
            {
                G.DrawRectangle(p1, x, y, width - 1, height - 1);
            }

            #endregion

            #region " DrawText "

            private Point drawTextPoint;

            private Size drawTextSize;

            protected void DrawText(Brush b1, HorizontalAlignment a, int x, int y)
            {
                DrawText(b1, Text, a, x, y);
            }

            protected void DrawText(Brush b1, string text, HorizontalAlignment a, int x, int y)
            {
                if (text.Length == 0)
                    return;

                drawTextSize = Measure(text);
                drawTextPoint = Center(drawTextSize);

                switch (a)
                {
                    case HorizontalAlignment.Left:
                        G.DrawString(text, Font, b1, x, drawTextPoint.Y + y);
                        break;
                    case HorizontalAlignment.Center:
                        G.DrawString(text, Font, b1, drawTextPoint.X + x, drawTextPoint.Y + y);
                        break;
                    case HorizontalAlignment.Right:
                        G.DrawString(text, Font, b1, Width - drawTextSize.Width - x, drawTextPoint.Y + y);
                        break;
                }
            }

            protected void DrawText(Brush b1, Point p1)
            {
                if (Text.Length == 0)
                    return;
                G.DrawString(Text, Font, b1, p1);
            }

            protected void DrawText(Brush b1, int x, int y)
            {
                if (Text.Length == 0)
                    return;
                G.DrawString(Text, Font, b1, x, y);
            }

            #endregion

            #region " DrawImage "

            private Point drawImagePoint;

            protected void DrawImage(HorizontalAlignment a, int x, int y)
            {
                DrawImage(image, a, x, y);
            }

            protected void DrawImage(Image image, HorizontalAlignment a, int x, int y)
            {
                if (image == null)
                    return;
                drawImagePoint = Center(image.Size);

                switch (a)
                {
                    case HorizontalAlignment.Left:
                        G.DrawImage(image, x, drawImagePoint.Y + y, image.Width, image.Height);
                        break;
                    case HorizontalAlignment.Center:
                        G.DrawImage(image, drawImagePoint.X + x, drawImagePoint.Y + y, image.Width, image.Height);
                        break;
                    case HorizontalAlignment.Right:
                        G.DrawImage(image, Width - image.Width - x, drawImagePoint.Y + y, image.Width, image.Height);
                        break;
                }
            }

            protected void DrawImage(Point p1)
            {
                DrawImage(image, p1.X, p1.Y);
            }

            protected void DrawImage(int x, int y)
            {
                DrawImage(image, x, y);
            }

            protected void DrawImage(Image image, Point p1)
            {
                DrawImage(image, p1.X, p1.Y);
            }

            protected void DrawImage(Image image, int x, int y)
            {
                if (image == null)
                    return;
                G.DrawImage(image, x, y, image.Width, image.Height);
            }

            #endregion

            #region " DrawGradient "

            private LinearGradientBrush drawGradientBrush;

            private Rectangle drawGradientRectangle;

            protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height)
            {
                drawGradientRectangle = new Rectangle(x, y, width, height);
                DrawGradient(blend, drawGradientRectangle);
            }

            protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height, float angle)
            {
                drawGradientRectangle = new Rectangle(x, y, width, height);
                DrawGradient(blend, drawGradientRectangle, angle);
            }

            protected void DrawGradient(ColorBlend blend, Rectangle r)
            {
                drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, 90f);
                drawGradientBrush.InterpolationColors = blend;
                G.FillRectangle(drawGradientBrush, r);
            }

            protected void DrawGradient(ColorBlend blend, Rectangle r, float angle)
            {
                drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, angle);
                drawGradientBrush.InterpolationColors = blend;
                G.FillRectangle(drawGradientBrush, r);
            }


            protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height)
            {
                drawGradientRectangle = new Rectangle(x, y, width, height);
                DrawGradient(c1, c2, drawGradientRectangle);
            }

            protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height, float angle)
            {
                drawGradientRectangle = new Rectangle(x, y, width, height);
                DrawGradient(c1, c2, drawGradientRectangle, angle);
            }

            protected void DrawGradient(Color c1, Color c2, Rectangle r)
            {
                drawGradientBrush = new LinearGradientBrush(r, c1, c2, 90f);
                G.FillRectangle(drawGradientBrush, r);
            }

            protected void DrawGradient(Color c1, Color c2, Rectangle r, float angle)
            {
                drawGradientBrush = new LinearGradientBrush(r, c1, c2, angle);
                G.FillRectangle(drawGradientBrush, r);
            }

            #endregion

            #region " DrawRadial "

            private readonly GraphicsPath drawRadialPath;
            private PathGradientBrush drawRadialBrush1;
            private LinearGradientBrush drawRadialBrush2;

            private Rectangle drawRadialRectangle;

            public void DrawRadial(ColorBlend blend, int x, int y, int width, int height)
            {
                drawRadialRectangle = new Rectangle(x, y, width, height);
                DrawRadial(blend, drawRadialRectangle, width / 2, height / 2);
            }

            public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, Point center)
            {
                drawRadialRectangle = new Rectangle(x, y, width, height);
                DrawRadial(blend, drawRadialRectangle, center.X, center.Y);
            }

            public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, int cx, int cy)
            {
                drawRadialRectangle = new Rectangle(x, y, width, height);
                DrawRadial(blend, drawRadialRectangle, cx, cy);
            }

            public void DrawRadial(ColorBlend blend, Rectangle r)
            {
                DrawRadial(blend, r, r.Width / 2, r.Height / 2);
            }

            public void DrawRadial(ColorBlend blend, Rectangle r, Point center)
            {
                DrawRadial(blend, r, center.X, center.Y);
            }

            public void DrawRadial(ColorBlend blend, Rectangle r, int cx, int cy)
            {
                drawRadialPath.Reset();
                drawRadialPath.AddEllipse(r.X, r.Y, r.Width - 1, r.Height - 1);

                drawRadialBrush1 = new PathGradientBrush(drawRadialPath);
                drawRadialBrush1.CenterPoint = new Point(r.X + cx, r.Y + cy);
                drawRadialBrush1.InterpolationColors = blend;

                if (G.SmoothingMode == SmoothingMode.AntiAlias)
                {
                    G.FillEllipse(drawRadialBrush1, r.X + 1, r.Y + 1, r.Width - 3, r.Height - 3);
                }
                else
                {
                    G.FillEllipse(drawRadialBrush1, r);
                }
            }


            protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height)
            {
                drawRadialRectangle = new Rectangle(x, y, width, height);
                DrawRadial(c1, c2, drawRadialRectangle);
            }

            protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height, float angle)
            {
                drawRadialRectangle = new Rectangle(x, y, width, height);
                DrawRadial(c1, c2, drawRadialRectangle, angle);
            }

            protected void DrawRadial(Color c1, Color c2, Rectangle r)
            {
                drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, 90f);
                G.FillEllipse(drawRadialBrush2, r);
            }

            protected void DrawRadial(Color c1, Color c2, Rectangle r, float angle)
            {
                drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, angle);
                G.FillEllipse(drawRadialBrush2, r);
            }

            #endregion

            #region " CreateRound "

            private GraphicsPath createRoundPath;

            private Rectangle createRoundRectangle;

            public GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
            {
                createRoundRectangle = new Rectangle(x, y, width, height);
                return CreateRound(createRoundRectangle, slope);
            }

            public GraphicsPath CreateRound(Rectangle r, int slope)
            {
                createRoundPath = new GraphicsPath(FillMode.Winding);
                createRoundPath.AddArc(r.X, r.Y, slope, slope, 180f, 90f);
                createRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270f, 90f);
                createRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0f, 90f);
                createRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90f, 90f);
                createRoundPath.CloseFigure();
                return createRoundPath;
            }

            #endregion
        }

        private static class ThemeShare
        {
            #region " Animation "

            private static int frames;
            private static bool invalidate;

            private static readonly PrecisionTimer ThemeTimer = new PrecisionTimer();
            //1000 / 50 = 20 FPS
            private const int Fps = 50;

            private const int Rate = 10;

            public delegate void AnimationDelegate(bool invalidate);


            private static readonly List<AnimationDelegate> Callbacks = new List<AnimationDelegate>();

            private static void HandleCallbacks(IntPtr state, bool reserve)
            {
                invalidate = (frames >= Fps);
                if (invalidate)
                    frames = 0;

                lock (Callbacks)
                {
                    for (int I = 0; I <= Callbacks.Count - 1; I++)
                    {
                        Callbacks[I].Invoke(invalidate);
                    }
                }

                frames += Rate;
            }

            private static void InvalidateThemeTimer()
            {
                if (Callbacks.Count == 0)
                {
                    ThemeTimer.Delete();
                }
                else
                {
                    ThemeTimer.Create(0, Rate, HandleCallbacks);
                }
            }

            public static void AddAnimationCallback(AnimationDelegate callback)
            {
                lock (Callbacks)
                {
                    if (Callbacks.Contains(callback))
                        return;

                    Callbacks.Add(callback);
                    InvalidateThemeTimer();
                }
            }

            public static void RemoveAnimationCallback(AnimationDelegate callback)
            {
                lock (Callbacks)
                {
                    if (!Callbacks.Contains(callback))
                        return;

                    Callbacks.Remove(callback);
                    InvalidateThemeTimer();
                }
            }

            #endregion
        }

        private enum MouseState : byte
        {
            None = 0,
            Over = 1,
            Down = 2,
            Block = 3
        }

        private struct Bloom
        {
            public Bloom(string name, Color value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; }

            public Color Value { get; set; }

            public string ValueHex
            {
                get { return string.Concat("#", Value.R.ToString("X2", null), Value.G.ToString("X2", null), Value.B.ToString("X2", null)); }
                set
                {
                    try
                    {
                        Value = ColorTranslator.FromHtml(value);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }

        private class PrecisionTimer : IDisposable
        {
            public delegate void TimerDelegate(IntPtr r1, bool r2);

            private IntPtr handle;
            private TimerDelegate timerCallback;
            public bool Enabled { get; private set; }

            public void Dispose()
            {
                Delete();
            }

            [DllImport("kernel32.dll", EntryPoint = "CreateTimerQueueTimer")]
            private static extern bool CreateTimerQueueTimer(ref IntPtr handle, IntPtr queue, TimerDelegate callback, IntPtr state, uint dueTime, uint period, uint flags);

            [DllImport("kernel32.dll", EntryPoint = "DeleteTimerQueueTimer")]
            private static extern bool DeleteTimerQueueTimer(IntPtr queue, IntPtr handle, IntPtr callback);

            public void Create(uint dueTime, uint period, TimerDelegate callback)
            {
                if (Enabled)
                    return;

                timerCallback = callback;
                bool success = CreateTimerQueueTimer(ref handle, IntPtr.Zero, timerCallback, IntPtr.Zero, dueTime, period, 0);

                if (!success)
                    ThrowNewException("CreateTimerQueueTimer");
                Enabled = success;
            }

            public void Delete()
            {
                if (!Enabled)
                    return;
                bool success = DeleteTimerQueueTimer(IntPtr.Zero, handle, IntPtr.Zero);

                if (!success && Marshal.GetLastWin32Error() != 997)
                {
                    ThrowNewException("DeleteTimerQueueTimer");
                }

                Enabled = !success;
            }

            private void ThrowNewException(string name)
            {
                throw new Exception($"{name} failed. Win32Error: {Marshal.GetLastWin32Error()}");
            }
        }
    }
}