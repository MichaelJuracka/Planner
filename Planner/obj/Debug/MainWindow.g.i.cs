// Updated by XamlIntelliSenseFileGenerator 04.01.2021 13:18:00
#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "549D833B2623ED2D2BBC2C503B4D9B537F890C9BCB044B3EF0FC4D1B347A8B2D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Planner;
using Planner.Views.UserControls;
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


namespace Planner
{


    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 19 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image busImage;

#line default
#line hidden


#line 22 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Home;

#line default
#line hidden


#line 31 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem newRoute;

#line default
#line hidden


#line 36 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem newPassenger;

#line default
#line hidden


#line 41 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem newStation;

#line default
#line hidden


#line 47 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem importPassengersContextMenu;

#line default
#line hidden


#line 49 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem importStationContextMenu;

#line default
#line hidden


#line 53 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem newState;

#line default
#line hidden


#line 55 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem newRegion;

#line default
#line hidden


#line 57 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem newProvider;

#line default
#line hidden


#line 68 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem uCRouteMenu;

#line default
#line hidden


#line 73 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem uCPassengerMenu;

#line default
#line hidden


#line 78 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem uCStationMenu;

#line default
#line hidden


#line 83 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem uCStateRegionProviderMenu;

#line default
#line hidden


#line 93 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem stationOrderTabControl;

#line default
#line hidden


#line 100 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Planner.Views.UserControls.RouteUserControl ucRoute;

#line default
#line hidden


#line 101 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Planner.Views.UserControls.StationUserControl ucStation;

#line default
#line hidden


#line 102 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Planner.Views.UserControls.StateRegionProviderUserControl uCStateRegionProvider;

#line default
#line hidden


#line 103 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Planner.Views.UserControls.PassengerUserControl uCPassenger;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Planner;component/mainwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler)
        {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:

#line 9 "..\..\MainWindow.xaml"
                    ((Planner.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);

#line default
#line hidden
                    return;
                case 2:
                    this.busImage = ((System.Windows.Controls.Image)(target));
                    return;
                case 3:
                    this.Home = ((System.Windows.Controls.MenuItem)(target));

#line 22 "..\..\MainWindow.xaml"
                    this.Home.Click += new System.Windows.RoutedEventHandler(this.Home_Click);

#line default
#line hidden
                    return;
                case 4:
                    this.newRoute = ((System.Windows.Controls.MenuItem)(target));

#line 31 "..\..\MainWindow.xaml"
                    this.newRoute.Click += new System.Windows.RoutedEventHandler(this.newRoute_Click);

#line default
#line hidden
                    return;
                case 5:
                    this.newPassenger = ((System.Windows.Controls.MenuItem)(target));

#line 36 "..\..\MainWindow.xaml"
                    this.newPassenger.Click += new System.Windows.RoutedEventHandler(this.newPassenger_Click);

#line default
#line hidden
                    return;
                case 6:
                    this.newStation = ((System.Windows.Controls.MenuItem)(target));

#line 41 "..\..\MainWindow.xaml"
                    this.newStation.Click += new System.Windows.RoutedEventHandler(this.newStation_Click);

#line default
#line hidden
                    return;
                case 7:
                    this.importPassengersContextMenu = ((System.Windows.Controls.MenuItem)(target));

#line 47 "..\..\MainWindow.xaml"
                    this.importPassengersContextMenu.Click += new System.Windows.RoutedEventHandler(this.importPassengersContextMenu_Click);

#line default
#line hidden
                    return;
                case 8:
                    this.importStationContextMenu = ((System.Windows.Controls.MenuItem)(target));

#line 49 "..\..\MainWindow.xaml"
                    this.importStationContextMenu.Click += new System.Windows.RoutedEventHandler(this.importStationContextMenu_Click);

#line default
#line hidden
                    return;
                case 9:
                    this.newState = ((System.Windows.Controls.MenuItem)(target));

#line 53 "..\..\MainWindow.xaml"
                    this.newState.Click += new System.Windows.RoutedEventHandler(this.newState_Click);

#line default
#line hidden
                    return;
                case 10:
                    this.newRegion = ((System.Windows.Controls.MenuItem)(target));

#line 55 "..\..\MainWindow.xaml"
                    this.newRegion.Click += new System.Windows.RoutedEventHandler(this.newRegion_Click);

#line default
#line hidden
                    return;
                case 11:
                    this.newProvider = ((System.Windows.Controls.MenuItem)(target));

#line 57 "..\..\MainWindow.xaml"
                    this.newProvider.Click += new System.Windows.RoutedEventHandler(this.newProvider_Click);

#line default
#line hidden
                    return;
                case 12:
                    this.test = ((System.Windows.Controls.MenuItem)(target));

#line 60 "..\..\MainWindow.xaml"
                    this.test.Click += new System.Windows.RoutedEventHandler(this.test_Click);

#line default
#line hidden
                    return;
                case 13:
                    this.uCRouteMenu = ((System.Windows.Controls.MenuItem)(target));

#line 68 "..\..\MainWindow.xaml"
                    this.uCRouteMenu.Click += new System.Windows.RoutedEventHandler(this.uCRouteMenu_Click);

#line default
#line hidden
                    return;
                case 14:
                    this.uCPassengerMenu = ((System.Windows.Controls.MenuItem)(target));

#line 73 "..\..\MainWindow.xaml"
                    this.uCPassengerMenu.Click += new System.Windows.RoutedEventHandler(this.uCPassengerMenu_Click);

#line default
#line hidden
                    return;
                case 15:
                    this.uCStationMenu = ((System.Windows.Controls.MenuItem)(target));

#line 78 "..\..\MainWindow.xaml"
                    this.uCStationMenu.Click += new System.Windows.RoutedEventHandler(this.uCStationMenu_Click);

#line default
#line hidden
                    return;
                case 16:
                    this.uCStateRegionProviderMenu = ((System.Windows.Controls.MenuItem)(target));

#line 83 "..\..\MainWindow.xaml"
                    this.uCStateRegionProviderMenu.Click += new System.Windows.RoutedEventHandler(this.uCStateRegionProviderMenu_Click);

#line default
#line hidden
                    return;
                case 17:
                    this.stationOrderTabControl = ((System.Windows.Controls.MenuItem)(target));

#line 93 "..\..\MainWindow.xaml"
                    this.stationOrderTabControl.Click += new System.Windows.RoutedEventHandler(this.stationOrderTabControl_Click);

#line default
#line hidden
                    return;
                case 18:
                    this.ucRoute = ((Planner.Views.UserControls.RouteUserControl)(target));
                    return;
                case 19:
                    this.ucStation = ((Planner.Views.UserControls.StationUserControl)(target));
                    return;
                case 20:
                    this.uCStateRegionProvider = ((Planner.Views.UserControls.StateRegionProviderUserControl)(target));
                    return;
                case 21:
                    this.uCPassenger = ((Planner.Views.UserControls.PassengerUserControl)(target));
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

