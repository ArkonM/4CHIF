using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Threading;
using System.ComponentModel;

namespace Sorting
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
       ObservableCollection<Int32> sortList = new ObservableCollection<Int32>();
        int _checks = 0;
        int _swaps = 0;
        int _selected = -1;

        public ObservableCollection<Int32> List
        {
            set
            {
                sortList = value;
                NotifyPropertyChanged(x => x.List);
            }
            get
            {
                return sortList;
            }
        }
        public int Checks {
            set
            {
                _checks = value;
                NotifyPropertyChanged(x => x.Checks);
            }
            get
            {
                return _checks;
            }
        }
        public int Swaps
        {
            set
            {
                _swaps = value;
                NotifyPropertyChanged(x => x.Swaps);
            }
            get
            {
                return _swaps;
            }
        }

        public int Selected
        {
            set
            {
                _selected = value;
                NotifyPropertyChanged(x => x.Selected);
            }
            get
            {
                return _selected;
            }
        }
        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
            sortList.Clear();
            for (int i = 0; i < 50; i++)
            {
                sortList.Add(rand.Next(200));
            }
            Checks = 0;
            Swaps = 0;
            this.DataContext = this;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            sortList.Clear();
            for (int i = 0; i < 50; i++)
            {
                sortList.Add(rand.Next(200));
            }
            Checks = 0;
            Swaps = 0;
            this.DataContext = this;
        }


        private void Bubble_Click(object sender, RoutedEventArgs e)
        {
            int size = sortList.Count;
            ThreadPool.QueueUserWorkItem(o =>
            {
                bool swapped = false;
                do {
                    swapped = false;
                    for (int i = 0; i < size - 1; ++i, Selected = i)
                    {
                        try
                        {
                            this.Dispatcher.Invoke(
                              System.Windows.Threading.DispatcherPriority.Normal
                              , new System.Windows.Threading.DispatcherOperationCallback(delegate
                              {
                                  Checks++;
                                  if (sortList[i] > sortList[i + 1])
                                  {
                                      Swaps++;
                                      int temp = sortList[i];
                                      sortList[i] = sortList[i + 1];
                                      sortList[i + 1] = temp;
                                      swapped = true;
                                  }
                                  return null;
                              }), null);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }
                        Thread.Sleep(50);
                    }
                    size = size - 1;
                } while (swapped == true);

            });
        }


        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged<TValue>
                     (System.Linq.Expressions.Expression<Func<MainWindow, TValue>> propertySelector)
        {
            if (PropertyChanged != null)
            {
                var memberExpression = propertySelector.Body as System.Linq.Expressions.MemberExpression;
                if (memberExpression != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
                }
            }
        }
        #endregion


        private void Cocktail_Click(object sender, RoutedEventArgs e)
        {
            bool swapped = true;
            int start = 0;
            int size = sortList.Count;

            ThreadPool.QueueUserWorkItem(o =>
            {
                do
                {

                    // reset the swapped flag on entering the
                    // loop, because it might be true from a
                    // previous iteration.
                    swapped = false;
                    for (int i = start; i < size - 1; ++i, Selected = i)
                    {
                        try
                        {
                            this.Dispatcher.Invoke(
                              System.Windows.Threading.DispatcherPriority.Normal
                              , new System.Windows.Threading.DispatcherOperationCallback(delegate
                              {
                                  Checks++;
                                  // loop from bottom to top same as
                                  // the bubble sort

                                  if (sortList[i] > sortList[i + 1])
                                  {
                                      int temp = sortList[i];
                                      sortList[i] = sortList[i + 1];
                                      sortList[i + 1] = temp;
                                      swapped = true;
                                  }
                                  return null;
                              }), null);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }
                        Thread.Sleep(50);
                    }
                    // if nothing moved, then array is sorted.
                    if (swapped == false)
                        break;

                    // otherwise, reset the swapped flag so that it
                    // can be used in the next stage
                    swapped = false;

                    // move the end point back by one, because
                    // item at the end is in its rightful spot
                    size = size - 1;

                    // from top to bottom, doing the
                    // same comparison as in the previous stage
                    for (int i = size - 1; i >= start; i--, Selected = (i+1))
                    {
                        try
                        {
                            this.Dispatcher.Invoke(
                              System.Windows.Threading.DispatcherPriority.Normal
                              , new System.Windows.Threading.DispatcherOperationCallback(delegate
                              {
                                  Checks++;
                                  // loop from bottom to top same as
                                  // the bubble sort

                                  if (sortList[i] > sortList[i + 1])
                                  {
                                      int temp = sortList[i];
                                      sortList[i] = sortList[i + 1];
                                      sortList[i + 1] = temp;
                                      swapped = true;
                                  }
                                  return null;
                              }), null);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }
                        Thread.Sleep(50);
                    }

                    // increase the starting point, because
                    // the last stage would have moved the next
                    // smallest number to its rightful spot.
                    start = start + 1;
                } while (swapped == true);
            });
        }
    }
}
