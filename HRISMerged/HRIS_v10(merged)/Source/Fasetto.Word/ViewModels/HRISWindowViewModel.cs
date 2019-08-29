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
    public class HRISWindowViewModel : BaseViewModel
    {
        #region Private Member

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow;

        #endregion

        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HRISWindowViewModel(Window window)
        {
            mWindow = window;
        }

        #endregion
    }
}
