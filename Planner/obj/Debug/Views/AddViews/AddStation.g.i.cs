﻿#pragma checksum "..\..\..\..\Views\AddViews\AddStation.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A5382F273CD67101FD2C1F4DA16759D30239561B40D964102622F1434FE43296"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Planner.Views.AddViews;
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


namespace Planner.Views.AddViews {
    
    
    /// <summary>
    /// AddStation
    /// </summary>
    public partial class AddStation : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\..\Views\AddViews\AddStation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nameTextbox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Views\AddViews\AddStation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox departureTimeTextbox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Views\AddViews\AddStation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox departurePlaceTextbox;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Views\AddViews\AddStation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descriptionTextbox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Views\AddViews\AddStation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox gpsTextbox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Views\AddViews\AddStation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox isInCzeCheckBox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Views\AddViews\AddStation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox stateCombobox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Views\AddViews\AddStation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox regionCombobox;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Views\AddViews\AddStation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addButton;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Views\AddViews\AddStation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancelButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Planner;component/views/addviews/addstation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\AddViews\AddStation.xaml"
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
            
            #line 9 "..\..\..\..\Views\AddViews\AddStation.xaml"
            ((Planner.Views.AddViews.AddStation)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.nameTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.departureTimeTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.departurePlaceTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.descriptionTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.gpsTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.isInCzeCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            this.stateCombobox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 42 "..\..\..\..\Views\AddViews\AddStation.xaml"
            this.stateCombobox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.stateCombobox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.regionCombobox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.addButton = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\..\Views\AddViews\AddStation.xaml"
            this.addButton.Click += new System.Windows.RoutedEventHandler(this.addButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.cancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\..\Views\AddViews\AddStation.xaml"
            this.cancelButton.Click += new System.Windows.RoutedEventHandler(this.cancelButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
