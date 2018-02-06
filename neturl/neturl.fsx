(* read http-link line by line and print to console *)
open System
open System.Net
open System.IO
open System.Text

let mutable enc = Encoding.GetEncoding(1251)

let http(url: string) = seq {
    let req = HttpWebRequest.CreateHttp(url)
    let res = req.GetResponse()
    use stream = new StreamReader(res.GetResponseStream(), enc)
    while not stream.EndOfStream do
        let line = stream.ReadLine()
        if not (isNull line) then yield line
}

let httpBinary(url: string) = seq {
    let req = HttpWebRequest.CreateHttp(url)
    let res = req.GetResponse()
    use stream = res.GetResponseStream()
    let mutable size = 1024
    while size > 0 do
        let b = Array.zeroCreate size
        size <- stream.Read(b,0,size)
        printfn "read %A bytes" size
        if size > 0 then yield b |> Array.take(size)
}

let print(url: string) = for line in http(url) do printfn "%s" line

let load(url: string, fileName: string) =
    use fileStream = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None)
    for bytes in httpBinary(url) do 
        printfn "%A r" bytes.Length
        fileStream.Write(bytes, 0, bytes.Length)
    fileStream.Close() |> ignore

//print("http://www.amic.ru")

load("https://e-trust.gosuslugi.ru/Shared/DownloadCert?thumbprint=8CAE88BBFD404A7A53630864F9033606E1DC45E2","guc.cer")
//load ("http://rostelecom.ru/cdp/vguc1.crl","vguc1.crl")
