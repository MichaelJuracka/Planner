   M S B A M L   `   `   `  ����B  >Planner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Planner.Views.AddViewsPlanner.Views.AddViews  5   	   )    #Planner.Views.AddViews.AddPassenger   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Planner.Views.AddViews     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ AddPassenger��5	   	   $	��610��6   $	��650��6+   .��5
      �  �� TextAlignment$ Right=�6   $��14(�6+   $��Medium"�6C   $��5q�6a    � ColumnDefinitions 5   
   �� 5      $	��170 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $	��100 �6   �� 5      $��35 �6   �� 5      $��60 �6   �� 5      $��60 �6   �� 5      $��60 �6   �� 5      $��* �6   .+�5   
   �� $��Obchodní připad:��6   $��0��6,   $��Center=�69   �� 5   
   $��Jméno:��6   $��1��6"   $��Center=�6/   �� 5   
   $��Příjmení:��6   $��2��6%   $��Center=�62   �� 5    
   $��Telefon:��6   $��3��6$   $��Center=�61   �� 5!   
   $��Email:��6   $��4��6"   $��Center=�6/   �� 5"   
   $��Dodatečné informace:��6   $��5��60   $��Center=�6=   �� 5#   
   $��
Vlastník:��6   $��6��6%   $��Center=�62   �� 5$   
   $��Jízda:��6   $��7��6"   $��Center=�6/   �� 5%   
   -   5        $��boardingTextBlock��5%   U   $��Nástupní stanice:��6   $��8��6-   $��Center=�6:   6U   �� 5&   
   -   5        $��exitTextBlock��5&   U   $��Výstupní stanice:��6   $��9��6-   $��Center=�6:   6U   �� 5(   
   -   5        $��businesCaseTextBox��5(      $��0��6,   $��1��69   $��5q�6�    �� TextAlignment$
 Left=�6�   �� 5)   
   -   5        $��firstNameTextBox��5)      $��1��6*   $��1��67   $��5q�6G   $
 Left=�6R   �� 5*   
   -   5        $��secondNameTextBox��5*      $��2��6+   $��1��68   $��5q�6H   $
 Left=�6S   �� 5+   
   -   5        $��phoneTextBox��5+      $��3��6&   $��1��63   $��5q�6C   $
 Left=�6N   �� 5,   
   -   5        $��emailTextBox��5,      $��4��6&   $��1��63   $��5q�6C   $
 Left=�6N   �� 5-   
   -   5        $��additionInfoTextBox��5-      $��5��6-   $��1��6:    �� TextWrapping$
 Wrap=�6J   " �� VerticalScrollBarVisibility$ Visible=�6^   $��5q�6�   $
 Left=�6�   �� 5.   
   -	   5        $��ownerComboBox��5.      $��6��6(   $��1��65   $��5q�6E   �� 5/   
   $��7��6   $��1��6"   $
��Left=�62    �� Orientation$ Vertical=�6M   $��5q�6d   .+�50      �� -
   5        $��routeTextBlock��50      $y�Hidden=�6.   �� 51      -   5        $��chooseRouteButton��51      $��Vybrat jízdu��6.   $��0 8 0 0q�6E   $��5 0 5 0q�6V   6h   52      �� 53   
   $��8��6   $��1��6"   $
��Left=�62   $ Vertical=�6M   $��5q�6d   .+�54      �� -   5        $��boardingStationTextBlock��54      $y�Hidden=�68   $
��Left=�6L   �� 55      -   5        $!��chooseBoardingStationButton��55      $��Vybrat nástupní stanici��68   $��0 8 0 0q�6Z   $��5 0 5 0q�6k   6}   56      �� 57   
   $��9��6   $��1��6"   $
��Left=�62   $ Vertical=�6M   $��5q�6d   .+�58      �� -   5        $��exitStationTextBlock��58      $y�Hidden=�64   $
��Left=�6H   �� 59      -   5        $��chooseExitStationButton��59      $��Vybrat výstupní stanici��64   $��0 8 0 0q�6V   $��5 0 5 0q�6g   6y   5:      �� 5<   
   $��10��6   $��2��6#   $��Center=�67   $��Center=�6R   $ 
Horizontal=�6o   .+�5=      �� -   5        $��submitButton��5=      $��Přidej��6)   $��	10 5 10 5q�6:   	 �� 	IsDefault	 . 6_   �� 5>      -   5        $��cancelButton��5>      $��Zrušit��6)   $��	10 5 10 5q�6:   $��8 0 0 0q�6N   6_   5?      5A      5B      