﻿#pragma checksum "..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E26A5528309A0319E44D4BC8D2C71DF5F6AB8171"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TPIS;
using TPIS.Command;
using TPIS.TPISCanvas;
using TPIS.TPISCommand;


namespace TPIS {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 106 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl Menu;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton AddLine;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton AddStraightLine;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl ElementList;
        
        #line default
        #line hidden
        
        
        #line 188 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PropertyStateChange;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PropertyStateChangeFig;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ResultStateChange;
        
        #line default
        #line hidden
        
        
        #line 198 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ResultStateChangeFig;
        
        #line default
        #line hidden
        
        
        #line 207 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ResultWindow;
        
        #line default
        #line hidden
        
        
        #line 249 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl PortResults;
        
        #line default
        #line hidden
        
        
        #line 301 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox PropertyWindow;
        
        #line default
        #line hidden
        
        
        #line 348 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.StatusBarItem statusBar;
        
        #line default
        #line hidden
        
        
        #line 351 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl projectTab;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TPIS;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 69 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.NewProject_Excuted);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 70 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.OpenProject_Excuted);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 71 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Copy_Excuted);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 72 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Paste_Excuted);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 73 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Del_Excuted);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 74 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Save_Excuted);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 75 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.SaveAll_Excuted);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 76 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Print_Excuted);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 77 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Exit_Excuted);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 78 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Up_Excuted);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 79 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Down_Excuted);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 80 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Left_Excuted);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 81 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Right_Excuted);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 82 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Cut_Excuted);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 83 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.DrawGrid_Excuted);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 84 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Redo_Excuted);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 85 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Undo_Excuted);
            
            #line default
            #line hidden
            return;
            case 18:
            this.Menu = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 19:
            this.AddLine = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 125 "..\..\MainWindow.xaml"
            this.AddLine.Checked += new System.Windows.RoutedEventHandler(this.TPISLineTypeSelected);
            
            #line default
            #line hidden
            
            #line 125 "..\..\MainWindow.xaml"
            this.AddLine.Unchecked += new System.Windows.RoutedEventHandler(this.TPISLineTypeUnSelected);
            
            #line default
            #line hidden
            return;
            case 20:
            this.AddStraightLine = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 133 "..\..\MainWindow.xaml"
            this.AddStraightLine.Checked += new System.Windows.RoutedEventHandler(this.TPISLineTypeSelected);
            
            #line default
            #line hidden
            
            #line 133 "..\..\MainWindow.xaml"
            this.AddStraightLine.Unchecked += new System.Windows.RoutedEventHandler(this.TPISLineTypeUnSelected);
            
            #line default
            #line hidden
            return;
            case 21:
            this.ElementList = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 22:
            this.PropertyStateChange = ((System.Windows.Controls.Button)(target));
            
            #line 188 "..\..\MainWindow.xaml"
            this.PropertyStateChange.Click += new System.Windows.RoutedEventHandler(this.btn_PropertyStateChange);
            
            #line default
            #line hidden
            return;
            case 23:
            this.PropertyStateChangeFig = ((System.Windows.Controls.Image)(target));
            return;
            case 24:
            this.ResultStateChange = ((System.Windows.Controls.Button)(target));
            
            #line 195 "..\..\MainWindow.xaml"
            this.ResultStateChange.Click += new System.Windows.RoutedEventHandler(this.btn_ResultStateChange);
            
            #line default
            #line hidden
            return;
            case 25:
            this.ResultStateChangeFig = ((System.Windows.Controls.Image)(target));
            return;
            case 26:
            this.ResultWindow = ((System.Windows.Controls.ListBox)(target));
            return;
            case 28:
            this.PortResults = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 29:
            this.PropertyWindow = ((System.Windows.Controls.ListBox)(target));
            return;
            case 31:
            this.statusBar = ((System.Windows.Controls.Primitives.StatusBarItem)(target));
            return;
            case 32:
            this.projectTab = ((System.Windows.Controls.TabControl)(target));
            
            #line 351 "..\..\MainWindow.xaml"
            this.projectTab.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ProjectTab_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 27:
            
            #line 234 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCurveWindow_Click);
            
            #line default
            #line hidden
            break;
            case 30:
            
            #line 328 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCurveWindow_Click);
            
            #line default
            #line hidden
            break;
            case 33:
            
            #line 355 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ProjectItem_Close);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

