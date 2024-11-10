using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace date_calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        Dictionary<TextBox, string> text_for_textbox = new Dictionary<TextBox, string>();
        public MainWindow()
        {
            InitializeComponent();
            //DateTime selectedDate = dp_1.SelectedDate ?? DateTime.MinValue;
            //bool isWeekend = selectedDate.DayOfWeek == DayOfWeek.Saturday || selectedDate.DayOfWeek == DayOfWeek.Sunday;
            text_for_textbox = new Dictionary<TextBox, string> { { tb_count_days, "дней" }, { tb_count_weeks, "недель" }, { tb_count_mounths, "месяцев" }, { tb_count_years, "лет" },{tb_count_days_2,"дней"} };
            cb_select_action.SelectedIndex = 0;
            cb_select_action_2.SelectedIndex = 0;
            rb_3.IsChecked = true;
            rb_1.IsChecked = true;
        }

        private void count_days_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if(textBox.Text == text_for_textbox[textBox]) { textBox.Text = string.Empty; }
            
        }

        private void count_days_MouseLeave(object sender, MouseEventArgs e)
        {
            
            TextBox textBox = sender as TextBox;
            if(textBox.Text == "" && textBox.IsFocused == false)
            {
                textBox.Text = text_for_textbox[textBox];
            }
        }

        private void tb_count_days_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                textBox.Text = text_for_textbox[textBox];
            }
        }

        private void tb_count_days_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!int.TryParse(e.Text,out int res))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (rb_1.IsChecked == true)
            {
                double days = double.TryParse(tb_count_days.Text, out double result) ? result : 0;
                double week = double.TryParse(tb_count_weeks.Text, out double result2) ? result2*7 : 0;
                int mounths = int.TryParse(tb_count_mounths.Text, out int result3) ? result3 : 0;
                int years = int.TryParse(tb_count_years.Text, out int result4) ? result4 : 0;
                days += week;
                DateTime selectedDate = dp_1.SelectedDate ?? DateTime.MinValue;
                if (cb_select_action.SelectedIndex == 0)
                {
                    
                    bool isWeekend = selectedDate.DayOfWeek == DayOfWeek.Saturday || selectedDate.DayOfWeek == DayOfWeek.Sunday;
                    selectedDate = selectedDate.AddDays(days);
                    selectedDate = selectedDate.AddMonths(mounths);
                    selectedDate = selectedDate.AddYears(years);
                }
                else
                {
                    selectedDate = selectedDate.AddDays(-days);
                    selectedDate = selectedDate.AddMonths(-mounths);
                    selectedDate = selectedDate.AddYears(-years);
                }
                lb_finish_date_1.Content = selectedDate.ToString("dd/MM/yy");
                stack_panel_result_1.Visibility = Visibility.Visible;
                
            }
            if(rb_3.IsChecked == true)
            {
                if (dp_1.Text == "" || dp_2.Text == "")
                {
                    return;
                }
                stack_panel_2_2.Visibility = Visibility.Visible;
                DateTime startDate = dp_1.SelectedDate.Value< dp_2.SelectedDate.Value? dp_1.SelectedDate.Value: dp_2.SelectedDate.Value;
                DateTime endDate = dp_1.SelectedDate.Value > dp_2.SelectedDate.Value ? dp_1.SelectedDate.Value : dp_2.SelectedDate.Value;

                int workingDays = 0;
                int usuallyDays = 0;

                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        workingDays++; 
                    }
                    usuallyDays++;
                }
                lb_usually_day.Content = usuallyDays.ToString();
                lb_working_day.Content = workingDays.ToString();
            }
            if (rb_2.IsChecked == true)
            {
                double day_count = 0;
                if(double.TryParse(tb_count_days_2.Text, out double res))
                {
                    day_count = res;
                }
                DateTime startDate = dp_1.SelectedDate.Value;
                DateTime endDate = dp_1.SelectedDate.Value;
                int workingDays = 0;
                endDate = endDate.AddDays(day_count);

                if (cb_select_action_2.SelectedIndex == 0)
                {
                    while (workingDays < day_count)
                    {
                        startDate = startDate.AddDays(1);
                        if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
                        {
                            workingDays++;
                        }
                    }
                    
                }
                else
                {
                    while (workingDays < day_count)
                    {
                        startDate = startDate.AddDays(-1);
                        if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
                        {
                            workingDays++;
                        }
                    }
                }
                stack_panel_result_2.Visibility = Visibility.Visible;
                lb_finish_date_2.Content = startDate.ToShortDateString().ToString();
            }
        }

        private void rb_1_Checked(object sender, RoutedEventArgs e)
        {
                if (stack_panel_2_2 != null)
                {
                    stack_panel_2_2.Visibility = Visibility.Hidden;
                    stack_panel_1.Visibility = Visibility.Visible;
                    stack_panel_3.Visibility = Visibility.Collapsed;
                stack_panel_2.Visibility = Visibility.Collapsed;
                stack_panel_result_2.Visibility = Visibility.Collapsed;
            }
                
            
            
        }

        private void rb_3_Checked(object sender, RoutedEventArgs e)
        {
            if (stack_panel_2_2 != null)
            {
                stack_panel_result_2.Visibility = Visibility.Collapsed;
                stack_panel_result_1.Visibility = Visibility.Hidden;
                stack_panel_1.Visibility = Visibility.Collapsed;
                stack_panel_3.Visibility = Visibility.Visible;
                stack_panel_2.Visibility = Visibility.Collapsed;
                stack_panel_2_2.Visibility = Visibility.Collapsed;
            }
        }

        private void rb_2_Checked(object sender, RoutedEventArgs e)
        {
            if (stack_panel_2_2 != null)
            {
                stack_panel_result_2.Visibility = Visibility.Collapsed;
                stack_panel_result_1.Visibility = Visibility.Collapsed;
                stack_panel_1.Visibility = Visibility.Collapsed;
                stack_panel_3.Visibility = Visibility.Collapsed;
                stack_panel_2.Visibility = Visibility.Visible;
                stack_panel_2_2.Visibility = Visibility.Collapsed;
            }
        }
    }
}
