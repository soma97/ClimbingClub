﻿#pragma checksum "C:\Users\milos\Documents\Visual Studio 2017\source\repos\ClimbingClub\HomeView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3864EB0A7D7725F32E71185D759410AF"
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
    partial class HomeView : 
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
            case 2: // HomeView.xaml line 23
                {
                    this.UsernameBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3: // HomeView.xaml line 25
                {
                    this.PasswordBox = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 4: // HomeView.xaml line 26
                {
                    this.LoginButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.LoginButton).Click += this.LoginButton_Click;
                }
                break;
            case 5: // HomeView.xaml line 27
                {
                    this.AddUserButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddUserButton).Click += this.AddUserButton_Click;
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

