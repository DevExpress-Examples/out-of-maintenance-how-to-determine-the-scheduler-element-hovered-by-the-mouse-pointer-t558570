Imports DevExpress.Xpf.Scheduling
Imports DevExpress.Xpf.Scheduling.VisualData
Imports System
Imports System.Text
Imports System.Windows
Imports System.Windows.Input

Namespace Scheduler_HitInfo

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub

#Region "#hittest"
        Private Sub SchedulerControl_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            ' Obtain hit information under the test point.
            Dim position As Point = e.GetPosition(Me.schedulerControl1)
            Dim hitInfo As ISchedulerHitInfo = Me.schedulerControl1.CalcHitInfo(position)
            If hitInfo IsNot Nothing Then
                Me.hitResultsHeader.Text = "Hit Test Results"
                Dim builder As StringBuilder = New StringBuilder()
                builder.AppendLine([Enum].GetName(GetType(SchedulerHitTestType), hitInfo.HitTestType))
                Select Case hitInfo.HitTestType
                    Case SchedulerHitTestType.Appointment
                        Dim appViewModel As AppointmentViewModel = TryCast(hitInfo.ViewModel, AppointmentViewModel)
                        If appViewModel IsNot Nothing Then
                            builder.AppendLine("Subject: " & appViewModel.Appointment.Subject)
                            builder.AppendLine("Start: " & appViewModel.Appointment.Start.ToString())
                            builder.AppendLine("End: " & appViewModel.Appointment.End.ToString())
                        End If

                    Case SchedulerHitTestType.Cell
                        builder.AppendLine("Interval: " & hitInfo.ViewModel.Interval.ToString())
                        builder.AppendLine("Selected: " & hitInfo.ViewModel.IsSelected.ToString())
                    Case SchedulerHitTestType.Ruler
                        Dim rulerViewModel As TimeRulerCellViewModel = TryCast(hitInfo.ViewModel, TimeRulerCellViewModel)
                        If rulerViewModel IsNot Nothing Then
                            builder.AppendLine("Time: " & rulerViewModel.Time.ToString())
                            builder.AppendLine("Time Scale: " & rulerViewModel.TimeScale.ToString())
                        End If

                End Select

                Me.hitResultsText.Text = builder.ToString()
            Else
                ClearResults()
            End If
        End Sub

#End Region  ' #hittest
        Private Sub ClearResults()
            Me.hitResultsText.Text = "Move the mouse pointer over the scheduler to get information on the element which is hovered over."
        End Sub

        Private Sub schedulerControl1_MouseLeave(ByVal sender As Object, ByVal e As MouseEventArgs)
            ClearResults()
        End Sub
    End Class
End Namespace
