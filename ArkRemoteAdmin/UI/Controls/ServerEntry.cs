using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin.UI.Controls
{
    internal sealed class ServerEntry : RadioButton
    {
        private readonly Pen hoverOuterPen;
        private readonly Pen innerPen;
        private readonly Pen selHoverOuterPen;
        private readonly Pen selNoHoverOuterPen;
        private Button btnConfigure;
        private Button btnDelete;
        private LinearGradientBrush hoverBackGradient;
        private bool isFocused;
        private bool isHovered;
        private bool isKeyDown;
        private bool isMouseDown;
        private LinearGradientBrush pressedBackGradient;
        private Rectangle rectBackGrad;
        private LinearGradientBrush selHoverBackGrad;
        private LinearGradientBrush selNoHoverBackGrad;

        [DefaultValue(false)]
        public override bool AutoSize
        {
            get { return false; }
            set { base.AutoSize = value; }
        }

        private bool IsPressed
        {
            get
            {
                if (isKeyDown) return true;
                if (isMouseDown) return isHovered;
                return false;
            }
        }

        public ServerEntry()
        {
            innerPen = new Pen(Color.FromArgb(100, byte.MaxValue, byte.MaxValue, byte.MaxValue));
            hoverOuterPen = new Pen(Color.FromArgb(184, 214, 251));
            selHoverOuterPen = new Pen(Color.FromArgb(125, 162, 206));
            selNoHoverOuterPen = new Pen(Color.FromArgb(217, 217, 217));
            Font = new Font("Tahoma", 8f);
            refreshGradient();
            SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Height = 70;
            CreateButtons();
        }

        private void CreateButtons()
        {
            btnConfigure = new Button();
            btnConfigure.Size = new Size(90, 22);
            btnConfigure.BackColor = Color.Transparent;
            btnConfigure.Text = "Konfigurieren";
            btnConfigure.FlatStyle = FlatStyle.System;
            btnConfigure.Location = new Point(Width - (btnConfigure.Width + 12), 12);
            btnConfigure.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConfigure.Visible = false;
            Controls.Add(btnConfigure);

            btnDelete = new Button();
            btnDelete.Size = new Size(90, 22);
            btnDelete.BackColor = Color.Transparent;
            btnDelete.Text = "Entfernen";
            btnDelete.FlatStyle = FlatStyle.System;
            btnDelete.Location = new Point(Width - (btnDelete.Width + 12), 38);
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.Visible = false;
            Controls.Add(btnDelete);
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            btnConfigure.Visible = Checked;
            btnDelete.Visible = Checked;
        }

        private void drawBackground(Graphics g)
        {
            if (isHovered)
                g.ExcludeClip(rectBackGrad);
            g.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            g.ResetClip();
            if (isHovered && isFocused && IsPressed)
            {
                g.FillRectangle(pressedBackGradient, rectBackGrad);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawPath(innerPen, GraphicUtils.RoundRect(0, 1, Width - 1, Height - 2, 3, 1));
                g.DrawPath(selHoverOuterPen, GraphicUtils.RoundRect(-1, 0, Width + 1, Height, 3, 1));
            }
            else if (Checked)
            {
                g.FillRectangle(selHoverBackGrad, rectBackGrad);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawPath(innerPen, GraphicUtils.RoundRect(0, 1, Width - 1, Height - 2, 3, 1));
                g.DrawPath(selHoverOuterPen, GraphicUtils.RoundRect(-1, 0, Width + 1, Height, 3, 1));
            }
            else
            {
                if (!isHovered && !ClientRectangle.Contains(PointToClient(MousePosition)))
                    return;
                g.FillRectangle(hoverBackGradient, rectBackGrad);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawPath(innerPen, GraphicUtils.RoundRect(0, 1, Width - 1, Height - 2, 3, 1));
                g.DrawPath(hoverOuterPen, GraphicUtils.RoundRect(-1, 0, Width + 1, Height, 3, 1));
            }
        }

        private void drawImage(Graphics g)
        {
            Image image = Properties.Resources.server;
            g.DrawImage(image, new Point(6, Height / 2 - image.Height / 2));
        }

        private void drawText(Graphics g)
        {
            int x = 60;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("server.xunion.net:27021");
            Rectangle bounds = new Rectangle(new Point(x, 0), new Size(Width - x, ClientRectangle.Height));
            TextRenderer.DrawText(g, "xUnion.net", new Font("Tahoma", 14f), new Rectangle(bounds.X, bounds.Y + 10, bounds.Width, bounds.Height / 2), ForeColor, TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(g, stringBuilder.ToString(), Font, new Rectangle(bounds.X, bounds.Y + 35, bounds.Width, bounds.Height / 2), ForeColor, TextFormatFlags.VerticalCenter);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            pevent.Graphics.InterpolationMode = InterpolationMode.Low;
            drawBackground(pevent.Graphics);
            drawText(pevent.Graphics);
            drawImage(pevent.Graphics);
        }

        protected override void OnClick(EventArgs e)
        {
            isKeyDown = isMouseDown = false;
            base.OnClick(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            isFocused = true;
            base.OnEnter(e);
        }

        protected override void OnKeyDown(KeyEventArgs kevent)
        {
            if (kevent.KeyCode == Keys.Space)
            {
                isKeyDown = true;
                Invalidate();
            }
            base.OnKeyDown(kevent);
        }

        protected override void OnKeyUp(KeyEventArgs kevent)
        {
            if (isKeyDown && kevent.KeyCode == Keys.Space)
            {
                isKeyDown = false;
                Invalidate();
            }
            base.OnKeyUp(kevent);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            isFocused = isKeyDown = isMouseDown = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!isMouseDown && e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            Invalidate();
            if (mevent.Button == MouseButtons.None)
                return;
            if (!ClientRectangle.Contains(mevent.X, mevent.Y))
            {
                if (!isHovered)
                    return;
                isHovered = false;
                Invalidate();
            }
            else
            {
                if (isHovered)
                    return;
                isHovered = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (!isMouseDown)
                return;
            isMouseDown = false;
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            refreshGradient();
        }

        private void refreshGradient()
        {
            rectBackGrad = new Rectangle(1, 1, Width - 2, Height - 2);
            hoverBackGradient = new LinearGradientBrush(new Rectangle(rectBackGrad.X, rectBackGrad.Y, 1, rectBackGrad.Height), Color.FromArgb(250, 251, 253), Color.FromArgb(235, 243, 253), 90f);
            selHoverBackGrad = new LinearGradientBrush(new Rectangle(rectBackGrad.X, rectBackGrad.Y, 1, rectBackGrad.Height), Color.FromArgb(220, 235, 252), Color.FromArgb(193, 219, 252), 90f);
            selNoHoverBackGrad = new LinearGradientBrush(new Rectangle(rectBackGrad.X, rectBackGrad.Y, 1, rectBackGrad.Height), Color.FromArgb(248, 248, 248), Color.FromArgb(229, 229, 229), 90f);
            pressedBackGradient = new LinearGradientBrush(new Rectangle(rectBackGrad.X, rectBackGrad.Y, 1, rectBackGrad.Height), GraphicUtils.DarkenColor(Color.FromArgb(220, 235, 252), 20), GraphicUtils.DarkenColor(Color.FromArgb(193, 219, 252), 20), 90f);
        }
    }
}