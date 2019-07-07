﻿Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms


Public Class FlowArrowMatrix1
    Inherits Control


    Private DotPatterns() As ULong


    Private BlinkTimer As Timer

    Private m_arrowColor As Color

    Private currentWidth As Single

    Private currentHeight As Single

    Private m_arrow As FlowArrowMatrix1.ArrowDir

    Private m_valueFlow As Boolean

    Private _blinkInterval As Integer

    Private flagTimer As Boolean

    <Browsable(True), DefaultValue(500), Description("The arrow LED blinking interval in milliseconds."), RefreshProperties(RefreshProperties.All)>
    Public Property ArrowBlinkInterval() As Integer
        Get
            Return Me._blinkInterval
        End Get
        Set(ByVal value As Integer)
            If Not Versioned.IsNumeric(value) Then
                value = 500
            End If
            If value < 0 Then
                value = 500
            End If
            If Me._blinkInterval <> value Then
                Me._blinkInterval = value
                Me.BlinkTimer.Interval = Me._blinkInterval
                Me.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True), DefaultValue(3), Description("Select arrow direction (Right, Left, Up, Down)."), RefreshProperties(RefreshProperties.All)>
    Public Property ArrowDirection() As FlowArrowMatrix1.ArrowDir
        Get
            Return Me.m_arrow
        End Get
        Set(ByVal value As FlowArrowMatrix1.ArrowDir)
            Dim flag As Boolean
            If Me.m_arrow <> value Then
                Me.currentWidth = CSng(Me.Width)
                Me.currentHeight = CSng(Me.Height)
                If Me.m_arrow <> FlowArrowMatrix1.ArrowDir.Right AndAlso Me.m_arrow <> FlowArrowMatrix1.ArrowDir.Left OrElse value = FlowArrowMatrix1.ArrowDir.Right OrElse value = FlowArrowMatrix1.ArrowDir.Left Then
                    If (Me.m_arrow = FlowArrowMatrix1.ArrowDir.Up OrElse Me.m_arrow = FlowArrowMatrix1.ArrowDir.Down) AndAlso value <> FlowArrowMatrix1.ArrowDir.Up AndAlso value <> FlowArrowMatrix1.ArrowDir.Down Then
                        GoTo Label1
                    End If
                    flag = False
                    GoTo Label0
                End If
Label1:
                flag = True
Label0:
                If Not flag Then
                    Me.m_arrow = value
                Else
                    Me.m_arrow = value
                    'INSTANT VB TODO TASK: There is no VB equivalent to 'checked' in this context:
                    'ORIGINAL LINE: this.Width = checked((int)Math.Round((double)this.currentHeight));
                    Me.Width = CInt(Math.Truncate(Math.Round(CDbl(Me.currentHeight))))
                    'INSTANT VB TODO TASK: There is no VB equivalent to 'checked' in this context:
                    'ORIGINAL LINE: this.Height = checked((int)Math.Round((double)this.currentWidth));
                    Me.Height = CInt(Math.Truncate(Math.Round(CDbl(Me.currentWidth))))
                    Me.FlowArrowMatrix1_Resize(Me, Nothing)
                End If
            End If
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True), DefaultValue(GetType(Color), "Red"), Description("The arrow LEDs color."), RefreshProperties(RefreshProperties.All)>
    Public Property ArrowLEDColor() As Color
        Get
            Return Me.m_arrowColor
        End Get
        Set(ByVal value As Color)
            If Me.m_arrowColor <> value Then
                Me.m_arrowColor = value
                Me.Invalidate()
            End If
        End Set
    End Property

    'private virtual Timer BlinkTimer
    '{
    '    [DebuggerNonUserCode]
    '    get
    '    {
    '        return this._BlinkTimer;
    '    }
    '    [DebuggerNonUserCode]
    '    set
    '    {
    '        FlowArrowMatrix1 flowArrowMatrix1 = this;
    '        EventHandler eventHandler = new EventHandler(flowArrowMatrix1.tmr_Tick);
    '        if (this._BlinkTimer != null)
    '        {
    '            this._BlinkTimer.Tick -= eventHandler;
    '        }
    '        this._BlinkTimer = value;
    '        if (this._BlinkTimer != null)
    '        {
    '            this._BlinkTimer.Tick += eventHandler;
    '        }
    '    }
    '}

    <Browsable(True), Description("Constant value used to maintain the ratio between width and height."), RefreshProperties(RefreshProperties.All)>
    Public ReadOnly Property SizeScaleCoefficient() As Single
        Get
            Return 0.7154471F
        End Get
    End Property

    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            If String.Compare(MyBase.Text, value) <> 0 Then
                MyBase.Text = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True), DefaultValue(False), Description("Indicates whether there is a flow."), RefreshProperties(RefreshProperties.All)>
    Public Property Value() As Boolean
        Get
            Return Me.m_valueFlow
        End Get
        Set(ByVal value As Boolean)
            If Me.m_valueFlow <> value Then
                Me.m_valueFlow = value
                If Not Me.m_valueFlow Then
                    Me.BlinkTimer.Enabled = False
                    Me.flagTimer = False
                Else
                    Me.BlinkTimer.Enabled = True
                End If
            End If
            Me.Invalidate()
        End Set
    End Property



    Public Sub New()
        Dim flowArrowMatrix1 As FlowArrowMatrix1 = Me
        AddHandler MyBase.Resize, AddressOf flowArrowMatrix1.FlowArrowMatrix1_Resize
        Dim flowArrowMatrix11 As FlowArrowMatrix1 = Me
        AddHandler MyBase.Click, AddressOf flowArrowMatrix11.FlowArrowMatrix1_Click

        Me.DotPatterns = New ULong(4) {}
        Me.m_arrowColor = Color.Red
        Me.m_arrow = FlowArrowMatrix1.ArrowDir.Down
        Me._blinkInterval = 500
        Me.SetStyle(ControlStyles.ContainerControl Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.DoubleBuffered = True
        Me.DoubleBuffered = True
        MyBase.BackColor = Color.Transparent
        Me.Size = New Size(87, 121)
        Me.DotPatterns(0) = Convert.ToUInt64("00100010100010100010001000100010001", 2)
        Me.DotPatterns(1) = Convert.ToUInt64("10001000100010001000101000101000100", 2)
        Me.DotPatterns(2) = Convert.ToUInt64("10001010100010000000100010101000100", 2)
        Me.DotPatterns(3) = Convert.ToUInt64("00100010101000100000001000101010001", 2)
        Me.BlinkTimer = New Timer() With {
         .Interval = Me._blinkInterval,
         .Enabled = False
        }
        Me.flagTimer = False
    End Sub


    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try

        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub FlowArrowMatrix1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Focus()
    End Sub

    Private Sub FlowArrowMatrix1_Resize(ByVal sender As Object, ByVal e As EventArgs)
        If (If(Me.m_arrow = FlowArrowMatrix1.ArrowDir.Right OrElse Me.m_arrow = FlowArrowMatrix1.ArrowDir.Left, False, True)) Then
            If Me.Height < 25 Then
                Me.Height = 25
            End If
            'INSTANT VB TODO TASK: There is no VB equivalent to 'checked' in this context:
            'ORIGINAL LINE: this.Width = checked((int)Math.Round((double)((float)(0.7154471f * (float)this.Height))));
            Me.Width = CInt(Math.Truncate(Math.Round(CDbl(CSng(0.7154471F * CSng(Me.Height))))))
            If Me.Width < 18 Then
                Me.Width = 18
            End If
        Else
            If Me.Width < 25 Then
                Me.Width = 25
            End If
            'INSTANT VB TODO TASK: There is no VB equivalent to 'checked' in this context:
            'ORIGINAL LINE: this.Height = checked((int)Math.Round((double)((float)(0.7154471f * (float)this.Width))));
            Me.Height = CInt(Math.Truncate(Math.Round(CDbl(CSng(0.7154471F * CSng(Me.Width))))))
            If Me.Height < 18 Then
                Me.Height = 18
            End If
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim num As Integer
        Dim num1 As Integer
        Dim num2 As Integer
        Dim num3 As Integer
        Dim colorArray() As Color
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality
        If (If(Me.m_arrow = FlowArrowMatrix1.ArrowDir.Right OrElse Me.m_arrow = FlowArrowMatrix1.ArrowDir.Left, False, True)) Then
            num = 6
            num1 = 4
            num2 = 5
            'INSTANT VB TODO TASK: There is no VB equivalent to 'checked' in this context:
            'ORIGINAL LINE: num3 = checked((int)Math.Round((double)num2 + 0.25));
            num3 = CInt(Math.Truncate(Math.Round(CDbl(num2) + 0.25)))
        Else
            num = 4
            num1 = 6
            num2 = 7
            'INSTANT VB TODO TASK: There is no VB equivalent to 'checked' in this context:
            'ORIGINAL LINE: num3 = checked((int)Math.Round((double)num2 + 0.25));
            num3 = CInt(Math.Truncate(Math.Round(CDbl(num2) + 0.25)))
        End If
        Dim num4 As Integer = num
        For i As Integer = 0 To num4
            'INSTANT VB TODO TASK: There is no VB equivalent to 'checked' in this context:
            'ORIGINAL LINE: short num5 = checked((short)num1);
            Dim num5 As Short = CShort(num1)
            Dim j As Short = 0
            'INSTANT VB TODO TASK: There is no VB equivalent to 'checked' in this context:
            'ORIGINAL LINE: for (short j = 0; j <= num5; j = checked((short)(j + 1)))
            Do While j <= num5
                'INSTANT VB TODO TASK: There is no VB equivalent to 'checked' in this context:
                'ORIGINAL LINE: long num6 = checked((long)Math.Round(Math.Pow(2, (double)(checked(checked(i * num2) + j)))));
                Dim num6 As Long = CLng(Math.Truncate(Math.Round(Math.Pow(2, CDbl((i * num2) + j)))))
                Dim rectangle As New Rectangle(Convert.ToInt32(CDbl(j) * (CDbl(Me.Width) / CDbl(num2))), Convert.ToInt32(CDbl(i) * (CDbl(Me.Width) / CDbl(num3))), Convert.ToInt32(CDbl(Me.Width) / CDbl(num2) - 1), Convert.ToInt32(CDbl(Me.Width) / CDbl(num2) - 1))
                Dim graphicsPath As New GraphicsPath()
                graphicsPath.AddEllipse(rectangle)
                Dim pathGradientBrush As New PathGradientBrush(graphicsPath)
                'INSTANT VB TODO TASK: There is no VB equivalent to 'checked' in this context:
                'ORIGINAL LINE: if (((void*)(checked((long)this.DotPatterns[(int)this.m_arrow])) & num6) > (long)0)
                If (CLng(Me.DotPatterns(CInt(Me.m_arrow))) And num6) > CLng(0) Then
                    rectangle.Inflate(-1, -1)
                    e.Graphics.FillEllipse(New SolidBrush(Color.Black), rectangle)
                    If Not Me.flagTimer Then
                        e.Graphics.DrawEllipse(New Pen(Color.FromArgb(185, Me.m_arrowColor), 2.0F), rectangle)
                        pathGradientBrush.CenterColor = ControlPaint.Light(Me.m_arrowColor)
                        colorArray = New Color() {Color.FromArgb(25, ControlPaint.Light(Me.m_arrowColor))}
                        pathGradientBrush.SurroundColors = colorArray
                    Else
                        e.Graphics.DrawEllipse(New Pen(Color.FromArgb(45, Me.m_arrowColor), 2.0F), rectangle)
                        pathGradientBrush.CenterColor = ControlPaint.DarkDark(Me.m_arrowColor)
                        colorArray = New Color() {ControlPaint.Dark(Me.m_arrowColor)}
                        pathGradientBrush.SurroundColors = colorArray
                    End If
                    e.Graphics.FillEllipse(pathGradientBrush, rectangle)
                End If
                j = CShort(j + 1)
            Loop
        Next i
        If Not String.IsNullOrEmpty(Me.Text) Then
            Dim stringFormat As New StringFormat() With {
             .Alignment = StringAlignment.Center,
             .LineAlignment = StringAlignment.Center
            }
            Dim graphics As Graphics = e.Graphics
            'INSTANT VB NOTE: The variable text was renamed since Visual Basic does not handle local variables named the same as class members well:
            Dim text_Renamed As String = Me.Text
            'INSTANT VB NOTE: The variable font was renamed since Visual Basic does not handle local variables named the same as class members well:
            Dim font_Renamed As Font = Me.Font
            Dim solidBrush As New SolidBrush(Me.ForeColor)
            'INSTANT VB TODO TASK: There is no VB equivalent to 'checked' in this context:
            'ORIGINAL LINE: Point point = new Point(checked((int)Math.Round((double)this.Width / 2)), checked((int)Math.Round((double)this.Height / 2)));
            Dim point As New Point(CInt(Math.Round(CDbl(Me.Width) / 2)), CInt(Math.Round(CDbl(Me.Height) / 2)))
            graphics.DrawString(text_Renamed, font_Renamed, solidBrush, point, stringFormat)
        End If
    End Sub

    Private Sub tmr_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Me.flagTimer = Not Me.flagTimer
        Me.Invalidate()
    End Sub

    Public Enum ArrowDir
        Right
        Left
        Up
        Down
    End Enum
End Class

