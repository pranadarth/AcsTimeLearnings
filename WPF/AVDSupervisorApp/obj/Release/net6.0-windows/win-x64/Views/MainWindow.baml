   M S B A M L   `   `   `  ���� K  GAVDSupervisorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AVDSupervisorApp.ViewsAVDSupervisorApp.Views  '    !AVDSupervisorApp.Views.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6local$clr-namespace:AVDSupervisorApp.Views       � WindowStartupLocation$  CenterScreen=� � Title$ AVD Supervisor App��$	��450��$	��800�� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers	 P� Property
 P� Value��%�  '��       �� " ��.���  � �� �#007ACC��  � ��
 �������  � ��
 ���� ��  � ��$ 0q���  � �� �� " ��.R��� #��z��$ 8��#��z��#��z��._��� $��Center=�$��Center=� P� 	 �@J�IsMouseOver$

 True��. ���  � �� �#66B3FF��  � ��
 �   ���  � ��
 �   ���  � ��$	 1.5q�P� 	 � {�$
 False��. ���  � ��
 �������  � ��
 �������  �@�ToolTip$$ Please select a user to start!��.��� ���#2ce3a0 � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0��$��50��
������ $��10q� �� Padding$ 5q�$ 8��._��  � ColumnDefinitions �� $
��1.5* ��� $
��Auto ��� $��* �.+��� -    ��Name$ SearchTextBox��
 � Tag$ Search Email��$��0q�
���   �$��Center=�$��Stretch=�$��0��   SearchTextBoxStyle#��[  �� -   $��
Search ⌕��$��30��$��18(� � ToolTip$$ Please select a user to start!��$��Right=�  SearchButtonStyle#��[ $��0��$	��100��$	��5 2q��� -   $��Logout��$��Right=�$��Stretch=�$��0,0,10,0q�$��2��$��80�� � Cursor$
 Hand��$��14(�$
��Bold"��� $��1��$��10q����#FFFFFF$ 5��._�� .+��� -    ��Name$ UserDataGrid��$��10q�$y�Visible=�.+��� $��16(�$
��0 10q��� $�User Name: ��$
��Bold"� �� -    ��Name$ UserNameRun��$� ��$
��Bold"�
���   ��� $��16(�$
��0 10q��� $�Status: ��$
��Bold"� �� -   $ 	StatusRun��$� ��$
��Bold"�
��� � ��� -    ��Name$ ControlCheckBox��$��Take Control��$
��Bold"����� " ��.���  � y�$ 	Collapsed=� ��  �� Binding ��  �� ElementName$ 	StatusRun�� �� Path$
 Text� �� Value
 Active.a���  � y�$ Visible=��� -    ��Name$ ConnectButton��$��Connect��$	��100��$��50��$��16(�${�False��