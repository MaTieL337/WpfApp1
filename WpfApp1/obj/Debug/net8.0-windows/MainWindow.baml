   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ] YXceed.Wpf.Toolkit.NET5, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f41t)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   !localclr-namespace:WpfApp1  5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��400��6)   $��Arial)�65   $��16(�6H    � 
ResizeMode$ NoResize=�6V   .��5
      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $!��Wybierz opcję obliczania: ��6   $��2��66    �� TextAlignment$ Center=�6J   $��0 5 0 0q�6a   �� 5   
   -   5        $	��log��5   c   $��20,30,20,10q�6   
���  ��
�������.��6n   
Logarytm6x   �� 5   
   -   5        $��wyborLog��5   �   $��
20,72,20,0q�6   $��22��6'   $	��Top=�63   $��2��6K   $��12(�6\   ${�false��6�   .@�5      �� .��6   Oblicz działanie6-   �� 5      .��6   Znajdz liczbe logarytmowaną68   �� 5      .��6   Znajdz podstawe6,   5      �� 5   
   -   5        $
��ciag��5   s   $��1��6   
���  ��$��20,30,20,10q�62   
�������.��6   Ciąg6�   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5   
   -   5        $��nn��5   �   $��1��6   $��25��6+   $��2��67   $	��100��6K   $��	124 0 0 0q�6W      	Watermark$ Wartość N��6j   $��14(�6�   $
{�True��6�     5   
   -   5        $
��pole��5   v   $��1��6   $��25��6+   $��2��67   $	��284��6K   $��0 0 60 0q�6W   $  ��6i   $��14(�6�   ${�False��6�   �� 5    
   -   5        $��oblicz��5    �   $��1��6   $��1��6   $��right=�6.   $��25��6J   
�������$��60��6m   $��0 0 20 0q�6x   ${�false��6�   .��6�   Oblicz6�   �� 5!   
   -   5        $
��kalk��5!   5   $��2��6   $��2��6!   $�� ��6A   $��10 0 0 0q�6I   6]   5"      5#      