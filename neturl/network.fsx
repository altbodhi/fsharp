open System.Net
open System.Text 
open System.Net.Sockets

type stream               = NetworkStream
let curry g b n           = g(b,0,n) |> ignore; b
let read  n (s : stream)  = curry s.Read (Array.zeroCreate n) n,s
let write b (s : stream)  = curry s.Write b b.Length; s
let close (b,(s : stream))= s.Close(); b
let connect host port     = TcpClient(host,port).GetStream()

let response : byte[] = 
  connect "google.com" 80
  |> write (Encoding.Default.GetBytes("GET / HTTP/1.1\r\n\r\n"))
  |> read 1024
  |> close

printfn "%A"  (Encoding.Default.GetString(response))
