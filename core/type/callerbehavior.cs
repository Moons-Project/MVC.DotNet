using MVC.Engine;

namespace MVC.Core {
  public class CallerBehaviour : BaseBehavior {

    protected void Call<TCommand>(TCommand cmd) where TCommand : ICommand {
      Core.Call<TCommand>(cmd);
    }

    protected void Call(string cmdFullName) {
      Core.Call(cmdFullName);
    }

    protected object Post(Command cmd) {
      return Core.Post(cmd);
    }

    protected TResult Post<TCommand, TResult>(TCommand cmd) where TCommand : ICommand {
      return Core.Post<TCommand, TResult>(cmd);
    }

  }
}