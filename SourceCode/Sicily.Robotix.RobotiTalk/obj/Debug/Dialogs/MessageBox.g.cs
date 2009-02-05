﻿#pragma checksum "..\..\..\Dialogs\MessageBox.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "50079D42DC8A6EC4B473309E79C2ADB7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace Sicily.Robotix.MicroController.CommunicationApplication.Dialogs {
    
    
    /// <summary>
    /// MessageBox
    /// </summary>
    public partial class MessageBox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 4 "..\..\..\Dialogs\MessageBox.xaml"
        internal Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox _this;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Dialogs\MessageBox.xaml"
        internal System.Windows.Controls.Button _ok;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Dialogs\MessageBox.xaml"
        internal System.Windows.Controls.Button _yes;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Dialogs\MessageBox.xaml"
        internal System.Windows.Controls.Button _no;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Dialogs\MessageBox.xaml"
        internal System.Windows.Controls.Button _cancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Sicily.Robotix.RobotiTalk;component/dialogs/messagebox.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Dialogs\MessageBox.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this._this = ((Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox)(target));
            
            #line 7 "..\..\..\Dialogs\MessageBox.xaml"
            this._this.Loaded += new System.Windows.RoutedEventHandler(this.this_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this._ok = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Dialogs\MessageBox.xaml"
            this._ok.Click += new System.Windows.RoutedEventHandler(this.ok_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this._yes = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\Dialogs\MessageBox.xaml"
            this._yes.Click += new System.Windows.RoutedEventHandler(this.yes_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this._no = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\Dialogs\MessageBox.xaml"
            this._no.Click += new System.Windows.RoutedEventHandler(this.no_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this._cancel = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\Dialogs\MessageBox.xaml"
            this._cancel.Click += new System.Windows.RoutedEventHandler(this.cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
