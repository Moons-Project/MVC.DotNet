using System;
using System.Collections.Generic;

namespace MVC.Engine {

  /// <summary>
  /// 基础行为类
  /// </summary>
  public class GameObject {

    public static List<GameObject> System = new List<GameObject>();

    public string name;
    public BaseBehavior behavior;

    public GameObject(string name, BaseBehavior behavior) {
      this.name = name;
      this.behavior = behavior;

      System.Add(this);
    }

  }
}