   M S B A M L   `   `   `  ����G  CProject21WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Project21WPF.UI_Interface.Registration&Project21WPF.UI_Interface.Registration  5   	   ?    9Project21WPF.UI_Interface.Registration.RegistrationWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Flocal4clr-namespace:Project21WPF.UI_Interface.Registration     5   	     � Title$  RegistrationWindow��5   	   $	��350��6$   $	��380��61   .��5	      �  � RowDefinitions 5
   
   �� 5      $��10 �6   �� 5      $��30 �6   �� 5      $��5 �6   �� 5      $��30 �6   �� 5      $��5 �6   �� 5      $��30 �6   �� 5      $��5 �6   �� 5      $��30 �6   �� 5      $��5 �6   �� 5      $��30 �6   �� 5      $��5 �6   �� 5      $��30 �6   �� 5      $��5 �6   �� 5      $��40 �6   �� 5      $��5 �6   �� 5      $��40 �6   �� 5      $��5 �6   �� 5      $��5 �6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5       $	��160 �6   �� 5!      $	��100 �6   .+�5$   
   G� -   5         G�Name$ PasswordBox��5$      $��18(�6+   $��1��69   $��7��6I   G� 5%   
   -   5        $ PasswordBoxConfirmed��5%      $��18(�64   $��1��6B   $��11��6R   �� 5&   
   -   5         ��Name$ TxtLogin��5&       �� TextWrapping$
 Wrap=�6$   $��18(�68   $�� ��6F   $��1��6N   $��3��6^   �� 5'   
   -   5         ��Name$ 
ExitButton��5'      $��
Назад��6%   $��18(�65   $��1��6C   $��15��6S   �� 5(   
   -   5        $ RegistrationButton��5(      $��Регистрация��6-   $��18(�6C   $��1��6Q   $��13��6a   �� 5)   
   $
��Wrap=�6   $%��Имя пользователя��6(    �� TextAlignment$ Center=�6@   $��16(�6W   $��1��6e   $��1��6u   �� 5*   
   $
��Wrap=�6   $��Пароль��6(   $ Center=�66   $��16(�6M   $��1��6[   $��5��6k   �� 5+   
   $
��Wrap=�6   $)��#Подтвердить пароль��6(   $ Center=�6B   $��16(�6Y   $��1��6g   $��9��6w   5,      5-      