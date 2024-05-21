using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool? procedura = null;
        // Jeśli procedura równa się null to nic się nie dzieje. Jeśli True to oblicza logarytm a jeśli False to ciąg.

        private void wybierzProcedure(object sender, RoutedEventArgs e) // Ta metoda służy do zmiany stanu aplikacji na podstawie wyboru użytkownika między
                                                                        // logarytmy a ciągi. Ustawia odpowiednie właściwości dla przycisków i pola tekstowego
                                                                        // , aby dostosować interfejs użytkownika do wybranej opcji.
        {
            Button btn = sender as Button;
            kalk.Text = "";
            pole.Text = "";
            if (btn.Name == "log")
            {
                procedura = true;
                log.Background = Brushes.Green;
                ciag.Background = Brushes.Red;
                wyborLog.IsEnabled = true;
                wyborLog.SelectedIndex = -1;
                oblicz.IsEnabled = false;
                pole.Watermark = "Wpisz równanie logarytmiczne";
                pole.Width = 284;
                pole.Margin = new Thickness(0, 0, 60, 0);
            }
            else
            {
                procedura = false;
                log.Background = Brushes.Red;
                ciag.Background = Brushes.Green;
                wyborLog.IsEnabled = false;
                pole.IsEnabled = true;
                oblicz.IsEnabled = true;
                pole.Watermark = "Wpisz elementy po przecinku";
                pole.Width = 184;
                pole.Margin = new Thickness(0, 0, 160, 0);
            }
        }

        private void zmianaLog(object sender, RoutedEventArgs e)  //Ta metoda aktywuje pole tekstowe i przycisk obliczania,
                                                                  //gdy procedura logarytmiczna jest wybrana przez użytkownika.
        {
            if (procedura==true) 
            {
                pole.IsEnabled = true;
                oblicz.IsEnabled = true;
            }
        }

        double Evaluate(string expression) //Oblicza wartość wyrażenia zawierającego logarytmy.
                                           //Wykorzystuje rekurencję do rozwiązania wyrażeń
                                           //zagnieżdżonych logarytmów, zastępując je ich wartościami numerycznymi.
        {

            while (expression.Contains("log_"))
            {
                int startIndex = expression.IndexOf("log_");
                int endIndex = expression.IndexOf(")", startIndex);
                string logExpr = expression.Substring(startIndex + 4, endIndex - startIndex - 4);
                string[] parts = logExpr.Split('(');
                double baseValue = double.Parse(parts[0]);
                double argument = double.Parse(parts[1].Replace(".", ","));
                double result = Math.Log(argument, baseValue);
                expression = expression.Replace($"log_{logExpr})", result.ToString().Replace(",", "."));
            }


            while (expression.Contains("log("))
            {
                int startIndex = expression.IndexOf("log(");
                int endIndex = expression.IndexOf(")", startIndex);
                string logExpr = expression.Substring(startIndex + 4, endIndex - startIndex - 4);
                double argument = double.Parse(logExpr.Replace(".", ","));
                double result = Math.Log(argument, 10);
                expression = expression.Replace($"log({logExpr})", result.ToString().Replace(",", "."));
            }

            return EvaluateMathExpression(expression);
        }

        double EvaluateMathExpression(string expression)
        {
            expression = expression.Replace("log_", "Math.Log(");

            return Convert.ToDouble(new System.Data.DataTable().Compute(expression, ""));
        }

        private void obliczenia(object sender, RoutedEventArgs e) // Logika obliczeń.
        {
            if(procedura==true) 
            {
                if (wyborLog.SelectedIndex == 0)
                {
                    try
                    {
                        pole.Text = pole.Text.Replace(" ", "");
                        double result = Evaluate(pole.Text);
                        kalk.Text = "Wynik: "+result.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else if (wyborLog.SelectedIndex == 1)
                {
                    string expression = pole.Text;

                    expression = expression.Replace(" ", "");
                    expression = expression.ToLower();

                    if (!expression.StartsWith("log_") && !expression.StartsWith("log(") || !expression.Contains("="))
                    {
                        MessageBox.Show("Nieprawidłowy format równania logarytmicznego!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    double result;
                    if (expression.StartsWith("log("))
                    {
                        if (double.TryParse(expression.Substring(expression.IndexOf("=") + 1), out result))
                        {
                            double x = Math.Pow(Math.E, result);
                            kalk.Text = $"Wartość x to: {x}";

                        }
                        else
                        {
                            MessageBox.Show("Nieprawidłowa wartość liczby!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                    {
                        string baseString = expression.Substring(4, expression.IndexOf("(") - 4);
                        double logBase;
                        if (double.TryParse(baseString, out logBase) && double.TryParse(expression.Substring(expression.IndexOf("=") + 1), out result))
                        {
                            double x = Math.Pow(logBase, result);
                            kalk.Text = $"Wartość x to: {x}";
                        }
                        else
                        {
                            MessageBox.Show("Nieprawidłowa wartość podstawy logarytmu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                }
                else
                {
                    string expression = pole.Text;
                    expression = expression.Replace(" ", "");
                    if (!expression.StartsWith("log_") || !expression.Contains(")") || !expression.Contains("=")){MessageBox.Show("Nieprawidłowy format równania!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);}
                    int baseIndex = expression.IndexOf("log_") + 4;
                    int openParenIndex = expression.IndexOf("(");
                    int closeParenIndex = expression.IndexOf(")");
                    int equalIndex = expression.IndexOf("=");
                    if (double.TryParse(expression.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1), out double argument) &&
                        double.TryParse(expression.Substring(equalIndex + 1), out double result))
                    {
                        double baseValue = Math.Pow(argument, 1 / result);
                        kalk.Text = $"Wartość x to: {baseValue}";
                    }
                    else
                    {
                        MessageBox.Show("Wprowadzono nieprawidłowe wartości!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            else 
            {
                kalk.Text = "";
                int n = pole.Text.Replace(" ", "").Split(',').Length;
                double[] elements = new double[n];
                try
                {
                    for (int i = 0; i < pole.Text.Replace(" ", "").Split(',').Length; i++)
                    {
                        elements[i] = Convert.ToDouble(pole.Text.Replace(" ", "").Split(',')[i]);
                    }
                    double[] differences = new double[n - 1];
                    for (int i = 0; i < n - 1; i++)
                    {
                        differences[i] = elements[i + 1] - elements[i];
                    }
                    bool isArithmetic = true;
                    bool isGeometric = true;
                    for (int i = 1; i < n - 1; i++)
                    {
                        if (differences[i] != differences[i - 1])
                        {
                            isArithmetic = false;
                            break;
                        }
                    }
                    double ratio = elements[1] / elements[0];
                    for (int i = 1; i < n - 1; i++)
                    {
                        if (elements[i + 1] / elements[i] != ratio)
                        {
                            isGeometric = false;
                            break;
                        }
                    }
                    int czyrosnie = 0;
                    int sprawdzacz = 0;
                    for (int i = 0; i < n - 1; i++)
                    {
                        if (elements[i + 1] > elements[i])
                        {
                            czyrosnie = 1;
                            if (sprawdzacz == 2)
                            {
                                kalk.Text += "\nCiąg jest zmienny";
                                czyrosnie = 0;
                                break;
                            }
                            sprawdzacz = 1;
                        }
                        else if (elements[i + 1] < elements[i])
                        {
                            czyrosnie = 2;
                            if (sprawdzacz == 1)
                            {
                                kalk.Text += "\nCiąg jest zmienny";
                                czyrosnie = 0;
                                break;
                            }
                            sprawdzacz = 2;
                        }
                        else
                        {
                            kalk.Text += "\nCiag jest stały";
                            break;
                        }
                    }
                    if (czyrosnie == 1)
                    {
                        kalk.Text += "\nCiag rośnie";
                    }
                    else if (czyrosnie == 2)
                    {
                        kalk.Text += "\nCiąg Maleje";
                    }
                    if (isArithmetic)
                    {
                        kalk.Text += "\nCiąg arytmetyczny";
                    }
                    else if (isGeometric)
                    {
                        kalk.Text += "\nCiąg geometryczny";
                    }
                    else
                    {
                        kalk.Text += "\nInny ciąg";
                    }
                    int nth = Convert.ToInt32(nn.Text);
                    if (isArithmetic)
                    {
                        double a = elements[0];
                        double d = differences[0];
                        double nthElement = a + (nth - 1) * d;
                        kalk.Text +=  $"\nWartość {nth} elementu ciągu wynosi: {nthElement}";
                    }
                    else if (isGeometric)
                    {
                        double a = elements[0];
                        double r = ratio;
                        double nthElement = a * Math.Pow(r, nth - 1);
                        kalk.Text += $"\nWartość {nth} elementu ciągu wynosi: {nthElement}";
                    }
                    else
                    {
                        MessageBox.Show("Nie można obliczyć wartości elementu ciągu dla innego typu ciągu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Ciąg został nieprawidłowo wprowadzony", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
    }
}