   M S B A M L   `   `   `  ����K  GAVDSupervisorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AVDSupervisorApp.ViewsAVDSupervisorApp.Views  5   	   '    !AVDSupervisorApp.Views.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:AVDSupervisorApp.Views     5   	     � WindowStartupLocation$  CenterScreen=�5       � Title$ AVD Supervisor App��5   	   $	��450��6$   $	��800��61    � 	Resources 5	       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers	 P� Property
 P� Value��%A  '��       �� 5
   
   " ��6   .�5      ��  � �� �#007ACC6+   �� 5       � ��
 �����6+   �� 5       � ��
 ���� 6,   �� 5       � ��$ 0q�60   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 8��6K   #��z��6\   #��z��6�   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5       5      P� 5      	 �@J�IsMouseOver$

 True��61   . �5      ��  � �� �#66B3FF63   �� 5       � ��
 �   �63   �� 5       � ��
 �   �64   �� 5       � ��$	 1.5q�68   5      P� 5      	 � {�$
 False��6/   . �5       ��  � ��
 �����63   �� 5!       � ��
 �����63   �� 5"       �@�ToolTip$$ Please select a user to start!��60   5#      5%      5&      .��5'      � ���#2ce3a0 � RowDefinitions 5)   
   �� 5*      $
��Auto �6   �� 5+      $��* �6   .+�5.   
   �� $��0��6   $��50��6   
������ $��10q�6C    �� Padding$ 5q�6O   $ 8��6[   ._�5/      �  � ColumnDefinitions 50      �� 51      $
��1.5* �6'   �� 52      $
��Auto �6'   �� 53      $��* �6'   .+�56      �� -   5         ��Name$ SearchTextBox��56      
 � Tag$ Search Email��61   $��0q�6D   
���   �$��Center=�57      $��Stretch=�65   $��0��6S      SearchTextBoxStyle#��[  6c   �� 58      -   5        $��
Search ⌕��58   4   $��30��6G   $��18(�6U    � ToolTip$$ Please select a user to start!��6c   $��Right=�59        SearchButtonStyle#��[ 67   $��0��6b   $	��100��6r   $	��5 2q�6~   �� 5<      -   5        $��Logout��5<      $��Right=�6*   $��Stretch=�6F   $��0,0,10,0q�6b   $��2��6t   $��80��6�    � Cursor$
 Hand��6�   $��14(�5=   6   $
��Bold"�6D   5>      5?      �� 5B   
   $��1��6   $��10q�6   ���#FFFFFF$ 5��6?   ._�5C      � .+�5D      �� -   5         ��Name$ UserDataGrid��5D   >   $��10q�6   $y�Visible=�6)   .+�5E      �� $��16(�6    $
��0 10q�6/   �� 5F      $�User Name: ��6   $
��Bold"�6-    �� 5G      -   5         ��Name$ UserNameRun��5G      $� ��6/   $
��Bold"�67   
���   �6I   5H      �� 5I      $��16(�6    $
��0 10q�6.   �� 5J      $�Status: ��6   $
��Bold"�6*    �� 5K      -   5        $ 	StatusRun��5K      $� ��6.   $
��Bold"�66   
��� � �6H   5L      �� 5M      -   5         ��Name$ ControlCheckBox��5M   H   $��Take Control��6   $
��Bold"�66   ��5N      �� 5O      " ��6$   .�5P   "   ��  � y�$ 	Collapsed=�6?    5Q   "   �� 5R   &    �� Binding 62   ��  �� ElementName$ 	StatusRun�� �� Path$
 Text� �� Value
 Active6g   .a�5S   *   ��  � y�$ Visible=�6G   5T   '   5V      5X      5Y      �� 5Z      -   5         ��Name$ ConnectButton��5Z      $��Connect��60   $	��100��6B   $��50��6N   $��16(�6Z   ${�False��6�   5[      5\      5`      5a      