   M S B A M L   `   `   `  ����P  LIMClientFirstDemoLisa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:IMClientFirstDemoLisaIMClientFirstDemoLisa  5   	   &     IMClientFirstDemoLisa.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:IMClientFirstDemoLisa     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��260��6)   .��5	      � $	��419��6   $��Bottom=�6   ��5   
   �� 5      .��5      �  � Offset$ 0Q�6    � Color$
 Blue��6*   69   � 5      $	 0.2Q�6   $ 	LightBlue��6,   6@   5       � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5      6   �� 5      $��30 �6   6*   .+�5   
   �� $��0��6   $��	0,2,0,337q�6"   $��2��65   .+�5      �� -   5         ��Name$ label��5      
�������$��Label��66   $��21.333(�6F   $
��Bold"�6X   $��	0,0,161,0q�6j   5      �� 5   
   -   5         ��Name$ listBoxUsers��5      $��1��6(   ���	#FFC6C2F0��5      �� 5      .R�5      �  � ColumnDefinitions 5       �� 5!      61   �� 5"      61   �� 5#      61   .+�5%      �� $��0��6    ��60   �� *Image+6K   �� 5&      $��1��6$   $��26(�64   ��6B   �� *
UserName+ �� Mode$ OneTime=�6k   �� 5'      $��2��6$   $��26(�64   ��6C   �� *DisplayState+$ OneWay=�6o   5(      5)      ��5+      �� 5,       �� 
TargetType" ��6   .�5-      -   5        �� 5.      	 �� Property	 � |�
 �� Value
 .  6B   5/      51      �� 52   
   $��2��6   .+�53      �� -   5         ��Name$ 	logoutBtn��53      $��切换用户��6(   $��Center=�67   $��65��6T   6_   54      �� 55   
   -   5        $ button��55      ���	#FF6CB2E2
�������$��添加好友��6K   $
��Left=�6Z   $��176,10,0,21q�6u   $��76��6�   6�   57      58      