using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegExRazorReplace.ViewModels.DesignTime
{
  class MainWindowViewModelExample : MainWindowViewModel
  {
    protected override void Initialize()
    {
      this.Input = "INSERT INTO <user>.AppCommand (ID, Name, Description, StatusID, ContextID) VALUES (N'4de409b0-49fe-4ffd-ae2e-31bc910a9feb', N'nGroup.Info.eEvolution.MiddleLayer.CommandManager.DPDZonenTabelleCommand', NULL, 0, 'f99ad222-9552-4fd2-806b-f9cb3b031036');";
      this.Output = "INSERT INTO <user>.AppCommand (ID, Name, Description, StatusID, ContextID) VALUES (N'4DE409B0-49FE-4FFD-AE2E-31BC910A9FEB', N'nGroup.Info.eEvolution.MiddleLayer.CommandManager.DPDZonenTabelleCommand', NULL, 0, 'F99AD222-9552-4FD2-806B-F9CB3B031036');";

      var list = new List<ParseEntryViewModel>() {
        new ParseEntryViewModelExample()
        {
          RegEx = "[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}",
          Razor = "@Model.Value.ToUpper()",
          Name = "Beispiel für Guids",
          MainWindowViewModel = this
        },
        new ParseEntryViewModelExample()
        {
          RegEx = "Noch ein Beispiel Regex",
          RegExDiagnostics = "Hier steht dann der Regex Fehler",
          Razor = "@Model.Value",
          RazorDiagnostics = "Hier steht dann der Razor Fehler",
          Name = "Beispiel für Guids",
          MainWindowViewModel = this
        },
        new ParseEntryViewModelExample()
        {
          RegEx = "Noch ein Beispiel Regex",
          Razor = "@Model.Value",
          Name = "Beispiel für Guids",
          IsExpanded = false,
          MainWindowViewModel = this
        }
      };

      Entries = new BindingList<ParseEntryViewModel>(list);
    }
  }
}
