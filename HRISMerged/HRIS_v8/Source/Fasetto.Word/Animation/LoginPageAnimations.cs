using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Fasetto.Word
{
    /// <summary>
    /// Helpers to animate pages in specific ways
    /// </summary>
    public static class LoginPageAnimations
    {
        /// <summary>
        /// Slides a page in from the right
        /// </summary>
        /// <param name="page">The page to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromTop(this Page page, float seconds)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddLoginSlideFromTop(seconds, page.WindowWidth);

            // Add fade in animation
            sb.AddLoginFadeIn(seconds);

            // Start animating
            sb.Begin(page);

            // Make page visible
            page.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Slides a page out to the left
        /// </summary>
        /// <param name="page">The page to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeft(this Page page, float seconds)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddLoginSlideToLeft(seconds, page.WindowWidth);

            // Add fade in animation
            sb.AddLoginFadeOut(seconds);

            // Start animating
            sb.Begin(page);

            // Make page visible
            page.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }
    }
}
