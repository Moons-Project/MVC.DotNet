using System;
using MVC.Engine;
using MVC.Core;

namespace MVC.Main {
  public abstract class PlayerCommand : Command {
    public override Type GetController() {
            return typeof(PlayerController);
    }
  }

    public class PrintCommand : PlayerCommand { }

    public class CompareAttackCommand : PlayerCommand {
      public int OtherAttack {get; private set;}

      public CompareAttackCommand (int OtherAttack = 0) {
        this.OtherAttack = OtherAttack;
      }
    }
}