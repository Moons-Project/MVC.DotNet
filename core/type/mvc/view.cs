using MVC.Engine;

namespace MVC.Core {

  /// <summary>
  /// 只包含物体或者资源的引用
  /// </summary>
  public abstract class View : BaseBehavior {

    /// <summary>
    /// 摧毁函数
    /// </summary>
    public new void Destroy() {
      // Pass
    }

  }
}