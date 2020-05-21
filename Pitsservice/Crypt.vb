Imports System.Security.Cryptography
Imports System.IO
Imports System.Text

Public Class Crypt

    'SymmCrypto is a wrapper of System.Security.Cryptography.SymmetricAlgorithm classes
    ' and simplifies the interface. It supports customized SymmetricAlgorithm as well.

    'Supported .Net intrinsic SymmetricAlgorithm classes.

    Public Enum SymmProvEnum
        DES
        'RC2
        'Rijndael
    End Enum

    Private mobjCryptoService As SymmetricAlgorithm

    'Constructor for using an intrinsic .Net SymmetricAlgorithm class.
    Public Sub New(ByVal cryptType As SymmProvEnum)
        Select Case cryptType
            Case SymmProvEnum.DES
                mobjCryptoService = New DESCryptoServiceProvider()
                'Case SymmProvEnum.RC2
                '    mobjCryptoService = New RC2CryptoServiceProvider()
                'Case SymmProvEnum.Rijndael
                '    mobjCryptoService = New RijndaelManaged()
        End Select
    End Sub

    'Constructor for using a customized SymmetricAlgorithm class.
    Public Sub New(ByVal serviceProvider As SymmetricAlgorithm)
        mobjCryptoService = serviceProvider
    End Sub


    'Depending on the legal key size limitations of a specific CryptoService provider
    ' and length of the private key provided, padding the secret key with space character
    ' to meet the legal size of the algorithm.
    Private Function GetLegalKey(ByVal key As String) As Byte()
        Dim sTemp As String
        If mobjCryptoService.LegalKeySizes.Length > 0 Then
            Dim LessSize As Int32 = 0
            Dim MoreSize As Int32 = mobjCryptoService.LegalKeySizes(0).MinSize
            ' key sizes are in bits
            While key.Length * 8 > MoreSize
                LessSize = MoreSize
                MoreSize += mobjCryptoService.LegalKeySizes(0).SkipSize
            End While
            sTemp = key.PadRight(MoreSize \ 8, " "c)
        Else
            sTemp = key
        End If

        'convert the secret key to byte array
        Return ASCIIEncoding.ASCII.GetBytes(sTemp)
    End Function

    Public Function Encrypting(ByVal source As String, ByVal key As String) As String
        Dim BytIn As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(source)
        ' create a MemoryStream so that the process can be done without I/O files
        Dim ms As New System.IO.MemoryStream()

        Dim BytKey As Byte() = GetLegalKey(key)

        ' set the private key
        mobjCryptoService.Key = BytKey
        mobjCryptoService.IV = BytKey

        ' create an Encryptor from the Provider Service instance
        Dim Encrypto As ICryptoTransform = mobjCryptoService.CreateEncryptor()

        ' create Crypto Stream that transforms a stream using the encryption
        Dim cs As New CryptoStream(ms, Encrypto, CryptoStreamMode.Write)

        ' write out encrypted content into MemoryStream
        cs.Write(BytIn, 0, BytIn.Length)
        cs.FlushFinalBlock()

        ' get the output and trim the '\0' bytes
        Dim BytOut As Byte() = ms.GetBuffer()
        Dim i As Int32 = 0
        While i < BytOut.Length
            If BytOut(i) = 0 Then
                Exit While
            End If
            i += 1
        End While

        ' convert into Base64 so that the result can be used in xml
        Return System.Convert.ToBase64String(BytOut, 0, i)
    End Function

    Public Function Decrypting(ByVal source As String, ByVal key As String) As String
        ' convert from Base64 to binary
        Dim BytIn As Byte() = System.Convert.FromBase64String(source)
        ' create a MemoryStream with the input
        Dim Ms As New System.IO.MemoryStream(BytIn, 0, BytIn.Length)

        Dim BytKey As Byte() = GetLegalKey(key)

        ' set the private key
        mobjCryptoService.Key = BytKey
        mobjCryptoService.IV = BytKey

        ' create a Decryptor from the Provider Service instance
        Dim Encrypto As ICryptoTransform = mobjCryptoService.CreateDecryptor()

        ' create Crypto Stream that transforms a stream using the decryption
        Dim cs As New CryptoStream(Ms, Encrypto, CryptoStreamMode.Read)


        ' read out the result from the Crypto Stream
        Dim sr As New System.IO.StreamReader(cs)
        Return sr.ReadToEnd()
    End Function

End Class
