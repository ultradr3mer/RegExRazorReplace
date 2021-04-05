using RegExRazorReplace.Composite;

namespace RegExRazorReplace.Extensions
{
  public static class BaseViewModelExtensions
  {
    #region Methods

    public static Tvm Create<Tvm, Tdata>(Tdata data) where Tvm : BaseViewModel<Tdata>, new()
    {
      var vm = new Tvm();
      vm.SetDataModel(data);
      return vm;
    }

    public static Tvm GetWithDataModel<Tvm, Tdata>(this Tvm value, Tdata data) where Tvm : BaseViewModel<Tdata>
    {
      value.SetDataModel(data);
      return value;
    }

    #endregion Methods
  }
}