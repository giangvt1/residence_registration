   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Project.ViewProject.View  5   	   7 clr-namespace:Project.ViewModelsProject.ViewModels  5   	       Project.View.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Project.View     5   	   7
viewmodels clr-namespace:Project.ViewModels     5   	     � Title$  Login��5	   	   $	��500��6   $	��400��6$    � WindowStartupLocation$ CenterScreen=�5
   	   .��5      � $��20q�6   
������� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0,0,0,20q�6"   $��Center=�64   .+�5      �� $��Welcome Back!��6   $��24(�6-   $
��Bold"�6;   �� 5      $��Log in to your account��6   $��14(�66   5      �� 5   
   $��1��6   $	��0,5q�6"   .+�5      �� $��Email:��6   �� 5      -   5         ��Name$ emailTextBox��5      $��30��5      ��5      �� *Email+ �� UpdateSourceTrigger$ PropertyChanged=�5      5       �� 5#   
   $��2��6   $	��0,5q�6"   .+�5$      �� $��	Password:��6   G� 5%      -   5         G�Name$ passwordBox��5%      $��30��5'      5(      �� 5+   
   $��3��6   $	��0,5q�6"   .+�5,      �� $��Role:��6   �� 5-      -   5         ��Name$ roleComboBox��5-      $��30��50      ��5.      �� *Roles+ �� SelectedItem 5/      �� *SelectedRole+50      51      �� 54   
   $��4��6   $��Login��6   $��35��55      $	��100��6   $��Center=�6)   $��20q�6F   ��54   .   �� *LoginCommand+55   F   56      57      