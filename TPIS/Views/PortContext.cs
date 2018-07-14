﻿using System.Windows;
using System.Windows.Controls;
using TPIS.Model;
using TPIS.Model.Common;

namespace TPIS.Views
{
    class PortContext : ContextMenu
    {
        public Port port;

        public PortContext(Port p)
        {
            port = p;

            MenuItem MenuItemUnDef = new MenuItem();
            MenuItemUnDef.Header = "设为未定义";
            if (port.type == NodType.DefOut || port.type == NodType.DefIn && port.link == null)
                MenuItemUnDef.IsEnabled = true;
            else
                MenuItemUnDef.IsEnabled = false;

            MenuItem MenuItemDefIn = new MenuItem();
            MenuItemDefIn.Header = "设为进口";
            if (port.type == NodType.Undef || port.type == NodType.DefOut && port.link == null)
                MenuItemDefIn.IsEnabled = true;
            else
                MenuItemDefIn.IsEnabled = false;

            MenuItem MenuItemDefOut = new MenuItem();
            MenuItemDefOut.Header = "设为出口";
            if (port.type == NodType.Undef || port.type == NodType.DefIn && port.link == null)
                MenuItemDefOut.IsEnabled = true;
            else
                MenuItemDefOut.IsEnabled = false;

            MenuItemUnDef.Click += btUnDef_Click;
            MenuItemDefIn.Click += btDefIn_Click;
            MenuItemDefOut.Click += btDefOut_Click;

            Items.Add(MenuItemUnDef);
            Items.Add(MenuItemDefIn);
            Items.Add(MenuItemDefOut);
        }

        private void btUnDef_Click(object sender, RoutedEventArgs e)
        {
            port.Type = NodType.Undef;
        }

        private void btDefIn_Click(object sender, RoutedEventArgs e)
        {
            port.Type = NodType.DefIn;
        }

        private void btDefOut_Click(object sender, RoutedEventArgs e)
        {
            port.Type = NodType.DefOut;
        }
    }
}