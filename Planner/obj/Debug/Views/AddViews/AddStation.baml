   M S B A M L   `   `   `  ����B  >Planner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Planner.Views.AddViewsPlanner.Views.AddViews  5   	   '    !Planner.Views.AddViews.AddStation   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Planner.Views.AddViews     5   	   -   5          � Title$  Nová stanice��5   	   $	��500��6   $	��700��6+   $	��500��67   $	��700��6G   $	��500��6V   $	��700��6f    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      � $��5q�6    �� TextAlignment$ Right=�6   $��Medium"�66   $��14(�6T    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $	��100 �6   �� 5      $	��100 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��170 �6   �� 5      $��* �6   .+�5   
   �� $��Název Stanice:��6   $��0��6*   $��Center=�67   �� 5   
   $��Čas Odjezdu:��6   $��1��6(   $��Center=�65   �� 5   
   $��Místo Odjezdu:��6   $��2��6*   $��Center=�67   �� 5   
   $��Popis:��6   $��3��6"   $��Center=�6/   �� 5   
   $��GPS Souřadnice:��6   $��4��6+   $��Center=�68   �� 5    
   $&�� Je stanice nástupní stanicí?:��6   $��5��69   $��Center=�6F   �� 5!   
   $��Stát:��6   $��6��6!   $��Center=�6.   �� 5"   
   $��Oblast:��6   $��7��6#   $��Center=�60   �� 5$   
   -   5        $��nameTextbox��5$      $��5q�6%   $��1��60   $��0��6@    �� TextAlignment$
 Left=�6M   �� 5%   
   -   5        $��departureTimeTextbox��5%      $��5q�6.   $��1��69   $��1��6I   $
 Left=�6V   �� 5&   
   -   5        $��departurePlaceTextbox��5&      $��5q�6/   $��1��6:   $��2��6J   $
 Left=�6W    �� TextWrapping$
 Wrap=�6l   " �� VerticalScrollBarVisibility$ Visible=�6�   �� 5'   
   -   5        $��descriptionTextbox��5'      $��5q�6,   $��1��67   $��3��6G   $
 Left=�6T   $
 Wrap=�6i   $ Visible=�6}   �� 5(   
   -   5        $��
gpsTextbox��5(      $��5q�6$   $��1��6/   $��4��6?   $
 Left=�6L   �� 5)   
   -   5        $��isInCzeCheckBox��5)   $    d� 	IsChecked$
 TrueY�6   $��5q�6;   $��1��6F   $��5��6V   $��Center=�6c   �� 5*   
   -   5        $��stateCombobox��5*      $��1��6(   $��6��68   $��5q�6E   ��6P   �� *State+6�   �� 5+   
   -	   5        $��regionCombobox��5+      $��1��6)   $��7��69   $��5q�6F   ��6Q   �� *Region+6r   �� 5-   
   	 �� Orientation$	 
Horizontal=�6   $��8��6.   $��Center=�6;   $��Center=�6V   $��1��6s   .+�5.      �� -
   5        $��	addButton��5.      $��Přidat��6&   $��	10 5 10 5q�67   
 �� 	IsDefault
 . 6c   �� 5/      -   5        $��cancelButton��5/      $��Zrušit��6)   $��	10 5 10 5q�6:   $��8 0 0 0q�6N   6_   50      52      53      