﻿namespace RegExRazorReplace.ViewModels
{
  using Microsoft.Practices.Unity;
  using Prism.Events;
  using Prism.Mvvm;
  using RegExRazorReplace.Data;
  using RegExRazorReplace.Events;
  using RegExRazorReplace.Properties;
  using RegExRazorReplace.Services;
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.ComponentModel;

  internal class MainWindowViewModel : BindableBase
  {
    #region Fields

    private TemplateService templateService;

    #endregion Fields

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="MainWindowViewModel" /> class.</summary>
    public MainWindowViewModel()
    {
      //this.PropertyChanged += this.MainWindowViewModelPropertyChanged;

      this.Initialize();
    }

    protected virtual void Initialize()
    {
      var container = ContainerFactory.Create();
      this.templateService = container.Resolve<TemplateService>();
      var eventAggregator = container.Resolve<IEventAggregator>();
      eventAggregator.GetEvent<ParseCompleted>().Subscribe(this.HandleParseCompletedEvent, ThreadOption.UIThread);

      this.Input = "INSERT INTO <user>.AppCommand (ID, Name, Description, StatusID, ContextID) VALUES (N'4de409b0-49fe-4ffd-ae2e-31bc910a9feb', N'nGroup.Info.eEvolution.MiddleLayer.CommandManager.DPDZonenTabelleCommand', NULL, 0, 'f99ad222-9552-4fd2-806b-f9cb3b031036');";

      var entry = container.Resolve<ParseEntryViewModel>();
      entry.Name = "Guid Uppern";
      entry.RegEx = "[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}";
      entry.Razor = "@Model.Value.ToUpper()";
      entry.MainWindowViewModel = this;

      var list = new List<ParseEntryViewModel>() {
        entry
      };

      Entries = new BindingList<ParseEntryViewModel>(list);
    }

    #endregion Constructors

    #region Properties

    /// <summary>Gets or sets the input.</summary>
    public string Input { get; set; }

    public string Output { get; set; }

    /// <summary>Gets or sets the result.</summary>
    public string Result { get; set; }

    public BindingList<ParseEntryViewModel> Entries { get; set; }

    #endregion Properties

    #region Methods

    /// <summary>Handles the parse completed event.</summary>
    /// <param name="result">The result data.</param>
    private void HandleParseCompletedEvent(ParseResult result)
    {
      this.Result = result.Value;
    }

    #endregion Methods
  }
}