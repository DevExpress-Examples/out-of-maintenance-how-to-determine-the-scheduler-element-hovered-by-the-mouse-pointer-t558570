using DevExpress.Xpf.Scheduling;
using DevExpress.Xpf.Scheduling.VisualData;
using System;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Scheduler_HitInfo {
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
        }
        #region #hittest
        private void SchedulerControl_MouseMove(object sender, MouseEventArgs e)
        {
            // Obtain hit information under the test point.
            Point position = e.GetPosition(schedulerControl1);
            ISchedulerHitInfo hitInfo = schedulerControl1.CalcHitInfo(position);
            if (hitInfo != null) {
                this.hitResultsHeader.Text = "Hit Test Results";
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(Enum.GetName(typeof(SchedulerHitTestType), hitInfo.HitTestType));
                switch (hitInfo.HitTestType) {
                    case SchedulerHitTestType.Appointment:
                        AppointmentViewModel appViewModel = hitInfo.ViewModel as AppointmentViewModel;
                        if (appViewModel != null) {
                            builder.AppendLine("Subject: " + appViewModel.Appointment.Subject);
                            builder.AppendLine("Start: " + appViewModel.Appointment.Start.ToString());
                            builder.AppendLine("End: " + appViewModel.Appointment.End.ToString());
                        }
                        break;
                    case SchedulerHitTestType.Cell:
                        builder.AppendLine("Interval: " + hitInfo.ViewModel.Interval.ToString());
                        builder.AppendLine("Selected: " + hitInfo.ViewModel.IsSelected.ToString());
                        break;
                    case SchedulerHitTestType.Ruler:
                       TimeRulerCellViewModel rulerViewModel = hitInfo.ViewModel as TimeRulerCellViewModel;
                        if (rulerViewModel != null) {
                            builder.AppendLine("Time: " + rulerViewModel.Time.ToString());
                            builder.AppendLine("Time Scale: " + rulerViewModel.TimeScale.ToString());
                        }
                        break;
                }
                this.hitResultsText.Text = builder.ToString();
            }
            else {
                ClearResults();
            }
        }
        #endregion #hittest

        private void ClearResults() {
            this.hitResultsText.Text = "Move the mouse pointer over the scheduler to get information on the element which is hovered over.";
        }
        private void schedulerControl1_MouseLeave(object sender, MouseEventArgs e)
        {
            ClearResults();
        }

    }
}
