﻿#pragma checksum "C:\Users\milos\Documents\Visual Studio 2017\source\repos\ClimbingClub\MemberInfoView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C34715A411F74D14EE3FC023CB877172"
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
    partial class MemberInfoView : 
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
            case 1: // MemberInfoView.xaml line 1
                {
                    this.InfoPage = (global::Windows.UI.Xaml.Controls.Page)(target);
                }
                break;
            case 2: // MemberInfoView.xaml line 23
                {
                    this.MemberInfoBox = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3: // MemberInfoView.xaml line 24
                {
                    this.BackButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BackButton).Click += this.BackButton_Click;
                }
                break;
            case 4: // MemberInfoView.xaml line 36
                {
                    this.PaymentList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 5: // MemberInfoView.xaml line 63
                {
                    this.PaymentButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.PaymentButton).Click += this.PaymentButton_Click;
                }
                break;
            case 6: // MemberInfoView.xaml line 64
                {
                    this.LoaningButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.LoaningButton).Click += this.LoaningButton_Click;
                }
                break;
            case 7: // MemberInfoView.xaml line 65
                {
                    this.ChangeButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.ChangeButton).Click += this.ChangeButton_Click;
                }
                break;
            case 8: // MemberInfoView.xaml line 66
                {
                    this.DeleteButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.DeleteButton).Click += this.DeleteButton_Click;
                }
                break;
            case 9: // MemberInfoView.xaml line 51
                {
                    this.DebtList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 10: // MemberInfoView.xaml line 60
                {
                    this.DebtSum = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 13: // MemberInfoView.xaml line 44
                {
                    global::Windows.UI.Xaml.Controls.Button element13 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element13).Click += this.DeleteButton_Click_1;
                }
                break;
            case 14: // MemberInfoView.xaml line 29
                {
                    this.TrainingCalendar = (global::Windows.UI.Xaml.Controls.CalendarView)(target);
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

