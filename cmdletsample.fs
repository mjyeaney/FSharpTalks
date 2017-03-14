namespace FSUtils

open System.Management.Automation
open System.Text
open System.Text.RegularExpressions

/// Grep-like cmdlet Search-File
[<Cmdlet("Say", "Hello")>]
type SearchFileCmdlet() =
    inherit PSCmdlet()
    
    /// Called once per object coming from the pipeline.
    override this.ProcessRecord() =
        this.WriteObject("Hello from F#!!!!")