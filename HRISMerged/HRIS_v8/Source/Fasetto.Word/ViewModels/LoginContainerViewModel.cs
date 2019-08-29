using Fasetto.Word.Core;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace Fasetto.Word
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class LoginContainerViewModel : BaseViewModel
    {
        #region Private Member

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow;

        #endregion

        #region Public Properties
        
        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 42;

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.LagoLogin;

        #endregion

        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginContainerViewModel(Window window)
        {
            // Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
        }

        #endregion
    }
}
