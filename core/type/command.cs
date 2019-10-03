using System;

namespace MVC.Core {
  /// <summary>
  /// Command接口
  /// </summary>
  public interface ICommand {
    Type GetController();
  }

  /// <summary>
  /// Command类
  /// </summary>
  public abstract class Command : ICommand {
    public abstract Type GetController();
  }
}