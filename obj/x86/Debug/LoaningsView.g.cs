﻿#pragma checksum "C:\Users\milos\Documents\Visual Studio 2017\source\repos\ClimbingClub\LoaningsView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2B71E8EAD679483FE0272E82F66FE19A"
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
    partial class LoaningsView : 
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
            case 2: // LoaningsView.xaml line 35
                {
                    this.LoaningList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 4: // LoaningsView.xaml line 47
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.DescriptionButton_Click;
                }
                break;
            case 5: // LoaningsView.xaml line 48
                {
                    global::Windows.UI.Xaml.Controls.Button element5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element5).Click += this.ReturnButton_Click;
                }
                break;
            case 6: // LoaningsView.xaml line 23
                {
                    this.SearchBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBox).TextChanged += this.SearchBox_TextChanged;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBox).LostFocus += this.SearchBox_LostFocus;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SearchBox).GotFocus += this.SearchBox_GotFocus;
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

