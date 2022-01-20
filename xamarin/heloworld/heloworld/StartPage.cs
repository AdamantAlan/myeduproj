using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace heloworld
{
    class StartPage:ContentPage
    {
        public StartPage()
        {
            Label label = new Label() { Text = "Hello world!"};
            Content = label;
            Button button = new Button();
        }

    }
}
