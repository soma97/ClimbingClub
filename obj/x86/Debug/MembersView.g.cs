﻿#pragma checksum "C:\Users\milos\Documents\Visual Studio 2017\source\repos\ClimbingClub\MembersView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A1D0F71BD5A5961B27DE94A4715866BE"
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
    partial class MembersView : 
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
            case 2: // MembersView.xaml line 12
                {
                    this.MemberGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // MembersView.xaml line 40
                {
                    this.MemberList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 5: // MembersView.xaml line 48
                {
                    global::Windows.UI.Xaml.Controls.CheckBox element5 = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    ((global::Windows.UI.Xaml.Controls.CheckBox)element5).Click += this.CheckBox_Click;
                }
                break;
            case 6: // MembersView.xaml line 49
                {
                    global::Windows.UI.Xaml.Controls.Button element6 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element6).Click += this.DetailsButton_Click;
                }
                break;
            case 7: // MembersView.xaml line 21
                {
                    this.SearchBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBox).GotFocus += this.SearchBox_GotFocus;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBox).LostFocus += this.SearchBox_LostFocus;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBox).TextChanged += this.SearchBox_TextChanged;
                }
                break;
            case 8: // MembersView.xaml line 22
                {
                    this.NameBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.NameBox).LostFocus += this.NameBox_LostFocus;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.NameBox).GotFocus += this.NameBox_GotFocus;
                }
                break;
            case 9: // MembersView.xaml line 23
                {
                    this.SurnameBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SurnameBox).GotFocus += this.SurnameBox_GotFocus;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SurnameBox).LostFocus += this.SurnameBox_LostFocus;
                }
                break;
            case 10: // MembersView.xaml line 24
                {
                    this.AddButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddButton).Click += this.AddButton_Click;
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

