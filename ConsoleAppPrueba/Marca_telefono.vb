Option Explicit On

Module Module1

    Sub Main()
        'marcarNumeroTelefono("7500500")
    End Sub


    '    Dim objComunicar

    '    Public Sub marcarNumeroTelefono(numero As String)
    '        Dim cadConexion As String
    '        Dim resultado As Integer
    '        Dim modem As String

    '        On Error GoTo cError

    '        objComunicar = CreateObject("MSCommLib.MSComm")

    '        'Enviamos la cadena ATDT que equivale a 
    '        'descolgar el modem y marcar el número indicado por tonos 
    '        'ATDP por pulsos 
    '        'https://support.microsoft.com/en-us/kb/164659
    '        If opTonos.Value = True Then
    '            cadConexion$ = "ATDT" + numero + ";" + Chr$(13)
    '        End If
    '        If opPulsos.Value = True Then
    '            cadConexion$ = "ATDP" + numero + ";" + Chr$(13)
    '        End If

    '        'Indicaremos el puerto COM a utilizar (donde esté conectado el módem) 
    '        objComunicar.CommPort = CInt(txtPuerto.Text)

    '        'En la configuración le indicaremos 
    '        '300 = 300 baudios (velocidad) 
    '        'N = sin paridad 
    '        '8 = 8 bits de datos 
    '        '1 = bit de parada 
    '        objComunicar.Settings = "300,N,8,1"

    '        objComunicar.PortOpen = True

    '        'Liberamos el búfer de salida 
    '        objComunicar.InBufferCount = 0

    '        'Enviamos los comandos AT con el número al módem 
    '        objComunicar.Output = cadConexion$

    '        'Esperamos a recibir "OK" desde el módem 
    '        Do
    '            resultado = DoEvents()
    '            ' Si hay datos en el Buffer los leemos 
    '            If objComunicar.InBufferCount Then
    '                modem$ = modem$ + objComunicar.Input
    '                'Comprobamos si los datos leídos son "OK" 
    '                If InStr(modem$, "OK") Then
    '                    'Podemos indicar al usuario que descuelgue el teléfono 
    '                    MsgBox("Descuelge su teléfono y pulse 'Aceptar' para cerrar" _
    '                        + " el módem. Podrá continuar la llamada en su teléfono." _
    '                        + Chr(13) + Chr(13) + "Recuerde que si pulsa 'Aceptar' " _
    '                        + "sin descolgar su teléfono se cortará la llamada.", _
    '                        vbInformation + vbOKOnly)
    '                    Exit Do
    '                End If
    '            End If
    '        Loop

    '        'Desconectamos el módem, para ello enviamos el comando "ATH" 
    '        objComunicar.Output = "ATH" + Chr$(13)
    '        'Cerramos el puerto 
    '        objComunicar.PortOpen = False

    'cSalir:
    '        Exit Sub

    'cError:
    '        MsgBox("Error en la marcación del número de teléfono: " & _
    '            Err.Number & " - " & Err.Description, vbExclamation)
    '        GoTo cSalir
    '    End Sub


    '    Private Sub bColgar_Click()
    '        On Error GoTo cError

    '        'Desconectamos el módem, para ello enviamos el comando "ATH" 
    '        objComunicar.Output = "ATH" + Chr$(13)

    'cSalir:
    '        Exit Sub

    'cError:
    '        MsgBox("Error en la marcación del número de teléfono: " & _
    '            Err.Number & " - " & Err.Description, vbExclamation)
    '        GoTo cSalir
    '    End Sub

    '    Private Sub bMarcar_Click()
    '        marcarNumeroTelefono(txtNumero.Text)
    '    End Sub


End Module
