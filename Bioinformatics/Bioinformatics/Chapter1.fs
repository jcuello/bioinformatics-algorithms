namespace Bioinformatics

module Chapter1 =
  open System

  let myPatternCount (text:string) (pattern:string) =
    // is Seq.windowed optimized for large text?
    let test = 
        Seq.windowed pattern.Length text
        |> Seq.countBy (fun kmer ->
            // there has to be a better way to convert from array chars to string
            if  (kmer |> String.Concat) = pattern then pattern
            else "")
        |> Seq.maxBy (fun ((kmer,_):string*int) -> kmer.Length)
    snd test

  let patternCount(text:string, pattern:string) =
    let patternLength = pattern.Length
    let textLength = text.Length
    let mutable count = 0
    for i in 0..textLength-patternLength do
        if text.Substring(i, patternLength) = pattern then
            count <- count + 1
    count