#pragma checksum "C:\Users\HassanAbbas\Desktop\wbclient\wbclient\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F223F5A397FF27B74AF62FFE5B452885"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace wbclient {
    
    
    public partial class MainPage : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button btnDrawPen;
        
        internal System.Windows.Controls.Button btnDrawLine;
        
        internal System.Windows.Controls.Button btnDrawCircle;
        
        internal System.Windows.Controls.Button btnDrawCircle_Copy;
        
        internal System.Windows.Controls.Button btnDrawTriangle;
        
        internal System.Windows.Controls.Button btnDrawEraser;
        
        internal System.Windows.Controls.Button btnHiglight;
        
        internal System.Windows.Controls.Button btnDrawSelect;
        
        internal System.Windows.Controls.Button btnDrawClearCanvas;
        
        internal System.Windows.Controls.ComboBox roomselected_combo;
        
        internal System.Windows.Controls.Button Connect_btn;
        
        internal System.Windows.Controls.ComboBox combBordSize;
        
        internal System.Windows.Controls.ComboBox combColorPik;
        
        internal System.Windows.Controls.ComboBox combFillCol;
        
        internal System.Windows.Controls.TextBox uname_textbox;
        
        internal System.Windows.Controls.Canvas paintcanvas;
        
        internal System.Windows.Media.Animation.Storyboard storyBoard;
        
        internal System.Windows.Controls.TextBox receiveBox;
        
        internal System.Windows.Controls.ListBox roomusers_lbox;
        
        internal System.Windows.Controls.TextBox sendBox;
        
        internal System.Windows.Controls.Button Send_Btn;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/wbclient;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.btnDrawPen = ((System.Windows.Controls.Button)(this.FindName("btnDrawPen")));
            this.btnDrawLine = ((System.Windows.Controls.Button)(this.FindName("btnDrawLine")));
            this.btnDrawCircle = ((System.Windows.Controls.Button)(this.FindName("btnDrawCircle")));
            this.btnDrawCircle_Copy = ((System.Windows.Controls.Button)(this.FindName("btnDrawCircle_Copy")));
            this.btnDrawTriangle = ((System.Windows.Controls.Button)(this.FindName("btnDrawTriangle")));
            this.btnDrawEraser = ((System.Windows.Controls.Button)(this.FindName("btnDrawEraser")));
            this.btnHiglight = ((System.Windows.Controls.Button)(this.FindName("btnHiglight")));
            this.btnDrawSelect = ((System.Windows.Controls.Button)(this.FindName("btnDrawSelect")));
            this.btnDrawClearCanvas = ((System.Windows.Controls.Button)(this.FindName("btnDrawClearCanvas")));
            this.roomselected_combo = ((System.Windows.Controls.ComboBox)(this.FindName("roomselected_combo")));
            this.Connect_btn = ((System.Windows.Controls.Button)(this.FindName("Connect_btn")));
            this.combBordSize = ((System.Windows.Controls.ComboBox)(this.FindName("combBordSize")));
            this.combColorPik = ((System.Windows.Controls.ComboBox)(this.FindName("combColorPik")));
            this.combFillCol = ((System.Windows.Controls.ComboBox)(this.FindName("combFillCol")));
            this.uname_textbox = ((System.Windows.Controls.TextBox)(this.FindName("uname_textbox")));
            this.paintcanvas = ((System.Windows.Controls.Canvas)(this.FindName("paintcanvas")));
            this.storyBoard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("storyBoard")));
            this.receiveBox = ((System.Windows.Controls.TextBox)(this.FindName("receiveBox")));
            this.roomusers_lbox = ((System.Windows.Controls.ListBox)(this.FindName("roomusers_lbox")));
            this.sendBox = ((System.Windows.Controls.TextBox)(this.FindName("sendBox")));
            this.Send_Btn = ((System.Windows.Controls.Button)(this.FindName("Send_Btn")));
        }
    }
}

