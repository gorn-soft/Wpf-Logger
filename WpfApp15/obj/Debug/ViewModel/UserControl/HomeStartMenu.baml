   M S B A M L   `   `   `  ����A  =Logger, Version=2.1.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:WpfApp15.ViewModel.UserControlWpfApp15.ViewModel.UserControl  5      2    ,WpfApp15.ViewModel.UserControl.HomeStartMenu   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Ad3http://schema s.microsoft.com/expression/blend/2008     5      >local,clr-namespace:WpfApp15.ViewModel.UserControl     5      V RMaterialDesignThemes.Wpf, Version=3.1.2.1363, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      � $	��315��6   $	��315��6   $	��740��6(   $	��740��67   .+�5   
   ?  9MaterialDesignThemes.Wpf.Transitions.TransitioningContent  -   5           Name$  TrainsitionigContentSlide��5   .   C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffect 6Q   D  >MaterialDesignThemes.Wpf.Transitions.TransitionEffectExtension  *SlideInFromLeft+   Duration$ 0:0:0.9X�.��5      �  � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   .+�5      �� 
�������$��
2,9,5,8.19q�5   .    �� CornerRadius$ 9,9,10,7��6K   $��0��5      $	��300��6)   $��10q�6:   ��5      ��  �� ElementName$ StatGr�� �� Path$ 
Background�._�5      � -   5         �Name$ StatGr��5      ���	#FFF3F3F3$��3q�6B    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      	   
BlurRadius$	 20Q�6/   
   Color$
 	#FFDEDEDE��6?      ShadowDepth$ 1Q�6Q   .+�5      �� $	��200��6%   .+�5      �� $��10,10,51,10q�6(   $��Gill Sans Ultra Bold)�6=   $��14(�6_   $��Статистика��6m   ���	#FFC000006   �� 5      $E��?/Logger;component/ViewModel/Images/завантаження.jpg��6$   $	��150��6a   $��UniformToFill=�6n   $��0,0,46,0q�6�   �� 5      $��8(�6(   $��5,5,51,5q�65   $
��Wrap=�6G   $��Champagne & Limousines)�6[   $��40��6�   ��Приложение записывает информацию о всех нажатиях клавиш в файл отчета, о всех запускаемых приложениях в файл отчета.5!      �� 5"       �� Orientation$ 
Horizontal=�6)   $��Center=�6B   $��15,-10,51,0q�6_   $	��134��6t   .+�5#   "   �� $��35��6(   $s��m/Logger;component/ViewModel/Images/statics-analytics-market-data-research-performance-graph-chart-7-24988.png��64   �� 5$   "   $��75��6)   $��Перейти��6]   $��9(�6o   $��Arial Black)�6|   $��10q�6�   $
��Bold"�6�   ��64   �� *ChangeTabControlStat+6�   5%      5&      5'      5(      �� 5)      
�������$��
2,9,5,8.19q�5*   .   $ 9,9,10,7��6K   $��1��5+      $	��300��6)   $��10q�6:   ��5)      �� $ StatGr��$ 
Background�._�5,      � ���	#FFF3F3F3$��3q�63    5-        5.      $	 20Q�6/   $
 	#FFDEDEDE��6?   $ 1Q�6Q   .+�51      �� $	��200��6%   .+�52      �� $��10,10,35,10q�6(   $��Gill Sans Ultra Bold)�6=   $��14(�6_   $ ��Модерирование��6m   ���	#FFC000006�   �� 53      $@��:/Logger;component/ViewModel/Images/20191104224338_5708.jpg��6$   $	��150��6h   $��UniformToFill=�6u   $��0,0,48,0q�6�   �� 54      $��8(�6(   $��5,5,53,5q�65   $
��Wrap=�6G   $��Champagne & Limousines)�6[   ��Приложение анализирует клавиши, нажатые пользователем. Если запускается запрещенная программа, приложение закрывает запрещенную программу.58      �� 59      $ 
Horizontal=�6)   $��Center=�6B   $��20,-16,48,0q�6_   $	��132��6t   .+�5:   "   �� $��35��6(   $;��5/Logger;component/ViewModel/Images/Teleconference.png��64   �� 5;   "   $��75��6)   $��Перейти��6]   $��Arial Black)�6o   $��10q�6�   $��9(�6�   $
��Bold"�6�   ��64   �� *ChangeTabControlStat+6�   5<      5=      5>      5?      �� 5@      
�������$��
2,9,5,8.19q�5A   .   $ 9,9,10,7��6K   $��2��5B      $	��300��6)   $��10q�6:   ��5@      �� $ StatGr��$ 
Background�._�5C      � ���	#FFF3F3F3$��3q�62    5D        5E      $	 20Q�6/   $
 	#FFDEDEDE��6?   $ 1Q�6Q   .+�5H      �� $	��200��6%   .+�5I      �� $��10,10,48,10q�6(   $��Gill Sans Ultra Bold)�6=   $��14(�6_   $��
Отчет��6m   ���	#FFC000006�   �� 5J      $4��./Logger;component/ViewModel/Images/unnamed.jpg��6$   $	��150��6\   $��UniformToFill=�6i   $��0,0,48,0q�6�   �� 5K      $��8(�6(   $��5,5,54,5q�65   $
��Wrap=�6G   $��Champagne & Limousines)�6[   $��36��6�   {yПользователь может просмотреть информацию о клавишах и процессах5N      �� 5O      $ 
Horizontal=�6)   $��Center=�6B   $��
15,-5,54,0q�6_   $	��131��6s   .+�5P   "   �� $��35��6(   $A��;/Logger;component/ViewModel/Images/investicii-otchet-v2.png��64   �� 5Q   "   $��75��6)   $��Перейти��6_   $��Arial Black)�6q   $��10q�6�   $��9(�6�   $
��Bold"�6�   ��64   �� *ChangeTabControlOtchet+6�   5R      5S      5T      5U      �� 5V      
�������$��
2,9,5,8.19q�5W   .   $ 9,9,10,7��6K   $��3��5X      $	��300��6)   $��10q�6:   ��5V      �� $ StatGr��$ 
Background�._�5Y      � ���	#FFF3F3F3$��3q�63    5Z        5[      $	 20Q�6+   $
 	#FFDEDEDE��6;   $ 1Q�6M   .+�5^      �� $	��200��6!   .+�5_      �� $��10,10,49,10q�6$   $��Gill Sans Ultra Bold)�69   $��14(�6[   $��Настройки��6i   ���	#FFC000006�   �� 5`      $7��1/Logger;component/ViewModel/Images/images (1).jpg��6    $	��150��6[   $��UniformToFill=�6h   $��0,0,43,0q�6�   �� 5a      $��8(�6$   $��5q�61   $
��Wrap=�6<   $��Champagne & Limousines)�6P   $��36��6x   dbПозволяет пользователю настроить опции для слежения.5c      �� 5d      $ 
Horizontal=�6%   $��Center=�6>   $��
20,-5,49,0q�6[   $	��131��6o   .+�5e      �� $��35��6$   $7��1/Logger;component/ViewModel/Images/images (1).png��60   �� 5f      $��75��6%   $��Перейти��6]   $��Arial Black)�6o   $��10q�6�   $
��Bold"�6�   $��9(�6�   ��60   �� *ChangeTabControlSettings+6�   5g      5h      5i      5j      5k      5l      5m      5n      