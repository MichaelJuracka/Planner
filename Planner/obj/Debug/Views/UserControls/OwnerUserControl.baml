   M S B A M L   `   `   `  ����B  >Planner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Planner.Views.UserControlsPlanner.Views.UserControls  5      1    +Planner.Views.UserControls.OwnerUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Planner.Views.UserControls     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $	��220 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��350 �6   .+�5   
   � $��0��6   $��0��6   $��14(�6,   $��Medium"�6D   $��10q�6b     5      �� 5      $��40 �6    �� 5      $��* �6    �� 5      $��30 �6    �� 5      $��30 �6    �� 5      $��50 �6     5      �� 5      $��80 �6#   �� 5      $��* �6#   .+�5!      �� $��0��6   $��2��6%   $��Filtr dopravci��69   $��22(�6O   $��Center=�6]   $��Center=�6x   �� 5#      $��1��6   $��0��6%   $��Id��65   $��Bottom=�6?   $
��Left=�6Z   �� 5$      $��2��6   $��0��6%   $��Název��65   $��Center=�6B   $
��Left=�6]   �� 5%      $��3��6   $��0��6%   $��Email��65   $��Center=�6B   $
��Left=�6]   �� 5'      -   5        $��ownerIdTextBox��5'   3   $��1��6   $��1��6#   $��Bottom=�6I   $	��150��6d   $
��Left=�6p   6�   �� 5(      -   5        $��ownerNameTextBox��5(   3   $��2��6   $��1��6#   $��Center=�6K   $	��150��6f   $
��Left=�6r   �� 5)      -   5        $��ownerEmailTextBox��5)   3   $��3��6   $��1��6#   $��Center=�6L   $	��150��6g   $
��Left=�6s   �� 5+      -   5        $��filterOwnerButton��5+      $��Vyhledat��6.   $��4��6A   $��2��6N   $��Center=�6b   $
��Left=�6}   $��80 0 0 0q�6�   $��	10 2 10 2q�6�    �� 	IsDefault . 6�   5-      � 51   
   $��1��6   $��2��6   $��14(�60   $��Medium"�6H    52      �� 53      $	��100 �6#   �� 54      $��* �6#     56      �� 57      $��60 �6    �� 58      $��30 �6    �� 59      $��30 �6    �� 5:      $��* �6    .+�5=      �� $��1��6   $��0��6%   $��Jméno��65   $��Center=�6B   �� 5>      $��2��6   $��0��6%   $��Email��65   $��Center=�6B   �� 5@      -   5        $��nameTextBox��5@   3   $��1��6   $��1��6#   $��Center=�6F   $	��200��6a   $
��Left=�6m   �� 5A      -   5        $��emailTextBox��5A   3   $��2��6   $��1��6#   $��Center=�6G   $	��200��6b   $
��Left=�6n   �� 5C      -   5        $��updateButton��5C      $��Uložit��6)   $��3��6:   $��2��6G   $	��Top=�6[   $��Center=�6s   $��	0 10 50 0q�6�   $��5 0 5 0q�6�   6�   5E      &   System.Windows.Controls.DataGrid  5I   
   -   5        $��dataGridOwners��5I         
IsReadOnly . $��1��6;   $��0��6H      AutoGenerateColumns .     SelectionMode$ Single=�6t      AlternatingRowBackground
 �����   Columns 5J      0  *System.Windows.Controls.DataGridTextColumn  5K      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 40 61   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6<   �� *	OwnerId+  5L      $ Název��6%   $	 * 64   
 6>   �� *Name+  5M      $ Email��6%   $	 * 64   
 6>   �� *Email+5O      5Q      5R      