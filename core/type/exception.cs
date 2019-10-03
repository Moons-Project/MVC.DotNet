using System;

namespace MVC.Core {
  /// <summary>
  /// 基础异常类
  /// </summary>
  public class CoreException : Exception {
    public CoreException() : base() { }
    public CoreException(string message) : base(message) { }
    public CoreException(string message, Exception exception) : base(message,
      exception) { }
  }
}