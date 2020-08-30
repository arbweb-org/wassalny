using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace p_wassalny
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
        }
    }
}