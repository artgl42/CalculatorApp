using CalculatorApp.Toolbox;
using System;
using System.Windows;

namespace CalculatorApp
{
    public class CalculatorViewModel : NotifyPropertyChanged
    {
        private CalculatorModel _calculator;

        public string DisplayText
        {
            get
            {
                return _calculator.GetDisplayText();
            }
            set
            {
                _calculator.SetDisplayText(value);
                OnPropertyChanged();
            }
        }

        public DelegateCommand MenuItemClickCommand { get; private set; }
        public DelegateCommand ButtonClickCommand { get; private set; }
        public DelegateCommand CopyCommand { get; private set; }
        public DelegateCommand PasteCommand { get; private set; }

        public CalculatorViewModel()
        {
            _calculator = new CalculatorModel();
            MenuItemClickCommand = new DelegateCommand(param => ExecuteMenuItemClickCommand(param));
            ButtonClickCommand = new DelegateCommand(param => ExecuteButtonClickCommand(param), param => CanExecuteButtonClickCommand(param));
            CopyCommand = new DelegateCommand(param => ExecuteCopyCommand(), param => CanExecuteCopyCommand());
            PasteCommand = new DelegateCommand(param => ExecutePasteCommand(), param => CanExecutePasteCommand());
        }
       
        #region Commands
        private bool CanExecuteButtonClickCommand(object parameter)
        {
            return parameter is string;
        }

        private void ExecuteButtonClickCommand(object parameter)
        {
            DisplayText = parameter as string;
        }

        private bool CanExecuteCopyCommand()
        {
            return !string.IsNullOrWhiteSpace(DisplayText);
        }

        private void ExecuteCopyCommand()
        {
            Clipboard.SetText(DisplayText);
        }

        private bool CanExecutePasteCommand()
        {
            return Clipboard.ContainsText(TextDataFormat.UnicodeText);
        }

        private void ExecutePasteCommand()
        {
            DisplayText = Clipboard.GetText(TextDataFormat.UnicodeText);
        }

        private void ExecuteMenuItemClickCommand(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Exit":
                    Application.Current.Shutdown();
                    break;
                case "?":
                    string title = "Information";
                    string info = "MIT License: Copyright (c) 2020 Artem Glushkov \n GitHub: github.com/artgl42/CalculatorApp";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Information;
                    MessageBox.Show(info, title, button, icon);
                    break;
                default:
                    string themeName = parameter.ToString();
                    Application.Current.MainWindow.Resources = new ResourceDictionary() { Source = new Uri($"/Themes/{themeName}/{themeName}.xaml", UriKind.Relative) };
                    break;
            }
        }
        #endregion
    }
}
