   M S B A M L   `   `   `  ����>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WPF.ViewWPF.View  5   	       WPF.View.RegularView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WPF.View     5   	     � Title$  RegularView��5   	   $	��350��6   $	��550��6*   .��5
      � .+�5   
   �� .@�5      �� $��Golden membership��6   .��5      � .+�5      �� -   5        $��Change��5      $
��Left=�6.   $��227,140,0,0q�6I   $	��Top=�6^   $��75��6v   6�   5      5      �� 5      $��Vehicles��6   .��5      � .+�5      &   System.Windows.Controls.DataGrid  -   5        $��dataGrid��5   �      AutoGenerateColumns .  $	��247��6�   $
��Left=�6�   $��
129,31,0,0q�6�   $	��Top=�6�   $	��393��6     SelectionMode$ Single=�6     
IsReadOnly . ��6   ��  �� Path$
 List� �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem 6f   �� *
Selected+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��61   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6?   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6I   �� *Name+  5      $ Model��61   $	 * 6@   
 6J   �� *Model+  5      $
 Year��61   $	 * 6?   
 6I   �� *Year+  5      $ PricePerHour��61   $	 * 6G   
 6Q   �� *PricePerHour+  5      $ PriceVehicle��61   $	 * 6G   
 6Q   �� *PriceVehicle+  5      $ 	Available��61   $	 * 6D   
 6N   �� *	Available+5      �� 5      -   5         ��Name$
 Rent��5      $
��Rent��6+   $
��Left=�6:   $��	10,10,0,0q�6U   $	��Top=�6h   $��75��6�   ��6�   �� *RentVehicleCommand+(  "System.Windows.Controls.DatePicker  5       -   5        $
��Left=�5    !   $��	10,53,0,0q�6<   $	��Top=�6O   6g   �� 5!      -   5         ��Name$ billTextBox��5!      $
��Left=�63   $��23��6N   $��
10,103,0,0q�6Z    �� TextWrapping$
 Wrap=�6n   $�� ��6�   $	��Top=�6�   $	��100��6�   5#      5$      �� 5%      $��LogOut��6   .��5&      � .+�5'      �� -   5        $ LogOut��5'      $��LogOut��6-   $
��Left=�6>   $��217,117,0,0q�6Y   $	��Top=�6n   $��75��6�   6�   5(      5)      5*      5+      5,      