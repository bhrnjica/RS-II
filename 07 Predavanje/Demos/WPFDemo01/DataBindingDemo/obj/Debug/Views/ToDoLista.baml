   M S B A M L   `   `   `  ����F  BMinimalMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DataBindingDemo.ViewModelsDataBindingDemo.ViewModels  5      %    DataBindingDemo.Views.ToDoLista   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?
ViewModels(clr-namespace:DataBindingDemo.ViewModels     5        J� InputBindings  5      �� 5   
   
 �� Key$ Enter��6    �� Command 6!   �� *AddToDoCommand+.��5      �� $	��336��6   .+�5   
   �� 
����  �$��5,5,5,0q�6"   $��Unesite ToDo naziv��63   �� 5   
   $��5q�6T   ��6   �� *ToDoZadatak+ �� UpdateSourceTrigger$ PropertyChanged=�6T   �� 5   
   
����  �$��5,5,5,0q�6"   $��
ToDo Lista��63   �� 5   
   $	��200��6X   $��5q�6e   ��6   �� *	History+ �� SelectedItem 62   �� *SelectedItem+��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� ��6$   �� *Naziv+�� 5      $��10��6    �� 5      
���  ��$��Prioritet: ��65   �� 5      
���  ����65   �� *	Prioritet+5      5      5      �� 5    
   $ 
Horizontal=�6   $��10,5,5,5q�6.   .+�5!      �� $��50��69   $	��Add��6D   ��6   �� *AddToDoCommand+6D   �� 5"      $��50��6   �� 5#      $��50��6<   $��Delete��6G   ��6   �� *DeleteToDoCommand+6G   5$      5%      5'      