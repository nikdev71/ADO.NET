   M S B A M L   `   `   `  ����@  <Stock, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:StockStock  5   	       Stock.EditSupplier   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Stock     5   	     � Title$  EditSupplier��5   	   $	��350��6   $	��300��6+    � WindowStartupLocation$ CenterScreen=�67    � 	Resources 5	       �� 
TargetType �� Property �� Value��%�   '��       �� 5
   
   " ��6   .�5      ��  � ��$ 20 0 0 0q�6'   6:   �� 5       � ��$ Center=�62   6C   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� -   5        $
��Head��5      $��2��6    $��Add supplier��64   $��Center=�6H   6g   �� 5   
   $��1��6   $��Supplier Name��6!   68   �� 5   
   $��2��6   $��Product(ID)��6!   66   �� 5   
   $��3��6   $��Delivery Name��6!   68   �� 5   
   -   5        $��SupplierName��5      $��1��6&   $��1��63   $��20q�6C   6Q   �� 5    
   -   5        $
��PrID��5       $��2��6   $��2��6+   $��20q�6;   6G   �� 5!   
   -   5        $��DelDate��5!      $��3��6!   $��4��6.   $��20q�6>   6w   �� 5"   
   -   5        $��5��5"      $��Submit��6   $��20q�6/   6R   �� 5#   
   -   5        $��5��5#      $��1��6   $��Cancel��6.   $��20q�6?   6d   5$      5%      