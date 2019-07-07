Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class PilotLight3Color
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap

    Private bitmap_4 As Bitmap

    Private bitmap_5 As Bitmap

    Private rectangle_0 As Rectangle

    Private decimal_0 As Decimal

    Private bool_0 As Boolean

    Private stringFormat_0 As StringFormat

    Private solidBrush_0 As SolidBrush

    Private bool_1 As Boolean

    Private bool_2 As Boolean

    Private lightColorOption_0 As PilotLight3Color.LightColorOption

    Private lightColorOption_1 As PilotLight3Color.LightColorOption

    Private lightColorOption_2 As PilotLight3Color.LightColorOption

    Private legendPlates_0 As PilotLight3Color.LegendPlates

    Private outputType_0 As OutputType

    Private bitmap_6 As Bitmap

    Private double_0 As Double

    Private int_0 As Integer

    Private int_1 As Integer

    Public Property LegendPlate As PilotLight3Color.LegendPlates
        Get
            Return Me.legendPlates_0
        End Get
        Set(ByVal value As PilotLight3Color.LegendPlates)
            Me.legendPlates_0 = value
            Me.OnResize(EventArgs.Empty)
            Me.method_0()
        End Set
    End Property

    Public Property LightColor2 As PilotLight3Color.LightColorOption
        Get
            Return Me.lightColorOption_0
        End Get
        Set(ByVal value As PilotLight3Color.LightColorOption)
            Me.lightColorOption_0 = value
            Me.method_0()
        End Set
    End Property

    Public Property LightColor3 As PilotLight3Color.LightColorOption
        Get
            Return Me.lightColorOption_2
        End Get
        Set(ByVal value As PilotLight3Color.LightColorOption)
            Me.lightColorOption_2 = value
            Me.method_0()
        End Set
    End Property

    Public Property LightColorOff As PilotLight3Color.LightColorOption
        Get
            Return Me.lightColorOption_1
        End Get
        Set(ByVal value As PilotLight3Color.LightColorOption)
            Me.lightColorOption_1 = value
            Me.method_0()
        End Set
    End Property

    Public Property OutputType As OutputType
        Get
            Return Me.outputType_0
        End Get
        Set(ByVal value As OutputType)
            Me.outputType_0 = value
        End Set
    End Property

    <Category("Appearance")>
    Public Property SelectColor2 As Boolean
        Get
            Return Me.bool_1
        End Get
        Set(ByVal value As Boolean)
            If (value <> Me.bool_1) Then
                Me.bool_1 = value
                MyBase.Invalidate()
                Me.OnValueSelectColor1Changed(EventArgs.Empty)
            End If
        End Set
    End Property

    <Category("Appearance")>
    Public Property SelectColor3 As Boolean
        Get
            Return Me.bool_2
        End Get
        Set(ByVal value As Boolean)
            If (value <> Me.bool_2) Then
                Me.bool_2 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Private Property tmrError As System.Windows.Forms.Timer

    Public Sub New()
        MyBase.New()
        Me.rectangle_0 = New Rectangle()
        Me.stringFormat_0 = New StringFormat()
        Me.lightColorOption_0 = PilotLight3Color.LightColorOption.Green
        Me.lightColorOption_1 = PilotLight3Color.LightColorOption.White
        Me.lightColorOption_2 = PilotLight3Color.LightColorOption.Red
        Me.legendPlates_0 = PilotLight3Color.LegendPlates.Large
        Me.outputType_0 = OutputType.MomentarySet
        Me.tmrError = New System.Windows.Forms.Timer()
        Me.double_0 = CDbl(My.Resources.LegendPlate.Height) / CDbl(My.Resources.LegendPlate.Width)
        Me.stringFormat_0 = New StringFormat() With
        {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }
        Me.solidBrush_0 = New SolidBrush(Color.Black)
    End Sub

    Private Sub method_0()
        Dim num As Decimal
        Dim num1 As Decimal
        Dim graphic As Graphics
        If (Me.legendPlates_0 <> PilotLight3Color.LegendPlates.Large) Then
            num = New Decimal(CSng(MyBase.Width) / CSng(My.Resources.LegendPlateShort.Width))
            num1 = New Decimal(CSng(MyBase.Height) / CSng(My.Resources.LegendPlateShort.Height))
        Else
            num = New Decimal(CSng(MyBase.Width) / CSng(My.Resources.LegendPlate.Width))
            num1 = New Decimal(CSng(MyBase.Height) / CSng(My.Resources.LegendPlate.Height))
        End If
        If (Decimal.Compare(num, num1) >= 0) Then
            Me.decimal_0 = num1
        Else
            Me.decimal_0 = num
        End If
        If (Decimal.Compare(Me.decimal_0, Decimal.Zero) > 0) Then
            If (Me.bitmap_0 IsNot Nothing) Then
                Me.bitmap_0.Dispose()
            End If
            If (Me.legendPlates_0 <> PilotLight3Color.LegendPlates.Large) Then
                Me.bitmap_0 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlateShort.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlateShort.Height), Me.decimal_0)))
                Me.double_0 = CDbl(My.Resources.LegendPlateShort.Height) / CDbl(My.Resources.LegendPlateShort.Width)
                graphic = Graphics.FromImage(Me.bitmap_0)
                graphic.DrawImage(My.Resources.LegendPlateShort, 0!, 0!, Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlateShort.Width), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlateShort.Height), Me.decimal_0)))
            Else
                Me.bitmap_0 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlate.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlate.Height), Me.decimal_0)))
                Me.double_0 = CDbl(My.Resources.LegendPlate.Height) / CDbl(My.Resources.LegendPlate.Width)
                graphic = Graphics.FromImage(Me.bitmap_0)
                graphic.DrawImage(My.Resources.LegendPlate, 0!, 0!, Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlate.Width), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlate.Height), Me.decimal_0)))
            End If
            Me.bitmap_1 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.WhitePilotOn.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.GreenPilotOff.Height), Me.decimal_0)))
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.GreenPilotOn.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.GreenPilotOff.Height), Me.decimal_0)))
            Me.bitmap_3 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.RedPilotOn.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.GreenPilotOff.Height), Me.decimal_0)))
            graphic = Graphics.FromImage(Me.bitmap_1)
            Dim graphic1 As Graphics = Graphics.FromImage(Me.bitmap_2)
            Dim graphic2 As Graphics = Graphics.FromImage(Me.bitmap_3)
            If (Me.lightColorOption_1 = PilotLight3Color.LightColorOption.Green) Then
                graphic.DrawImage(My.Resources.GreenPilotOn, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            ElseIf (Me.lightColorOption_1 = PilotLight3Color.LightColorOption.Red) Then
                graphic.DrawImage(My.Resources.RedPilotOn, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            ElseIf (Me.lightColorOption_1 = PilotLight3Color.LightColorOption.Blue) Then
                graphic.DrawImage(My.Resources.BluePilotOn, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            ElseIf (Me.lightColorOption_1 <> PilotLight3Color.LightColorOption.White) Then
                graphic.DrawImage(My.Resources.YellowPilotOn, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            Else
                graphic.DrawImage(My.Resources.WhitePilotOn, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            End If
            If (Me.lightColorOption_0 = PilotLight3Color.LightColorOption.Green) Then
                graphic1.DrawImage(My.Resources.GreenPilotOn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            ElseIf (Me.lightColorOption_0 = PilotLight3Color.LightColorOption.Red) Then
                graphic1.DrawImage(My.Resources.RedPilotOn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            ElseIf (Me.lightColorOption_0 = PilotLight3Color.LightColorOption.Blue) Then
                graphic1.DrawImage(My.Resources.BluePilotOn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            ElseIf (Me.lightColorOption_0 <> PilotLight3Color.LightColorOption.White) Then
                graphic1.DrawImage(My.Resources.YellowPilotOn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            Else
                graphic1.DrawImage(My.Resources.WhitePilotOn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            End If
            If (Me.lightColorOption_2 = PilotLight3Color.LightColorOption.Green) Then
                graphic2.DrawImage(My.Resources.GreenPilotOn, 0, 0, Me.bitmap_3.Width, Me.bitmap_3.Height)
            ElseIf (Me.lightColorOption_2 = PilotLight3Color.LightColorOption.Red) Then
                graphic2.DrawImage(My.Resources.RedPilotOn, 0, 0, Me.bitmap_3.Width, Me.bitmap_3.Height)
            ElseIf (Me.lightColorOption_2 = PilotLight3Color.LightColorOption.Blue) Then
                graphic2.DrawImage(My.Resources.BluePilotOn, 0, 0, Me.bitmap_3.Width, Me.bitmap_3.Height)
            ElseIf (Me.lightColorOption_2 <> PilotLight3Color.LightColorOption.White) Then
                graphic2.DrawImage(My.Resources.YellowPilotOn, 0, 0, Me.bitmap_3.Width, Me.bitmap_3.Height)
            Else
                graphic2.DrawImage(My.Resources.WhitePilotOn, 0, 0, Me.bitmap_3.Width, Me.bitmap_3.Height)
            End If
            Me.bitmap_5 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.PilotLightDown.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.PilotLightDown.Height), Me.decimal_0)))
            graphic = Graphics.FromImage(Me.bitmap_5)
            graphic.DrawImage(My.Resources.PilotLightDown, 0, 0, Me.bitmap_5.Width, Me.bitmap_5.Height)
            graphic.Dispose()
            graphic1.Dispose()
            Me.rectangle_0.X = 0
            Me.rectangle_0.Width = MyBase.Width
            Me.rectangle_0.Y = 0
            If (Me.legendPlates_0 <> PilotLight3Color.LegendPlates.Large) Then
                Me.rectangle_0.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.18))
            Else
                Me.rectangle_0.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.4))
            End If
            If (Me.bitmap_6 IsNot Nothing) Then
                Me.bitmap_6.Dispose()
            End If
            Me.bitmap_6 = New Bitmap(MyBase.Width, MyBase.Height)
            If (Me.bool_1) Then
                Me.bitmap_4 = Me.bitmap_2
            ElseIf (Not Me.bool_2) Then
                Me.bitmap_4 = Me.bitmap_1
            Else
                Me.bitmap_4 = Me.bitmap_3
            End If
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        Me.bool_0 = True
        If (MyBase.Enabled) Then
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mevent As MouseEventArgs)
        MyBase.OnMouseUp(mevent)
        Me.bool_0 = False
        If (MyBase.Enabled) Then
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Not (Me.bitmap_0 Is Nothing Or Me.bitmap_6 Is Nothing) And Me.bitmap_4 IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_6)
            graphic.DrawImage(Me.bitmap_0, 0, 0)
            Me.bitmap_4 = Me.bitmap_1
            If (Me.bool_2) Then
                Me.bitmap_4 = Me.bitmap_3
            ElseIf (Me.bool_1) Then
                Me.bitmap_4 = Me.bitmap_2
            End If
            If (Me.legendPlates_0 <> PilotLight3Color.LegendPlates.Large) Then
                graphic.DrawImage(Me.bitmap_4, Convert.ToInt32(CDbl(Me.bitmap_0.Width) / 2 - CDbl(Me.bitmap_4.Width) / 2), Convert.ToInt32(CDbl(Me.bitmap_0.Height) * 0.59 - CDbl(Me.bitmap_4.Height) / 2))
            Else
                graphic.DrawImage(Me.bitmap_4, Convert.ToInt32(CDbl(Me.bitmap_0.Width) / 2 - CDbl(Me.bitmap_4.Width) / 2), Convert.ToInt32(CDbl(Me.bitmap_0.Height) * 0.68 - CDbl(Me.bitmap_4.Height) / 2))
            End If
            If (Not String.IsNullOrEmpty(Me.Text)) Then
                If (Me.solidBrush_0.Color <> MyBase.ForeColor) Then
                    Me.solidBrush_0.Color = MyBase.ForeColor
                End If
                graphic.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_0)
            End If
            If (Me.bool_0) Then
                If (Me.legendPlates_0 <> PilotLight3Color.LegendPlates.Large) Then
                    graphic.DrawImage(Me.bitmap_5, Convert.ToSingle(Decimal.Multiply(New Decimal(102L), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(170L), Me.decimal_0)))
                Else
                    graphic.DrawImage(Me.bitmap_5, Convert.ToSingle(Decimal.Multiply(New Decimal(102L), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(360L), Me.decimal_0)))
                End If
            End If
            painte.Graphics.DrawImage(Me.bitmap_6, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        If (Me.legendPlates_0 <> PilotLight3Color.LegendPlates.Large) Then
            Me.double_0 = CDbl(My.Resources.LegendPlateShort.Height) / CDbl(My.Resources.LegendPlateShort.Width)
        Else
            Me.double_0 = CDbl(My.Resources.LegendPlate.Height) / CDbl(My.Resources.LegendPlate.Width)
        End If
        MyBase.OnResize(e)
        If (Me.int_1 < MyBase.Height Or Me.int_0 < MyBase.Width) Then
            If (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= Me.double_0) Then
                MyBase.Height = CInt(Math.Round(CDbl(MyBase.Width) * Me.double_0))
            Else
                MyBase.Width = CInt(Math.Round(CDbl(MyBase.Height) / Me.double_0))
            End If
        ElseIf (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= Me.double_0) Then
            MyBase.Width = CInt(Math.Round(CDbl(MyBase.Height) / Me.double_0))
        Else
            MyBase.Height = CInt(Math.Round(CDbl(MyBase.Width) * Me.double_0))
        End If
        Me.int_0 = MyBase.Width
        Me.int_1 = MyBase.Height
        Me.method_0()
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub

    Protected Overridable Sub OnValueSelectColor1Changed(ByVal e As EventArgs)
        RaiseEvent ValueSelectColor1Changed(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler


    Public Event ValueSelectColor1Changed As EventHandler


    Public Enum LegendPlates
        Large
        Small
    End Enum

    Public Enum LightColorOption
        Red
        Green
        Blue
        Yellow
        White
    End Enum
End Class
