Module Module1

    Sub DeleteTempFiles(ByVal path As String)
        If My.Computer.FileSystem.DirectoryExists(path & "\memorytest") Then
            My.Computer.FileSystem.DeleteDirectory(path & "\memorytest", FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If
    End Sub

    Sub Main(ByVal args() As String)
        Console.Title = "EzMemoryTest by Melody"
        Dim path As String = Environment.CurrentDirectory
        If args.Count > 0 Then
            path = args(0)
        End If
        If Not My.Computer.FileSystem.DirectoryExists(path) Then
            Console.Error.WriteLine("Directory """ & path & """ does not exist.")
            Environment.Exit(1)
            Exit Sub
        End If
        Try
            Console.Out.WriteLine("Deleting past tempfiles (if any)...")
            DeleteTempFiles(path)
            Console.Out.WriteLine("Creating temp directory...")
            My.Computer.FileSystem.CreateDirectory(path & "\memorytest")
            My.Computer.FileSystem.CreateDirectory(path & "\memorytest\bindir")
            My.Computer.FileSystem.CreateDirectory(path & "\memorytest\extracted")
            Console.Out.WriteLine("Generating 1 GB of random data...")
            Dim bytes(1073741824) As Byte
            Dim r As New Random()
            r.Next()
            For i = 0 To 1073741823
                bytes(i) = r.Next(0, 256)
            Next
            Console.Out.WriteLine("Writing data to disk...")
            My.Computer.FileSystem.WriteAllBytes(path & "\memorytest\bindir\memorytest.bin", bytes, False)
            Console.Out.WriteLine("Clearing memory...")
            bytes = Nothing
            System.GC.Collect()
            Console.Out.WriteLine("Compressing data...")
            IO.Compression.ZipFile.CreateFromDirectory(path & "\memorytest\bindir", path & "\memorytest\memorytest.zip", IO.Compression.CompressionLevel.Optimal, False)
            Console.Out.WriteLine("Clearing memory...")
            System.GC.Collect()
            Console.Out.WriteLine("Extracting data...")
            IO.Compression.ZipFile.ExtractToDirectory(path & "\memorytest\memorytest.zip", path & "\memorytest\extracted")
            Console.Out.WriteLine("Clearing memory...")
            System.GC.Collect()
            Console.Out.WriteLine("Importing files into memory...")
            ReDim bytes(1073741824)
            Dim bytes2(1073741824) As Byte
            bytes = My.Computer.FileSystem.ReadAllBytes(path & "\memorytest\bindir\memorytest.bin")
            bytes2 = My.Computer.FileSystem.ReadAllBytes(path & "\memorytest\extracted\memorytest.bin")
            Console.WriteLine("Comparing bytes...")
            For i = 0 To 1073741823
                If bytes(i) <> bytes2(i) Then
                    Throw New Exception("Bytes did not match.")
                End If
            Next
            Console.Out.WriteLine("All bytes matched - Memory test was successful!")
            Console.Out.WriteLine("Deleting tempfiles...")
            DeleteTempFiles(path)
            If args.Count < 2 Then
                Console.WriteLine("Press ENTER to exit...")
                Dim k As ConsoleKeyInfo = Console.ReadKey()
                While k.Key <> ConsoleKey.Enter
                    k = Console.ReadKey()
                End While
            End If
            Environment.Exit(0)
        Catch ex As Exception
            Console.Error.WriteLine("Memory test was not successful")
            Console.Error.WriteLine(ex.Message)
            Console.Out.WriteLine("Deleting tempfiles...")
            Try
                DeleteTempFiles(path)
            Catch ex2 As Exception
            End Try
        End Try
        If args.Count < 2 Then
            Console.WriteLine("Press ENTER to exit...")
            Dim k As ConsoleKeyInfo = Console.ReadKey()
            While k.Key <> ConsoleKey.Enter
                k = Console.ReadKey()
            End While
        End If
        Environment.Exit(1)
    End Sub

End Module
