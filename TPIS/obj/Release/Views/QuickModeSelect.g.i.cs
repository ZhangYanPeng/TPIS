﻿#pragma checksum "..\..\..\Views\QuickModeSelect.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0951A63F4D76A94098C1CDC452E4533C6F2B7A3A"
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
using TPIS.Views;


namespace TPIS.Views {
    
    
    /// <summary>
    /// QuickModeSelect
    /// </summary>
    public partial class QuickModeSelect : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 18 "..\..\..\Views\QuickModeSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander DesignModeList;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Views\QuickModeSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox DesignCheckBox;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Views\QuickModeSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander CalModeList;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Views\QuickModeSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CalCheckBox;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\Views\QuickModeSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander InterModeList;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\Views\QuickModeSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox InterCheckBox;
        
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
            System.Uri resourceLocater = new System.Uri("/TPIS;component/views/quickmodeselect.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\QuickModeSelect.xaml"
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
            this.DesignModeList = ((System.Windows.Controls.Expander)(target));
            return;
            case 2:
            this.DesignCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 25 "..\..\..\Views\QuickModeSelect.xaml"
            this.DesignCheckBox.Click += new System.Windows.RoutedEventHandler(this.TotalChecked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CalModeList = ((System.Windows.Controls.Expander)(target));
            return;
            case 5:
            this.CalCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 68 "..\..\..\Views\QuickModeSelect.xaml"
            this.CalCheckBox.Click += new System.Windows.RoutedEventHandler(this.TotalChecked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.InterModeList = ((System.Windows.Controls.Expander)(target));
            return;
            case 8:
            this.InterCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 111 "..\..\..\Views\QuickModeSelect.xaml"
            this.InterCheckBox.Click += new System.Windows.RoutedEventHandler(this.TotalChecked);
            
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
            case 3:
            
            #line 41 "..\..\..\Views\QuickModeSelect.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Click += new System.Windows.RoutedEventHandler(this.ItemCheck);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 84 "..\..\..\Views\QuickModeSelect.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Click += new System.Windows.RoutedEventHandler(this.ItemCheck);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 127 "..\..\..\Views\QuickModeSelect.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Click += new System.Windows.RoutedEventHandler(this.ItemCheck);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

