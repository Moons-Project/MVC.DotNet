using System;
using MVC.Core;
using MVC.Main;
using MVC.Engine;

namespace MVC {

  class GameStart : CallerBehaviour {
    public void Run() {
      Call(new PlayerCommand.PrintCommand());
    }
  }

  class Program : CallerBehaviour {
    static void Main(string[] args) {
      GameStart Game = new GameStart();
      Game.Run();
    }
  }
}