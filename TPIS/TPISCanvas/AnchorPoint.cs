using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;

namespace TPIS.TPISCanvas
{

    public enum AnchorPointType
    {
        UL,U,UR,
        L,R,
        DL,D,DR
    }

    public class AnchorPoint : Canvas
    {
        public AnchorPointType Type { get; set; }

        public AnchorPoint(AnchorPointType type)
        {
            Type = type;
            Rectangle rect = new Rectangle()
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.LightGoldenrodYellow,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
            this.Width = 8;
            this.Height = 8;
            this.Children.Add(rect);
            base.MouseEnter += new MouseEventHandler(Element_MouseEnter);
        }

        void Element_MouseEnter(object sender, MouseEventArgs e)
        {
            switch (Type)
            {
                case AnchorPointType.U:
                case AnchorPointType.D: this.Cursor = Cursors.SizeNS; break;
                case AnchorPointType.L:
                case AnchorPointType.R: this.Cursor = Cursors.SizeWE; break;
                case AnchorPointType.UL:
                case AnchorPointType.DR: this.Cursor = Cursors.SizeNWSE;break;
                case AnchorPointType.UR:
                case AnchorPointType.DL: this.Cursor = Cursors.SizeNESW; break;
            }
            Mouse.OverrideCursor = null;
        }
}

    partial class DesignerComponent : Canvas
    { 
        public void InitAnchorPoints()
        {
            if(!this.IsSelected)
            {
                this.Children.Add( new AnchorPoint(AnchorPointType.UL) );
                this.Children.Add(new AnchorPoint(AnchorPointType.U));
                this.Children.Add(new AnchorPoint(AnchorPointType.UR));
                this.Children.Add(new AnchorPoint(AnchorPointType.L));
                this.Children.Add(new AnchorPoint(AnchorPointType.R));
                this.Children.Add(new AnchorPoint(AnchorPointType.DL));
                this.Children.Add(new AnchorPoint(AnchorPointType.D));
                this.Children.Add(new AnchorPoint(AnchorPointType.DR));
                this.IsSelected = true;
            }
            RePosAnchorPoints();
        }

        public void RePosAnchorPoints()
        {
            foreach(UIElement uie in this.Children)
            {
                if (uie is AnchorPoint)
                {
                    AnchorPoint ap = (AnchorPoint)uie;
                    if (ap.Type == AnchorPointType.UL || ap.Type == AnchorPointType.U || ap.Type == AnchorPointType.UR)
                        ap.SetValue(Canvas.TopProperty, 0.0);
                    else if (ap.Type == AnchorPointType.L || ap.Type == AnchorPointType.R)
                    {
                        ap.SetValue(Canvas.TopProperty, this.Height / 2 - 4);
                    }
                    else
                    {
                        ap.SetValue(Canvas.TopProperty, this.Height - 8);
                    }

                    if (ap.Type == AnchorPointType.UL || ap.Type == AnchorPointType.L || ap.Type == AnchorPointType.DL)
                        ap.SetValue(Canvas.LeftProperty, 0.0);
                    else if (ap.Type == AnchorPointType.U || ap.Type == AnchorPointType.D)
                    {
                        ap.SetValue(Canvas.LeftProperty, this.Width / 2 - 4);
                    }
                    else
                    {
                        ap.SetValue(Canvas.LeftProperty, this.Width - 8);
                    }
                }
            }
        }

        public void Component_Select()
        {
            InitAnchorPoints();
        }

        public void Component_UnSelect()
        {
            this.Children.Clear();
        }
    }
}
