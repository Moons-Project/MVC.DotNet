using System;
using System.Collections.Generic;

namespace MVC.Engine {

  /// <summary>
  /// 基础行为类
  /// </summary>
  public class GameObject {

    public static List<GameObject> Engine = new List<GameObject>();

    public string name;
    public BaseBehavior behavior;

    public GameObject(string name, BaseBehavior behavior) {
      this.name = name;
      this.behavior = behavior;
      this.behavior.Start();

      Engine.Add(this);
      // log to make thing visiable
      System.Console.WriteLine($"Engine add Item {this.name}");
    }

  }
}