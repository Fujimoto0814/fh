using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp3.Utils
{
    public class MouseDownBehavior
    {
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command"
                , typeof(ICommand)
                , typeof(MouseDownBehavior)
                , new PropertyMetadata(null, OnCommandChanged));

        public static object GetCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter",
                typeof(object),
                typeof(MouseDownBehavior),
                new PropertyMetadata(null));

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;
            if (element == null)
                return;

            if (e.NewValue is ICommand)
            {
                element.MouseDown += element_MouseDown;
            }
            else

            {
                element.MouseDown -= element_MouseDown;

            }
        }

        private static void element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as UIElement;
            if (element == null) return;

            var cmd = GetCommand(element);
            var param = GetCommandParameter(element);
            if (cmd.CanExecute(param))
                cmd.Execute(param);
        }
    }
}