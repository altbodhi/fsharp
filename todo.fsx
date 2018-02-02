open System
open System.Windows.Forms

type JobId = JobId of int 
type JobName = JobName of string
type TagId = TagId of int
type TagName = TagName of string


type Tag = {Id: TagId; Name: TagName}

type Tags = Tag list

type JobStatus = 
    | New of DateTime
    | Do of DateTime
    | Done of DateTime

type Job = { Id: JobId; Name: JobName; TagList : Tags option; Status : JobStatus }

let AddTag (job:Job, tag:Tag) = { job with TagList = Some(if job.TagList.IsNone then [tag] else job.TagList.Value @ [tag]); Status = JobStatus.New  DateTime.Now }
let test() =
    let job1 = { Id = JobId 1; Name= JobName "сделать зарядку"; TagList = None; Status = JobStatus.New  DateTime.Now}
    let tag1 = {Id = TagId 1; Name=TagName "Здоровье"}
    let tag2 = {Id = TagId 2; Name=TagName "Дом"}
    let job2 =  AddTag(job1, tag1)
    let job2 =  AddTag(job2, tag2)
    printfn "%A" job1
    printfn "%A" job2

type BaseForm(caption:string) as it = 
    inherit Form()
    do it.Text<-caption


let main() = 
    Application.Run(new BaseForm("Задачи"))

do main()    
