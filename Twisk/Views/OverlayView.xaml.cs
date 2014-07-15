using System.Windows;
using System.Windows.Input;

namespace eZet.Twisk.Views {
    /// <summary>
    /// Interaction logic for OverlayView.xaml
    /// </summary>
    public partial class OverlayView {
        public OverlayView() {
            InitializeComponent();
        }

        private void OverlayView_OnMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
