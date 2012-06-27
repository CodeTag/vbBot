Imports System.Text.RegularExpressions

Class Bot
    Private letter As Char = " "c
    Private x As Integer
    Private y As Integer
    Private map As String()()

    Public Sub New(ByVal letter As Char)

        Me.letter = letter
    End Sub

    Public Sub updateMap(ByVal mapa As String)
        Dim rows As String() = Regex.Split(mapa, vbLf)
        map = New String(rows.Length - 1)() {}
        For i As Integer = 0 To rows.Length - 1
            map(i) = Regex.Split(rows(i), ",")
        Next
        Dim pos As Integer = mapa.IndexOf(Me.letter) / 2
        Me.y = pos / rows.Length
        Me.x = pos Mod rows.Length
    End Sub

    Public Function move() As [String]
        Dim random As New Random()
        Dim mov As Integer = random.[Next](0, 7)
        Console.WriteLine("mov " & mov)
        Select Case mov
            Case 0
                Return "N"
            Case 1
                Return "E"
            Case 2
                Return "S"
            Case 3
                Return "O"
            Case 4
                Return "BN"
            Case 5
                Return "BE"
            Case 6
                Return "BS"
            Case 7
                Return "BO"
        End Select
        Return "P"
    End Function

End Class
