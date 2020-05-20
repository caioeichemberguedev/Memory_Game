Imports System.Threading.Thread
Imports System.Drawing.Drawing2D
Public Class Form1

    Private Centrodatela As Point
    Private Listadecirculos As ArrayList = New ArrayList
    Private Listadesorteio As New List(Of Integer)
    Private Listadeescolha As New List(Of Integer)
    Private Niveldojogo As Integer = 0
    Private Circuloatual As Integer = -1
    Private Pontodomouse As Point
    Private Minhavezdeescolher As Boolean = False
    Private Estadodejogo As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ConfiguraçãoDaTela()
        CriandoObjetos()

    End Sub

    Private Sub ConfiguraçãoDaTela()

        Me.BackColor = Color.Black
        Me.Size = New Size(800, 400)
        Me.DoubleBuffered = True
        Me.Text = "Jogo de Memória"
        Me.CenterToScreen()

        Me.FormBorderStyle = FormBorderStyle.None
        Centrodatela = New Point(Me.Width / 2, Me.Height / 2)
        Me.FormBorderStyle = FormBorderStyle.Sizable

    End Sub

    Private Sub CriandoObjetos()

        Dim NovoPonto As Point
        NovoPonto = New Point(Centrodatela.X - 300, Centrodatela.Y)
        Listadecirculos.Add(New Classedecirculos(NovoPonto, Color.DeepSkyBlue, Brushes.DeepSkyBlue, False))
        NovoPonto = New Point(Centrodatela.X - 100, Centrodatela.Y)
        Listadecirculos.Add(New Classedecirculos(NovoPonto, Color.Red, Brushes.Red, False))
        NovoPonto = New Point(Centrodatela.X + 100, Centrodatela.Y)
        Listadecirculos.Add(New Classedecirculos(NovoPonto, Color.LimeGreen, Brushes.LimeGreen, False))
        NovoPonto = New Point(Centrodatela.X + 300, Centrodatela.Y)
        Listadecirculos.Add(New Classedecirculos(NovoPonto, Color.Yellow, Brushes.Yellow, False))
        Refresh()

    End Sub

    Private Sub Jogo()
        Listadesorteio.Clear()
        Listadeescolha.Clear()

        For k As Integer = 0 To Niveldojogo
            Dim Sorteio As New Random
            Dim Numero As Integer = Sorteio.Next(0, 3)
            Listadesorteio.Add(Numero)
            Listadecirculos(Numero).GSCirculoligado = True
            Refresh()
            Listadecirculos(Numero).GSCirculoligado = False
            Sleep(800)
            Refresh()
            'Sleep(600)
        Next
        Estadodejogo = 3
        Refresh()
        Sleep(300)
        Estadodejogo = 0
        Minhavezdeescolher = True

    End Sub

    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If Keys.Enter Then
            Jogo()
        End If
    End Sub

    Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If Minhavezdeescolher = True Then
            For k As Integer = 0 To Listadecirculos.Count - 1
                If VerificaSeEstaNoCirculo(Pontodomouse, k) = True Then
                    Listadeescolha.Add(Circuloatual)
                    Exit For
                End If
            Next
            If Listadeescolha.Count - 1 = Niveldojogo Then
                Minhavezdeescolher = False
                Listadecirculos(Circuloatual).GSCirculoligado = False
                Refresh()
                Sleep(1000) 'usuario entender que vai começar denovo talovez tudo ficar verde ou vermelho quando erra
                VerificaSeAcertouSequencia()
            End If
        End If
    End Sub

    Private Sub VerificaSeAcertouSequencia()
        If Listadeescolha.SequenceEqual(Listadesorteio) Then
            Niveldojogo += 1
            Estadodejogo = 1
            Refresh()
            Sleep(500)
            Estadodejogo = 0
            Refresh()
            Sleep(200)
        Else
            Niveldojogo = 0
            Estadodejogo = 2
            Refresh()
            Sleep(500)
            Estadodejogo = 0
            Refresh()
            Sleep(200)
        End If
        Jogo()
    End Sub

    Public Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If Minhavezdeescolher = True Then
            Pontodomouse = New Point(e.X, e.Y)
            For k As Integer = 0 To Listadecirculos.Count - 1
                If VerificaSeEstaNoCirculo(Pontodomouse, k) = True Then
                    Listadecirculos(Circuloatual).GSCirculoligado = True
                    Exit For
                Else
                    Listadecirculos(k).GSCirculoligado = False
                End If
            Next
            Refresh()
        End If
    End Sub

    Public Function VerificaSeEstaNoCirculo(ByVal P As Point, ByVal K As Integer) As Boolean
        If P.X > Listadecirculos(K).GSPontodocirculo.X - 75 And P.X < Listadecirculos(K).GSPontodocirculo.X + 75 And
            P.Y > Listadecirculos(K).GSPontodocirculo.Y - 75 And P.Y < Listadecirculos(K).GSPontodocirculo.Y + 75 Then
            Circuloatual = K
            Return True
        End If
        Circuloatual = -1
        Return False
    End Function


    Private Sub Desenho(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        For K As Integer = 0 To Listadecirculos.Count - 1
            Dim Tintadacaneta As Color = Listadecirculos(K).GSCordocirculo
            Dim Retangulolimitador As Rectangle = New Rectangle(Listadecirculos(K).GSPontodocirculo.x - 75, Listadecirculos(K).GSPontodocirculo.y - 75, 150, 150)
            If Estadodejogo = 0 Then 'CORES NORMAIS
                Dim MyPen As Pen = New Pen(Tintadacaneta, 5)
                e.Graphics.DrawEllipse(MyPen, Retangulolimitador)
                If Listadecirculos(K).GSCirculoligado = True Then
                    Dim Cordopreenchimento As Brush = Listadecirculos(K).GSPreenchimentodocirculo
                    e.Graphics.FillEllipse(Cordopreenchimento, Retangulolimitador)
                Else
                    e.Graphics.FillEllipse(Brushes.Black, Retangulolimitador)
                End If
            ElseIf Estadodejogo = 1 Then 'TUDO VERDE QUANDO USUARIO GANHA
                Dim MyPen As Pen = New Pen(Color.LimeGreen, 5)
                e.Graphics.DrawEllipse(MyPen, Retangulolimitador)
                e.Graphics.FillEllipse(Brushes.LimeGreen, Retangulolimitador)
            ElseIf Estadodejogo = 2 Then ' TUDO VERMELHO QUANDO USUARIO PERDE
                Dim MyPen As Pen = New Pen(Color.Red, 5)
                e.Graphics.DrawEllipse(MyPen, Retangulolimitador)
                e.Graphics.FillEllipse(Brushes.Red, Retangulolimitador)
            Else
                Dim MyPen As Pen = New Pen(Color.White, 5)
                e.Graphics.DrawEllipse(MyPen, Retangulolimitador)
                e.Graphics.FillEllipse(Brushes.White, Retangulolimitador)
            End If
        Next
    End Sub

End Class
