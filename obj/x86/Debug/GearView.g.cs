﻿#pragma checksum "C:\Users\milos\Documents\Visual Studio 2017\source\repos\ClimbingClub\GearView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E2CEE04329BB071FC1ECE46F5570DB32"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClimbingClub
{
    partial class GearView : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // GearView.xaml line 34
                {
                    this.GearList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 4: // GearView.xaml line 42
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.DeleteButton_Click;
                }
                break;
            case 5: // GearView.xaml line 23
                {
                    this.NameBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.NameBox).LostFocus += this.NameBox_LostFocus;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.NameBox).GotFocus += this.NameBox_GotFocus;
                }
                break;
            case 6: // GearView.xaml line 24
                {
                    this.DescrBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.DescrBox).GotFocus += this.DescrBox_GotFocus;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.DescrBox).LostFocus += this.DescrBox_LostFocus;
                }
                break;
            case 7: // GearView.xaml line 25
                {
                    this.AddGearButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddGearButton).Click += this.AddGearButton_Click;
                }
                break;
            case 8: // GearView.xaml line 26
                {
                    this.SearchBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBox).GotFocus += this.SearchBox_GotFocus;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBox).LostFocus += this.SearchBox_LostFocus;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBox).TextChanged += this.SearchBox_TextChanged;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

