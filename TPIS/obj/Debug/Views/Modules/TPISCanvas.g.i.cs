﻿#pragma checksum "..\..\..\..\Views\Modules\TPISCanvas.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "972BEE467C538A025A4C8A209998D4747FF9C2CD"
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
using TPIS.Model;
using TPIS.TPISCanvas;
using TPIS.Views.Modules;


namespace TPIS.TPISCanvas {
    
    
    /// <summary>
    /// ChangeLine
    /// </summary>
    public partial class ChangeLine : System.Windows.ResourceDictionary, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
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
            System.Uri resourceLocater = new System.Uri("/TPIS;component/views/modules/tpiscanvas.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Modules\TPISCanvas.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
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
            case 1:
            
            #line 93 "..\..\..\..\Views\Modules\TPISCanvas.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Port_MouseEnter);
            
            #line default
            #line hidden
            
            #line 93 "..\..\..\..\Views\Modules\TPISCanvas.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Port_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 93 "..\..\..\..\Views\Modules\TPISCanvas.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Port_MouseRightButtonDown);
            
            #line default
            #line hidden
            
            #line 93 "..\..\..\..\Views\Modules\TPISCanvas.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Port_MouseLeftButtonUp);
            
            #line default
            #line hidden
            break;
            case 2:
            
            #line 256 "..\..\..\..\Views\Modules\TPISCanvas.xaml"
            ((System.Windows.Shapes.Polyline)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.LineSelect);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

