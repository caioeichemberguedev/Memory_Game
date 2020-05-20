Public Class Classedecirculos

    'Dim Iddocirculo As Integer
    Dim Pontodocirculo As Point
    Dim Cordocirculo As Color
    Dim Preenchimentodocirculo As Brush
    Dim Circuloligado As Boolean


    Public Sub New(Pontodocirculo As Point, Cordocirculo As Color, Preenchimentodocirculo As Brush, Circuloligado As Boolean)

        'Me.Iddocirculo = Iddocirculo
        Me.Pontodocirculo = Pontodocirculo
        Me.Cordocirculo = Cordocirculo
        Me.Preenchimentodocirculo = Preenchimentodocirculo
        Me.Circuloligado = Circuloligado

    End Sub

    'Public Property GSIddocirculo() As Integer
    '    Get
    '        Return Iddocirculo
    '    End Get
    '    Set(Iddocirculo_ As Integer)
    '        Iddocirculo = Iddocirculo_
    '    End Set
    'End Property

    Public Property GSPontodocirculo() As Point
        Get
            Return Pontodocirculo
        End Get
        Set(Pontodocirculo_ As Point)
            Pontodocirculo = Pontodocirculo_
        End Set
    End Property

    Public Property GSCordocirculo() As Color
        Get
            Return Cordocirculo
        End Get
        Set(Cordocirculo_ As Color)
            Cordocirculo = Cordocirculo_
        End Set
    End Property

    Public Property GSPreenchimentodocirculo() As Brush
        Get
            Return Preenchimentodocirculo
        End Get
        Set(Preenchimentodocirculo_ As Brush)
            Preenchimentodocirculo = Preenchimentodocirculo_
        End Set
    End Property

    Public Property GSCirculoligado() As Boolean
        Get
            Return Circuloligado
        End Get
        Set(Circuloligado_ As Boolean)
            Circuloligado = Circuloligado_
        End Set
    End Property

End Class
