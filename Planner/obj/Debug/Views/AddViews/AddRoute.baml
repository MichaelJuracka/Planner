   M S B A M L   `   `   `  ����B  >Planner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Planner.Views.AddViewsPlanner.Views.AddViews  5   	   %    Planner.Views.AddViews.AddRoute   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Planner.Views.AddViews     5   	   -   5          � Title$  Nová jízda��5   	   $	��400��6   $	��650��6)   $	��400��65   $	��650��6E   $	��400��6T   $	��650��6d    � WindowStartupLocation$ CenterScreen=�5
   	   .��5      �  �� TextAlignment$ Right=�6   $��14(�6+   $��Medium"�6C   $��5q�6a    � ColumnDefinitions 5   
   �� 5      $	��170 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��35 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��30 �6   �� 5      $��35 �6   �� 5      $��* �6   .+�5   
   �� $��Datum odjezdu:��6   $��0��6*   $��Center=�67   �� 5   
   $��Svozová jízda:��6   $��1��6*   $��Center=�67   �� 5   
   $��Jízda zpět:��6   $��2��6'   $��Center=�64   �� 5    
   $��Reálná jízda:��6   $��3��6)   $��Center=�66   �� 5!   
   -   5        $��stateTextBlock��5!   P   $��Cílový stát:��6   $��4��6(   $��Center=�65   6P   �� 5"   
   -   5        $��regionTextBlock��5"   R   $��Cílová oblast:��6   $��5��6*   $��Center=�67   6R   �� 5#   
   $��	Dopravce:��6   $��6��6%   $��Center=�62   �� 5$   
   $��Spz autobusu:��6   $��7��6)   $��Center=�66   �� 5%   
   $��Typ autobusu:��6   $��8��6)   $��Center=�66   (  "System.Windows.Controls.DatePicker  5'   
   -   5        $��departureDatePicker��5'   `   $��5q�6   $��1��6    $��0��60   $
��Left=�6=      Text$  ��6X   6`   �� 5(   
   -   5        $��boardingRouteCheckbox��5(   h   $��5q�6   $��1��6   $��1��6.   $
��Left=�6;    d� 	IsChecked$ FalseY�6V   6�   �� 5)   
   -   5        $��routeBackCheckbox��5)   h   $��5q�6   $��1��6   $��2��6.   $
��Left=�6;   $ FalseY�6V   6�   �� 5*   
   -   5        $��realRouteCheckbox��5*   h   $��5q�6   $��1��6   $��3��6.   $
��Left=�6;   $ FalseY�6V   6h   �� 5+   
   -   5        $��stateComboBox��5+   ;   $��4��6   $��1��6    $��5q�60   6P   �� 5,   
   -	   5        $��regionComboBox��5,   ;   $��5��6   $��1��6    $��5q�60   6;   �� 5-   
   -
   5        $��providerComboBox��5-   ;   $��6��6   $��1��6    $��5q�60   6;   �� 5.   
   -   5        $��licensePlateTextBox��5.   ;   $��7��6   $��1��6   $��5q�60    �� TextAlignment$
 Left=�6V   �� 5/   
   -   5        $��busTypeComboBox��5/   ;   $��8��6   $��1��6    $��5q�60   6;   �� 51   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��Center=�6K   $��9��6f   $��2��6s   .+�52      �� -   5        $��	addButton��52      $��Přidat��6&   $��	10 5 10 5q�67   	 �� 	IsDefault	 . 6v   �� 53      -   5        $��cancelButton��53      $��Zrušit��6)   $��	10 5 10 5q�6:   $��8 0 0 0q�6N   6|   54      56      57      