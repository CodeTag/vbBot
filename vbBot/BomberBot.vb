Imports System.Net.Sockets
Imports System.Text
Imports System.Text.RegularExpressions

Class BomberBot
    Private inFromServer As Byte() = New Byte(1279) {}
    Private socketCliente As Socket = Nothing
    Private bot As Bot = Nothing

    Private conectado As Boolean = False

    Public Sub New()
        Try
            conectar("vbBot", "984198716")
            controlConexion()
        Catch e As Exception
            Console.WriteLine(e.Message)
            Console.ReadKey()
        End Try
    End Sub

    Private Sub conectar(ByVal user As [String], ByVal token As [String])
        socketCliente = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        socketCliente.Connect("localhost", 5000)

        socketCliente.Receive(inFromServer)
        Dim bienvenida As [String] = Encoding.UTF8.GetString(inFromServer)
        Console.WriteLine(bienvenida)

        Dim msg As Byte() = System.Text.Encoding.UTF8.GetBytes(Convert.ToString(user) & "," & Convert.ToString(token))

        socketCliente.Send(msg, msg.Length, SocketFlags.None)
        conectado = True
    End Sub

    Private Sub controlConexion()
        Dim response As Byte() = New Byte(511) {}
        While conectado
            Console.WriteLine("turno")

            socketCliente.Receive(response)
            Dim serverMessage As [String] = Encoding.UTF8.GetString(response)
            Console.WriteLine(serverMessage)
            Dim message As String() = Regex.Split(serverMessage, ";")

            If message.Length = 0 Then
                Continue While
            End If

            If message(0) = "EMPEZO" Then
                bot = New Bot(message(2)(0))
                bot.updateMap(message(1))
            ElseIf message(0) = "TURNO" Then
                Console.WriteLine("turno: " & message(1))
                bot.updateMap(message(2))
                Dim msg As Byte() = System.Text.Encoding.UTF8.GetBytes(bot.move())
                socketCliente.Send(msg, msg.Length, SocketFlags.None)
            ElseIf message(0) = "PERDIO" Then
                Console.WriteLine("perdi :(")
            End If
        End While
    End Sub
End Class
