﻿#pragma checksum "..\..\..\Pages\OperationMenuPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1064927B63A5E8A14C16E10393AFFD90"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MarkScan.Pages;
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


namespace MarkScan.Pages {
    
    
    /// <summary>
    /// OperationMenuPage
    /// </summary>
    public partial class OperationMenuPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\Pages\OperationMenuPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button newBt;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Pages\OperationMenuPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button continuebt;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Pages\OperationMenuPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button testConnectBt;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Pages\OperationMenuPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sendDataBt;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Pages\OperationMenuPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backPage;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Pages\OperationMenuPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox messageTxb;
        
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
            System.Uri resourceLocater = new System.Uri("/MarkScan;component/pages/operationmenupage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\OperationMenuPage.xaml"
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
            this.newBt = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Pages\OperationMenuPage.xaml"
            this.newBt.Click += new System.Windows.RoutedEventHandler(this.newBt_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.continuebt = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\Pages\OperationMenuPage.xaml"
            this.continuebt.Click += new System.Windows.RoutedEventHandler(this.continuebt_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.testConnectBt = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\Pages\OperationMenuPage.xaml"
            this.testConnectBt.Click += new System.Windows.RoutedEventHandler(this.testConnectBt_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.sendDataBt = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\Pages\OperationMenuPage.xaml"
            this.sendDataBt.Click += new System.Windows.RoutedEventHandler(this.sendDataBt_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.backPage = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\Pages\OperationMenuPage.xaml"
            this.backPage.Click += new System.Windows.RoutedEventHandler(this.backPage_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.messageTxb = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

