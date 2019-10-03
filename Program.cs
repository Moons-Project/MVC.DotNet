using System;
using MVC.Core;
using MVC.Engine;
using MVC.Main;

namespace MVC {

  class GameStart : CallerBehaviour {
    public void Run() {
      Call(new PrintCommand());
      Call(new CompareAttackCommand(10));

      // Screen.Log(typeof(PrintCommand).FullName);
      Call("MVC.Main.PrintCommand");
      // Call("MVC.Main.CompareAttackCommand");
    }
  }

  class Program : CallerBehaviour {
    static void Main(string[] args) {
      GameStart Game = new GameStart();
      Game.Run();
    }
  }
}